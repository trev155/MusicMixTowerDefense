using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class EnemyUnit : Unit {
    //---------- Constants ----------
    public static readonly float SELECTED_ALPHA = 0.8f;
    public static readonly float UNSELECTED_ALPHA = 0;

    //---------- Fields ----------
    public float maxHealth;
    public float currentHealth;
    public float armor;
    public int level;
    public EnemyAbilities abilities;

    public Transform selectedUnitCircle;

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
        // If currently selected a player unit, adjust accordingly
        if (GameEngine.GetInstance().playerUnitMovementAllowed) {
            GameEngine.GetInstance().DisablePlayerUnitMovement();
        }

        // Clear alpha of previously selected units
        if (GameEngine.GetInstance().playerUnitSelected != null) {
            Utils.SetAlpha(GameEngine.GetInstance().playerUnitSelected.attackRangeCircle.transform, PlayerUnit.UNSELECTED_ALPHA);
        }
        if (GameEngine.GetInstance().enemyUnitSelected != null) {
            Utils.SetAlpha(GameEngine.GetInstance().enemyUnitSelected.selectedUnitCircle, UNSELECTED_ALPHA);
        }

        // Update references to objects in the game engine
        GameEngine.GetInstance().playerUnitSelected = null;
        GameEngine.GetInstance().enemyUnitSelected = this;

        // Set alpha value
        Utils.SetAlpha(this.selectedUnitCircle, SELECTED_ALPHA);
        
        // Show data on unit selection panel
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
