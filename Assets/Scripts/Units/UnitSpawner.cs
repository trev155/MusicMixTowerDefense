// Controller for spawning units of any kind
using UnityEngine;

public class UnitSpawner : MonoBehaviour {
    // ---------- Fields ----------
    public UnitFactory unitFactory;

    public Transform playerUnitSpawnLocation;
    public Transform playerUnitSpawnLocationOffset;

    public Transform enemyUnitSpawnLocation;
    public Transform bonusUnitSpawnLocation;

    public Transform playerUnitInfantry;
    public Transform playerUnitMech;
    public Transform playerUnitLaser;
    public Transform playerUnitPsionic;
    public Transform playerUnitAcid;
    public Transform playerUnitBlade;
    public Transform playerUnitMagic;
    public Transform playerUnitFlame;

    public Transform enemyUnit;
    public Transform bountyUnit;
    public Transform bonusUnit;
    
    public Transform topInnerWall;
    public Transform leftInnerWall;
    public Transform rightInnerWall;
    public Transform bottomInnerWall;
    
    public static int uid = 0;

    // ---------- Methods ----------
    private void Awake() {
        this.unitFactory = new UnitFactory();
    }

    // Player Unit Creation Functions
    public PlayerUnit CreatePlayerUnit(PlayerUnitRank rank, UnitClass unitClass) {
        // Create player unit object
        Transform playerUnitPrefab = GetPlayerUnitPrefabFromUnitClass(unitClass);
        PlayerUnit playerUnit = Instantiate(playerUnitPrefab, playerUnitSpawnLocation).GetComponent<PlayerUnit>();

        // Initialize player unit data
        PlayerUnitData playerUnitData = unitFactory.CreatePlayerUnitData(rank, unitClass);
        playerUnit.InitializePlayerUnitGameObject(playerUnitData);
        
        // Prevent Stacking on Spawn
        MovePlayerUnitToOffset(playerUnit);

        // Check Achievement on every player unit creation
        GameEngine.GetInstance().achievementManager.CheckAchievementsForPlayerUnitCreation();

        // Display message
        MessageType msgType = MessageType.INFO;
        if (playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.S || playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.X) {
            msgType = MessageType.POSITIVE;
        }
        GameEngine.GetInstance().messageQueue.PushMessage("[" + playerUnit.GetPlayerUnitData().GetRank() + " Rank Unit] " + Utils.CleanEnumString(playerUnit.GetPlayerUnitData().GetUnitClass().ToString()), msgType);

        // Sound Effect
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.PLAYER_UNIT_CREATION_SOUND);

