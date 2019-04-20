/*
 * The GameEngine handles most of the main aspects of the game.
 * The GameEngine also contains references to specialized components, so that these smaller components
 * can be referenced from a centralized location.
 */
using UnityEngine;

public class GameEngine : MonoBehaviour {
    // Constants
    private readonly int INITIAL_TOKEN_COUNT = 30;
    private readonly int INITIAL_MINERAL_COUNT = 50;
    private readonly int INITIAL_GAS_COUNT = 25;
    private readonly int INITIAL_UNALLOCATED_HARVESTER_COUNT = 4;
    private readonly int INITIAL_MINERAL_HARVESTER_COUNT = 1;
    private readonly int INITIAL_GAS_HARVESTER_COUNT = 2;
    private readonly int INITIAL_B_CHOOSERS = 2;
    private readonly int INITIAL_A_CHOOSERS = 3;
    private readonly int INITIAL_S_CHOOSERS = 1;

    private readonly int MAX_ENEMY_UNITS = 100;

    // Singleton field
    public static GameEngine Instance { get; private set; } = null;
    public static GameEngine GetInstance() {
        return Instance;
    }

    // References to other management objects
    public UnitSpawner unitSpawner;
    public LevelManager levelManager;
    public UpgradeManager upgradeManager;
    public UnitMixer unitMixer;
    public AchievementManager achievementManager;

    public UnitSelectionPanel unitSelectionPanel;
    public GameDataPanel gameDataPanel;
    public GameTabs gameTabs;
    public ShopPanel shopPanel;
    public UpgradePanel upgradePanel;
    public HarvesterPanel harvesterPanel;
    public BonusPanel bonusPanel;
    public InfoTabs infoTabs;
    public GuidancePanel guidancePanel;
    public AchievementsPanel achievementsPanel;
    public MessageQueue messageQueue;
    public AdminPanel adminPanel;
    public GameOverPanel gameOverPanel;

    // Unit Selection
    public bool playerUnitMovementAllowed = false;
    public PlayerUnit playerUnitSelected;
    public EnemyUnit enemyUnitSelected;

    // Gameplay stats
    public int minerals;
    public int gas;
    public int tokenCount;
    public int kills;
    public GlobalTimer globalTimer;
    public int enemyUnitCount;

    public int unallocatedHarvesters;
    public int mineralHarvesters;
    public int gasHarvesters;

    public bool hasPiano;
    public bool hasDrum;
    public int drumCounter;

    public bool hasXUnit;

    public int bChoosers;
    public int aChoosers;
    public int sChoosers;

    public bool hasWall;

    // RNG
    public System.Random random = new System.Random();

    // Difficulty
    public GameMode gameMode;


    //---------- Initialization ----------
    private void Awake() {
        InitializeSingleton();
        globalTimer.BeginGlobalGameTimer();

        this.playerUnitMovementAllowed = false;
        this.playerUnitSelected = null;
        this.enemyUnitSelected = null;

        this.minerals = INITIAL_MINERAL_COUNT;
        this.gas = INITIAL_GAS_COUNT;
        this.kills = 0;
        this.tokenCount = INITIAL_TOKEN_COUNT;
        this.unallocatedHarvesters = INITIAL_UNALLOCATED_HARVESTER_COUNT;
        this.mineralHarvesters = INITIAL_MINERAL_HARVESTER_COUNT;
        this.gasHarvesters = INITIAL_GAS_HARVESTER_COUNT;

        this.hasPiano = false;
        this.hasDrum = false;
        this.drumCounter = 0;

        this.hasXUnit = false;

        this.bChoosers = INITIAL_B_CHOOSERS;
        this.aChoosers = INITIAL_A_CHOOSERS;
        this.sChoosers = INITIAL_S_CHOOSERS;
        
        this.gameDataPanel.UpdateTokenCountText(this.tokenCount);

        // Difficulty
        this.gameMode = SceneDataTransfer.CurrentGameMode;
        gameDataPanel.UpdateGameModeText(this.gameMode);
        
        // Start First Level
        StartCoroutine(levelManager.WaitBeforeStartingGame(5.0f));
    }

