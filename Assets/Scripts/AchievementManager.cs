using UnityEngine;
using System.Collections.Generic;


public class AchievementManager : MonoBehaviour {
    public List<Achievement> achievementsList;
    public System.Random random;

    private void Awake() {
        achievementsList = new List<Achievement> {
            new D_Mission(),
            new C_Mission(),
            new B_Mission(),
            new A_Mission(),
            new S_Mission(),
            new ABCD_Mission(),
            new C_Bonus(),
            new B_Bonus(),
            new FourB_Artist(),
            new Greed_Is_Good(),
            new Unlucky_Lotto(),
            new Rare_Collector(),
            new Teemo_Before_15(),
            new Five_Of_A_Kind(),
            new XSABCD_Mission(),
            new Triple_C_Collector(),
            new All_Bonus_Tokens(),
            new Rare_Seller()
        };

        random = new System.Random();
    }

    private void Start() {
        SetBonusMissionObjectives();
    }

    public void CheckAchievements() {
        Debug.Log("Checking Achievements");

        foreach (Achievement achievement in achievementsList) {
            if (achievement.isCompleted) {
                continue;
            }

            if (achievement.CheckCondition()) {
                achievement.isCompleted = true;
                achievement.GiveReward();
                IndicateCompleted(achievement.achievementName);
            }
        }
    }

    private void IndicateCompleted(string achievementName) {
        switch (achievementName) {
            case "D Mission":
                GameEngine.GetInstance().achievementsPanel.D_Mission_Complete.text = "Completed";
                break;
            case "C Mission":
                GameEngine.GetInstance().achievementsPanel.C_Mission_Complete.text = "Completed";
                break;
            case "B Mission":
                GameEngine.GetInstance().achievementsPanel.B_Mission_Complete.text = "Completed";
                break;
            case "A Mission":
                GameEngine.GetInstance().achievementsPanel.A_Mission_Complete.text = "Completed";
                break;
            case "S Mission":
                GameEngine.GetInstance().achievementsPanel.S_Mission_Complete.text = "Completed";
                break;
            case "ABCD Mission":
                GameEngine.GetInstance().achievementsPanel.ABCD_Mission_Complete.text = "Completed";
                break;
            case "C Bonus":
                GameEngine.GetInstance().achievementsPanel.C_Bonus_Complete.text = "Completed";
                break;
            case "B Bonus":
                GameEngine.GetInstance().achievementsPanel.B_Bonus_Complete.text = "Completed";
                break;
            case "4B Artist":
                GameEngine.GetInstance().achievementsPanel.FourB_Artist_Complete.text = "Completed";
                break;
            case "Greed Is Good":
                GameEngine.GetInstance().achievementsPanel.GreedIsGood_Complete.text = "Completed";
                break;
            case "Unlucky Lotto":
                GameEngine.GetInstance().achievementsPanel.UnluckyLotto_Complete.text = "Completed";
                break;
            case "Rare Collector":
                GameEngine.GetInstance().achievementsPanel.RareCollector_Complete.text = "Completed";
                break;
            case "Teemo Before 15":
                GameEngine.GetInstance().achievementsPanel.TeemoBefore15_Complete.text = "Completed";
                break;
            case "Five of 'A' Kind":
                GameEngine.GetInstance().achievementsPanel.FiveSameA_Complete.text = "Completed";
                break;
            case "XSABCD Mission":
                GameEngine.GetInstance().achievementsPanel.XSABCD_Complete.text = "Completed";
                break;
            case "Triple C Collector":
                GameEngine.GetInstance().achievementsPanel.TripleC_Complete.text = "Completed";
                break;
            case "All Bonus Tokens":
                GameEngine.GetInstance().achievementsPanel.FiveSameA_Complete.text = "Completed";
                break;
            case "Rare Seller":
                GameEngine.GetInstance().achievementsPanel.Rare_Seller_Complete.text = "Completed";
                break;
            default:
                throw new GameplayException("Unrecognized Achievement Name: " + achievementName + ". Did not switch completed text.");
        }
    }

    public void SetBonusMissionObjectives() {
        C_Bonus cBonus = (C_Bonus)achievementsList[6];
        Debug.Log(GameEngine.GetInstance().achievementsPanel);
        GameEngine.GetInstance().achievementsPanel.C_Bonus_Description.text = "Collect 3 C units of type: " + cBonus.targetClass.ToString() + ".\n(Bonus: Harvester)";

        B_Bonus bBonus = (B_Bonus)achievementsList[7];
        GameEngine.GetInstance().achievementsPanel.B_Bonus_Description.text = "Collect 3 B units of type: " + bBonus.targetClass.ToString() + ".\n(Bonus: 1 A Rank Token)";        
    }
}
