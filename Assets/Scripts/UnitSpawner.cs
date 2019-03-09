// Controller for spawning units of any kind
using UnityEngine;


public class UnitSpawner : MonoBehaviour {
    // ---------- Fields ----------
    public Transform playerUnitSpawnLocation;
    public Transform enemyUnitSpawnLocation;
    public Transform playerUnit;
    public Transform enemyUnit;
    public Transform playerUnitSelectionCircle;
    public Transform enemyUnitSelectedCircle;

    private static int uid = 0;
    private UnitFactory unitFactory;
    private System.Random random;


    // ---------- Methods ----------
    private void Awake() {
        unitFactory = new UnitFactory();
        random = new System.Random();
    }

    // Player Unit Creation Functions
    public PlayerUnit CreatePlayerUnit(PlayerUnitRank rank, int seed) {
        PlayerUnitData playerUnitData = unitFactory.CreatePlayerUnitData(rank, seed);
        PlayerUnit player = (PlayerUnit)Instantiate(playerUnit, playerUnitSpawnLocation).GetComponent<PlayerUnit>();
        player.InitializeProperties(playerUnitData);

        SetObjectName(player.gameObject);
        CreatePlayerUnitRangeCircle(player);

        return player;
    }
    
    public PlayerUnit CreateRandomDUnit() {
        int selection = random.Next(0, 6);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.D, selection);
        return playerUnit;
    }        
    
    public PlayerUnit CreateRandomCUnit() {
        int selection = random.Next(0, 6);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.C, selection);
        return playerUnit;
    }

    public PlayerUnit CreateRandomBUnit() {
        int selection = random.Next(0, 8);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.B, selection);
        return playerUnit;
    }

    public PlayerUnit CreateRandomAUnit() {
        int selection = random.Next(0, 8);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.A, selection);
        return playerUnit;
    }

    public PlayerUnit CreateRandomSUnit() {
        int selection = random.Next(0, 8);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.S, selection);
        return playerUnit;
    }

    public PlayerUnit CreateRandomXUnit() {
        int selection = random.Next(0, 8);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.X, selection);
        return playerUnit;
    }

    // Enemy Unit Creation Functions
    public EnemyUnit CreateEnemyUnit(int level) {
        EnemyUnitData enemyUnitData = unitFactory.CreateEnemyUnitData(level);
        EnemyUnit enemy = (EnemyUnit)Instantiate(enemyUnit, enemyUnitSpawnLocation).GetComponent<EnemyUnit>();
        enemy.InitializeProperties(enemyUnitData);
        SetObjectName(enemy.gameObject);

        Transform enemyUnitCircle = Instantiate(enemyUnitSelectedCircle, enemy.transform);
        enemy.selectedUnitCircle = enemyUnitCircle;

        return enemy;
    }

    // Other
    private void SetObjectName(GameObject obj) {
        obj.name = uid + "";
        uid += 1;
    }

    private void CreatePlayerUnitRangeCircle(PlayerUnit playerUnit) {
        Transform playerUnitRangeCircle = Instantiate(playerUnitSelectionCircle, playerUnit.transform);

        playerUnitRangeCircle.localScale = new Vector2(playerUnitRangeCircle.localScale.x * playerUnit.attackRange, playerUnitRangeCircle.localScale.y * playerUnit.attackRange);

        playerUnit.attackRangeCircle = playerUnitRangeCircle.GetComponent<AttackRangeCircle>();
        playerUnit.attackRangeCircle.playerUnit = playerUnit;
    }
}
