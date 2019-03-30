public class Unlucky_Lotto : Achievement {
    public Unlucky_Lotto() : base() {
        this.achievementName = "Unlucky Lotto";
    }

    public override bool CheckCondition() {
        return GameEngine.GetInstance().achievementManager.failedUnitLotto >= 10;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 2 Shop Tokens", MessageType.POSITIVE);
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete", MessageType.POSITIVE);
        GameEngine.GetInstance().IncreaseTokenCount(2);
    }
}