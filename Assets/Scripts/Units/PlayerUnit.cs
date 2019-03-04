using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class PlayerUnit : Unit {
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
        Debug.Log(pointerEventData);

        GameEngine.GetInstance().unitSelectionPanel.ShowUnitSelectionPanel(this);
        
        if (GameEngine.GetInstance().playerUnitSelected != null) {
            GameEngine.GetInstance().playerUnitSelected.attackRangeCircle.SetAlpha(AttackRangeCircle.UNSELECTED_ALPHA);
        }
        GameEngine.GetInstance().playerUnitSelected = this;
        GameEngine.GetInstance().enemyUnitSelected = null;
        GameEngine.GetInstance().playerUnitSelected.attackRangeCircle.SetAlpha(AttackRangeCircle.SELECTED_ALPHA);
    }

    public override List<string> GetDisplayUnitData() {
        List<string> unitData = new List<string>();
        string title = "[" + this.rank + " Rank] " + this.displayName;
        string attackDamage = "Attack Damage: " + this.attackDamage;
        int numUpgrades = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(this.unitClass);
        if (numUpgrades > 0) {
            attackDamage += " (+ " + this.attackUpgrade * numUpgrades + ")";
        }
        string attackUpgrade = "Attack Upgrade: " + this.attackUpgrade;
        string attackSpeed = "Attack Speed: " + this.attackCooldown;
        string movementSpeed = "Movement Speed: " + this.movementSpeed;
        string attackType = "Attack Type: " + this.attackType.ToString();

        unitData.Add(title);
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
        if (Vector2.Distance((Vector2)this.transform.position, movementDestination) < 0.1f) {
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
        Projectile proj = (Projectile)Instantiate(projectile, transform).GetComponent<Projectile>();
        float projectileDamage = this.attackDamage + (GameEngine.GetInstance().upgradeManager.GetNumUpgrades(this.unitClass) * this.attackUpgrade);
        proj.InitializeProperties(currentTarget, this, projectileDamage);
    }
}
