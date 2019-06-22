using UnityEngine;
using System.Collections.Generic;


public class UnitFactory {
    private static readonly string PLAYER_UNIT_DATA_PATH = "UnitData/PlayerUnits";
    private static readonly string ENEMY_UNIT_DATA_PATH = "UnitData/EnemyUnits";
    private static readonly string SPECIAL_ENEMY_UNIT_DATA_PATH = "UnitData/SpecialEnemyUnits";
    private static readonly string BONUS_UNIT_DATA_PATH = "UnitData/BonusUnitData";

    private Dictionary<PlayerUnitRank, Dictionary<UnitClass, PlayerUnitData>> playerUnitData;
    private Dictionary<int, EnemyUnitData> enemyUnitData;
    private Dictionary<string, EnemyUnitData> specialEnemyData;
    private Dictionary<int, EnemyUnitData> bonusUnitData;

    // ---------- Initialization ----------
    public UnitFactory() {
        InitializeFactory();
    }

    private void InitializeFactory() {
        InitializePlayerUnitData();
        InitializeEnemyUnitData();
        InitializeSpecialEnemyUnitData();
        InitializeBonusUnitData();
    }

    // Read in player unit data
    private void InitializePlayerUnitData() {
        playerUnitData = new Dictionary<PlayerUnitRank, Dictionary<UnitClass, PlayerUnitData>> {
            { PlayerUnitRank.D, new Dictionary<UnitClass, PlayerUnitData>() },
            { PlayerUnitRank.C, new Dictionary<UnitClass, PlayerUnitData>() },
            { PlayerUnitRank.B, new Dictionary<UnitClass, PlayerUnitData>() },
            { PlayerUnitRank.A, new Dictionary<UnitClass, PlayerUnitData>() },
            { PlayerUnitRank.S, new Dictionary<UnitClass, PlayerUnitData>() },
            { PlayerUnitRank.X, new Dictionary<UnitClass, PlayerUnitData>() }
        };

        PlayerUnitRank rank;
        string displayName;
        UnitClass unitClass;
        AttackType attackType;
        float movementSpeed;
        float attackDamage;
        float attackUpgrade;
        float attackCooldown;
        float attackRange;
        
        int lineIndex = 0;
        TextAsset playerData = Resources.Load<TextAsset>(PLAYER_UNIT_DATA_PATH);
        string[] lines = playerData.text.Split('\n');
        
        foreach (string line in lines) {
            if (lineIndex == 0) {
                lineIndex++;
                continue;   // skip file header
            }

            string[] lineTokens = line.Trim().Split(',');
            if (lineTokens.Length <= 1) {
                lineIndex++;
                continue;
            }

            rank = ConvertPlayerUnitRankString(lineTokens[0]);
            displayName = lineTokens[1];
            unitClass = ConvertUnitClassString(lineTokens[2]);
            attackType = ConvertAttackTypeString(lineTokens[3]);
            movementSpeed = float.Parse(lineTokens[4]);
            attackDamage = float.Parse(lineTokens[5]);
            attackUpgrade = float.Parse(lineTokens[6]);
            attackCooldown = float.Parse(lineTokens[7]);
            attackRange = float.Parse(lineTokens[8]);

            PlayerUnitData playerUnitData = new PlayerUnitData(displayName,
                unitClass,
                movementSpeed,
                rank,
                attackDamage,
                attackUpgrade,
                attackCooldown,
                attackRange,
                attackType);
            this.playerUnitData[rank].Add(unitClass, playerUnitData);

            lineIndex++;
        }
        Debug.Log("Finished reading player unit data. Read: [" + (lineIndex + 1) + "] number of lines.");
    }
    
