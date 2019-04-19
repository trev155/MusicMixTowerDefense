using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    //---------- Fields ----------
    private static readonly float SPLASH_CIRCLE_APPEARANCE_TIME = 0.1f;

    public Transform splashDamageCircle;
    public Transform largeSplashDamageCircle;

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
                InflictSplashDamage(enemyUnit, this.attackDamage, splashDamageCircle);  // this won't deal damage to the actual target, so deal with the actual target as normal
                StartCoroutine(WaitTimeBeforeInflictingDamage(enemyUnit, this.attackDamage, 0.1f));
            } else if (origin.attackType == AttackType.LARGE_SPLASH) {
                InflictSplashDamage(enemyUnit, this.attackDamage, largeSplashDamageCircle);
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
            HandleEnemyUnitDeath(enemyUnit);
        }
    }

    private void HandleEnemyUnitDeath(EnemyUnit enemyUnit) {
        GameEngine.GetInstance().IncrementKills();
        GameEngine.GetInstance().DecrementEnemyUnitCount();

        RemoveCurrentTargetForAllUnitsAttackingTarget(enemyUnit);

        if (enemyUnit.level == 0) {
            KilledSpecialEnemyUnit(enemyUnit.displayName);
        }
        Destroy(enemyUnit.gameObject);

        if (GameEngine.GetInstance().enemyUnitSelected == enemyUnit) {
            GameEngine.GetInstance().unitSelectionPanel.CloseUnitSelectionPanel();
        }
    }

    // ----- Inflict Splash Damage -----
    /*
     * Creates a game object, defined by the prefab parameter 'splashDamageCirclePrefab', centered around the 'enemyUnit'.
     * This prefab contains a SplashDamageCircle script, so this game object can be cast to a SplashDamageCircle.
     * This SplashDamageCircle is such that any enemy unit that touches it will take Mathf.Floor('damage' / 2) damage.
     * This game object is destroyed after a short time interval.
     */
    private void InflictSplashDamage(EnemyUnit enemyUnit, float damage, Transform splashDamageCirclePrefab) {
        SplashDamageCircle splashCircle = Instantiate(splashDamageCircle, enemyUnit.transform).GetComponent<SplashDamageCircle>();
        splashCircle.damage = Mathf.Floor(damage / 2);
        splashCircle.projectile = this;
        StartCoroutine(SplashCooldownTime(SPLASH_CIRCLE_APPEARANCE_TIME, splashCircle));
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
