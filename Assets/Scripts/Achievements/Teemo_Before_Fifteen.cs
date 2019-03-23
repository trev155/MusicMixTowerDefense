using UnityEngine;

public class Teemo_Before_15 : Achievement {
    public Teemo_Before_15() : base() {
        this.achievementName = "Teemo Before 15";
    }

    public override bool CheckCondition() {
        return false;
    }

    public override void GiveReward() {
        throw new System.NotImplementedException();
    }
}