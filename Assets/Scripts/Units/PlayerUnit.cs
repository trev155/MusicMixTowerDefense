public class PlayerUnit : Unit, IClickableUnit {
    //---------- Fields ----------
    public PlayerUnitRank rank;
    public float attackDamage;
    public float attackSpeed;
    public AttackType attackType;
    // TODO reference to the projectile prefab

    
    //---------- Methods ----------
    public string GetTitleData() {
        return "[" + this.rank + " Rank Unit] " + this.DisplayName;
    }

    public string GetBasicUnitData() {
        string data = "";
        data += "Rank: " + this.rank + "\n";
        data += "Attack Damage: " + this.attackDamage + "\n";
        data += "Attack Speed: " + this.attackSpeed + "\n";
        data += "Movement Speed: " + this.MovementSpeed + "\n";
        return data;
    }

    public string GetAdvancedUnitData() {
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
}
