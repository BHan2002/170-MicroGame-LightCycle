using UnityEngine;
using TMPro;

public class WinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winText;

    void OnEnable()
    {
        winText.color = new Color(winText.color.r, winText.color.g, winText.color.b, 1f);
        winText.text = "You Win!";
        StartCoroutine(TypeEffect(winText, winText.text, 0.05f));
    }

    private System.Collections.IEnumerator TypeEffect(TextMeshProUGUI textComponent, string fullText, float delay)
    {
        textComponent.text = "";

        foreach (char c in fullText)
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(delay);
        }

        yield return new WaitForSecondsRealtime(2f);

        float fadeDuration = 1f;
        float elapsedTime = 0f;
        Color originalColor = textComponent.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }
}