using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HintTrigger : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform hintPanel;
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI dialogueText;
    public Button okButton;

    [Header("Text Messages")]
    [TextArea] public string hintMessage;

    [Header("Animation Settings")]
    public float slideDuration = 0.35f;
    public float hiddenY = -300f;
    public float shownY = 80f;

    private Coroutine animCoroutine;

    void Start()
    {
        // SAFETY FIXES (do not remove)
        if (canvasGroup == null)
            canvasGroup = hintPanel.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = hintPanel.gameObject.AddComponent<CanvasGroup>();

        // Initialize panel hidden
        hintPanel.gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        hintPanel.anchoredPosition = new Vector2(0, hiddenY);
        hintPanel.gameObject.SetActive(false);

        if (okButton != null)
            okButton.onClick.AddListener(HideHint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        ShowHint();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        HideHint();
    }

    void ShowHint()
    {
        if (dialogueText != null)
            dialogueText.text = hintMessage;

        hintPanel.gameObject.SetActive(true);

        if (animCoroutine != null)
            StopCoroutine(animCoroutine);

        animCoroutine = StartCoroutine(AnimateHint(hiddenY, shownY, 1f));
    }

    void HideHint()
    {
        if (animCoroutine != null)
            StopCoroutine(animCoroutine);

        animCoroutine = StartCoroutine(AnimateHint(shownY, hiddenY, 0f));
    }

    IEnumerator AnimateHint(float fromY, float toY, float targetAlpha)
    {
        float elapsed = 0f;
        Vector2 startPos = new Vector2(0, fromY);
        Vector2 endPos = new Vector2(0, toY);

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;

            hintPanel.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, t);

            yield return null;
        }

        hintPanel.anchoredPosition = endPos;
        canvasGroup.alpha = targetAlpha;

        if (targetAlpha == 0f)
            hintPanel.gameObject.SetActive(false);
    }
}
