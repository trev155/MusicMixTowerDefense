using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class HUDManager : MonoBehaviour {
    // ---------- Fields ----------
    public RectTransform UnitSelectionPanel;
    public Text titleData;
    public Text basicUnitData;
    public Text typeSpecificUnitData;

    public bool unitIsSelected = false;

    // ---------- Methods ----------
    private void Awake() {
        // Hide by default
        UnitSelectionPanel.gameObject.SetActive(false);
    }

    // Temporary buttons for unit creation
    public void SpawnPlayerUnit() {
        GameEngine.Instance.unitSpawner.CreateRandomDUnit();
    }

    public void SpawnEnemyUnit() {
        GameEngine.Instance.unitSpawner.CreateEnemyUnit();
    }

    // Functions for updating HUD
    public void UpdateSelectedUnitData(IClickableUnit unit) {
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

    // Show and hide unit selection panel.
    public void ShowUnitSelectionPanel() {
        UnitSelectionPanel.gameObject.SetActive(true);
    }

    public void HideUnitSelectionPanel() {
        if (!unitIsSelected) {
            UnitSelectionPanel.gameObject.SetActive(false);
        }
    }
}
