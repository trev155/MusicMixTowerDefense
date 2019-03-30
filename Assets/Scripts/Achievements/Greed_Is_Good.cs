public class Greed_Is_Good : Achievement {
    public Greed_Is_Good() : base() {
        this.achievementName = "Greed Is Good";
    }

    public override bool CheckCondition() {
        return GameEngine.GetInstance().unallocatedHarvesters + GameEngine.GetInstance().mineralHarvesters + GameEngine.GetInstance().gasHarvesters >= 10;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: Harvester", MessageType.POSITIVE);
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete", MessageType.POSITIVE);
        GameEngine.GetInstance().AddHarvester();
    }
}
