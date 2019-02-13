/*
 * An object created when you click on the screen. 
 * If it collides with a unit, this is considered a unit selection.
 * It is small enough that it should only ever collide with 1 unit at a time. (and units should be separated by rigidbody physics anyway).
 */

using UnityEngine;

public class ClickCircleArea : MonoBehaviour {
    private bool unitIsSelected = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (unitIsSelected) {
            return;
        } else {
            // Update HUD
            if (collision.gameObject.tag == "PlayerUnit") {
                PlayerUnit playerUnit = (PlayerUnit)collision.gameObject.GetComponent<PlayerUnit>();
                GameEngine.Instance.hudManager.UpdateSelectedUnitData(playerUnit);
            } else if (collision.gameObject.tag == "EnemyUnit") {
                EnemyUnit enemyUnit = (EnemyUnit)collision.gameObject.GetComponent<EnemyUnit>();
                GameEngine.Instance.hudManager.UpdateSelectedUnitData(enemyUnit);
            }

            // TODO this should be called only when you click off the unit
            unitIsSelected = true;
        }    
    }
}
