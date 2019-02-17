public class UnitFactory {
    // ---------- Player Unit Creation ----------
    public PlayerUnitData CreatePlayerUnitData(PlayerUnitRank rank, int selection) {
        switch (rank) {
            case PlayerUnitRank.D:
                return CreateD(selection);
            case PlayerUnitRank.C:
                return CreateC(selection);
            case PlayerUnitRank.B:
                return CreateB(selection);
            case PlayerUnitRank.A:
                return CreateA(selection);
            case PlayerUnitRank.S:
                return CreateS(selection);
            case PlayerUnitRank.X:
                return CreateX(selection);
            default:
                throw new GameplayException("Rank value is invalid, cannot create unit");
        }
    }

    private PlayerUnitData CreateD(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.D;
        float attackDamage;
        float attackSpeed;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            case 0:
                displayName = "Infantry";
                movementSpeed = 2.0f;
                attackDamage = 5.0f;
                attackSpeed = 3.0f;
                attackRange = 2.6f;
                attackType = AttackType.NORMAL;
                break;
            case 1:
                displayName = "Mech";
                movementSpeed = 0.5f;
                attackDamage = 10.0f;
                attackSpeed = 0.8f;
                attackRange = 5.0f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            case 2:
                displayName = "Laser";
                movementSpeed = 1.0f;
                attackDamage = 12.0f;
                attackSpeed = 1.2f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
            case 3:
                displayName = "Psionic";
                movementSpeed = 1.0f;
                attackDamage = 14.0f;
                attackSpeed = 1.5f;
                attackRange = 1.8f;
                attackType = AttackType.SPLASH;
                break;
            case 4:
                displayName = "Acid";
                movementSpeed = 1.5f;
                attackDamage = 7.0f;
                attackSpeed = 2.0f;
                attackRange = 2.1f;
                attackType = AttackType.NORMAL;
                break;
            case 5:
                displayName = "Blade";
                movementSpeed = 4.0f;
                attackDamage = 3.5f;
                attackSpeed = 4.0f;
                attackRange = 2.4f;
                attackType = AttackType.NORMAL;
                break;
            default:
                throw new GameplayException("Selection value was invalid, cannot create unit.");
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackSpeed, attackRange, attackType);
    }

    private PlayerUnitData CreateC(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.C;
        float attackDamage;
        float attackSpeed;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            default:
                displayName = "Blade";
                movementSpeed = 4.0f;
                attackDamage = 3.5f;
                attackSpeed = 4.0f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackSpeed, attackRange, attackType);
    }

    private PlayerUnitData CreateB(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.B;
        float attackDamage;
        float attackSpeed;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            default:
                displayName = "Blade";
                movementSpeed = 4.0f;
                attackDamage = 3.5f;
                attackSpeed = 4.0f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackSpeed, attackRange, attackType);
    }

    private PlayerUnitData CreateA(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.A;
        float attackDamage;
        float attackSpeed;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            default:
                displayName = "Blade";
                movementSpeed = 4.0f;
                attackDamage = 3.5f;
                attackSpeed = 4.0f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackSpeed, attackRange, attackType);
    }

    private PlayerUnitData CreateS(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.S;
        float attackDamage;
        float attackSpeed;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            default:
                displayName = "Blade";
                movementSpeed = 4.0f;
                attackDamage = 3.5f;
                attackSpeed = 4.0f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackSpeed, attackRange, attackType);
    }

    private PlayerUnitData CreateX(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.X;
        float attackDamage;
        float attackSpeed;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            default:
                displayName = "Blade";
                movementSpeed = 4.0f;
                attackDamage = 3.5f;
                attackSpeed = 4.0f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackSpeed, attackRange, attackType);
    }

    // ---------- Enemy Unit Creation ----------
    public EnemyUnitData CreateEnemyUnitData(int level) {
        string displayName;
        float movementSpeed;
        float maxHealth;
        float currentHealth;
        float armor;
        int unitLevel;
        EnemyAbilities enemyAbilities;

        switch (level) {
            case 1:
                displayName = "Reaver";
                movementSpeed = 0.7f;
                maxHealth = 120.0f;
                currentHealth = maxHealth;
                armor = 0;
                unitLevel = 1;
                enemyAbilities = EnemyAbilities.NONE;
                break;
            default:
                throw new GameplayException("Unrecognized level value. Cannot create enemy user.");
        }

        return new EnemyUnitData(displayName, movementSpeed, maxHealth, armor, unitLevel, enemyAbilities);
    }

    public EnemyUnitData CreateBossUnit(int level) {
        // TODO will worry about bosses later
        return null;
    }

    public EnemyUnitData CreatePunchingBag(int number) {
        // TODO this is essentially the DA, and will worry about later
        return null;
    }
}
