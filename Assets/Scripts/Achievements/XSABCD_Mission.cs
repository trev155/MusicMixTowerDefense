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
            if (playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.X) {
                classToLookFor = playerUnit.GetPlayerUnitData().GetUnitClass();
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
            PlayerUnitRank playerUnitRank = playerUnit.GetPlayerUnitData().GetRank();
            UnitClass playerUnitClass = playerUnit.GetPlayerUnitData().GetUnitClass();

            if (playerUnitClass != classToLookFor) {
                continue;
            }

            if (playerUnitRank == PlayerUnitRank.D) {
                hasD = true;
            } else if (playerUnitRank == PlayerUnitRank.C) {
                hasC = true;
            } else if (playerUnitRank == PlayerUnitRank.B) {
                hasB = true;
            } else if (playerUnitRank == PlayerUnitRank.A) {
                hasA = true;
            } else if (playerUnitRank == PlayerUnitRank.S) {
                hasS = true;
            }
        }

        return hasS && hasA && hasB && hasC && hasD;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 1200 Gas", MessageType.POSITIVE);
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete", MessageType.POSITIVE);
        GameEngine.GetInstance().IncreaseGas(1200);
    }
}
