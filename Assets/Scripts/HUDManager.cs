using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour {
    // ---------- Constants ----------
    public readonly float HIGHLIGHTED_ALPHA = 0.8f;
    public readonly float UNHIGHLIGHTED_ALPHA = 0.1f;


    // ---------- Fields ----------
    public RectTransform unitSelectionPanel;
    public Text titleData;
    public Text basicUnitData;
    public Text typeSpecificUnitData;
    public Button moveUnitButton;
    public Text moveUnitButtonText;
    public Text moveUnitInstruction;
    public Transform moveableArea;


    // ---------- Methods ----------
    private void Awake() {
        // Hide by default
        unitSelectionPanel.gameObject.SetActive(false);
        moveUnitInstruction.gameObject.SetActive(false);
    }

    // Temporary buttons for unit creation
    public void SpawnPlayerUnit() {
        GameEngine.Instance.unitSpawner.CreateRandomDUnit();
    }

    public void SpawnEnemyUnit() {
        GameEngine.Instance.unitSpawner.CreateEnemyUnit();
    }

    // Show and hide unit selection panel.
    public void ShowUnitSelectionPanel(IClickableUnit unit) {
        unitSelectionPanel.gameObject.SetActive(true);
        UpdateSelectedUnitDataPanel(unit);
        if (unit is PlayerUnit) {
            GameEngine.Instance.playerUnitSelected = (PlayerUnit)unit;
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
    }

    // Functions for updating HUD
    public void UpdateSelectedUnitDataPanel(IClickableUnit unit) {
        DisplayTitleData(unit);
        DisplayBasicUnitData(unit);
        DisplayTypeSpecificUnitData(unit);
    }

    public void DisplayTitleData(IClickableUnit unit) {
        titleData.text = unit.GetTitleData();
    }
    
    public void DisplayBasicUnitData(IClickableUnit unit) {
        basicUnitData.text = unit.GetBasicUnitData();
    }

    public void DisplayTypeSpecificUnitData(IClickableUnit unit) {
        typeSpecificUnitData.text = unit.GetAdvancedUnitData();
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
}
