using UnityEngine;
using UnityEngine.EventSystems;


public class EnemyUnit : Unit {
    //---------- Fields ----------
    public float health;
    public float armor;
    public int level;
    public EnemyAbilities abilities;

    private Transform currentWaypointDestination;


    //---------- Methods ----------
    public override void OnPointerClick(PointerEventData pointerEventData) {
        if (GameEngine.Instance.playerUnitMovementAllowed) {
            return;
        }
        GameEngine.Instance.hudManager.ShowUnitSelectionPanel(this);
    }

    public override string GetTitleData() {
        return "Enemy: " + this.DisplayName;
    }

    public override string GetBasicUnitData() {
        string data = "";
        data += "Health: " + this.health + "\n";
        data += "Armor: " + this.armor + "\n";
        data += "Level: " + this.level + "\n";
        data += "Movement Speed: " + this.MovementSpeed + "\n";
        return data;
    }

    public override string GetAdvancedUnitData() {
        string data = "";
        data += "Abilities: " + this.abilities.ToString() + "\n";
        return data;
    }
    
    public void InitializeProperties(EnemyUnitData enemyUnitData) {
        this.DisplayName = enemyUnitData.GetDisplayName();
        this.MovementSpeed = enemyUnitData.GetMovementSpeed();
        this.health = enemyUnitData.GetMaxHealth();
        this.armor = enemyUnitData.GetArmor();
        this.level = enemyUnitData.GetLevel();
        this.abilities = enemyUnitData.GetEnemyAbilities();
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
        transform.position = Vector2.MoveTowards(transform.position, currentWaypointDestination.position, MovementSpeed * Time.deltaTime);
    }
}
