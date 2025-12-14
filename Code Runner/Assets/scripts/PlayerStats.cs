using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    public int health;

    [Header("Lives")]
    public int lives = 3;

    [Header("Damage Immunity")]
    public float immunityDuration = 1.2f;
    private bool isImmune = false;

    [Header("UI")]
    public PlayerHeartsUI heartsUI;
    public PlayerUI playerUI;

    private SpriteRenderer sr;
    private Color originalColor;
    private LevelManager levelManager;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        levelManager = FindObjectOfType<LevelManager>();

        health = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage, Vector2 hitSource)
    {
        if (isImmune) return;

        health -= damage;
        if (health < 0) health = 0;

        UpdateUI();
        StartCoroutine(Flicker());

        if (health <= 0)
        {
            lives--;

            if (lives > 0)
            {
                health = maxHealth;
                UpdateUI();

                if (levelManager != null)
                    levelManager.RespawnPlayer();
            }
            else
            {
                Debug.Log("GAME OVER CALLED");

                if (levelManager != null)
                    levelManager.GameOver();
            }
        }

        isImmune = true;
        Invoke(nameof(ResetImmunity), immunityDuration);
    }

    void ResetImmunity()
    {
        isImmune = false;
    }

    IEnumerator Flicker()
    {
        float t = 0f;
        while (t < immunityDuration)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = originalColor;
            yield return new WaitForSeconds(0.1f);
            t += 0.2f;
        }
    }

    void UpdateUI()
    {
        if (heartsUI != null)
            heartsUI.UpdateHearts();

        if (playerUI != null)
            playerUI.UpdateUI(health, lives);
    }
}
