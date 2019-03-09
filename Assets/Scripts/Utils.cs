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
}
