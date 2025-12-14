using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject[] enemies; // drag your enemies here

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                    enemy.SetActive(true);
            }
        }
    }
}
