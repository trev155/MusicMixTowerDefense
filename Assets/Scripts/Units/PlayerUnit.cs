using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerUnit : Unit {
    //---------- Fields ----------
    public PlayerUnitRank rank;
    public float attackDamage;
    public float attackSpeed;
    public AttackType attackType;

    public bool movementEnabled = false;
    public Vector2 movementDestination;


    //---------- Methods ----------
    public override void OnPointerClick(PointerEventData pointerEventData) {
        GameEngine.Instance.hudManager.ShowUnitSelectionPanel(this);
    }

    public override string GetTitleData() {
        return "[" + this.rank + " Rank Unit] " + this.DisplayName;
    }

    public override string GetBasicUnitData() {
        string data = "";
        data += "Rank: " + this.rank + "\n";
        data += "Attack Damage: " + this.attackDamage + "\n";
        data += "Attack Speed: " + this.attackSpeed + "\n";
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
        this.attackSpeed = playerUnitData.GetAttackSpeed();
        this.attackType = playerUnitData.GetAttackType();
    }

    // Click movement
    private void Update() {
        if (movementEnabled) {
            Move();
        }
    }

    private void Move() {
        if ((Vector2)this.transform.position == movementDestination) {
            movementEnabled = false;
            return;
        } else {
            this.transform.position = Vector2.MoveTowards(this.transform.position, movementDestination, Time.deltaTime * 3.0f * this.MovementSpeed);
        }
    }
}
