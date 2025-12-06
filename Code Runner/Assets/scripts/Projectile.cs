using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 3f;
    public LayerMask hitLayers; // optional: set to Enemy layer in inspector

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore player so bullets don't hit the shooter (if needed)
        if (other.CompareTag("Player")) return;

        // First try to find EnemyHealth on the collider or its parent
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy == null) enemy = other.GetComponentInParent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // If it hits environment (ground, walls), destroy bullet
        if (other.CompareTag("Ground") || other.CompareTag("Wall") || other.CompareTag("Platform"))
        {
            Destroy(gameObject);
            return;
        }

        // Optionally: destroy on any other layer that matches hitLayers
        if (hitLayers.value != 0)
        {
            if ((hitLayers.value & (1 << other.gameObject.layer)) != 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