    private UnitClass ConvertUnitClassString(string s) {
        switch (s) {
            case "Infantry":
                return UnitClass.INFANTRY;
            case "Mech":
                return UnitClass.MECH;
            case "Laser":
                return UnitClass.LASER;
            case "Psionic":
                return UnitClass.PSIONIC;
            case "Acid":
                return UnitClass.ACID;
            case "Blade":
                return UnitClass.BLADE;
            case "Magic":
                return UnitClass.MAGIC;
            case "Flame":
                return UnitClass.FLAME;
            default:
                throw new GameplayException("Could not convert string " + s + " to a UnitClass object.");
        }
    }

    private PlayerUnitRank ConvertPlayerUnitRankString(string s) {
        switch (s) {
            case "D":
                return PlayerUnitRank.D;
            case "C":
                return PlayerUnitRank.C;
            case "B":
                return PlayerUnitRank.B;
            case "A":
                return PlayerUnitRank.A;
            case "S":
                return PlayerUnitRank.S;
            case "X":
                return PlayerUnitRank.X;
            default:
                throw new GameplayException("Could not convert string " + s + " to a PlayerUnitRank object.");
        }
    }

    private AttackType ConvertAttackTypeString(string s) {
        switch (s) {
            case "Normal":
                return AttackType.NORMAL;
            case "Splash":
                return AttackType.SPLASH;
            case "LargeSplash":
                return AttackType.LARGE_SPLASH;
            default:
                throw new GameplayException("Could not convert string " + s + " to an AttackType object.");
        }
    }

    // Read in enemy unit data
    private void InitializeEnemyUnitData() {
        enemyUnitData = new Dictionary<int, EnemyUnitData>();

        int level;
        string displayName;
        float movementSpeed;
        float maxHealth;
        float armor;

        int lineIndex = 0;
        TextAsset data = Resources.Load<TextAsset>(ENEMY_UNIT_DATA_PATH);
        string[] lines = data.text.Split('\n');
        foreach (string line in lines) {
            // skip file header
            if (lineIndex == 0) {
                lineIndex++;
                continue;
            }

            // Parse the line
            string[] lineTokens = line.Trim().Split(',');
            if (lineTokens.Length <= 1) {
                lineIndex++;
                continue;
            }
            level = int.Parse(lineTokens[0]);
            displayName = lineTokens[1];
            movementSpeed = float.Parse(lineTokens[2]);
            maxHealth = float.Parse(lineTokens[3]);
            armor = float.Parse(lineTokens[4]);

            // Construct data object and store it
            EnemyUnitData enemyUnitData = EnemyUnitData.ConstructDataForNormal(displayName, movementSpeed, maxHealth, armor, level);
            this.enemyUnitData[level] = enemyUnitData;
            
            lineIndex++;
        }

        Debug.Log("Finished reading enemy unit data. Read: [" + (lineIndex + 1) + "] number of lines.");
    }
    
    // Read in special enemy unit data
    private void InitializeSpecialEnemyUnitData() {
        specialEnemyData = new Dictionary<string, EnemyUnitData>();

        int lineIndex = 0;
        TextAsset data = Resources.Load<TextAsset>(SPECIAL_ENEMY_UNIT_DATA_PATH);
        string[] lines = data.text.Split('\n');
        foreach (string line in lines) {
            // skip file header
            if (lineIndex == 0) {
                lineIndex++;
                continue;
            }

            // Parse the line
            string[] lineTokens = line.Trim().Split(',');
            if (lineTokens.Length <= 1) {
                lineIndex++;
                continue;
            }

            EnemyUnitData enemyUnitData;
            string displayName = lineTokens[0];
            float movementSpeed = float.Parse(lineTokens[1]);
            float maxHealth = float.Parse(lineTokens[2]);
            float armor = float.Parse(lineTokens[3]);
            float shields = float.Parse(lineTokens[4]);
            float shieldRegenRate = float.Parse(lineTokens[5]);
            if (displayName == "Bounty") {
                enemyUnitData = EnemyUnitData.ConstructDataForBounty(displayName, movementSpeed, maxHealth, armor);
            } else {
                enemyUnitData = EnemyUnitData.ConstructDataForBoss(displayName, movementSpeed, maxHealth, armor, shields, shieldRegenRate);
            }
            specialEnemyData[displayName] = enemyUnitData;
            
            lineIndex++;
        }

        Debug.Log("Finished reading special enemy unit data. Read: [" + (lineIndex + 1) + "] number of lines.");
    }