    private void InitializeSingleton() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            Instance = this;
        }
    }

    //---------- Methods ----------
    // Player Movement and Unit Selection
    public void EnablePlayerUnitMovement() {
        this.playerUnitMovementAllowed = true;
        this.unitSelectionPanel.moveUnitButtonText.text = "Disable Unit Movement";
        this.unitSelectionPanel.moveUnitInstruction.gameObject.SetActive(true);
        this.unitSelectionPanel.HighlightMoveableAreaAlpha();
    }

    public void DisablePlayerUnitMovement() {
        this.playerUnitMovementAllowed = false;
        this.unitSelectionPanel.moveUnitButtonText.text = "Enable Unit Movement";
        this.unitSelectionPanel.moveUnitInstruction.gameObject.SetActive(false);
        this.unitSelectionPanel.UnhighlightMoveableAreaAlpha();
    }

    public void ClearUnitSelectionObjects() {
        this.playerUnitSelected = null;
        this.enemyUnitSelected = null;
    }

    // Stats
    public void IncrementKills() {
        this.kills += 1;
        this.gameDataPanel.UpdateKillCounterText(this.kills);

        if (this.kills % 20 == 0) {
            this.messageQueue.PushMessage("20 Kills = 1 Token", MessageType.INFO);
            this.tokenCount++;
            this.gameDataPanel.UpdateTokenCountText(this.tokenCount);
        }

        if (this.hasPiano) {
            IncreaseMinerals(1);
        }

        if (this.hasDrum) {
            this.drumCounter += 1;
            if (this.drumCounter == 3) {
                this.drumCounter = 0;
                IncreaseGas(1);
            }
        }
    }

    // Shop Tokens
    public void IncreaseTokenCount(int count) {
        if (count < 1) {
            throw new GameplayException("Invalid value for count: " + count + ". Cannot increase token count.");
        }

        this.tokenCount += count;
        this.gameDataPanel.UpdateTokenCountText(this.tokenCount);
    }

    public void DecreaseTokenCount(int count) {
        if (count < 1) {
            throw new GameplayException("Invalid value for count: " + count + ". Cannot decrease token count.");
        }

        this.tokenCount -= count;
        this.gameDataPanel.UpdateTokenCountText(this.tokenCount);
    }

    // Minerals and Gas
    public void IncreaseMinerals(int val) {
        if (val < 0) {
            throw new GameplayException("Invalid value provided: " + val + ". Cannot increase mineral count.");
        }

        this.minerals += val;
        if (this.minerals >= 160) {
            this.messageQueue.PushMessage("160 Minerals = 1 Token", MessageType.INFO);
            this.minerals -= 160;
            IncreaseTokenCount(1);
        }
        this.gameDataPanel.UpdateMineralsText(this.minerals);
    }

    public void IncreaseGas(int val) {
        if (val < 0) {
            throw new GameplayException("Invalid value provided: " + val + ". Cannot increase gas count.");
        }

        this.gas += val;
        this.gameDataPanel.UpdateGasText(this.gas);

        unitMixer.CheckGasCombos();
    }

    public void DecreaseGas(int val) {
        if (val < 0) {
            throw new GameplayException("Invalid value provided: " + val + ". Cannot decrease gas count.");
        }

        this.gas -= val;
        this.gameDataPanel.UpdateGasText(this.gas);
    }

    // Harvesters
    public void AddHarvester() {
        this.unallocatedHarvesters++;
        this.harvesterPanel.SetUnallocatedHarvesters(this.unallocatedHarvesters);

        this.achievementManager.CheckAchievementsForHarvesterBonus();
    }

    public void AllocateMineralHarvester() {
        this.unallocatedHarvesters--;
        this.harvesterPanel.SetUnallocatedHarvesters(this.unallocatedHarvesters);

        this.mineralHarvesters++;
        this.harvesterPanel.SetMineralHarvesters(this.mineralHarvesters);
    }

    public void AllocateGasHarvester() {
        this.unallocatedHarvesters--;
        this.harvesterPanel.SetUnallocatedHarvesters(this.unallocatedHarvesters);

        this.gasHarvesters++;
        this.harvesterPanel.SetGasHarvesters(this.gasHarvesters);
    }

    public void DeallocateMineralHarvester() {
        this.unallocatedHarvesters++;
        this.harvesterPanel.SetUnallocatedHarvesters(this.unallocatedHarvesters);

        this.mineralHarvesters--;
        this.harvesterPanel.SetMineralHarvesters(this.mineralHarvesters);
    }

    public void DeallocateGasHarvester() {
        this.unallocatedHarvesters++;
        this.harvesterPanel.SetUnallocatedHarvesters(this.unallocatedHarvesters);

        this.gasHarvesters--;
        this.harvesterPanel.SetGasHarvesters(this.gasHarvesters);
    }

    // Bonus Tokens
    public void AddBBonusTokens(int val) {
        this.bChoosers += val;
        this.bonusPanel.UpdateBTokenCount(this.bChoosers);

        this.achievementManager.CheckAchievementsForBChoosers();
    }

    public void AddABonusTokens(int val) {
        this.aChoosers += val;
        this.bonusPanel.UpdateATokenCount(this.aChoosers);
    }

    public void AddSBonusTokens(int val) {
        this.sChoosers += val;
        this.bonusPanel.UpdateSTokenCount(this.sChoosers);
    }

    public void DecrementBBonusTokenCount() {
        this.bChoosers -= 1;
        this.bonusPanel.UpdateBTokenCount(this.bChoosers);
    }

    public void DecrementABonusTokenCount() {
        this.aChoosers -= 1;
        this.bonusPanel.UpdateATokenCount(this.aChoosers);
    }

    public void DecrementSBonusTokenCount() {
        this.sChoosers -= 1;
        this.bonusPanel.UpdateSTokenCount(this.sChoosers);
    }

    // Enemy Unit Count
    public void IncrementEnemyUnitCount() {
        this.enemyUnitCount++;
        this.gameDataPanel.UpdateEnemyUnitCountText(this.enemyUnitCount);

        if (this.enemyUnitCount >= MAX_ENEMY_UNITS) {
            GameOver();
        }
    }

    public void DecrementEnemyUnitCount() {
        this.enemyUnitCount--;
        this.gameDataPanel.UpdateEnemyUnitCountText(this.enemyUnitCount);
    }

    private void GameOver() {
        this.messageQueue.PushMessage("Game Over", MessageType.NEGATIVE);
        Time.timeScale = 0;
        gameOverPanel.transform.gameObject.SetActive(true);
        gameOverPanel.SetEndGameStats();
    }
}
