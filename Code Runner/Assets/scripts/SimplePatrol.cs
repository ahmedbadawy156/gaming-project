using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SimplePatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (pointA == null || pointB == null)
        {
            Debug.LogWarning(name + ": pointA or pointB not assigned. Disabling SimplePatrol.");
            enabled = false;
            return;
        }

        // start heading to B
        target = pointB;

        // Make sure Rigidbody is not kinematic
        if (rb.bodyType == RigidbodyType2D.Kinematic)
            rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // compute direction (normalized)
        Vector2 dir = (target.position - transform.position).normalized;
        // set horizontal velocity only
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

        // flip sprite based on velocity.x
        if (sr != null)
        {
            if (rb.velocity.x > 0.05f) sr.flipX = false;
            else if (rb.velocity.x < -0.05f) sr.flipX = true;
        }

        // update animator Speed parameter (absolute horizontal speed) IF controller exists
        if (anim != null && anim.runtimeAnimatorController != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }

        // switch target when near
        float dist = Vector2.Distance(transform.position, target.position);
        if (dist < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (pointA != null) Gizmos.DrawSphere(pointA.position, 0.05f);
        if (pointB != null) Gizmos.DrawSphere(pointB.position, 0.05f);
    }
}
