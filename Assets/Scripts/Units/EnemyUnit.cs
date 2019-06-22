﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class EnemyUnit : MonoBehaviour, IClickableUnit, IPointerClickHandler {
    //---------- Constants ----------
    public static readonly float SELECTED_ALPHA = 0.8f;
    public static readonly float UNSELECTED_ALPHA = 0;

    //---------- Fields ----------
    // Data Fields
    private EnemyUnitData enemyUnitData;
    private float currentHealth;
    
    // Data Fields - Getters and setters
    public EnemyUnitData GetEnemyUnitData() {
        return enemyUnitData;
    }
    
    public float GetCurrentHealth() {
        return currentHealth;
    }

    public void SetEnemyUnitData(EnemyUnitData enemyUnitData) {
        this.enemyUnitData = enemyUnitData;
    }

    public void SetCurrentHealth(float currentHealth) {
        this.currentHealth = currentHealth;
    }
    
    // Other fields
    public Transform selectedUnitCircle;
    public bool allowMovement = true;
    public Transform currentWaypointDestination;

    //---------- Methods ----------
    public void InitializeEnemyUnitGameObject(EnemyUnitData enemyUnitData, float initialHealth) {
        this.enemyUnitData = enemyUnitData;
        this.currentHealth = initialHealth;

        SetObjectName();
        if (enemyUnitData.GetEnemyType() == EnemyType.NORMAL) {
            SetNormalEnemyColor();
        }
        
        Transform enemyUnitCircle = transform.GetChild(0);
        selectedUnitCircle = enemyUnitCircle;
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        // If currently selected a player unit, adjust accordingly
        if (GameEngine.GetInstance().playerUnitMovementAllowed) {
            GameEngine.GetInstance().DisablePlayerUnitMovement();
        }

        // Clear alpha of previously selected units
        if (GameEngine.GetInstance().playerUnitSelected != null) {
            Utils.SetAlpha(GameEngine.GetInstance().playerUnitSelected.attackRangeCircle.transform, PlayerUnit.UNSELECTED_ALPHA);
        }
        if (GameEngine.GetInstance().enemyUnitSelected != null) {
            Utils.SetAlpha(GameEngine.GetInstance().enemyUnitSelected.selectedUnitCircle, UNSELECTED_ALPHA);
        }

        // Update references to objects in the game engine
        GameEngine.GetInstance().playerUnitSelected = null;
        GameEngine.GetInstance().enemyUnitSelected = this;

        // Set alpha value
        Utils.SetAlpha(selectedUnitCircle, SELECTED_ALPHA);
        
        // Show data on unit selection panel
        GameEngine.GetInstance().unitSelectionPanel.ShowUnitSelectionPanel(this);
    }

    public List<string> GetDisplayUnitData() {
        // TODO this should vary depending on the unit type
        List<string> unitData = new List<string>();
        string displayName = "Enemy: " + enemyUnitData.GetDisplayName();
        string health = "Health: " + currentHealth + " / " + enemyUnitData.GetMaxHealth();
        string armor = "Armor: " + enemyUnitData.GetArmor();
        string level = "Level: " + enemyUnitData.GetLevel();
        string movementSpeed = "Movement Speed: " + enemyUnitData.GetMovementSpeed();
        string enemyType = "Abilities: " + Utils.CleanEnumString(enemyUnitData.GetEnemyType().ToString());

        unitData.Add(displayName);
        unitData.Add(health);
        unitData.Add(armor);
        unitData.Add(level);
        unitData.Add(movementSpeed);
        unitData.Add(enemyType);

        return unitData;
    }
    
    private void Update() {
        if (allowMovement && currentWaypointDestination != null) {
            MoveToNextWaypoint();
        }
    }
    
    public void UpdateWaypointDestination(Transform destination) {
        currentWaypointDestination = destination;
    }

    private void MoveToNextWaypoint() {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        Vector2 movementDirection = (Vector2)currentWaypointDestination.position - (Vector2)transform.position;
        movementDirection.Normalize();
        rb2D.MovePosition(rb2D.position + (movementDirection * 1.5f) * enemyUnitData.GetMovementSpeed() * Time.deltaTime);
    }

    private void SetObjectName() {
        gameObject.name = UnitSpawner.uid + "";
        UnitSpawner.uid += 1;
    }

    private void SetNormalEnemyColor() {
        Color32 enemyColor;
        int level = GetEnemyUnitData().GetLevel();
        if (level < 6) {
            enemyColor = new Color32(126, 141, 164, 255);
        } else if (level < 11) {
            enemyColor = new Color32(147, 212, 214, 255);
        } else if (level < 16) {
            enemyColor = new Color32(31, 166, 98, 255);
        } else if (level < 21) {
            enemyColor = new Color32(166, 162, 31, 255);
        } else if (level < 26) {
            enemyColor = new Color32(243, 122, 45, 255);
        } else if (level < 31) {
            enemyColor = new Color32(200, 39, 44, 255);
        } else if (level < 36) {
            enemyColor = new Color32(200, 139, 124, 255);
        } else if (level < 41) {
            enemyColor = new Color32(104, 68, 80, 255);
        } else {
            throw new GameplayException("Enemy level not recognized. Failed to set enemy color.");
        }

        transform.GetComponent<SpriteRenderer>().color = enemyColor;
    }
}
