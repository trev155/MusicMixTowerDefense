using UnityEngine;

public class Utils {
    public static void SetAlpha(Transform obj, float alpha) {
        if (alpha < 0 || alpha > 1) {
            Debug.LogWarning("Cannot set alpha of object " + obj + ". Value of " + alpha + " was invalid.");
            return;
        }
        
        Color objColor = obj.GetComponent<SpriteRenderer>().color;
        objColor.a = alpha;
        obj.GetComponent<SpriteRenderer>().color = objColor;
    }

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
}
