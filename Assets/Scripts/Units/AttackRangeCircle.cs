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

    /*
     * Remove all null objects in the internal list.
     */
    public void Cleanup() {
        Debug.Log("Having to clean up attack range circle's internal list for unit: " + playerUnit.name);
        foreach (EnemyUnit enemyUnit in enemyUnitsInRange) {
            if (enemyUnit == null) {
                enemyUnitsInRange.Remove(enemyUnit);
            }
        }
    }
}
