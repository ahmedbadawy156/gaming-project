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
        Debug.Log(name + " took damage: " + dmg + " -> HP = " + currentHP);

        if (currentHP <= 0)
        {
            StartCoroutine(HandleDeath());
        }
        else
        {
            if (anim != null && anim.runtimeAnimatorController != null)
            {
                Debug.Log(name + " triggering HURT");
                anim.SetTrigger("Hurt");
            }
            else
            {
                Debug.LogWarning(name + " has no Animator or no Controller, can't play Hurt.");
            }
        }
    }

    IEnumerator HandleDeath()
    {
        isDead = true;

        if (anim != null && anim.runtimeAnimatorController != null)
        {
            Debug.Log(name + " triggering DIE");
            anim.SetTrigger("Die");
        }
        else
        {
            Debug.LogWarning(name + " has no Animator or no Controller, can't play Die.");
        }

        float wait = deathAnimLength > 0f ? deathAnimLength : 0.6f;
        yield return new WaitForSeconds(wait);

        if (deathVFX != null) Instantiate(deathVFX, transform.position, Quaternion.identity);
        if (deathSound != null) AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
    }
}
