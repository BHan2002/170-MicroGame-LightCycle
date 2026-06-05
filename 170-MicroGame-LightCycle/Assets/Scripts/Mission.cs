using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mission : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionText;

    void Start()
    {
        // Type effect in and out mission text
        missionText.text = "Mission: Survive!";
        StartCoroutine(TypeEffect(missionText, missionText.text, 0.05f));
    }

    private System.Collections.IEnumerator TypeEffect(TextMeshProUGUI textComponent, string fullText, float delay)
    {
        textComponent.text = "";
        foreach (char c in fullText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(delay);
        }

        // Wait for a moment before fading out
        yield return new WaitForSeconds(2f);

        // Fade out the text
        float fadeDuration = 1f;
        float elapsedTime = 0f;
        Color originalColor = textComponent.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure the text is fully transparent at the end
        textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}