public class EnemyUnitData {
    private string displayName;
    private float movementSpeed;
    private float maxHealth;
    private float armor;
    private float maxShields;
    private float shieldRegenerationRate;
    private int level;
    private EnemyType enemyType;

    // Constructors
    public EnemyUnitData(
        string displayName, 
        float movementSpeed, 
        float maxHealth,
        float armor,
        float maxShields,
        float shieldRegenerationRate,
        int level,
        EnemyType enemyType) {
        this.displayName = displayName;
        this.movementSpeed = movementSpeed;
        this.maxHealth = maxHealth;
        this.armor = armor;
        this.maxShields = maxShields;
        this.shieldRegenerationRate = shieldRegenerationRate;
        this.level = level;
        this.enemyType = enemyType;
    }

    public static EnemyUnitData ConstructDataForNormal(string displayName, float movementSpeed, float maxHealth, float armor, int level) {
        return new EnemyUnitData(displayName, movementSpeed, maxHealth, armor, 0, 0, level, EnemyType.NORMAL);
    }

    public static EnemyUnitData ConstructDataForBounty(string displayName, float movementSpeed, float maxHealth, float armor) {
        return new EnemyUnitData(displayName, movementSpeed, maxHealth, armor, 0, 0, 0, EnemyType.BOUNTY);
    }

    public static EnemyUnitData ConstructDataForBonus(float maxHealth, float armor, float shields, float shieldRegenerationRate) {
        return new EnemyUnitData("Bonus Token", 0, maxHealth, armor, shields, shieldRegenerationRate, 0, EnemyType.BONUS);
    }

    public static EnemyUnitData ConstructDataForBoss(string displayName, float movementSpeed, float maxHealth, float armor, float shields, float shieldRegenerationRate) {
        return new EnemyUnitData(displayName, movementSpeed, maxHealth, armor, shields, shieldRegenerationRate, 0, EnemyType.BONUS);
    }

    // Getters and setters
    public string GetDisplayName() {
        return displayName;
    }

    public float GetMovementSpeed() {
        return movementSpeed;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public float GetArmor() {
        return armor;
    }

    public float GetMaxShields() {
        return maxShields;
    }

    public float GetShieldRegenerationRate() {
        return shieldRegenerationRate;
    }

    public int GetLevel() {
        return level;
    }

    public EnemyType GetEnemyType() {
        return enemyType;
    }

    public void SetDisplayName(string displayName) {
        this.displayName = displayName;
    }

    public void SetMovementSpeed(float movementSpeed) {
        this.movementSpeed = movementSpeed;
    }

    public void SetMaxHealth(float maxHealth) {
        this.maxHealth = maxHealth;
    }

    public void SetArmor(float armor) {
        this.armor = armor;
    }

    public void SetMaxShields(float maxShields) {
        this.maxShields = maxShields;
    }

    public void SetShieldRegenerationRate(float shieldRegenerationRate) {
        this.shieldRegenerationRate = shieldRegenerationRate;
    }

    public void SetLevel(int level) {
        this.level = level;
    }

    public void SetEnemyType(EnemyType enemyType) {
        this.enemyType = enemyType;
    }
}