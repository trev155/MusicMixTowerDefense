using UnityEngine;

public class D_Mission : Achievement {
    public D_Mission() : base() {
        this.achievementName = "D Mission";
    }

    public override bool CheckCondition() {
        bool hasInfantry = false;
        bool hasMech = false;
        bool hasLaser = false;
        bool hasPsionic = false;
        bool hasAcid = false;
        bool hasBlade = false;

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();

            if (playerUnit.GetPlayerUnitData().GetRank() != PlayerUnitRank.D) {
                continue;
            }

            UnitClass unitClass = playerUnit.GetPlayerUnitData().GetUnitClass();
            if (unitClass == UnitClass.INFANTRY) {
                hasInfantry = true;
            } else if (unitClass == UnitClass.MECH) {
                hasMech = true;
            } else if (unitClass == UnitClass.LASER) {
                hasLaser = true;
            } else if (unitClass == UnitClass.PSIONIC) {
                hasPsionic = true;
            } else if (unitClass == UnitClass.ACID) {
                hasAcid = true;
            } else if (unitClass == UnitClass.BLADE) {
                hasBlade = true;
            }
        }

        return hasInfantry && hasMech && hasLaser && hasPsionic && hasAcid && hasBlade;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 2 Shop Tokens", MessageType.POSITIVE);
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete", MessageType.POSITIVE);
        GameEngine.GetInstance().IncreaseTokenCount(2);
    }
}
