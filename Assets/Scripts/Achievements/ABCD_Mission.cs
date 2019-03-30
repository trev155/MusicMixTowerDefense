using UnityEngine;

public class ABCD_Mission : Achievement {
    public ABCD_Mission() : base() {
        this.achievementName = "ABCD Mission";
    }

    public override bool CheckCondition() {
        if (CheckABCDForClass(UnitClass.INFANTRY)) {
            return true;
        } else if (CheckABCDForClass(UnitClass.MECH)) {
            return true;
        } else if (CheckABCDForClass(UnitClass.LASER)) {
            return true;
        } else if (CheckABCDForClass(UnitClass.PSIONIC)) {
            return true;
        } else if (CheckABCDForClass(UnitClass.ACID)) {
            return true;
        } else if (CheckABCDForClass(UnitClass.BLADE)) {
            return true;
        } else {
            return false;
        }
    }

    private bool CheckABCDForClass(UnitClass unitClassToCheck) {
        bool hasA = false;
        bool hasB = false;
        bool hasC = false;
        bool hasD = false;

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();
            
            if (playerUnit.rank == PlayerUnitRank.S || playerUnit.rank == PlayerUnitRank.X || playerUnit.unitClass != unitClassToCheck) {
                continue;
            }
            if (playerUnit.unitClass == UnitClass.MAGIC || playerUnit.unitClass == UnitClass.FLAME) {
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
            }
        }

        return hasA && hasB && hasC && hasD;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 1 B Rank Token", MessageType.POSITIVE);
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete", MessageType.POSITIVE);
        GameEngine.GetInstance().AddBBonusTokens(1);
    }
}
