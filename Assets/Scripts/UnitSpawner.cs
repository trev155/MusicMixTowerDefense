﻿// Controller for spawning units of any kind
using UnityEngine;


public class UnitSpawner : MonoBehaviour {
    // ---------- Fields ----------
    public Transform playerUnitSpawnLocation;
    public Transform enemyUnitSpawnLocation;
    public Transform playerUnit;
    public Transform enemyUnit;
    public Transform playerUnitSelectionCircle;

    private static int uid = 0;
    private UnitFactory unitFactory;
    private System.Random random;


    // ---------- Methods ----------
    private void Awake() {
        unitFactory = new UnitFactory();
        random = new System.Random();
    }

    // Player Unit Creation Functions
    public void CreatePlayerUnit(PlayerUnitRank rank, int seed) {
        PlayerUnitData playerUnitData = unitFactory.CreatePlayerUnitData(rank, seed);
        PlayerUnit player = (PlayerUnit)Instantiate(playerUnit, playerUnitSpawnLocation).GetComponent<PlayerUnit>();
        player.InitializeProperties(playerUnitData);

        SetObjectName(player.gameObject);
        CreatePlayerUnitRangeCircle(player);
    }
    

    public void CreateRandomDUnit() {
        int selection = random.Next(0, 6);
        CreatePlayerUnit(PlayerUnitRank.D, selection);
    }        
    
    public void CreateRandomCUnit() {
        int selection = random.Next(0, 6);
        CreatePlayerUnit(PlayerUnitRank.C, selection);
    }

    public void CreateRandomBUnit() {
        int selection = random.Next(0, 8);
        CreatePlayerUnit(PlayerUnitRank.B, selection);
    }

    public void CreateRandomAUnit() {
        int selection = random.Next(0, 8);
        CreatePlayerUnit(PlayerUnitRank.A, selection);
    }

    public void CreateRandomSUnit() {
        int selection = random.Next(0, 8);
        CreatePlayerUnit(PlayerUnitRank.S, selection);
    }

    public void CreateRandomXUnit() {
        int selection = random.Next(0, 8);
        CreatePlayerUnit(PlayerUnitRank.X, selection);
    }

    // Enemy Unit Creation Functions
    public void CreateEnemyUnit(int level) {
        EnemyUnitData enemyUnitData = unitFactory.CreateEnemyUnitData(level);
        EnemyUnit e = (EnemyUnit)Instantiate(enemyUnit, enemyUnitSpawnLocation).GetComponent<EnemyUnit>();
        e.InitializeProperties(enemyUnitData);
        SetObjectName(e.gameObject);
    }

    // Other
    private void SetObjectName(GameObject obj) {
        obj.name = uid + "";
        uid += 1;
    }

    private void CreatePlayerUnitRangeCircle(PlayerUnit playerUnit) {
        Transform playerUnitRangeCircle = Instantiate(playerUnitSelectionCircle, playerUnit.gameObject.transform);

        playerUnitRangeCircle.localScale = new Vector2(playerUnitRangeCircle.localScale.x * playerUnit.attackRange, playerUnitRangeCircle.localScale.y * playerUnit.attackRange);

        playerUnit.attackRangeCircle = playerUnitRangeCircle.GetComponent<AttackRangeCircle>();
        playerUnit.attackRangeCircle.playerUnit = playerUnit;
    }
}
