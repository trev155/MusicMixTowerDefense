using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class HUDManager : MonoBehaviour {
    // ---------- Constants ----------
    private static readonly float HIGHLIGHTED_ALPHA = 0.4f;
    private static readonly float UNHIGHLIGHTED_ALPHA = 0.05f;


    // ---------- Fields ----------
    public RectTransform unitSelectionPanel;
    public Text textSlot1;
    public Text textSlot2;
    public Text textSlot3;
    public Text textSlot4;
    public Text textSlot5;
    public Text textSlot6;
    public Button moveUnitButton;
    public Text moveUnitButtonText;
    public Text moveUnitInstruction;

    public Transform moveableArea;

    public Text killCounter;


    // ---------- Methods ----------
    private void Awake() {
        // Hide by default
        unitSelectionPanel.gameObject.SetActive(false);
        moveUnitInstruction.gameObject.SetActive(false);
    }

    // Show and hide unit selection panel.
    public void ShowUnitSelectionPanel(IClickableUnit unit) {
        unitSelectionPanel.gameObject.SetActive(true);
        UpdateSelectedUnitDataPanel(unit);
        if (unit is PlayerUnit) {
            moveUnitButton.gameObject.SetActive(true);
        } else {
            moveUnitButton.gameObject.SetActive(false);
        }
    }

    public void HideUnitSelectionPanel() {
        unitSelectionPanel.gameObject.SetActive(false);
    }

    public void CloseUnitSelectionPanelButton() {
        HideUnitSelectionPanel();
        GameEngine.Instance.DisablePlayerUnitMovement();
        GameEngine.Instance.playerUnitSelected.attackRangeCircle.SetAlpha(AttackRangeCircle.UNSELECTED_ALPHA);
        GameEngine.Instance.ClearUnitSelectionObjects();
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
    }
    
    // Player Unit Selection movement
    public void MoveUnitButton() {
        if (GameEngine.Instance.playerUnitMovementAllowed) {
            GameEngine.Instance.DisablePlayerUnitMovement();
        } else {
            GameEngine.Instance.EnablePlayerUnitMovement();
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
            Debug.LogWarning("Cannot set moveable area alpha. Value of " + alpha + " was invalid.");
            return;
        }
        Color moveableAreaColor = moveableArea.GetComponent<SpriteRenderer>().color;
        moveableAreaColor.a = alpha;
        moveableArea.GetComponent<SpriteRenderer>().color = moveableAreaColor;
    }

    // Game data Panel
    public void UpdateKillCounter(int kills) {
        killCounter.text = "Enemy Units Killed: " + kills;
    }
}
