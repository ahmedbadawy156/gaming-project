using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 2;
    int currentHP;
    Animator anim;
    bool isDead = false;

    [Tooltip("Set to Die animation length (seconds) for reliable timing.")]
    public float deathAnimLength = 0.8f;
    public GameObject deathVFX;
    public AudioClip deathSound;

    void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHP -= dmg;
        if (currentHP <= 0)
        {
            StartCoroutine(HandleDeath());
        }
        else
        {
            if (anim != null) anim.SetTrigger("Hurt");
        }
    }

    IEnumerator HandleDeath()
    {
        isDead = true;
        if (anim != null) anim.SetTrigger("Die");

        if (deathAnimLength > 0f)
            yield return new WaitForSeconds(deathAnimLength);
        else
            yield return new WaitForSeconds(0.6f); // fallback

        if (deathVFX != null) Instantiate(deathVFX, transform.position, Quaternion.identity);
        if (deathSound != null) AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
    }
}
