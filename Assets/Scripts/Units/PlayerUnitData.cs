public class PlayerUnitData {
    private readonly string displayName;
    private readonly float movementSpeed;
    private readonly PlayerUnitRank rank;
    private readonly float attackDamage;
    private readonly float attackCooldown;
    private readonly float attackRange;
    private readonly AttackType attackType;

    public PlayerUnitData(
        string displayName,
        float movementSpeed,
        PlayerUnitRank rank,
        float attackDamage,
        float attackSpeed,
        float attackRange,
        AttackType attackType) {
        this.displayName = displayName;
        this.movementSpeed = movementSpeed;
        this.rank = rank;
        this.attackDamage = attackDamage;
        this.attackCooldown = attackSpeed;
        this.attackRange = attackRange;
        this.attackType = attackType;
    }

    public string GetDisplayName() {
        return this.displayName;
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
