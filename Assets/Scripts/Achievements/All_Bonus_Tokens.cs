using UnityEngine;

public class All_Bonus_Tokens : Achievement {
    public All_Bonus_Tokens() : base() {
        this.achievementName = "All Bonus Tokens";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}
