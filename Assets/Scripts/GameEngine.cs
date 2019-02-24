/*
 * The GameEngine handles most of the main aspects of the game.
 * The GameEngine also contains references to specialized components, so that these smaller components
 * can be referenced from a centralized location.
 */
using UnityEngine;


public class GameEngine : MonoBehaviour {
    // Singleton field
    public static GameEngine Instance { get; private set; } = null;

    private void Awake() {
        // singleton initialization
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            Instance = this;
        }
    }

    // References to other management objects
    public HUDManager hudManager;
    public UnitSpawner unitSpawner;
    public LevelManager levelManager;
    
    // Player Unit Movement
    public bool playerUnitMovementAllowed = false;
    public PlayerUnit playerUnitSelected;

    public EnemyUnit enemyUnitSelected;

    // Gameplay stats
    public int level;
    public int kills;


    //---------- Methods ----------
    public void EnablePlayerUnitMovement() {
        this.playerUnitMovementAllowed = true;
        this.hudManager.moveUnitButtonText.text = "Disable Unit Movement";
        this.hudManager.moveUnitInstruction.gameObject.SetActive(true);
        this.hudManager.HighlightMoveableAreaAlpha();
    }

    public void DisablePlayerUnitMovement() {
        this.playerUnitMovementAllowed = false;
        this.hudManager.moveUnitButtonText.text = "Enable Unit Movement";
        this.hudManager.moveUnitInstruction.gameObject.SetActive(false);
        this.hudManager.UnhighlightMoveableAreaAlpha();
    }

    public void ClearUnitSelectionObjects() {
        this.playerUnitSelected = null;
        this.enemyUnitSelected = null;
    }

    public void IncrementKills() {
        this.kills += 1;
        this.hudManager.UpdateKillCounter(this.kills);
    }

    public void PlayLevel() {
        levelManager.StartLevel(this.level);
    }
}
