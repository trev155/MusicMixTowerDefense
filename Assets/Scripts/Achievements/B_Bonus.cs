﻿using UnityEngine;

public class B_Bonus : Achievement {
    public UnitClass targetClass;

    public B_Bonus() : base() {
        this.achievementName = "B Bonus";
    }

    public override bool CheckCondition() {
        int targetCount = 0;
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();
            if (playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.B && playerUnit.GetPlayerUnitData().GetUnitClass() == this.targetClass) {
                targetCount += 1;
            }
        }

        return targetCount >= 3;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 1 A Rank Token", MessageType.POSITIVE);
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete", MessageType.POSITIVE);
        GameEngine.GetInstance().AddABonusTokens(1);
    }
}
