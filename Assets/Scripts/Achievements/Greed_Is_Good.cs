using UnityEngine;


public class Greed_Is_Good : Achievement {
    public Greed_Is_Good() : base() {
        this.achievementName = "Greed Is Good";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}
