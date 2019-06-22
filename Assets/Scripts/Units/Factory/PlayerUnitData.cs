public class PlayerUnitData {
    private string displayName;
    private UnitClass unitClass;
    private float movementSpeed;
    private PlayerUnitRank rank;
    private float attackDamage;
    private float attackUpgrade;
    private float attackCooldown;
    private float attackRange;
    private AttackType attackType;

    public PlayerUnitData(
        string displayName,
        UnitClass unitClass,
        float movementSpeed,
        PlayerUnitRank rank,
        float attackDamage,
        float attackUpgrade,
        float attackCooldown,
        float attackRange,
        AttackType attackType) {
        this.displayName = displayName;
        this.unitClass = unitClass;
        this.movementSpeed = movementSpeed;
        this.rank = rank;
        this.attackDamage = attackDamage;
        this.attackUpgrade = attackUpgrade;
        this.attackCooldown = attackCooldown;
        this.attackRange = attackRange;
        this.attackType = attackType;
    }

    public string GetDisplayName() {
        return displayName;
    }

    public UnitClass GetUnitClass() {
        return unitClass;
    }

    public float GetMovementSpeed() {
        return movementSpeed;
    }

    public PlayerUnitRank GetRank() {
        return rank;
    }

    public float GetAttackDamage() {
        return attackDamage;
    }

    public float GetAttackUpgrade() {
        return attackUpgrade;
    }
    
    public float GetAttackCooldown() {
        return attackCooldown;
    }

    public float GetAttackRange() {
        return attackRange;
    }

    public AttackType GetAttackType() {
        return attackType;
    }

    public void SetDisplayName(string displayName) {
        this.displayName = displayName;
    }

    public void SetUnitClass(UnitClass unitClass) {
        this.unitClass = unitClass;
    }

    public void SetRank(PlayerUnitRank rank) {
        this.rank = rank;
    }

    public void SetAttackDamage(float attackDamage) {
        this.attackDamage = attackDamage;
    }

    public void SetAttackUpgrade(float attackUpgrade) {
        this.attackUpgrade = attackUpgrade;
    }

    public void SetAttackCooldown(float attackCooldown) {
        this.attackCooldown = attackCooldown;
    }

    public void SetAttackRange(float attackRange) {
        this.attackRange = attackRange;
    }

    public void SetAttackType(AttackType attackType) {
        this.attackType = attackType;
    }
}
