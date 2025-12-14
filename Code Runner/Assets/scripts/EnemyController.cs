using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3f;
    public float patrolDistance = 3f;

    [Header("Attack")]
    public int damage = 1;
    public float attackCooldown = 1f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Transform player;

    private Vector2 startPos;
    private int patrolDir = 1;
    private bool chasing = false;
    private bool attacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        startPos = transform.position;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        chasing = distance < 6f;

        // Update walk animation
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void FixedUpdate()
    {
        if (attacking) return;

        if (chasing)
            Chase();
        else
            Patrol();
    }

    void Patrol()
    {
        rb.velocity = new Vector2(patrolDir * patrolSpeed, rb.velocity.y);

        if (Mathf.Abs(transform.position.x - startPos.x) >= patrolDistance)
            patrolDir *= -1;

        FaceDirection(patrolDir);
    }

    void Chase()
    {
        float dir = Mathf.Sign(player.position.x - transform.position.x);
        rb.velocity = new Vector2(dir * chaseSpeed, rb.velocity.y);

        FaceDirection((int)dir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !attacking)
        {
            StartCoroutine(Attack(collision.gameObject));
        }
    }

    IEnumerator Attack(GameObject playerObj)
    {
        attacking = true;

        rb.velocity = Vector2.zero;
        anim.SetTrigger("Attack");

        PlayerStats stats = playerObj.GetComponent<PlayerStats>();
        if (stats != null)
            stats.TakeDamage(damage, transform.position);

        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    // ✅ THIS IS THE FIX
    void FaceDirection(int dir)
    {
        if (dir > 0)
            sr.flipX = false; // facing right
        else if (dir < 0)
            sr.flipX = true;  // facing left
    }
}
