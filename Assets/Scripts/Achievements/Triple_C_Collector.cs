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

            if (playerUnit.GetPlayerUnitData().GetRank() != PlayerUnitRank.C) {
                continue;
            }

            UnitClass playerUnitClass = playerUnit.GetPlayerUnitData().GetUnitClass();
            if (playerUnitClass == UnitClass.INFANTRY) {
                numInfantry += 1;
            } else if (playerUnitClass == UnitClass.MECH) {
                numMech += 1;
            } else if (playerUnitClass == UnitClass.LASER) {
                numLaser += 1;
            } else if (playerUnitClass == UnitClass.PSIONIC) {
                numPsionic += 1;
            } else if (playerUnitClass == UnitClass.ACID) {
                numAcid += 1;
            } else if (playerUnitClass == UnitClass.BLADE) {
                numBlade += 1;
            }
        }

        return ((numInfantry >= 3) && (numMech >= 3) && (numLaser >= 3) && (numPsionic >= 3) && (numAcid >= 3) && (numBlade >= 3));
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 1 S Rank Token", MessageType.POSITIVE);
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete", MessageType.POSITIVE);
        GameEngine.GetInstance().AddSBonusTokens(1);
    }
}
