using UnityEngine;

public class Five_Of_A_Kind : Achievement {
    public Five_Of_A_Kind() : base() {
        this.achievementName = "Five of 'A' Kind";
    }

    public override bool CheckCondition() {
        int numInfantry = 0;
        int numMech = 0;
        int numLaser = 0;
        int numPsionic = 0;
        int numAcid = 0;
        int numBlade = 0;
        int numMagic = 0;
        int numFlame = 0;

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();

            if (playerUnit.rank != PlayerUnitRank.A) {
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
            } else if (playerUnit.unitClass == UnitClass.MAGIC) {
                numMagic += 1;
            } else if (playerUnit.unitClass == UnitClass.FLAME) {
                numFlame += 1;
            }
        }

        return ((numInfantry >= 5) || (numMech >= 5) || (numLaser >= 5) || (numPsionic >= 5) 
            || (numAcid >= 5) || (numBlade >= 5) || (numMagic >= 5) || (numFlame >= 5));
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 1 B Rank Token, 1 A Rank Token", MessageType.POSITIVE);
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete", MessageType.POSITIVE);
        GameEngine.GetInstance().AddBBonusTokens(1);
        GameEngine.GetInstance().AddABonusTokens(1);
    }
}
