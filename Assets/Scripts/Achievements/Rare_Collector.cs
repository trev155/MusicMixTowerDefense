using UnityEngine;

public class Rare_Collector : Achievement {
    public Rare_Collector() : base() {
        this.achievementName = "Rare Collector";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}
