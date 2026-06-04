using UnityEngine;
using TMPro;

public class LoseText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loseText;

    void OnEnable()
    {
        loseText.color = new Color(loseText.color.r, loseText.color.g, loseText.color.b, 1f);
        loseText.text = "You Lose!";
        StartCoroutine(TypeEffect(loseText, loseText.text, 0.05f));
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