using UnityEngine;
using UnityEngine.EventSystems;


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
        this.DisplayName = enemyUnitData.GetDisplayName();
        this.MovementSpeed = enemyUnitData.GetMovementSpeed();
        this.maxHealth = enemyUnitData.GetMaxHealth();
        this.currentHealth = enemyUnitData.GetMaxHealth();
        this.armor = enemyUnitData.GetArmor();
        this.level = enemyUnitData.GetLevel();
        this.abilities = enemyUnitData.GetEnemyAbilities();
    }

    public override void OnPointerClick(PointerEventData pointerEventData) {
        if (GameEngine.Instance.playerUnitMovementAllowed) {
            GameEngine.Instance.DisablePlayerUnitMovement();
        }
        if (GameEngine.Instance.playerUnitSelected != null) {
            GameEngine.Instance.playerUnitSelected.attackRangeCircle.SetAlpha(AttackRangeCircle.UNSELECTED_ALPHA);
        }
        GameEngine.Instance.hudManager.ShowUnitSelectionPanel(this);
    }

    public override string GetTitleData() {
        return "Enemy: " + this.DisplayName;
    }

    public override string GetBasicUnitData() {
        string data = "";
        data += "Health: " + this.currentHealth + " / " + this.maxHealth + "\n";
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
