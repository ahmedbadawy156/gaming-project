using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    // ================== SHOOTING ==================
    [Header("Gun")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 12f;
    public float fireCooldown = 0.25f;
    public int damagePerShot = 1;

    [Header("Ammo")]
    public bool useAmmo = true;
    public int maxAmmo = 10;
    public int currentAmmo;

    [Header("Audio")]
    public AudioClip shootSound;
    private AudioSource audioSource;

    private float lastFireTime;
    // ==============================================

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        currentAmmo = 0;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleShoot();

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetBool("Grounded", isGrounded);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            whatIsGround
        );
    }

    // ================= MOVEMENT =================
    void HandleMovement()
    {
        float move = 0f;

        if (Input.GetKey(leftKey))
        {
            move = -1f;
            sr.flipX = true;
        }
        else if (Input.GetKey(rightKey))
        {
            move = 1f;
            sr.flipX = false;
        }

        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // ================= SHOOTING =================
    void HandleShoot()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.X))
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (Time.time < lastFireTime + fireCooldown)
            return;

        if (useAmmo && currentAmmo <= 0)
        {
            Debug.Log("No ammo!");
            return;
        }

        lastFireTime = Time.time;

        if (useAmmo)
            currentAmmo--;

        anim.SetTrigger("Shoot");

        Fire();
    }

    void Fire()
    {
        if (projectilePrefab == null || firePoint == null)
            return;

        // 🔊 Play shoot sound
        if (shootSound != null)
        {
            if (audioSource != null)
                audioSource.PlayOneShot(shootSound);
            else
                AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }

        GameObject bullet = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        float direction = sr.flipX ? -1f : 1f;

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = new Vector2(direction * projectileSpeed, 0f);
        }

        Projectile p = bullet.GetComponent<Projectile>();
        if (p != null)
        {
            p.SetDamage(damagePerShot);
        }
    }

    // ============ AMMO PICKUP SUPPORT ============
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        Debug.Log("Ammo: " + currentAmmo);
    }
}
