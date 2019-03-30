using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour {
    public Text InfantryLevel;
    public Text MechLevel;
    public Text LaserLevel;
    public Text PsionicLevel;
    public Text AcidLevel;
    public Text BladeLevel;
    public Text MagicLevel;
    public Text FlameLevel;
    public Text InfantryCost;
    public Text MechCost;
    public Text LaserCost;
    public Text PsionicCost;
    public Text AcidCost;
    public Text BladeCost;
    public Text MagicCost;
    public Text FlameCost;

    public void UpgradeInfantry() {
        GameEngine.GetInstance().upgradeManager.AttemptUpgrade(UnitClass.INFANTRY);
    }

    public void UpgradeMech() {
        GameEngine.GetInstance().upgradeManager.AttemptUpgrade(UnitClass.MECH);
    }

    public void UpgradeLaser() {
        GameEngine.GetInstance().upgradeManager.AttemptUpgrade(UnitClass.LASER);
    }

    public void UpgradePsionic() {
        GameEngine.GetInstance().upgradeManager.AttemptUpgrade(UnitClass.PSIONIC);
    }

    public void UpgradeAcid() {
        GameEngine.GetInstance().upgradeManager.AttemptUpgrade(UnitClass.ACID);
    }

    public void UpgradeBlade() {
        GameEngine.GetInstance().upgradeManager.AttemptUpgrade(UnitClass.BLADE);
    }

    public void UpgradeMagic() {
        GameEngine.GetInstance().upgradeManager.AttemptUpgrade(UnitClass.MAGIC);
    }

    public void UpgradeFlame() {
        GameEngine.GetInstance().upgradeManager.AttemptUpgrade(UnitClass.FLAME);
    }

    // UI Methods
    public void DisplayUpgradeCompleteMessage(UnitClass unitClass, int level) {
        GameEngine.GetInstance().messageQueue.PushMessage("Upgrade Complete! [" + unitClass.ToString() + " Level " + level + "]", MessageType.POSITIVE);
    }

    public void DisplayCantAffordUpgradeMessage(UnitClass unitClass, int upgradeCost) {
        GameEngine.GetInstance().messageQueue.PushMessage("Cannot purchase upgrade for class: [" + unitClass.ToString() + "]. " +
            "Upgrade cost is [" + upgradeCost + " gas], currently have [" + GameEngine.GetInstance().gas + " gas]", MessageType.NEGATIVE);
    }

    public void SetUnitClassUpgradeDataDisplay(UnitClass unitClass) {
        switch (unitClass) {
            case UnitClass.INFANTRY:
                InfantryLevel.text = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(unitClass) + "";
                InfantryCost.text = "Cost: " + GameEngine.GetInstance().upgradeManager.GetUpgradeCost(unitClass) + " Gas";
                break;
            case UnitClass.MECH:
                MechLevel.text = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(unitClass) + "";
                MechCost.text = "Cost: " + GameEngine.GetInstance().upgradeManager.GetUpgradeCost(unitClass) + " Gas";
                break;
            case UnitClass.LASER:
                LaserLevel.text = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(unitClass) + "";
                LaserCost.text = "Cost: " + GameEngine.GetInstance().upgradeManager.GetUpgradeCost(unitClass) + " Gas";
                break;
            case UnitClass.PSIONIC:
                PsionicLevel.text = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(unitClass) + "";
                PsionicCost.text = "Cost: " + GameEngine.GetInstance().upgradeManager.GetUpgradeCost(unitClass) + " Gas";
                break;
            case UnitClass.ACID:
                AcidLevel.text = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(unitClass) + "";
                AcidCost.text = "Cost: " + GameEngine.GetInstance().upgradeManager.GetUpgradeCost(unitClass) + " Gas";
                break;
            case UnitClass.BLADE:
                BladeLevel.text = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(unitClass) + "";
                BladeCost.text = "Cost: " + GameEngine.GetInstance().upgradeManager.GetUpgradeCost(unitClass) + " Gas";
                break;
            case UnitClass.MAGIC:
                MagicLevel.text = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(unitClass) + "";
                MagicCost.text = "Cost: " + GameEngine.GetInstance().upgradeManager.GetUpgradeCost(unitClass) + " Gas";
                break;
            case UnitClass.FLAME:
                FlameLevel.text = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(unitClass) + "";
                FlameCost.text = "Cost: " + GameEngine.GetInstance().upgradeManager.GetUpgradeCost(unitClass) + " Gas";
                break;
            default:
                throw new GameplayException("Unrecognized Unit Class: " + unitClass.ToString());
        }
    }

    public void UpdateUpgradePanelData(UnitClass unitClass) {
        int level = GameEngine.GetInstance().upgradeManager.GetNumUpgrades(unitClass);
        GameEngine.GetInstance().upgradePanel.DisplayUpgradeCompleteMessage(unitClass, level);
        GameEngine.GetInstance().upgradePanel.SetUnitClassUpgradeDataDisplay(unitClass);
    }
}
