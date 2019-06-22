using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class PlayerUnit : MonoBehaviour, IClickableUnit, IPointerClickHandler {
    //---------- Constants ----------
    public static readonly float SELECTED_ALPHA = 0.5f;
    public static readonly float UNSELECTED_ALPHA = 0.05f;

    //---------- Fields ----------
    // Data Fields
    private PlayerUnitData playerUnitData;
    
    // Data Fields - Getters and Setters
    public PlayerUnitData GetPlayerUnitData() {
        return playerUnitData;
    }
    
    public void SetPlayerUnitData(PlayerUnitData playerUnitData) {
        this.playerUnitData = playerUnitData;
    }

    // Other Fields
    public bool movementEnabled = false;
    public Vector2 movementDestination;

    public Transform projectilePrefab;
    public AttackRangeCircle attackRangeCircle; // the actual game object representing the attack range circle
    public Transform attackRangeCirclePrefab;   // the prefab used to create the attack range circle

    public EnemyUnit currentTarget;
    public bool isAttacking = false;
    public bool switchTargets = false;

    //---------- Methods ----------
    public void InitializePlayerUnitGameObject(PlayerUnitData playerUnitData) {
        SetPlayerUnitData(playerUnitData);

        UnitSpawner.SetObjectName(gameObject);
        
        // Rank Indication
        RankIndicatorBar rankIndicatorBar = GetComponentInChildren<RankIndicatorBar>();
        rankIndicatorBar.Initialize(GetPlayerUnitData().GetRank());

        // Create range circle
        CreatePlayerUnitRangeCircle();
    }

    private void CreatePlayerUnitRangeCircle() {
        Transform playerUnitRangeCircle = Instantiate(attackRangeCirclePrefab, transform);

        playerUnitRangeCircle.localScale = new Vector2(playerUnitRangeCircle.localScale.x * GetPlayerUnitData().GetAttackRange(), 
                                                       playerUnitRangeCircle.localScale.y * GetPlayerUnitData().GetAttackRange());

        attackRangeCircle = playerUnitRangeCircle.GetComponent<AttackRangeCircle>();
        attackRangeCircle.playerUnit = this;
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        // Check that we actually selected the unit, as opposed to any of its child objects
        bool clickedOnPlayerUnit = pointerEventData.pointerEnter.gameObject.name == this.name;
        if (!clickedOnPlayerUnit) {
            return;
        }

        // Clear alpha of previously selected units
        if (GameEngine.GetInstance().playerUnitSelected != null) {
            Utils.SetAlpha(GameEngine.GetInstance().playerUnitSelected.attackRangeCircle.transform, UNSELECTED_ALPHA);
            
        }
        if (GameEngine.GetInstance().enemyUnitSelected != null) {
            Utils.SetAlpha(GameEngine.GetInstance().enemyUnitSelected.selectedUnitCircle, EnemyUnit.UNSELECTED_ALPHA);
        }

        // Update references to objects in the game engine
        GameEngine.GetInstance().playerUnitSelected = this;
        GameEngine.GetInstance().enemyUnitSelected = null;

        // Set alpha value
        Utils.SetAlpha(GameEngine.GetInstance().playerUnitSelected.attackRangeCircle.transform, SELECTED_ALPHA);

        // Show data on unit selection panel
        GameEngine.GetInstance().unitSelectionPanel.ShowUnitSelectionPanel(this);
    }

    public List<string> GetDisplayUnitData() {
        List<string> unitData = new List<string>();
        string title = "[" + GetPlayerUnitData().GetRank() + " Rank] " + GetPlayerUnitData().GetDisplayName();
        string unitType = "Unit Class: " + Utils.CleanEnumString(GetPlayerUnitData().GetUnitClass().ToString());
        string attackDamage = "Attack Damage: " + GetPlayerUnitData().GetAttackDamage();
        int numUpgrades = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(GetPlayerUnitData().GetUnitClass());
        if (numUpgrades > 0) {
            attackDamage += " (+ " + GetPlayerUnitData().GetAttackUpgrade() * numUpgrades + ")";
        }
        string attackUpgrade = "Attack Upgrade: " + GetPlayerUnitData().GetAttackUpgrade();
        string attackSpeed = "Attack Speed: " + GetPlayerUnitData().GetAttackCooldown();
        string movementSpeed = "Movement Speed: " + GetPlayerUnitData().GetMovementSpeed();
        string attackType = "Attack Type: " + Utils.CleanEnumString(GetPlayerUnitData().GetAttackType().ToString());

        unitData.Add(title);
        unitData.Add(unitType);
        unitData.Add(attackDamage);
        unitData.Add(attackUpgrade);
        unitData.Add(attackSpeed);
        unitData.Add(movementSpeed);
        unitData.Add(attackType);

        return unitData;
    }

    /*
     * Update per frame. Handle movement and attacking.
     */
    private void Update() {
        if (movementEnabled) {
            MoveToDestination(movementDestination);
        }

        if (attackRangeCircle.enemyUnitsInRange.Count > 0 && !isAttacking) {
            SetTargetToClosestUnitInRange();
            StartCoroutine(AttackTargetLoop(GetPlayerUnitData().GetAttackCooldown()));
        }

    }

    /*
     * Collision handling.
     * If we collide with another PlayerUnit, we want our unit to stop by clearing the movementDestination and movementEnabled properties.
     * We also want to prevent the other PlayerUnit from bouncing as well, so we zero out its velocity.
     */
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "PlayerUnit") {
            movementDestination = transform.position;
            movementEnabled = false;

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 0);
        }
    }

    /*
     * Collision handling - prevent pushing.
     * If a player unit collides with another player unit, it will stop. If you proceed to move in the same direction,
     * the other player unit will be "pushed", until you "let go".
     */
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "PlayerUnit") {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 0);
        }
    }

    // Click Movement
    public void MoveToDestination(Vector2 destination) {
        if (Vector2.Distance(transform.position, destination) < 0.1) {
            movementEnabled = false;
            return;
        }

        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        Vector2 movementDirection = destination - (Vector2)transform.position;
        movementDirection.Normalize();
        rb2D.MovePosition(rb2D.position + (movementDirection * 2.5f)  * GetPlayerUnitData().GetMovementSpeed() * Time.deltaTime);
    }

    // Attacking
    private void SetTargetToClosestUnitInRange() {
        float lowestDistance = float.MaxValue;
        EnemyUnit lowestDistanceEnemy = null;

        foreach (EnemyUnit enemyUnit in attackRangeCircle.enemyUnitsInRange) {
            if (enemyUnit == null) {
                // TODO can this bug be handled better?
                attackRangeCircle.Cleanup();
                continue;
            }

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
            if (!attackRangeCircle.enemyUnitsInRange.Contains(currentTarget) || switchTargets) {
                switchTargets = false;
                break;
            }
            AttackTarget();
            yield return new WaitForSeconds(cooldown);
        }
        isAttacking = false;
    }

    private void AttackTarget() {
        Projectile projectile = Instantiate(projectilePrefab, transform).GetComponent<Projectile>();
        projectile.transform.parent = gameObject.transform.parent;     // detach projectile from player unit so projectile doesn't follow player unit
        float projectileDamage = playerUnitData.GetAttackDamage() + (GameEngine.GetInstance().upgradeManager.GetNumUpgrades(GetPlayerUnitData().GetUnitClass()) * GetPlayerUnitData().GetAttackUpgrade());
        projectile.InitializeProperties(currentTarget, this, projectileDamage);

        GameEngine.GetInstance().audioManager.PlayProjectileAttackSound(playerUnitData.GetUnitClass(), playerUnitData.GetRank());
    }
}
