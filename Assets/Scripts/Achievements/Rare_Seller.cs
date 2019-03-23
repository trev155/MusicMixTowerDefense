using UnityEngine;

public class Rare_Seller : Achievement {
    public Rare_Seller() : base() {
        this.achievementName = "Rare Seller";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}
