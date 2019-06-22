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
            PlayerUnitRank playerUnitRank = playerUnit.GetPlayerUnitData().GetRank();
            UnitClass playerUnitClass = playerUnit.GetPlayerUnitData().GetUnitClass();

            if (playerUnitRank == PlayerUnitRank.S || playerUnitRank == PlayerUnitRank.X || playerUnitClass != unitClassToCheck) {
                continue;
            }
            if (playerUnitClass == UnitClass.MAGIC || playerUnitClass == UnitClass.FLAME) {
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
