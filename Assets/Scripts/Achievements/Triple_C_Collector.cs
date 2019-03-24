using UnityEngine;

public class Triple_C_Collector : Achievement {
    public Triple_C_Collector() : base() {
        this.achievementName = "Triple C Collector";
    }

    public override bool CheckCondition() {
        int numInfantry = 0;
        int numMech = 0;
        int numLaser = 0;
        int numPsionic = 0;
        int numAcid = 0;
        int numBlade = 0;

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();

            if (playerUnit.rank != PlayerUnitRank.C) {
                continue;
            }

            if (playerUnit.unitClass == UnitClass.INFANTRY) {
                numInfantry += 1;
            } else if (playerUnit.unitClass == UnitClass.MECH) {
                numMech += 1;
            } else if (playerUnit.unitClass == UnitClass.LASER) {
                numLaser += 1;
            } else if (playerUnit.unitClass == UnitClass.PSIONIC) {
                numPsionic += 1;
            } else if (playerUnit.unitClass == UnitClass.ACID) {
                numAcid += 1;
            } else if (playerUnit.unitClass == UnitClass.BLADE) {
                numBlade += 1;
            }
        }

        return ((numInfantry >= 3) && (numMech >= 3) && (numLaser >= 3) && (numPsionic >= 3) && (numAcid >= 3) && (numBlade >= 3));
    }

    public override void GiveReward() {
        Debug.Log(this.achievementName + " Complete");
        Debug.Log("Bonus: 1 S Rank Token");
        GameEngine.GetInstance().AddSBonusTokens(1);
    }
}
