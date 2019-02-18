using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerUnit : Unit {
    //---------- Fields ----------
    public PlayerUnitRank rank;
    public float attackDamage;
    public float attackCooldown;
    public float attackRange;
    public AttackType attackType;

    public bool movementEnabled = false;
    public Vector2 movementDestination;

    public AttackRangeCircle attackRangeCircle;

    //---------- Methods ----------
    /*
     * Handler for unit selection. Show unit selection panel data. Save unit selected in game engine. 
     */ 
    public override void OnPointerClick(PointerEventData pointerEventData) {
        GameEngine.Instance.hudManager.ShowUnitSelectionPanel(this);
        
        if (GameEngine.Instance.playerUnitSelected != null) {
            GameEngine.Instance.playerUnitSelected.attackRangeCircle.SetAlpha(AttackRangeCircle.UNSELECTED_ALPHA);
        }
        GameEngine.Instance.playerUnitSelected = this;
        GameEngine.Instance.playerUnitSelected.attackRangeCircle.SetAlpha(AttackRangeCircle.SELECTED_ALPHA);
    }

    public override string GetTitleData() {
        return "[" + this.rank + " Rank Unit] " + this.DisplayName;
    }

    public override string GetBasicUnitData() {
        string data = "";
        data += "Rank: " + this.rank + "\n";
        data += "Attack Damage: " + this.attackDamage + "\n";
        data += "Attack Speed: " + this.attackCooldown + "\n";
        data += "Movement Speed: " + this.MovementSpeed + "\n";
        return data;
    }

    public override string GetAdvancedUnitData() {
        string data = "";
        data += "Attack Type: " + this.attackType.ToString() + "\n";
        return data;
    }

    public void InitializeProperties(PlayerUnitData playerUnitData) {
        this.DisplayName = playerUnitData.GetDisplayName();
        this.MovementSpeed = playerUnitData.GetMovementSpeed();
        this.rank = playerUnitData.GetRank();
        this.attackDamage = playerUnitData.GetAttackDamage();
        this.attackCooldown = playerUnitData.GetAttackSpeed();
        this.attackRange = playerUnitData.GetAttackRange();
        this.attackType = playerUnitData.GetAttackType();
    }

    // Click movement
    private void Update() {
        if (movementEnabled) {
            Move();
        }
    }

    private void Move() {
        if (Vector2.Distance((Vector2)this.transform.position, movementDestination) < 0.1f) {
            movementEnabled = false;
            return;
        } else {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementDestination, Time.deltaTime * 2.0f * this.MovementSpeed);
        }
    }

    // Collisions 
    private void OnCollisionEnter2D(Collision2D collision) {
        this.movementEnabled = false;
    }
}
