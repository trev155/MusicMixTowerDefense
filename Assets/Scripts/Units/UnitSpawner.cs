// Controller for spawning units of any kind
using UnityEngine;

public class UnitSpawner : MonoBehaviour {
    // ---------- Fields ----------
    public UnitFactory unitFactory;

    public Transform playerUnitSpawnLocation;
    public Transform playerUnitSpawnLocationOffset;

    public Transform enemyUnitSpawnLocation;

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

    public Transform playerUnitSelectionCircle;
    public Transform enemyUnitSelectedCircle;

    public Transform topInnerWall;
    public Transform leftInnerWall;
    public Transform rightInnerWall;
    public Transform bottomInnerWall;
    
    private static int uid = 0;

    // ---------- Methods ----------
    private void Awake() {
        this.unitFactory = new UnitFactory();
    }

    // Player Unit Creation Functions
    public PlayerUnit CreatePlayerUnit(PlayerUnitRank rank, UnitClass unitClass) {
        // Create player unit object
        PlayerUnitData playerUnitData = unitFactory.CreatePlayerUnitData(rank, unitClass);
        Transform playerUnitPrefab = GetPlayerUnitPrefabFromUnitClass(unitClass);
        PlayerUnit playerUnit = Instantiate(playerUnitPrefab, playerUnitSpawnLocation).GetComponent<PlayerUnit>();
        playerUnit.InitializeProperties(playerUnitData);
        RankIndicatorBar rankIndicatorBar = playerUnit.GetComponentInChildren<RankIndicatorBar>();
        rankIndicatorBar.Initialize(rank);
        SetObjectName(playerUnit.gameObject);

        // Create range circle
        CreatePlayerUnitRangeCircle(playerUnit);

        // Prevent Stacking on Spawn
        MovePlayerUnitToOffset(playerUnit);

        // Check Achievement on every player unit creation
        GameEngine.GetInstance().achievementManager.CheckAchievementsForPlayerUnitCreation();

        // Display message
        MessageType msgType = MessageType.INFO;
        if (playerUnit.rank == PlayerUnitRank.S || playerUnit.rank == PlayerUnitRank.X) {
            msgType = MessageType.POSITIVE;
        }
        GameEngine.GetInstance().messageQueue.PushMessage("[" + playerUnit.rank + " Rank Unit] " + Utils.CleanEnumString(playerUnit.unitClass.ToString()), msgType);

        // Sound Effect
        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.PLAYER_UNIT_CREATION_SOUND);

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

        enemy.InitializeProperties(enemyUnitData);
        SetObjectName(enemy.gameObject);
        SetEnemyColor(enemy);

        if (GameEngine.GetInstance().gameMode == GameMode.EASY) {
            enemy.currentHealth = Mathf.Floor(enemy.maxHealth * 0.6f);
        } else if (GameEngine.GetInstance().gameMode == GameMode.NORMAL) {
            enemy.currentHealth = Mathf.Floor(enemy.maxHealth * 0.8f);
        }

        Transform enemyUnitCircle = enemy.transform.GetChild(0);
        enemy.selectedUnitCircle = enemyUnitCircle;

        GameEngine.GetInstance().IncrementEnemyUnitCount();

        return enemy;
    }

    public EnemyUnit CreateBounty() {
        EnemyUnitData enemyUnitData = unitFactory.CreateBountyUnit();
        EnemyUnit enemy = Instantiate(bountyUnit, enemyUnitSpawnLocation).GetComponent<EnemyUnit>();

        enemy.InitializeProperties(enemyUnitData);
        SetObjectName(enemy.gameObject);

        Transform enemyUnitCircle = enemy.transform.GetChild(0);
        enemy.selectedUnitCircle = enemyUnitCircle;

        // Initialize health regenerator
        HealthRegenerator healthRegenerator = enemy.GetComponent<HealthRegenerator>();
        healthRegenerator.enemyUnit = enemy;
        healthRegenerator.SetRegenerationRate(enemy.displayName);

        return enemy;
    }

    // Other functions
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
        player.transform.position = Vector2.MoveTowards(player.transform.position, playerUnitSpawnLocationOffset.position, Time.deltaTime);
    }

    private void SetEnemyColor(EnemyUnit enemy) {
        Color32 enemyColor;
        if (enemy.level < 6) {
            enemyColor = new Color32(126, 141, 164, 255);
        } else if (enemy.level < 11) {
            enemyColor = new Color32(147, 212, 214, 255);
        } else if (enemy.level < 16) {
            enemyColor = new Color32(31, 166, 98, 255);
        } else if (enemy.level < 21) {
            enemyColor = new Color32(166, 162, 31, 255);
        } else if (enemy.level < 26) {
            enemyColor = new Color32(243, 122, 45, 255);
        } else if (enemy.level < 31) {
            enemyColor = new Color32(200, 39, 44, 255);
        } else if (enemy.level < 36) {
            enemyColor = new Color32(200, 139, 124, 255);
        } else if (enemy.level < 41) {
            enemyColor = new Color32(104, 68, 80, 255);
        } else {
            throw new GameplayException("Enemy level not recognized. Failed to set enemy color.");
        }

        enemy.transform.GetComponent<SpriteRenderer>().color = enemyColor;
    }
}
