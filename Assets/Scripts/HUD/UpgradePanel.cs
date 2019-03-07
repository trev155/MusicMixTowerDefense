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
        AttemptUpgrade(UnitClass.INFANTRY);
    }

    public void UpgradeMech() {
        AttemptUpgrade(UnitClass.MECH);
    }

    public void UpgradeLaser() {
        AttemptUpgrade(UnitClass.LASER);
    }

    public void UpgradePsionic() {
        AttemptUpgrade(UnitClass.PSIONIC);
    }

    public void UpgradeAcid() {
        AttemptUpgrade(UnitClass.ACID);
    }

    public void UpgradeBlade() {
        AttemptUpgrade(UnitClass.BLADE);
    }

    public void UpgradeMagic() {
        AttemptUpgrade(UnitClass.MAGIC);
    }

    public void UpgradeFlame() {
        AttemptUpgrade(UnitClass.FLAME);
    }

    private void AttemptUpgrade(UnitClass unitClass) {
        int upgradeCost = GameEngine.GetInstance().upgradeManager.GetUpgradeCost(unitClass);
        if (GameEngine.GetInstance().vespene >= upgradeCost) {
            GameEngine.GetInstance().DecreaseVespene(upgradeCost);
            int nextLevel = GameEngine.GetInstance().upgradeManager.IncrementUpgradeClass(unitClass);
            DisplayUpgradeCompleteMessage(unitClass, nextLevel);
            SetUnitClassUpgradeDataDisplay(unitClass);
        } else {
            DisplayCantAffordUpgradeMessage(unitClass, upgradeCost);
        }
    }

    private void DisplayUpgradeCompleteMessage(UnitClass unitClass, int level) {
        Debug.Log("Upgrade Complete! [" + unitClass.ToString() + " Level " + level + "]");
    }

    private void DisplayCantAffordUpgradeMessage(UnitClass unitClass, int upgradeCost) {
        Debug.Log("Cannot purchase upgrade for class: [" + unitClass.ToString() + "]. " +
            "Upgrade cost is [" + upgradeCost + " gas], currently have [" + GameEngine.GetInstance().vespene + " gas]");
    }

    private void SetUnitClassUpgradeDataDisplay(UnitClass unitClass) {
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
}
