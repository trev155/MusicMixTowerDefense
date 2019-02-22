using UnityEngine;
using System.Collections.Generic;


public class AttackRangeCircle : MonoBehaviour {
    //---------- Fields ----------
    public static readonly float SELECTED_ALPHA = 0.6f;
    public static readonly float UNSELECTED_ALPHA = 0.1f;

    public PlayerUnit playerUnit;
    public List<EnemyUnit> enemyUnitsInRange = new List<EnemyUnit>();

    //---------- Methods ----------
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyUnit") {
            enemyUnitsInRange.Add(collision.gameObject.GetComponent<EnemyUnit>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyUnit") {
            enemyUnitsInRange.Remove(collision.gameObject.GetComponent<EnemyUnit>());
        }
    }

    public void SetAlpha(float alpha) {
        if (alpha < 0 || alpha > 1) {
            Debug.LogWarning("Cannot set moveable area alpha. Value of " + alpha + " was invalid.");
            return;
        }
        Color circleColor = GetComponent<SpriteRenderer>().color;
        circleColor.a = alpha;
        GetComponent<SpriteRenderer>().color = circleColor;
    }
}
