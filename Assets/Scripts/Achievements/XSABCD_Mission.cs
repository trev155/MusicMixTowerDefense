using UnityEngine;

public class XSABCD_Mission : Achievement {
    public XSABCD_Mission() : base() {
        this.achievementName = "XSABCD Mission";
    }

    public override bool CheckCondition() {
        if (GameEngine.GetInstance().hasXUnit) {
            return false;
        }

        // TODO should we conside rare units?
        // Since can only have 1 X unit, we know which class we are looking for
        UnitClass classToLookFor = UnitClass.NONE;
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();
            if (playerUnit.rank == PlayerUnitRank.X) {
                classToLookFor = playerUnit.unitClass;
                break;
            }
        }

        bool hasS = false;
        bool hasA = false;
        bool hasB = false;
        bool hasC = false;
        bool hasD = false;
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();

            if (playerUnit.unitClass != classToLookFor) {
                continue;
            }

            if (playerUnit.rank == PlayerUnitRank.D) {
                hasD = true;
            } else if (playerUnit.rank == PlayerUnitRank.C) {
                hasC = true;
            } else if (playerUnit.rank == PlayerUnitRank.B) {
                hasB = true;
            } else if (playerUnit.rank == PlayerUnitRank.A) {
                hasA = true;
            } else if (playerUnit.rank == PlayerUnitRank.S) {
                hasS = true;
            }
        }

        return hasS && hasA && hasB && hasC && hasD;
    }

    public override void GiveReward() {
        Debug.Log(this.achievementName + " Complete");
        Debug.Log("Bonus: 1200 Gas");
        GameEngine.GetInstance().IncreaseGas(1200);
    }
}
