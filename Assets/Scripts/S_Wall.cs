using UnityEngine;

public class S_Wall : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "EnemyUnit") {
            EnemyUnit enemyUnit = collision.GetComponent<EnemyUnit>();
            if (enemyUnit.GetEnemyUnitData().GetEnemyType() == EnemyType.BOUNTY) {
                enemyUnit.allowMovement = false;
                Rigidbody2D rb2d = enemyUnit.GetComponent<Rigidbody2D>();
                rb2d.velocity = Vector2.zero;
            }
        }
    }
}
