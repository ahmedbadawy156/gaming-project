using UnityEngine;
using TMPro;   // ✅ IMPORTANT

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI livesText;

    public void UpdateUI(int health, int lives)
    {
        if (healthText != null)
            healthText.text = "Health: " + health;

        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }
}
