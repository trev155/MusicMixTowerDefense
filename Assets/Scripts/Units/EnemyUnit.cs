using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class EnemyUnit : Unit {
    //---------- Fields ----------
    public float maxHealth;
    public float currentHealth;
    public float armor;
    public int level;
    public EnemyAbilities abilities;

    private Transform currentWaypointDestination;


    //---------- Methods ----------
    public void InitializeProperties(EnemyUnitData enemyUnitData) {
        this.displayName = enemyUnitData.GetDisplayName();
        this.movementSpeed = enemyUnitData.GetMovementSpeed();
        this.maxHealth = enemyUnitData.GetMaxHealth();
        this.currentHealth = enemyUnitData.GetMaxHealth();
        this.armor = enemyUnitData.GetArmor();
        this.level = enemyUnitData.GetLevel();
        this.abilities = enemyUnitData.GetEnemyAbilities();
    }

    public override void OnPointerClick(PointerEventData pointerEventData) {
        if (GameEngine.GetInstance().playerUnitMovementAllowed) {
            GameEngine.GetInstance().DisablePlayerUnitMovement();
        }
        if (GameEngine.GetInstance().playerUnitSelected != null) {
            GameEngine.GetInstance().playerUnitSelected.attackRangeCircle.SetAlpha(AttackRangeCircle.UNSELECTED_ALPHA);
        }

        if (GameEngine.GetInstance().enemyUnitSelected != this) {
            GameEngine.GetInstance().enemyUnitSelected = this;
        }
        
        GameEngine.GetInstance().unitSelectionPanel.ShowUnitSelectionPanel(this);
    }

    public override List<string> GetDisplayUnitData() {
        List<string> unitData = new List<string>();
        string title = "Enemy: " + this.displayName;
        string attackDamage = "Health: " + this.currentHealth + " / " + this.maxHealth;
        string attackSpeed = "Armor: " + this.armor;
        string movementSpeed = "Level: " + this.level;
        string attackType = "Movement Speed: " + this.movementSpeed;
        string abilities = "Abilities: " + this.abilities.ToString();

        unitData.Add(title);
        unitData.Add(attackDamage);
        unitData.Add(attackSpeed);
        unitData.Add(movementSpeed);
        unitData.Add(attackType);
        unitData.Add(abilities);

        return unitData;
    }
    
    private void Update() {
        if (currentWaypointDestination != null) {
            MoveToNextWaypoint();
        }
    }
    
    public void UpdateWaypointDestination(Transform destination) {
        currentWaypointDestination = destination;
    }

    private void MoveToNextWaypoint() {
        transform.position = Vector2.MoveTowards(transform.position, currentWaypointDestination.position, movementSpeed * Time.deltaTime);
    }
}
