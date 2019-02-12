public class PlayerUnit : Unit, IClickableUnit {
    //---------- Fields ----------
    public PlayerUnitRank rank;
    public float attackDamage;
    public float attackSpeed;
    public AttackType attackType;
    // TODO reference to the projectile prefab

    
    //---------- Methods ----------
    public string GetUnitTypeString() {
        return "Player Unit";
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
