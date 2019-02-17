using UnityEngine;


public class AttackRangeCircle : MonoBehaviour {
    public static readonly float SELECTED_ALPHA = 0.6f;
    public static readonly float UNSELECTED_ALPHA = 0.1f;

    public void SetAlpha(float alpha) {
        if (alpha < 0 || alpha > 1) {
            Debug.LogWarning("Cannot set moveable area alpha. Value of " + alpha + " was invalid.");
            return;
        }
        Color circleColor = this.GetComponent<SpriteRenderer>().color;
        circleColor.a = alpha;
        this.GetComponent<SpriteRenderer>().color = circleColor;
    }
}
