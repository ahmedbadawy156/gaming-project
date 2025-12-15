using System.Collections;
using UnityEngine;
using TMPro;

public class LevelIntroText : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;
    public float showTime = 3f;

    void Start()
    {
        StartCoroutine(ShowIntro());
    }

    IEnumerator ShowIntro()
    {
        welcomeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showTime);
        welcomeText.gameObject.SetActive(false);
    }
}
