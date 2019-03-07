using UnityEngine;
using System.Collections.Generic;


public class UpgradeManager : MonoBehaviour {
    private Dictionary<UnitClass, int> upgradeMap;

    private void Awake() {
        upgradeMap = new Dictionary<UnitClass, int> {
            { UnitClass.INFANTRY, 0 },
            { UnitClass.MECH, 0 },
            { UnitClass.LASER, 0 },
            { UnitClass.PSIONIC, 0 },
            { UnitClass.ACID, 0 },
            { UnitClass.BLADE, 0 },
            { UnitClass.MAGIC, 0 },
            { UnitClass.FLAME, 0 }
        };
    }

    public int GetNumUpgrades(UnitClass unitClass) {
        return upgradeMap[unitClass];
    }

    public void SetNumUpgrades(UnitClass unitClass, int numUpgrades) {
        upgradeMap[unitClass] = numUpgrades;
    }

    public int IncrementUpgradeClass(UnitClass unitClass) {
        upgradeMap[unitClass] += 1;
        return upgradeMap[unitClass];
    }

    public void PrintAllUpgrades() {
        foreach (KeyValuePair<UnitClass, int> entry in upgradeMap) {
            Debug.Log(entry.Value.ToString() + ": " + entry.Key);
        }
    }

    public int GetUpgradeCost(UnitClass unitClass) {
        int currentUpgradeLevel = upgradeMap[unitClass];
        int upgradeCost = currentUpgradeLevel + 10;
        return upgradeCost;
    }
}