using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class UnitSelectionPanel : MonoBehaviour {
    // ---------- Constants ----------
    private static readonly float HIGHLIGHTED_ALPHA = 0.2f;
    private static readonly float UNHIGHLIGHTED_ALPHA = 0.02f;

    // ---------- Fields ----------
    public Transform unitSelectionPanel;
    public Button closeUnitSelectionPanelButton;

    public Text textSlot1;
    public Text textSlot2;
    public Text textSlot3;
    public Text textSlot4;
    public Text textSlot5;
    public Text textSlot6;
    public Text textSlot7;

    public Button moveUnitButton;
    public Text moveUnitButtonText;
    public Text moveUnitInstruction;

    public Button sellUnitButton;
    public Transform sellUnitModal;
    public Text sellUnitModalText;

    public Transform moveableArea;


    // ---------- Startup ----------
    private void Awake() {
        unitSelectionPanel.gameObject.SetActive(false);
        closeUnitSelectionPanelButton.gameObject.SetActive(false);
        moveUnitInstruction.gameObject.SetActive(false);
    }

    // ---------- Toggle Unit Selection Panel ----------
    public void ShowUnitSelectionPanel(IClickableUnit unit) {
        unitSelectionPanel.gameObject.SetActive(true);
        UpdateSelectedUnitDataPanel(unit);
        if (unit is PlayerUnit) {
            moveUnitButton.gameObject.SetActive(true);
            sellUnitButton.gameObject.SetActive(true);
        } else {
            moveUnitButton.gameObject.SetActive(false);
            sellUnitButton.gameObject.SetActive(false);
        }

        closeUnitSelectionPanelButton.gameObject.SetActive(true);
    }

    public void CloseUnitSelectionPanelButton() {
        CloseUnitSelectionPanel();
    }

    public void CloseUnitSelectionPanel() {
        HideUnitSelectionPanel();
        GameEngine.GetInstance().DisablePlayerUnitMovement();
        if (GameEngine.GetInstance().playerUnitSelected != null) {
            Utils.SetAlpha(GameEngine.GetInstance().playerUnitSelected.attackRangeCircle.transform, PlayerUnit.UNSELECTED_ALPHA);
        }
        if (GameEngine.GetInstance().enemyUnitSelected != null) {
            Utils.SetAlpha(GameEngine.GetInstance().enemyUnitSelected.selectedUnitCircle, EnemyUnit.UNSELECTED_ALPHA);
        }
        GameEngine.GetInstance().ClearUnitSelectionObjects();

        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
    }

    private void HideUnitSelectionPanel() {
        unitSelectionPanel.gameObject.SetActive(false);
        closeUnitSelectionPanelButton.gameObject.SetActive(false);
    }

    // ---------- Unit selection panel data ----------
    /* 
     * Calls the unit object's GetDisplayUnitData() function, which returns a list of strings.
     * These strings get mapped to the textSlot game objects.
     */
    public void UpdateSelectedUnitDataPanel(IClickableUnit unit) {
        List<string> unitData = unit.GetDisplayUnitData();

        textSlot1.text = "";
        textSlot2.text = "";
        textSlot3.text = "";
        textSlot4.text = "";
        textSlot5.text = "";
        textSlot6.text = "";
        textSlot7.text = "";

        if (unitData.Count > 0) {
            textSlot1.text = unitData[0];
        }
        if (unitData.Count > 1) {
            textSlot2.text = unitData[1];
        }
        if (unitData.Count > 2) {
            textSlot3.text = unitData[2];
        }
        if (unitData.Count > 3) {
            textSlot4.text = unitData[3];
        }
        if (unitData.Count > 4) {
            textSlot5.text = unitData[4];
        }
        if (unitData.Count > 5) {
            textSlot6.text = unitData[5];
        }
        if (unitData.Count > 6) {
            textSlot7.text = unitData[6];
        }
    }

    // ---------- Player Unit Selection - Toggle Movement ----------
    public void MoveUnitButton() {
        if (GameEngine.GetInstance().playerUnitMovementAllowed) {
            GameEngine.GetInstance().DisablePlayerUnitMovement();
        } else {
            GameEngine.GetInstance().EnablePlayerUnitMovement();
        }

        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
    }

    public void HighlightMoveableAreaAlpha() {
        SetMoveableAreaAlpha(HIGHLIGHTED_ALPHA);
    }

    public void UnhighlightMoveableAreaAlpha() {
        SetMoveableAreaAlpha(UNHIGHLIGHTED_ALPHA);
    }

    private void SetMoveableAreaAlpha(float alpha) {
        if (alpha < 0 || alpha > 1) {
            throw new GameplayException("Cannot set moveable area alpha. Value of " + alpha + " was invalid.");
        }
        Color moveableAreaColor = moveableArea.GetComponent<SpriteRenderer>().color;
        moveableAreaColor.a = alpha;
        moveableArea.GetComponent<SpriteRenderer>().color = moveableAreaColor;
    }

    // ---------- Unit Selling ----------
    public void SellUnitButton() {
        sellUnitModal.gameObject.SetActive(true);

        PlayerUnit unitToSell = GameEngine.GetInstance().playerUnitSelected;
        if (IsRareUnit(unitToSell)) {
            int tokenRefund = ComputeTokenRefund(unitToSell.GetPlayerUnitData().GetRank());
            sellUnitModalText.text = "Are you sure you want to sell this unit ?\n (You will receive " + tokenRefund + " tokens for selling a Rare " + unitToSell.GetPlayerUnitData().GetRank() + " rank unit)";
        } else {
            int gasRefund = ComputeGasRefund(unitToSell.GetPlayerUnitData().GetRank());
            sellUnitModalText.text = "Are you sure you want to sell this unit ?\n (You will receive " + gasRefund + " gas for selling a " + unitToSell.GetPlayerUnitData().GetRank() + " rank unit)";
        }
        
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
    }

    public void ConfirmSellUnit() {
        sellUnitModal.gameObject.SetActive(false);
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
        GameEngine.GetInstance().audioManager.PlayAudioAfterTime(AudioManager.SELL_UNIT, 0.1f);

        SellUnit(GameEngine.GetInstance().playerUnitSelected);
    }

    public void DenySellUnit() {
        sellUnitModal.gameObject.SetActive(false);
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
    }

    /*
     * Sell unit sequence. 
     * If rare unit sold, check rare unit seller achievement. 
     * Selling regular units gives gas, selling rare units gives tokens.
     * Destroy the player unit at the end and close its unit selection panel.
     */
    private void SellUnit(PlayerUnit unitToSell) {
        if (IsRareUnit(unitToSell)) {
            int tokenRefund = ComputeTokenRefund(unitToSell.GetPlayerUnitData().GetRank());
            GameEngine.GetInstance().IncreaseTokenCount(tokenRefund);

            GameEngine.GetInstance().achievementManager.rareUnitsSold += 1;
            GameEngine.GetInstance().achievementManager.CheckAchievementsForUnitSelling();
        } else {
            int gasRefund = ComputeGasRefund(unitToSell.GetPlayerUnitData().GetRank());
            GameEngine.GetInstance().IncreaseGas(gasRefund);
        }
        
        Destroy(unitToSell.gameObject);
        GameEngine.GetInstance().unitSelectionPanel.CloseUnitSelectionPanelButton();
    }

    private bool IsRareUnit(PlayerUnit playerUnit) {
        return playerUnit.GetPlayerUnitData().GetUnitClass() == UnitClass.MAGIC || playerUnit.GetPlayerUnitData().GetUnitClass() == UnitClass.FLAME;
    }
    
    private int ComputeGasRefund(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return 30;
            case PlayerUnitRank.C:
                return 60;
            case PlayerUnitRank.B:
                return 120;
            case PlayerUnitRank.A:
                return 240;
            case PlayerUnitRank.S:
                return 480;
            case PlayerUnitRank.X:
                return 960;
            default:
                throw new GameplayException("Unrecognized player unit rank. Cannot sell unit.");
        }
    }

    private int ComputeTokenRefund(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.B:
                return 4;
            case PlayerUnitRank.A:
                return 8;
            case PlayerUnitRank.S:
                return 16;
            case PlayerUnitRank.X:
                return 32;
            default:
                throw new GameplayException("Unrecognized player unit rank for rare unit. Cannot sell unit.");
        }
    }
}
