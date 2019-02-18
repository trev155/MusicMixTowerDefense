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
    public Coroutine currentAttackLoop;
    

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

    /*
     * If no units in range:
     * - check to see if we are currently attacking and if so, stop
     * - don't do anything in any case
     * If units are in range, and we have a target set:
     * - if we currently have a target set (isAttacking = true), don't do anything
     * - otherwise start attacking
     * Otherwise, we don't have a target set, so find a new target
     * - stop the current attack routine
     * - indicate that we are not attacking anymore
     */
    private void Update() {
        if (enemyUnitsInRange.Count == 0) {
            if (currentAttackLoop != null) {
                StopCoroutine(currentAttackLoop);
                isAttacking = false;
            }
            return;
        }
        
        if (enemyUnitsInRange.Contains(currentTarget)) {
            if (isAttacking) {
                return;
            }
            
            currentAttackLoop = StartCoroutine(AttackTargetLoop(playerUnit.attackCooldown));
            isAttacking = true;
        } else {
            if (currentAttackLoop != null) {
                StopCoroutine(currentAttackLoop);
            }
            isAttacking = false;
            SetTargetToClosestUnitInRange();
        }
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

    private void AttackTarget() {
        Projectile proj = (Projectile)Instantiate(projectile, transform).GetComponent<Projectile>();
        proj.InitializeProperties(currentTarget, playerUnit, playerUnit.attackDamage);
    }

    IEnumerator AttackTargetLoop(float cooldown) {
        yield return new WaitForSeconds(cooldown / 2);
        while (true) {
            AttackTarget();
            yield return new WaitForSeconds(cooldown);
        }
    }
}
