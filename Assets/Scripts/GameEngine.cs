/*
 * The GameEngine handles most of the main aspects of the game.
 * The GameEngine also contains references to specialized components, so that these smaller components
 * can be referenced from a centralized location.
 */
using UnityEngine;


public class GameEngine : MonoBehaviour {
    // Constants
    private readonly int INITIAL_TOKEN_COUNT = 12;

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

    public UnitSelectionPanel unitSelectionPanel;
    public ShopPanel shopPanel;
    public GameDataPanel gameDataPanel;
    public UpgradePanel upgradePanel;

    // Unit Selection
    public bool playerUnitMovementAllowed = false;
    public PlayerUnit playerUnitSelected;
    public EnemyUnit enemyUnitSelected;

    // Gameplay stats
    public int minerals;
    public int gas;
    public int level;
    public int tokenCount;
    public int kills;
    public GlobalTimer globalTimer;
    public int mineralHarvesters;
    public int gasHarvesters;

    public bool hasPiano;
    public bool hasDrum;
    public int drumCounter;

    public bool hasXUnit;

    //---------- Initialization ----------
    private void Awake() {
        InitializeSingleton();
        globalTimer.BeginGlobalGameTimer();

        this.playerUnitMovementAllowed = false;
        this.playerUnitSelected = null;
        this.enemyUnitSelected = null;

        this.minerals = 0;
        this.gas = 0;
        this.level = 0;
        this.kills = 0;
        this.tokenCount = INITIAL_TOKEN_COUNT;
        this.mineralHarvesters = 0;
        this.gasHarvesters = 0;

        this.hasPiano = false;
        this.hasDrum = false;
        this.drumCounter = 0;

        this.hasXUnit = false;
        
        this.gameDataPanel.UpdateTokenCountText(this.tokenCount);

        // Start First Level
        StartCoroutine(levelManager.WaitBeforeStartingGame(10.0f));
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
            Debug.Log("20 Kills = 1 Token");
            this.tokenCount += 1;
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

    public void IncreaseMinerals(int val) {
        this.minerals += val;
        if (this.minerals >= 160) {
            Debug.Log("160 Minerals = 1 Token");
            this.minerals -= 160;
        }

        this.gameDataPanel.UpdateMineralsText(this.minerals);
    }

    public void IncreaseGas(int val) {
        this.gas += val;
        this.gameDataPanel.UpdateGasText(this.gas);

        unitMixer.CheckGasCombos();
    }

    public void DecreaseGas(int val) {
        this.gas -= val;
        this.gameDataPanel.UpdateGasText(this.gas);
    }
}
