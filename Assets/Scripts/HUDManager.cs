using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    // ---------- Fields ----------
    public Text titleData;
    public Text basicUnitData;
    public Text typeSpecificUnitData;
    

    // ---------- Methods ----------
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
}
