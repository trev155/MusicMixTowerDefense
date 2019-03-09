﻿using UnityEngine;
using System.Collections.Generic;


public class UnitFactory : MonoBehaviour {
    private static readonly string PLAYER_UNIT_DATA_PATH = "UnitData/PlayerUnits";
    private static readonly string ENEMY_UNIT_DATA_PATH = "UnitData/EnemyUnits";

    private Dictionary<PlayerUnitRank, Dictionary<UnitClass, PlayerUnitData>> allPlayerData;
    private Dictionary<int, EnemyUnitData> allEnemyData;

    // ---------- Initialization ----------
    private void Awake() {
        InitializePlayerUnitDataDictionary();
        InitializeEnemyUnitDataDictionary();
    }

    private void InitializePlayerUnitDataDictionary() {
        allPlayerData = new Dictionary<PlayerUnitRank, Dictionary<UnitClass, PlayerUnitData>> {
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
            this.allPlayerData[rank].Add(unitClass, playerUnitData);

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

    private void InitializeEnemyUnitDataDictionary() {
        allEnemyData = new Dictionary<int, EnemyUnitData>();

        int level;
        string displayName;
        float movementSpeed;
        float maxHealth;
        float armor;
        EnemyAbilities enemyAbilities;

        int lineIndex = 0;
        TextAsset playerData = Resources.Load<TextAsset>(ENEMY_UNIT_DATA_PATH);
        string[] lines = playerData.text.Split('\n');
        foreach (string line in lines) {
            if (lineIndex == 0) {
                lineIndex++;
                continue;   // skip file header
            }

            string[] lineTokens = line.Trim().Split(',');
            if (lineTokens.Length <= 1) {
                continue;
            }
            
            level = int.Parse(lineTokens[0]);
            displayName = lineTokens[1];
            movementSpeed = float.Parse(lineTokens[2]);
            maxHealth = float.Parse(lineTokens[3]);
            armor = float.Parse(lineTokens[4]);
            enemyAbilities = ConvertEnemyAbilitiesString(lineTokens[5]);

            if (level == 0) {
                // TODO special cases to handle
                continue;
            }

            EnemyUnitData enemyUnitData = new EnemyUnitData(displayName, movementSpeed, maxHealth, armor, level, enemyAbilities);
            this.allEnemyData[level] = enemyUnitData;

            lineIndex++;
        }

        Debug.Log("Finished reading enemy unit data. Read: [" + (lineIndex + 1) + "] number of lines.");
    }

    private EnemyAbilities ConvertEnemyAbilitiesString(string s) {
        switch (s) {
            case "None":
                return EnemyAbilities.NONE;
            case "HealthRegeneration":
                return EnemyAbilities.HEALTH_REGEN;
            default:
                throw new GameplayException("Could not convert string " + s + " to an EnemyAbilities object.");
        }
    }


    // ---------- Player Unit Creation ----------
    public PlayerUnitData CreatePlayerUnitData(PlayerUnitRank rank, UnitClass unitClass) {
        PlayerUnitData playerUnitData = allPlayerData[rank][unitClass];
        return playerUnitData;
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
        EnemyUnitData enemyUnitData = allEnemyData[level];
        return enemyUnitData;
    }
}
