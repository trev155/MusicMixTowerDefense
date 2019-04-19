using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class PlayerUnit : Unit {
    //---------- Constants ----------
    public static readonly float SELECTED_ALPHA = 0.8f;
    public static readonly float UNSELECTED_ALPHA = 0.2f;

    //---------- Fields ----------
    public UnitClass unitClass;
    public PlayerUnitRank rank;
    public float attackDamage;
    public float attackUpgrade;
    public float attackCooldown;
    public float attackRange;
    public AttackType attackType;

    public bool movementEnabled = false;
    public Vector2 movementDestination;

    public AttackRangeCircle attackRangeCircle;
    public Projectile projectile;

    public EnemyUnit currentTarget;
    public bool isAttacking = false;
    public bool switchTargets = false;

    //---------- Methods ----------
    public void InitializeProperties(PlayerUnitData playerUnitData) {
        this.displayName = playerUnitData.GetDisplayName();
        this.unitClass = playerUnitData.GetUnitClass();
        this.movementSpeed = playerUnitData.GetMovementSpeed();
        this.rank = playerUnitData.GetRank();
        this.attackDamage = playerUnitData.GetAttackDamage();
        this.attackUpgrade = playerUnitData.GetAttackUpgrade();
        this.attackCooldown = playerUnitData.GetAttackSpeed();
        this.attackRange = playerUnitData.GetAttackRange();
        this.attackType = playerUnitData.GetAttackType();
    }

    public override void OnPointerClick(PointerEventData pointerEventData) {
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

    public override List<string> GetDisplayUnitData() {
        List<string> unitData = new List<string>();
        string title = "[" + this.rank + " Rank] " + this.displayName;
        string unitType = "Unit Class: " + Utils.CleanEnumString(this.unitClass.ToString());
        string attackDamage = "Attack Damage: " + this.attackDamage;
        int numUpgrades = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(this.unitClass);
        if (numUpgrades > 0) {
            attackDamage += " (+ " + this.attackUpgrade * numUpgrades + ")";
        }
        string attackUpgrade = "Attack Upgrade: " + this.attackUpgrade;
        string attackSpeed = "Attack Speed: " + this.attackCooldown;
        string movementSpeed = "Movement Speed: " + this.movementSpeed;
        string attackType = "Attack Type: " + Utils.CleanEnumString(this.attackType.ToString());

        unitData.Add(title);
        unitData.Add(unitType);
        unitData.Add(attackDamage);
        unitData.Add(attackUpgrade);
        unitData.Add(attackSpeed);
        unitData.Add(movementSpeed);
        unitData.Add(attackType);

        return unitData;
    }
    
    // Collisions 
    private void OnCollisionEnter2D(Collision2D collision) {
        this.movementEnabled = false;
    }

    // Update per frame
    private void Update() {
        if (movementEnabled) {
            Move();
        }

        if (attackRangeCircle.enemyUnitsInRange.Count > 0 && !isAttacking) {
            SetTargetToClosestUnitInRange();
            StartCoroutine(AttackTargetLoop(this.attackCooldown));
        }

    }

    // Click Movement
    private void Move() {
        if (Vector2.Distance(this.transform.position, movementDestination) < 0.1f) {
            movementEnabled = false;
            return;
        } else {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementDestination, Time.deltaTime * 2.0f * this.movementSpeed);
        }
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
        Projectile proj = Instantiate(projectile, transform).GetComponent<Projectile>();
        float projectileDamage = this.attackDamage + (GameEngine.GetInstance().upgradeManager.GetNumUpgrades(this.unitClass) * this.attackUpgrade);
        proj.InitializeProperties(currentTarget, this, projectileDamage);
    }
}
