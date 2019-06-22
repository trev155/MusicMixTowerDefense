using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    //---------- Fields ----------
    private static readonly float SPLASH_CIRCLE_APPEARANCE_TIME = 0.1f;

    public Transform splashDamageCircle;
    public Transform largeSplashDamageCircle;

    public EnemyUnit targetUnit;
    public PlayerUnit origin;
    public float movementSpeed;
    public float attackDamage;
    
    //---------- Methods ----------
    private void Update() {
        if (targetUnit == null) {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetUnit.transform.position, Time.deltaTime * movementSpeed);
    }

    public void InitializeProperties(EnemyUnit targetUnit, PlayerUnit origin, float attackDamage) {
        this.targetUnit = targetUnit;
        this.origin = origin;
        this.movementSpeed = 5.0f;
        this.attackDamage = attackDamage;
    }

    /*
     * If this projectile collides with an enemy unit, inflict damage to it.
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        if (targetUnit == null) {
            return;
            // throw new GameplayException("Target unit is NULL!");
        }

        if (collision.gameObject == targetUnit.gameObject) {
            EnemyUnit enemyUnit = targetUnit.GetComponent<EnemyUnit>();

            // Projectile landing audio
            GameEngine.GetInstance().audioManager.PlayProjectileLandingSound(origin.GetPlayerUnitData().GetUnitClass(), origin.GetPlayerUnitData().GetRank());

            if (origin.GetPlayerUnitData().GetAttackType() == AttackType.SPLASH) {
                InflictSplashDamage(enemyUnit, this.attackDamage, splashDamageCircle);  // this won't deal damage to the actual target, so deal with the actual target as normal
                StartCoroutine(WaitTimeBeforeInflictingDamage(enemyUnit, this.attackDamage, SPLASH_CIRCLE_APPEARANCE_TIME));
            } else if (origin.GetPlayerUnitData().GetAttackType() == AttackType.LARGE_SPLASH) {
                InflictSplashDamage(enemyUnit, this.attackDamage, largeSplashDamageCircle);
                StartCoroutine(WaitTimeBeforeInflictingDamage(enemyUnit, this.attackDamage, SPLASH_CIRCLE_APPEARANCE_TIME));
            } else {
                InflictDamage(enemyUnit, this.attackDamage);
                Destroy(this.gameObject);
            }
        }
    }

    // ----- Inflict Damage -----
    public void InflictDamage(EnemyUnit enemyUnit, float damage) {
        float damageToInflict = damage - enemyUnit.GetEnemyUnitData().GetArmor();
        if (damageToInflict < 1) {
            damageToInflict = 1;
        }
        enemyUnit.SetCurrentHealth(enemyUnit.GetCurrentHealth() - damageToInflict);
        if (GameEngine.GetInstance().enemyUnitSelected == enemyUnit) {
            GameEngine.GetInstance().unitSelectionPanel.UpdateSelectedUnitDataPanel(enemyUnit);
        }
        
        if (enemyUnit.GetCurrentHealth() <= 0) {
            HandleEnemyUnitDeath(enemyUnit);
        }
    }

    private void HandleEnemyUnitDeath(EnemyUnit enemyUnit) {
        // update stats
        GameEngine.GetInstance().IncrementKills();
        GameEngine.GetInstance().DecrementEnemyUnitCount();

        // remove enemy unit from other player unit lists
        RemoveCurrentTargetForAllUnitsAttackingTarget(enemyUnit);

        // Regular enemy unit death sound effect
        if (enemyUnit.GetEnemyUnitData().GetLevel() > 0) {
            GameEngine.GetInstance().audioManager.PlayRegularEnemyUnitDeathSound(enemyUnit.GetEnemyUnitData().GetLevel());
        }

        // Check if killed special enemy unit
        if (enemyUnit.GetEnemyUnitData().GetLevel() == 0) {
            KilledSpecialEnemyUnit(enemyUnit.GetEnemyUnitData().GetEnemyType());
        }

        // Destroy enemy unit game object
        if (enemyUnit != null) {
            Destroy(enemyUnit.gameObject);
        }
        
        // Close unit selection panel for enemy unit if required
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
        SplashDamageCircle splashCircle = Instantiate(splashDamageCirclePrefab, enemyUnit.transform).GetComponent<SplashDamageCircle>();
        splashCircle.damage = Mathf.Floor(damage / 2);
        splashCircle.projectile = this;
        StartCoroutine(SplashCooldownTime(SPLASH_CIRCLE_APPEARANCE_TIME, splashCircle));
    }

    private IEnumerator SplashCooldownTime(float time, SplashDamageCircle splashCircle) {
        yield return new WaitForSeconds(time);

        if (splashCircle != null) {
            Destroy(splashCircle.gameObject);
        }
        Destroy(this.gameObject, 0.1f);     // Race condition - remove splash circle first    
    }

    private IEnumerator WaitTimeBeforeInflictingDamage(EnemyUnit enemyUnit, float damage, float time) {
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
    // TODO these should be moved out of this class - probably into EnemyUnit
    private void KilledSpecialEnemyUnit(EnemyType enemyType) {
        switch (enemyType) {
            case EnemyType.BOUNTY:
                GiveBountyReward();
                GameEngine.GetInstance().audioManager.PlaySpecialUnitDeathSound(enemyType);
                break;
            case EnemyType.BONUS:
                GameEngine.GetInstance().messageQueue.PushMessage("Bonus Token: 1 Shop Token", MessageType.POSITIVE);
                GameEngine.GetInstance().IncreaseTokenCount(1);
                GameEngine.GetInstance().BonusUnitKilled();
                break;
            default:
                throw new GameplayException("Unrecognized enemy type: " + enemyType.ToString());
        }
    }

    private void GiveBountyReward() {
        int choice = GameEngine.GetInstance().random.Next(1, 10);
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
