using UnityEngine;

public class Rare_Collector : Achievement {
    public Rare_Collector() : base() {
        this.achievementName = "Rare Collector";
    }

    public override bool CheckCondition() {
        int numRares = 0;

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();
            if (playerUnit.unitClass == UnitClass.MAGIC || playerUnit.unitClass == UnitClass.FLAME) {
                numRares++;
            }
        }

        return numRares >= 5;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 1 B Rank Token, 1 A Rank Token");
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete");
        GameEngine.GetInstance().AddBBonusTokens(1);
        GameEngine.GetInstance().AddABonusTokens(1);
    }
}
