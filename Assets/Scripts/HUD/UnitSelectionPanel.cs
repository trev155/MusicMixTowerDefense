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

    public Transform moveableArea;


    // ---------- Methods ----------
    private void Awake() {
        unitSelectionPanel.gameObject.SetActive(false);
        closeUnitSelectionPanelButton.gameObject.SetActive(false);
        moveUnitInstruction.gameObject.SetActive(false);
    }

    // Show and hide unit selection panel.
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
    }

    private void HideUnitSelectionPanel() {
        unitSelectionPanel.gameObject.SetActive(false);
        closeUnitSelectionPanelButton.gameObject.SetActive(false);
    }

    // Unit selection panel data
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

    // Player Unit Selection movement
    public void MoveUnitButton() {
        if (GameEngine.GetInstance().playerUnitMovementAllowed) {
            GameEngine.GetInstance().DisablePlayerUnitMovement();
        } else {
            GameEngine.GetInstance().EnablePlayerUnitMovement();
        }
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

    // Unit Selling
    public void SellUnitButton() {
        int gasRefund;
        switch (GameEngine.GetInstance().playerUnitSelected.rank) {
            case PlayerUnitRank.D:
                gasRefund = 30;
                break;
            case PlayerUnitRank.C:
                gasRefund = 60;
                break;
            case PlayerUnitRank.B:
                gasRefund = 120;
                break;
            case PlayerUnitRank.A:
                gasRefund = 240;
                break;
            case PlayerUnitRank.S:
                gasRefund = 480;
                break;
            case PlayerUnitRank.X:
                gasRefund = 960;
                break;
            default:
                throw new GameplayException("Unrecognized player unit rank. Cannot sell unit.");
        }

        CheckRareUnitSold();
        
        GameEngine.GetInstance().IncreaseGas(gasRefund);
        Destroy(GameEngine.GetInstance().playerUnitSelected.gameObject);
        GameEngine.GetInstance().unitSelectionPanel.CloseUnitSelectionPanelButton();
    }

    private void CheckRareUnitSold() {
        if (GameEngine.GetInstance().playerUnitSelected.unitClass == UnitClass.MAGIC || GameEngine.GetInstance().playerUnitSelected.unitClass == UnitClass.FLAME) {
            GameEngine.GetInstance().achievementManager.rareUnitsSold += 1;
            GameEngine.GetInstance().achievementManager.CheckAchievementsForUnitSelling();
        }
    }
}
