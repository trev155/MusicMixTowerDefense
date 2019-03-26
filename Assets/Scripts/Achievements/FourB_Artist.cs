public class FourB_Artist : Achievement {
    public FourB_Artist() : base() {
        this.achievementName = "4B Artist";
    }

    public override bool CheckCondition() {
        return GameEngine.GetInstance().bChoosers >= 4;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 1 B Rank Token");
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete");
        GameEngine.GetInstance().AddBBonusTokens(1);
    }
}
