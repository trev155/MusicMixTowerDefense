using UnityEngine;

public class B_Bonus : Achievement {
    public B_Bonus() : base() {
        this.achievementName = "B Bonus";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}