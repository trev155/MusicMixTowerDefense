using UnityEngine;
using System.Collections.Generic;


public class AttackRangeCircle : MonoBehaviour {
    public static readonly float SELECTED_ALPHA = 0.6f;
    public static readonly float UNSELECTED_ALPHA = 0.1f;

    public PlayerUnit playerUnit;
    public Transform projectile;

    private List<EnemyUnit> enemyUnitsInRange = new List<EnemyUnit>();
    private EnemyUnit currentTarget;
    private bool isAttacking = false;
    

    public void SetAlpha(float alpha) {
        if (alpha < 0 || alpha > 1) {
            Debug.LogWarning("Cannot set moveable area alpha. Value of " + alpha + " was invalid.");
            return;
        }
        Color circleColor = this.GetComponent<SpriteRenderer>().color;
        circleColor.a = alpha;
        this.GetComponent<SpriteRenderer>().color = circleColor;
    }

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

    private void Update() {
        if (enemyUnitsInRange.Count == 0) {
            return;
        }

        // Debug.Log("Current Target: " + currentTarget);
        
        if (enemyUnitsInRange.Contains(currentTarget)) {
            if (isAttacking) {
                return;
            }
            AttackTarget();
            isAttacking = true;
        } else {
            SetTargetToClosestUnitInRange();
            isAttacking = false;
        }
    }

    private void SetTargetToClosestUnitInRange() {
        float lowestDistance = float.MaxValue;
        EnemyUnit lowestDistanceEnemy = null;

        foreach (EnemyUnit enemyUnit in enemyUnitsInRange) {
            float distanceToEnemy = Vector2.Distance(enemyUnit.transform.position, this.transform.position);
            if (distanceToEnemy < lowestDistance) {
                lowestDistance = distanceToEnemy;
                lowestDistanceEnemy = enemyUnit;
            }
        }

        this.currentTarget = lowestDistanceEnemy;
    }

    private void AttackTarget() {
        // do this in a looped coroutine - speed determined by player unit's speed
        Projectile proj = (Projectile)Instantiate(projectile, this.transform).GetComponent<Projectile>();
        proj.InitializeProperties(this.currentTarget, this.playerUnit, this.playerUnit.attackDamage);
    }
}
