using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int damage = 1;
    public float pushback = 2f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var stats = col.gameObject.GetComponent<PlayerStats>();
            if (stats != null) stats.TakeDamage(damage);
            else
            {
                var pc = col.gameObject.GetComponent<PlayerController>();
                if (pc != null && pc.TryGetComponent(out PlayerStats ps)) ps.TakeDamage(damage);
            }

            // optional pushback
            Rigidbody2D prb = col.rigidbody;
            if (prb != null)
            {
                Vector2 dir = (col.transform.position.x < transform.position.x) ? Vector2.left : Vector2.right;
                prb.AddForce(dir * pushback, ForceMode2D.Impulse);
            }
        }
    }
}
