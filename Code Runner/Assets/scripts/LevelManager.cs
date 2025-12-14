using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform currentCheckpoint;
    public GameObject gameOverPanel;

    void Start()
    {
        // Always reset time on scene load
        Time.timeScale = 1f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && currentCheckpoint != null)
        {
            player.transform.position = currentCheckpoint.position;
        }
    }

    public void GameOver()
    {
        Debug.Log("SHOWING GAME OVER");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}
