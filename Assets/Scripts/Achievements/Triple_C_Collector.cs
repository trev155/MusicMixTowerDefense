using UnityEngine;

public class Triple_C_Collector : Achievement {
    public Triple_C_Collector() : base() {
        this.achievementName = "Triple C Collector";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}
