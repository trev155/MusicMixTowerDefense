using UnityEngine;
using System.Collections.Generic;


public class AttackRangeCircle : MonoBehaviour {
    //---------- Fields ----------
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
}
