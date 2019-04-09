using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    //---------- Fields ----------
    public Transform splashDamageCircle;

    public Unit targetUnit;
    public PlayerUnit origin;
    public float movementSpeed;
    public float attackDamage;

    private System.Random random;

    //---------- Methods ----------
    private void Awake() {
        random = new System.Random();
    }

    private void Update() {
        if (targetUnit == null) {
            Destroy(this.gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetUnit.transform.position, Time.deltaTime * movementSpeed);
    }

    public void InitializeProperties(Unit targetUnit, PlayerUnit origin, float attackDamage) {
        this.targetUnit = targetUnit;
        this.origin = origin;
        this.movementSpeed = 5.0f;
        this.attackDamage = attackDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == targetUnit.gameObject) {
            EnemyUnit enemyUnit = targetUnit.GetComponent<EnemyUnit>();

            if (origin.attackType == AttackType.SPLASH) {
                InflictSplashDamage(enemyUnit, this.attackDamage);  // this won't deal damage to the actual target, so deal with the actual target as normal
                StartCoroutine(WaitTimeBeforeInflictingDamage(enemyUnit, this.attackDamage, 0.1f));
            } else {
                InflictDamage(enemyUnit, this.attackDamage);
                Destroy(this.gameObject);
            }
        }
    }

    // ----- Inflict Damage -----
    public void InflictDamage(EnemyUnit enemyUnit, float damage) {
        float damageToInflict = damage - enemyUnit.armor;
        if (damageToInflict < 1) {
            damageToInflict = 1;
        }
        enemyUnit.currentHealth -= damageToInflict;
        
        if (GameEngine.GetInstance().enemyUnitSelected == enemyUnit) {
            GameEngine.GetInstance().unitSelectionPanel.UpdateSelectedUnitDataPanel(enemyUnit);
        }
        
        if (enemyUnit.currentHealth <= 0) {
            GameEngine.GetInstance().IncrementKills();
            RemoveCurrentTargetForAllUnitsAttackingTarget(enemyUnit);
            if (enemyUnit.level == 0) {
                KilledSpecialEnemyUnit(enemyUnit.displayName);
            }
            Destroy(enemyUnit.gameObject);

            if (GameEngine.GetInstance().enemyUnitSelected == enemyUnit) {
                GameEngine.GetInstance().unitSelectionPanel.CloseUnitSelectionPanel();
            }
        }
    }

    // ----- Inflict Splash Damage -----
    private void InflictSplashDamage(EnemyUnit enemyUnit, float damage) {
        // the SplashDamageCircle will call the InflictDamage() for every unit in its radius
        SplashDamageCircle splashCircle = Instantiate(splashDamageCircle, enemyUnit.transform).GetComponent<SplashDamageCircle>();
        splashCircle.damage = Mathf.Floor(damage / 2);
        splashCircle.projectile = this;
        // Splash circle appears for some time, where any units that enter its collider will be affected
        StartCoroutine(SplashCooldownTime(0.1f, splashCircle));
    }

    IEnumerator SplashCooldownTime(float time, SplashDamageCircle splashCircle) {
        yield return new WaitForSeconds(time);
        Destroy(splashCircle.gameObject);
        Destroy(this.gameObject);
    }

    IEnumerator WaitTimeBeforeInflictingDamage(EnemyUnit enemyUnit, float damage, float time) {
        yield return new WaitForSeconds(time);
        InflictDamage(enemyUnit, damage);
    }

    // ----- Checks for when enemy unit is killed -----
    private void RemoveCurrentTargetForAllUnitsAttackingTarget(EnemyUnit targetEnemyUnit) {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject playerUnit in playerUnits) {
            PlayerUnit p = playerUnit.GetComponent<PlayerUnit>();
            if (p.currentTarget == targetUnit) {
                p.attackRangeCircle.enemyUnitsInRange.Remove(targetEnemyUnit);
                p.currentTarget = null;
                p.switchTargets = true;
            }
            if (p.attackRangeCircle.enemyUnitsInRange.Contains(targetEnemyUnit)) {
                p.attackRangeCircle.enemyUnitsInRange.Remove(targetEnemyUnit);
            }
        }
    }

    // ----- Special Unit Kill Rewards -----
    private void KilledSpecialEnemyUnit(string enemyName) {
        switch (enemyName) {
            case "Bounty":
                GiveBountyReward();
                break;
            default:
                throw new GameplayException("Unrecognized enemy name");
        }
    }

    private void GiveBountyReward() {
        int choice = random.Next(1, 10);
        if (choice <= 2) {
            GameEngine.GetInstance().messageQueue.PushMessage("Bounty Bonus: 2 Shop Tokens", MessageType.POSITIVE);
            GameEngine.GetInstance().IncreaseTokenCount(2);
        } else if (choice <= 6) {
            GameEngine.GetInstance().messageQueue.PushMessage("Bounty Bonus: 3 Shop Tokens", MessageType.POSITIVE);
            GameEngine.GetInstance().IncreaseTokenCount(3);
        } else if (choice <= 8) {
            GameEngine.GetInstance().messageQueue.PushMessage("Bounty Bonus: 4 Shop Tokens", MessageType.POSITIVE);
            GameEngine.GetInstance().IncreaseTokenCount(4);
        } else {
            GameEngine.GetInstance().messageQueue.PushMessage("Bounty Bonus: Harvester", MessageType.POSITIVE);
            GameEngine.GetInstance().AddHarvester();
        }
    }
}
