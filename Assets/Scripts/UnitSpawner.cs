// Controller for spawning units of any kind
using UnityEngine;

public class UnitSpawner : MonoBehaviour {
    // ---------- Fields ----------
    public UnitFactory unitFactory;
    public Transform playerUnitSpawnLocation;
    public Transform playerUnitSpawnLocationOffset;
    public Transform enemyUnitSpawnLocation;
    public Transform playerUnit;
    public Transform enemyUnit;
    public Transform playerUnitSelectionCircle;
    public Transform enemyUnitSelectedCircle;
    public Transform topInnerWall;
    public Transform leftInnerWall;
    public Transform rightInnerWall;
    public Transform bottomInnerWall;
    
    private static int uid = 0;
    private System.Random random;

    // ---------- Methods ----------
    private void Awake() {
        random = new System.Random();
        this.unitFactory = new UnitFactory();
    }

    // Player Unit Creation Functions
    public PlayerUnit CreatePlayerUnit(PlayerUnitRank rank, int seed) {
        PlayerUnitData playerUnitData = unitFactory.CreatePlayerUnitData(rank, seed);
        PlayerUnit player = Instantiate(playerUnit, playerUnitSpawnLocation).GetComponent<PlayerUnit>();
        player.InitializeProperties(playerUnitData);
        SetObjectName(player.gameObject);

        CreatePlayerUnitRangeCircle(player);

        MovePlayerUnitToOffset(player);
        IgnorePlayerUnitCollisionWithInnerWalls(player);

        GameEngine.GetInstance().achievementManager.CheckAchievementsForPlayerUnitCreation();

        GameEngine.GetInstance().messageQueue.PushMessage("[" + player.rank + " Rank Unit] " + Utils.CleanEnumString(player.unitClass.ToString()));

        return player;
    }

    public PlayerUnit CreatePlayerUnit(PlayerUnitRank rank, UnitClass unitClass) {
        PlayerUnitData playerUnitData = unitFactory.CreatePlayerUnitData(rank, unitClass);
        PlayerUnit player = Instantiate(playerUnit, playerUnitSpawnLocation).GetComponent<PlayerUnit>();
        player.InitializeProperties(playerUnitData);
        SetObjectName(player.gameObject);

        CreatePlayerUnitRangeCircle(player);

        MovePlayerUnitToOffset(player);
        IgnorePlayerUnitCollisionWithInnerWalls(player);

        GameEngine.GetInstance().achievementManager.CheckAchievementsForPlayerUnitCreation();

        GameEngine.GetInstance().messageQueue.PushMessage("[" + player.rank + " Rank Unit] " + Utils.CleanEnumString(player.unitClass.ToString()));

        return player;
    }

    public PlayerUnit CreateRandomUnitOfRank(PlayerUnitRank rank) {
        if (rank == PlayerUnitRank.D) {
            return CreateRandomDUnit();
        } else if (rank == PlayerUnitRank.C) {
            return CreateRandomCUnit();
        } else if (rank == PlayerUnitRank.B) {
            return CreateRandomBUnit();
        } else if (rank == PlayerUnitRank.A) {
            return CreateRandomAUnit();
        } else if (rank == PlayerUnitRank.S) {
            return CreateRandomSUnit();
        } else {
            throw new GameplayException("Unsupported rank value: " + rank.ToString());
        }
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
        EnemyUnit enemy = Instantiate(enemyUnit, enemyUnitSpawnLocation).GetComponent<EnemyUnit>();
        enemy.InitializeProperties(enemyUnitData);
        SetObjectName(enemy.gameObject);

        Transform enemyUnitCircle = Instantiate(enemyUnitSelectedCircle, enemy.transform);
        enemy.selectedUnitCircle = enemyUnitCircle;

        return enemy;
    }

    public EnemyUnit CreateBounty() {
        EnemyUnitData enemyUnitData = unitFactory.CreateBountyUnit();
        EnemyUnit enemy = Instantiate(enemyUnit, enemyUnitSpawnLocation).GetComponent<EnemyUnit>();

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

    private void MovePlayerUnitToOffset(PlayerUnit player) {
        // Prevents stacking when spawned
        player.transform.position = Vector2.MoveTowards(player.transform.position, playerUnitSpawnLocationOffset.position, Time.deltaTime);
    }

    private void IgnorePlayerUnitCollisionWithInnerWalls(PlayerUnit player) {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), topInnerWall.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), leftInnerWall.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), rightInnerWall.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), bottomInnerWall.GetComponent<Collider2D>());
    }
}
