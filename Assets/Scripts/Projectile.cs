using UnityEngine;


public class Projectile : MonoBehaviour {
    public Unit targetUnit;
    public PlayerUnit origin;
    public float movementSpeed;
    public float attackDamage;


    public void InitializeProperties(Unit targetUnit, PlayerUnit origin, float attackDamage) {
        this.targetUnit = targetUnit;
        this.origin = origin;
        this.movementSpeed = 5.0f;
        this.attackDamage = attackDamage;
    }

    private void Update() {
        if (targetUnit == null) {
            Destroy(this.gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetUnit.transform.position, Time.deltaTime * movementSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == targetUnit.gameObject) {
            Destroy(this.gameObject);

            EnemyUnit enemyUnit = targetUnit.GetComponent<EnemyUnit>();
            InflictDamage(enemyUnit, this.attackDamage);
        }
    }

    private void InflictDamage(EnemyUnit enemyUnit, float damage) {
        enemyUnit.currentHealth -= (damage - enemyUnit.armor);
        
        if (enemyUnit.currentHealth <= 0) {
            GameEngine.Instance.IncrementKills();
            RemoveCurrentTargetForAllUnitsAttackingTarget(enemyUnit);
            Destroy(enemyUnit.gameObject);
        }
        
    }

    private void RemoveCurrentTargetForAllUnitsAttackingTarget(EnemyUnit targetEnemyUnit) {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject playerUnit in playerUnits) {
            PlayerUnit p = playerUnit.GetComponent<PlayerUnit>();
            if (p.attackRangeCircle.currentTarget == this.targetUnit) {
                p.attackRangeCircle.enemyUnitsInRange.Remove(targetEnemyUnit);
                p.attackRangeCircle.currentTarget = null;
                p.attackRangeCircle.switchTargets = true;
            }
        }
    }
}
