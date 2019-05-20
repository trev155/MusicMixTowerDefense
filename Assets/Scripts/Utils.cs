using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Utils {
    // Set alpha of a game object, has to have a SpriteRenderer
    public static void SetAlpha(Transform obj, float alpha) {
        if (alpha < 0 || alpha > 1) {
            Debug.LogWarning("Cannot set alpha of object " + obj + ". Value of " + alpha + " was invalid.");
            return;
        }
        
        Color objColor = obj.GetComponent<SpriteRenderer>().color;
        objColor.a = alpha;
        obj.GetComponent<SpriteRenderer>().color = objColor;
    }

    // Enum strings
    public static string CleanEnumString(string s) {
        string rv = s;
        rv = CapitalizeFirstLetterOnly(rv);
        rv = UnderscoreToSpace(rv);
        return rv;
    }

    private static string CapitalizeFirstLetterOnly(string s) {
        if (s.Length < 1) {
            return s;
        }

        return s[0].ToString().ToUpper() + s.Substring(1).ToLower();
    }

    private static string UnderscoreToSpace(string s) {
        return s.Replace('_', ' ');
    }

    // Fade Out
    public static IEnumerator DisplayAndFadeOutText(Text textObject, string text, float fadeOutTime) {
        textObject.text = text;
        
        Color originalColor = textObject.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime) {
            textObject.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }

        textObject.text = "";
        textObject.color = originalColor;
    }
}
