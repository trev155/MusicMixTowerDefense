﻿/*
 * The GameEngine handles most of the main aspects of the game.
 * The GameEngine also contains references to specialized components, so that these smaller components
 * can be referenced from a centralized location.
 */
using UnityEngine;


public class GameEngine : MonoBehaviour {
    // Singleton field
    public static GameEngine Instance { get; private set; } = null;
    public static GameEngine GetInstance() {
        return Instance;
    }

    // References to other management objects
    public UnitSpawner unitSpawner;
    public LevelManager levelManager;
    public UpgradeManager upgradeManager;

    public UnitSelectionPanel unitSelectionPanel;
    public ShopPanel shopPanel;
    public GameDataPanel gameDataPanel;

    // Unit Selection
    public bool playerUnitMovementAllowed = false;
    public PlayerUnit playerUnitSelected;
    public EnemyUnit enemyUnitSelected;

    // Gameplay stats
    public int level;
    public int kills;
    public int tokenCount;

    public bool hasPiano = false;
    public bool hasDrum = false;
    public int harvesterCount;


    //---------- Initialization ----------
    private void Awake() {
        InitializeSingleton();

        this.playerUnitMovementAllowed = false;
        this.playerUnitSelected = null;
        this.enemyUnitSelected = null;
        this.level = 0;
        this.kills = 0;
        this.tokenCount = 4;
        this.hasPiano = false;
        this.hasDrum = false;
        this.harvesterCount = 0;

        this.gameDataPanel.UpdateTokenCount(this.tokenCount);
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

    public void IncrementKills() {
        this.kills += 1;
        this.gameDataPanel.UpdateKillCounter(this.kills);
    }

    public void PlayLevel() {
        levelManager.StartLevel(this.level);
    }
}
