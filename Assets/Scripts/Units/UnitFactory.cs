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
        float attackCooldown;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            case 0:
                displayName = "Infantry";
                movementSpeed = 2.0f;
                attackDamage = 9.0f;
                attackCooldown = 0.6f;
                attackRange = 2.2f;
                attackType = AttackType.NORMAL;
                break;
            case 1:
                displayName = "Mech";
                movementSpeed = 0.5f;
                attackDamage = 36.0f;
                attackCooldown = 3.0f;
                attackRange = 5.0f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            case 2:
                displayName = "Laser";
                movementSpeed = 1.0f;
                attackDamage = 12.0f;
                attackCooldown = 1.5f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
            case 3:
                displayName = "Psionic";
                movementSpeed = 1.0f;
                attackDamage = 14.0f;
                attackCooldown = 1.2f;
                attackRange = 1.8f;
                attackType = AttackType.SPLASH;
                break;
            case 4:
                displayName = "Acid";
                movementSpeed = 1.5f;
                attackDamage = 13.0f;
                attackCooldown = 0.9f;
                attackRange = 2.1f;
                attackType = AttackType.NORMAL;
                break;
            case 5:
                displayName = "Blade";
                movementSpeed = 3.0f;
                attackDamage = 11.0f;
                attackCooldown = 1.0f;
                attackRange = 2.4f;
                attackType = AttackType.NORMAL;
                break;
            default:
                throw new GameplayException("Selection value was invalid, cannot create unit.");
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackCooldown, attackRange, attackType);
    }

    private PlayerUnitData CreateC(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.C;
        float attackDamage;
        float attackCooldown;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            case 0:
                displayName = "Infantry";
                movementSpeed = 2.0f;
                attackDamage = 19.0f;
                attackCooldown = 0.6f;
                attackRange = 2.2f;
                attackType = AttackType.NORMAL;
                break;
            case 1:
                displayName = "Mech";
                movementSpeed = 0.5f;
                attackDamage = 82.0f;
                attackCooldown = 3.0f;
                attackRange = 5.0f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            case 2:
                displayName = "Laser";
                movementSpeed = 1.0f;
                attackDamage = 27.0f;
                attackCooldown = 1.5f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
            case 3:
                displayName = "Psionic";
                movementSpeed = 1.0f;
                attackDamage = 32.0f;
                attackCooldown = 1.2f;
                attackRange = 1.8f;
                attackType = AttackType.SPLASH;
                break;
            case 4:
                displayName = "Acid";
                movementSpeed = 1.5f;
                attackDamage = 28.0f;
                attackCooldown = 0.9f;
                attackRange = 2.1f;
                attackType = AttackType.NORMAL;
                break;
            case 5:
                displayName = "Blade";
                movementSpeed = 3.0f;
                attackDamage = 23.0f;
                attackCooldown = 1.0f;
                attackRange = 2.4f;
                attackType = AttackType.NORMAL;
                break;
            default:
                throw new GameplayException("Selection value was invalid, cannot create unit.");
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackCooldown, attackRange, attackType);
    }

    private PlayerUnitData CreateB(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.B;
        float attackDamage;
        float attackCooldown;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            case 0:
                displayName = "Infantry";
                movementSpeed = 2.0f;
                attackDamage = 62.0f;
                attackCooldown = 0.6f;
                attackRange = 2.2f;
                attackType = AttackType.NORMAL;
                break;
            case 1:
                displayName = "Mech";
                movementSpeed = 0.5f;
                attackDamage = 226.0f;
                attackCooldown = 3.0f;
                attackRange = 5.0f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            case 2:
                displayName = "Laser";
                movementSpeed = 1.0f;
                attackDamage = 71.0f;
                attackCooldown = 1.5f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
            case 3:
                displayName = "Psionic";
                movementSpeed = 1.0f;
                attackDamage = 102.0f;
                attackCooldown = 1.2f;
                attackRange = 1.8f;
                attackType = AttackType.SPLASH;
                break;
            case 4:
                displayName = "Acid";
                movementSpeed = 1.5f;
                attackDamage = 74.0f;
                attackCooldown = 0.9f;
                attackRange = 2.1f;
                attackType = AttackType.NORMAL;
                break;
            case 5:
                displayName = "Blade";
                movementSpeed = 3.0f;
                attackDamage = 66.0f;
                attackCooldown = 1.0f;
                attackRange = 2.4f;
                attackType = AttackType.NORMAL;
                break;
            case 6:
                displayName = "Magic (Rare)";
                movementSpeed = 1.5f;
                attackDamage = 500.0f;
                attackCooldown = 7.0f;
                attackRange = 1.5f;
                attackType = AttackType.SPLASH;
                break;
            case 7:
                displayName = "Flame (Rare)";
                movementSpeed = 1.2f;
                attackDamage = 90.0f;
                attackCooldown = 2.0f;
                attackRange = 1.0f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            default:
                throw new GameplayException("Selection value was invalid, cannot create unit.");
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackCooldown, attackRange, attackType);
    }

    private PlayerUnitData CreateA(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.A;
        float attackDamage;
        float attackCooldown;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            case 0:
                displayName = "Infantry";
                movementSpeed = 2.0f;
                attackDamage = 134.0f;
                attackCooldown = 0.6f;
                attackRange = 2.2f;
                attackType = AttackType.NORMAL;
                break;
            case 1:
                displayName = "Mech";
                movementSpeed = 0.5f;
                attackDamage = 514.0f;
                attackCooldown = 3.0f;
                attackRange = 5.0f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            case 2:
                displayName = "Laser";
                movementSpeed = 1.0f;
                attackDamage = 166.0f;
                attackCooldown = 1.5f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
            case 3:
                displayName = "Psionic";
                movementSpeed = 1.0f;
                attackDamage = 232.0f;
                attackCooldown = 1.2f;
                attackRange = 1.8f;
                attackType = AttackType.SPLASH;
                break;
            case 4:
                displayName = "Acid";
                movementSpeed = 1.5f;
                attackDamage = 171.0f;
                attackCooldown = 0.9f;
                attackRange = 2.1f;
                attackType = AttackType.NORMAL;
                break;
            case 5:
                displayName = "Blade";
                movementSpeed = 3.0f;
                attackDamage = 145.0f;
                attackCooldown = 1.0f;
                attackRange = 2.4f;
                attackType = AttackType.NORMAL;
                break;
            case 6:
                displayName = "Magic (Rare)";
                movementSpeed = 1.5f;
                attackDamage = 900.0f;
                attackCooldown = 5.0f;
                attackRange = 2.0f;
                attackType = AttackType.SPLASH;
                break;
            case 7:
                displayName = "Flame (Rare)";
                movementSpeed = 1.2f;
                attackDamage = 210.0f;
                attackCooldown = 1.8f;
                attackRange = 1.2f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            default:
                throw new GameplayException("Selection value was invalid, cannot create unit.");
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackCooldown, attackRange, attackType);
    }

    private PlayerUnitData CreateS(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.S;
        float attackDamage;
        float attackCooldown;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            case 0:
                displayName = "Infantry";
                movementSpeed = 2.0f;
                attackDamage = 291.0f;
                attackCooldown = 0.5f;
                attackRange = 2.2f;
                attackType = AttackType.NORMAL;
                break;
            case 1:
                displayName = "Mech";
                movementSpeed = 0.5f;
                attackDamage = 1568.0f;
                attackCooldown = 3.0f;
                attackRange = 5.0f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            case 2:
                displayName = "Laser";
                movementSpeed = 1.0f;
                attackDamage = 364.0f;
                attackCooldown = 1.2f;
                attackRange = 3.0f;
                attackType = AttackType.NORMAL;
                break;
            case 3:
                displayName = "Psionic";
                movementSpeed = 1.0f;
                attackDamage = 645.0f;
                attackCooldown = 1.2f;
                attackRange = 2.0f;
                attackType = AttackType.SPLASH;
                break;
            case 4:
                displayName = "Acid";
                movementSpeed = 1.5f;
                attackDamage = 409.0f;
                attackCooldown = 0.8f;
                attackRange = 2.3f;
                attackType = AttackType.NORMAL;
                break;
            case 5:
                displayName = "Blade";
                movementSpeed = 3.0f;
                attackDamage = 364.0f;
                attackCooldown = 0.8f;
                attackRange = 2.4f;
                attackType = AttackType.NORMAL;
                break;
            case 6:
                displayName = "Magic (Rare)";
                movementSpeed = 1.5f;
                attackDamage = 2200.0f;
                attackCooldown = 5.0f;
                attackRange = 2.0f;
                attackType = AttackType.SPLASH;
                break;
            case 7:
                displayName = "Flame (Rare)";
                movementSpeed = 1.2f;
                attackDamage = 660.0f;
                attackCooldown = 1.8f;
                attackRange = 1.3f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            default:
                throw new GameplayException("Selection value was invalid, cannot create unit.");
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackCooldown, attackRange, attackType);
    }

    private PlayerUnitData CreateX(int selection) {
        string displayName;
        float movementSpeed;
        PlayerUnitRank rank = PlayerUnitRank.X;
        float attackDamage;
        float attackCooldown;
        float attackRange;
        AttackType attackType;

        switch (selection) {
            case 0:
                displayName = "Infantry";
                movementSpeed = 2.0f;
                attackDamage = 565.0f;
                attackCooldown = 0.45f;
                attackRange = 2.3f;
                attackType = AttackType.NORMAL;
                break;
            case 1:
                displayName = "Mech";
                movementSpeed = 0.5f;
                attackDamage = 4000.0f;
                attackCooldown = 3.0f;
                attackRange = 5.5f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            case 2:
                displayName = "Laser";
                movementSpeed = 1.0f;
                attackDamage = 920.0f;
                attackCooldown = 1.2f;
                attackRange = 4.0f;
                attackType = AttackType.NORMAL;
                break;
            case 3:
                displayName = "Psionic";
                movementSpeed = 1.0f;
                attackDamage = 1420.0f;
                attackCooldown = 1.2f;
                attackRange = 2.5f;
                attackType = AttackType.SPLASH;
                break;
            case 4:
                displayName = "Acid";
                movementSpeed = 1.5f;
                attackDamage = 1102.0f;
                attackCooldown = 0.7f;
                attackRange = 2.3f;
                attackType = AttackType.NORMAL;
                break;
            case 5:
                displayName = "Blade";
                movementSpeed = 3.0f;
                attackDamage = 745.0f;
                attackCooldown = 0.6f;
                attackRange = 2.5f;
                attackType = AttackType.NORMAL;
                break;
            case 6:
                displayName = "Magic (Rare)";
                movementSpeed = 1.5f;
                attackDamage = 4620.0f;
                attackCooldown = 4.0f;
                attackRange = 2.4f;
                attackType = AttackType.SPLASH;
                break;
            case 7:
                displayName = "Flame (Rare)";
                movementSpeed = 1.2f;
                attackDamage = 660.0f;
                // upgrade is 3x of S rank
                attackCooldown = 1.6f;
                attackRange = 1.4f;
                attackType = AttackType.LARGE_SPLASH;
                break;
            default:
                throw new GameplayException("Selection value was invalid, cannot create unit.");
        }

        return new PlayerUnitData(displayName, movementSpeed, rank, attackDamage, attackCooldown, attackRange, attackType);
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
                movementSpeed = 0.4f;
                maxHealth = 105.0f;
                currentHealth = maxHealth;
                armor = 0;
                unitLevel = 1;
                enemyAbilities = EnemyAbilities.NONE;
                break;
            case 2:
                displayName = "Archon";
                movementSpeed = 1.2f;
                maxHealth = 196.0f;
                currentHealth = maxHealth;
                armor = 0;
                unitLevel = 2;
                enemyAbilities = EnemyAbilities.NONE;
                break;
            case 3:
                displayName = "Dragoon";
                movementSpeed = 1.4f;
                maxHealth = 324.0f;
                currentHealth = maxHealth;
                armor = 1;
                unitLevel = 3;
                enemyAbilities = EnemyAbilities.NONE;
                break;
            case 4:
                displayName = "Templar";
                movementSpeed = 1.2f;
                maxHealth = 398.0f;
                currentHealth = maxHealth;
                armor = 2;
                unitLevel = 4;
                enemyAbilities = EnemyAbilities.NONE;
                break;
            case 5:
                displayName = "Vulture";
                movementSpeed = 2.1f;
                maxHealth = 476.0f;
                currentHealth = maxHealth;
                armor = 2;
                unitLevel = 5;
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