        return playerUnit;
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
        UnitClass randomUnitClass = GenerateRandomUnitClass(PlayerUnitRank.D);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.D, randomUnitClass);
        return playerUnit;
    }        
    
    public PlayerUnit CreateRandomCUnit() {
        UnitClass randomUnitClass = GenerateRandomUnitClass(PlayerUnitRank.C);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.C, randomUnitClass);
        return playerUnit;
    }

    public PlayerUnit CreateRandomBUnit() {
        UnitClass randomUnitClass = GenerateRandomUnitClass(PlayerUnitRank.B);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.B, randomUnitClass);
        return playerUnit;
    }

    public PlayerUnit CreateRandomAUnit() {
        UnitClass randomUnitClass = GenerateRandomUnitClass(PlayerUnitRank.A);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.A, randomUnitClass);
        return playerUnit;
    }

    public PlayerUnit CreateRandomSUnit() {
        UnitClass randomUnitClass = GenerateRandomUnitClass(PlayerUnitRank.S);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.S, randomUnitClass);
        return playerUnit;
    }

    public PlayerUnit CreateRandomXUnit() {
        UnitClass randomUnitClass = GenerateRandomUnitClass(PlayerUnitRank.X);
        PlayerUnit playerUnit = CreatePlayerUnit(PlayerUnitRank.X, randomUnitClass);
        return playerUnit;
    }

    private UnitClass GenerateRandomUnitClass(PlayerUnitRank playerUnitRank) {
        int selection;
        if (playerUnitRank == PlayerUnitRank.D || playerUnitRank == PlayerUnitRank.C) {
            selection = GameEngine.GetInstance().random.Next(0, 6);
        } else {
            selection = GameEngine.GetInstance().random.Next(0, 8);
        }

        switch (selection) {
            case 0:
                return UnitClass.INFANTRY;
            case 1:
                return UnitClass.MECH;
            case 2:
                return UnitClass.LASER;
            case 3:
                return UnitClass.PSIONIC;
            case 4:
                return UnitClass.ACID;
            case 5:
                return UnitClass.BLADE;
            case 6:
                return UnitClass.MAGIC;
            case 7:
                return UnitClass.FLAME;
            default:
                throw new GameplayException("Unrecognized selection value. Cannot generate random unit class");
        }
    }

    private Transform GetPlayerUnitPrefabFromUnitClass(UnitClass unitClass) {
        switch (unitClass) {
            case UnitClass.INFANTRY:
                return playerUnitInfantry;
            case UnitClass.MECH:
                return playerUnitMech;
            case UnitClass.LASER:
                return playerUnitLaser;
            case UnitClass.PSIONIC:
                return playerUnitPsionic;
            case UnitClass.ACID:
                return playerUnitAcid;
            case UnitClass.BLADE:
                return playerUnitBlade;
            case UnitClass.MAGIC:
                return playerUnitMagic;
            case UnitClass.FLAME:
                return playerUnitFlame;
            default:
                throw new GameplayException("Unsupported unit class. Cannot get prefab from unit class");
        }
    }

    // Enemy Unit Creation Functions
    public EnemyUnit CreateEnemyUnit(int level) {
        EnemyUnitData enemyUnitData = unitFactory.CreateEnemyUnitData(level);
        EnemyUnit enemy = Instantiate(enemyUnit, enemyUnitSpawnLocation).GetComponent<EnemyUnit>();

        float initialHealth = enemyUnitData.GetMaxHealth();
        if (GameEngine.GetInstance().gameMode == GameMode.EASY) {
            initialHealth = Mathf.Floor(initialHealth * 0.6f);
        } else if (GameEngine.GetInstance().gameMode == GameMode.NORMAL) {
            initialHealth = Mathf.Floor(initialHealth * 0.8f);
        }

        enemy.InitializeEnemyUnitGameObject(enemyUnitData, initialHealth);

        GameEngine.GetInstance().IncrementEnemyUnitCount();

        return enemy;
    }

    public EnemyUnit CreateBounty() {
        EnemyUnitData enemyUnitData = unitFactory.CreateEnemyUnitDataForBounty();
        EnemyUnit enemy = Instantiate(bountyUnit, enemyUnitSpawnLocation).GetComponent<EnemyUnit>();

        enemy.InitializeEnemyUnitGameObject(enemyUnitData, enemyUnitData.GetMaxHealth());
        
        GameEngine.GetInstance().IncrementEnemyUnitCount();

        return enemy;
    }

    public EnemyUnit CreateBonusUnit(int number) {
        EnemyUnitData enemyUnitData = unitFactory.CreateEnemyUnitDataForBonus(number);
        EnemyUnit enemy = Instantiate(bonusUnit, bonusUnitSpawnLocation).GetComponent<EnemyUnit>();

        enemy.InitializeEnemyUnitGameObject(enemyUnitData, enemyUnitData.GetMaxHealth());
        
        return enemy;
    }

    // Utility Functions
    public static void SetObjectName(GameObject obj) {
        SetObjectName(obj, "");
    }

    private static void SetObjectName(GameObject obj, string suffix) {
        obj.name = uid + suffix;
        uid += 1;
    }

    private void MovePlayerUnitToOffset(PlayerUnit player) {
        player.transform.position = Vector2.MoveTowards(player.transform.position, playerUnitSpawnLocationOffset.position, Time.deltaTime);
    }
}
