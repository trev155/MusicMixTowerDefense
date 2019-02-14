using UnityEngine;


public class EnemyUnit : Unit, IClickableUnit {
    //---------- Fields ----------
    public float health;
    public float armor;
    public int level;
    public EnemyAbilities abilities;

    private Transform currentWaypointDestination;

    //---------- Methods ----------
    public string GetTitleData() {
        return "Enemy: " + this.DisplayName;
    }

    public string GetBasicUnitData() {
        string data = "";
        data += "Health: " + this.health + "\n";
        data += "Armor: " + this.armor + "\n";
        data += "Level: " + this.level + "\n";
        data += "Movement Speed: " + this.MovementSpeed + "\n";
        return data;
    }

    public string GetAdvancedUnitData() {
        string data = "";
        data += "Abilities: " + this.abilities.ToString() + "\n";
        return data;
    }
    
    public void InitializeProperties(EnemyUnitData enemyUnitData) {
        this.DisplayName = enemyUnitData.GetDisplayName();
        this.MovementSpeed = enemyUnitData.GetMovementSpeed();
        this.health = enemyUnitData.GetHealth();
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
