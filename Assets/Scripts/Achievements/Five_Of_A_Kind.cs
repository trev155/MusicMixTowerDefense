using UnityEngine;

public class Five_Of_A_Kind : Achievement {
    public Five_Of_A_Kind() : base() {
        this.achievementName = "Five of 'A' Kind";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}