    // Read in bonus unit data
    private void InitializeBonusUnitData() {
        bonusUnitData = new Dictionary<int, EnemyUnitData>();

        int lineIndex = 0;
        TextAsset data = Resources.Load<TextAsset>(BONUS_UNIT_DATA_PATH);
        string[] lines = data.text.Split('\n');
        foreach (string line in lines) {
            // skip file header
            if (lineIndex == 0) {
                lineIndex++;
                continue;
            }

            // Parse the line
            string[] lineTokens = line.Trim().Split(',');
            if (lineTokens.Length <= 1) {
                lineIndex++;
                continue;
            }
            
            int number = int.Parse(lineTokens[0]);
            float maxHealth = float.Parse(lineTokens[1]);
            float armor = float.Parse(lineTokens[2]);
            float shields = float.Parse(lineTokens[3]);
            float shieldRegenRate = float.Parse(lineTokens[4]);
            EnemyUnitData enemyUnitData = EnemyUnitData.ConstructDataForBonus(maxHealth, armor, shields, shieldRegenRate);
            bonusUnitData[number] = enemyUnitData;
            
            lineIndex++;
        }

        Debug.Log("Finished reading bonus unit data. Read: [" + (lineIndex + 1) + "] number of lines.");
    }
    
    // ---------- Player Unit Creation ----------
    public PlayerUnitData CreatePlayerUnitData(PlayerUnitRank rank, UnitClass unitClass) {
        PlayerUnitData data = playerUnitData[rank][unitClass];
        return data;
    }

    public PlayerUnitData CreatePlayerUnitData(PlayerUnitRank rank, int selection) {
        UnitClass unitClass = ConvertIntegerToUnitClass(selection);

        if (unitClass == UnitClass.MAGIC || unitClass == UnitClass.FLAME) {
            if (rank == PlayerUnitRank.D || rank == PlayerUnitRank.C) {
                throw new GameplayException("Cannot create player unit data object. Invalid combination for rank and selection: " + rank + " " + selection);
            }
        }

        PlayerUnitData playerUnitData = CreatePlayerUnitData(rank, unitClass);
        return playerUnitData;
    }

    private UnitClass ConvertIntegerToUnitClass(int selection) {
        UnitClass unitClass;
        switch (selection) {
            case 0:
                unitClass = UnitClass.INFANTRY;
                break;
            case 1:
                unitClass = UnitClass.MECH;
                break;
            case 2:
                unitClass = UnitClass.LASER;
                break;
            case 3:
                unitClass = UnitClass.PSIONIC;
                break;
            case 4:
                unitClass = UnitClass.ACID;
                break;
            case 5:
                unitClass = UnitClass.BLADE;
                break;
            case 6:
                unitClass = UnitClass.MAGIC;
                break;
            case 7:
                unitClass = UnitClass.FLAME;
                break;
            default:
                throw new GameplayException("Invalid integer valid for selection: " + selection + "\n" + "Could not create player unit data object.");
        }
        return unitClass;
    }
    
    // ---------- Enemy Unit Creation ----------
    public EnemyUnitData CreateEnemyUnitData(int level) {
        EnemyUnitData data = enemyUnitData[level];
        return data;
    }

    public EnemyUnitData CreateEnemyUnitDataForBounty() {
        EnemyUnitData enemyUnitData = specialEnemyData["Bounty"];
        return enemyUnitData;
    }

    public EnemyUnitData CreateEnemyUnitDataForBonus(int number) {
        EnemyUnitData enemyUnitData = bonusUnitData[number];
        return enemyUnitData;
    }
}
