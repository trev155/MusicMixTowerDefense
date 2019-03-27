public class Rare_Seller : Achievement {
    public Rare_Seller() : base() {
        this.achievementName = "Rare Seller";
    }

    public override bool CheckCondition() {
        return GameEngine.GetInstance().achievementManager.rareUnitsSold >= 7;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 2 A Rank Tokens");
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete");
        GameEngine.GetInstance().AddABonusTokens(2);
    }
}
