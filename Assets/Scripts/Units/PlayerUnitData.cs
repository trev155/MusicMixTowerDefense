public class PlayerUnitData {
    private readonly string displayName;
    private readonly UnitClass unitClass;
    private readonly float movementSpeed;
    private readonly PlayerUnitRank rank;
    private readonly float attackDamage;
    private readonly float attackUpgrade;
    private readonly float attackCooldown;
    private readonly float attackRange;
    private readonly AttackType attackType;

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
        return this.displayName;
    }

    public UnitClass GetUnitClass() {
        return this.unitClass;
    }

    public float GetMovementSpeed() {
        return this.movementSpeed;
    }

    public PlayerUnitRank GetRank() {
        return this.rank;
    }

    public float GetAttackDamage() {
        return this.attackDamage;
    }

    public float GetAttackUpgrade() {
        return this.attackUpgrade;
    }
    
    public float GetAttackSpeed() {
        return this.attackCooldown;
    }

    public float GetAttackRange() {
        return this.attackRange;
    }

    public AttackType GetAttackType() {
        return this.attackType;
    }
}
