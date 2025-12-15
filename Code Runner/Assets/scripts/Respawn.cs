using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint;   // where player respawns
    public int damageOnFall = 1;     // how much damage falling causes

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Move player to respawn point
            other.transform.position = respawnPoint.position;

            // Reset velocity so player doesn't keep falling
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }

            // Apply damage
            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damageOnFall, transform.position);
            }
        }
    }
}

