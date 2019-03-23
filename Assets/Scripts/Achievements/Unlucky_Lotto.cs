using UnityEngine;

public class Unlucky_Lotto : Achievement {
    public Unlucky_Lotto() : base() {
        this.achievementName = "Unlucky Lotto";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}