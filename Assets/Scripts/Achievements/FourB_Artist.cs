using UnityEngine;

public class FourB_Artist : Achievement {
    public FourB_Artist() : base() {
        this.achievementName = "4B Artist";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}
