public class EnemyUnitData {
    private readonly string displayName;
    private readonly float movementSpeed;
    private readonly float maxHealth;
    private readonly float armor;
    private readonly int level;
    private readonly EnemyAbilities enemyAbilities;

    public EnemyUnitData(
        string displayName, 
        float movementSpeed, 
        float maxHealth, 
        float armor,
        int level,
        EnemyAbilities enemyAbilities) {
        this.displayName = displayName;
        this.movementSpeed = movementSpeed;
        this.maxHealth = maxHealth;
        this.armor = armor;
        this.level = level;
        this.enemyAbilities = enemyAbilities;
    }

    public string GetDisplayName() {
        return this.displayName;
    }

    public float GetMovementSpeed() {
        return this.movementSpeed;
    }

    public float GetMaxHealth() {
        return this.maxHealth;
    }

    public float GetArmor() {
        return this.armor;
    }
    
    public int GetLevel() {
        return this.level;
    }

    public EnemyAbilities GetEnemyAbilities() {
        return this.enemyAbilities;
    }
}