using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHeartsUI : MonoBehaviour
{
    public PlayerStats player;     // drag Player here (or auto-find)
    public Image heartPrefab;      // drag the Heart Image here
    public Transform heartsParent; // drag HeartsContainer here

    private List<Image> hearts = new List<Image>();

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerStats>();
        }

        CreateHearts();
        UpdateHearts();
    }

    void CreateHearts()
    {
        if (heartsParent == null || heartPrefab == null || player == null)
        {
            Debug.LogWarning("PlayerHeartsUI not set up correctly.");
            return;
        }

        // clear old hearts if any
        foreach (Transform child in heartsParent)
        {
            Destroy(child.gameObject);
        }
        hearts.Clear();

        // create hearts equal to maxHealth
        for (int i = 0; i < player.maxHealth; i++)
        {
            Image heart = Instantiate(heartPrefab, heartsParent);
            heart.enabled = true;
            hearts.Add(heart);
        }
    }

    public void UpdateHearts()
    {
        if (player == null || hearts.Count == 0)
            return;

        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].enabled = (i < player.health);
        }
    }
}
