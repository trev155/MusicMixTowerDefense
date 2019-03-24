using UnityEngine;

public class B_Bonus : Achievement {
    public UnitClass targetClass;
    private System.Random random;

    public B_Bonus() : base() {
        this.achievementName = "B Bonus";

        random = new System.Random();
        int choice = random.Next(0, 6);
        switch (choice) {
            case 0:
                targetClass = UnitClass.INFANTRY;
                break;
            case 1:
                targetClass = UnitClass.MECH;
                break;
            case 2:
                targetClass = UnitClass.LASER;
                break;
            case 3:
                targetClass = UnitClass.PSIONIC;
                break;
            case 4:
                targetClass = UnitClass.ACID;
                break;
            case 5:
                targetClass = UnitClass.BLADE;
                break;
            default:
                throw new GameplayException("Invalid choice option. Cannot select a B Unit Class Bonus.");
        }
    }

    public override bool CheckCondition() {
        int targetCount = 0;
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();
            if (playerUnit.rank == PlayerUnitRank.B && playerUnit.unitClass == this.targetClass) {
                targetCount += 1;
            }
        }

        return targetCount >= 3;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 1 A Rank Token");
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete");
        GameEngine.GetInstance().AddABonusTokens(1);
    }
}
