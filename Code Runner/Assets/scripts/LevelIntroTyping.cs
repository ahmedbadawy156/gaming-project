using System.Collections;
using UnityEngine;
using TMPro;

public class LevelIntroTyping : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;

    [Header("Text Settings")]
    [TextArea]
    public string message = "Welcome to Level 1 – C++ World";

    [Header("Typing")]
    public float typingSpeed = 0.05f;

    [Header("Timing")]
    public float stayTime = 1.5f;
    public float fadeSpeed = 1.5f;

    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        welcomeText.text = "";
        welcomeText.alpha = 0f;
        welcomeText.gameObject.SetActive(true);

        // Fade In
        while (welcomeText.alpha < 1f)
        {
            welcomeText.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // Typing effect
        foreach (char c in message)
        {
            welcomeText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait before fade out
        yield return new WaitForSeconds(stayTime);

        // Fade Out
        while (welcomeText.alpha > 0f)
        {
            welcomeText.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        welcomeText.gameObject.SetActive(false);
    }
}
