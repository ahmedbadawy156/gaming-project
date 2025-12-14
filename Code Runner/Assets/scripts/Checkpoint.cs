using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager lm = FindObjectOfType<LevelManager>();
            if (lm != null)
            {
                lm.currentCheckpoint = transform;
            }
        }
    }
}
