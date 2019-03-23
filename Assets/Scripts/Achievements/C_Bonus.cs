using UnityEngine;

public class C_Bonus : Achievement {
    public C_Bonus() : base() {
        this.achievementName = "C Bonus";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}
