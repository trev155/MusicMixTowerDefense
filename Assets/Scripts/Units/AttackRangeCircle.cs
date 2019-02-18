using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AttackRangeCircle : MonoBehaviour {
    public static readonly float SELECTED_ALPHA = 0.6f;
    public static readonly float UNSELECTED_ALPHA = 0.1f;

    public PlayerUnit playerUnit;
    public Transform projectile;

    public List<EnemyUnit> enemyUnitsInRange = new List<EnemyUnit>();
    public EnemyUnit currentTarget;
    public bool isAttacking = false;
    

    public void SetAlpha(float alpha) {
        if (alpha < 0 || alpha > 1) {
            Debug.LogWarning("Cannot set moveable area alpha. Value of " + alpha + " was invalid.");
            return;
        }
        Color circleColor = GetComponent<SpriteRenderer>().color;
        circleColor.a = alpha;
        GetComponent<SpriteRenderer>().color = circleColor;
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
        if (enemyUnitsInRange.Count == 0 || isAttacking) {
            return;
        }
        SetTargetToClosestUnitInRange();
        StartCoroutine(AttackTargetLoop(playerUnit.attackCooldown));
    }

    private void SetTargetToClosestUnitInRange() {
        float lowestDistance = float.MaxValue;
        EnemyUnit lowestDistanceEnemy = null;

        foreach (EnemyUnit enemyUnit in enemyUnitsInRange) {
            float distanceToEnemy = Vector2.Distance(enemyUnit.transform.position, transform.position);
            if (distanceToEnemy < lowestDistance) {
                lowestDistance = distanceToEnemy;
                lowestDistanceEnemy = enemyUnit;
            }
        }

        currentTarget = lowestDistanceEnemy;
    }

    IEnumerator AttackTargetLoop(float cooldown) {
        isAttacking = true;
        while (true) {
            if (!enemyUnitsInRange.Contains(currentTarget)) {
                break;
            }
            AttackTarget();
            yield return new WaitForSeconds(cooldown);
        }
        isAttacking = false;
    }

    private void AttackTarget() {
        Projectile proj = (Projectile)Instantiate(projectile, transform).GetComponent<Projectile>();
        proj.InitializeProperties(currentTarget, playerUnit, playerUnit.attackDamage);
    }
}
