using UnityEngine;
using System.Collections.Generic;


public class AchievementManager : MonoBehaviour {
    public List<Achievement> achievementsList;

    public int rareUnitsSold;
    public int failedUnitLotto;

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

        rareUnitsSold = 0;
        failedUnitLotto = 0;
    }

    private void Start() {
        SetBonusMissionObjectives();
    }

    public void CheckAllAchievements() {
        foreach (Achievement achievement in achievementsList) {
            if (achievement.isCompleted) {
                continue;
            }

            if (achievement.CheckCondition()) {
                achievement.isCompleted = true;
                achievement.GiveReward();
                GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.MESSAGE_ACHIEVEMENT);
                IndicateCompleted(achievement.achievementName);
            }
        }
    }

    private void CheckAchievements(params int[] achievementIndices) {
        for (int i = 0; i < achievementIndices.Length; i++) {
            int index = achievementIndices[i];
            Achievement achievement = achievementsList[index];
            if (achievement.isCompleted) {
                continue;
            }

            if (achievement.CheckCondition()) {
                achievement.isCompleted = true;
                achievement.GiveReward();
                GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.MESSAGE_ACHIEVEMENT);
                IndicateCompleted(achievement.achievementName);
            }
        }
    }

    public void CheckAchievementsForPlayerUnitCreation() {
        CheckAchievements(0, 1, 2, 3, 4, 5, 6, 7, 11, 13, 14, 15);
    }

    public void CheckAchievementsForBChoosers() {
        CheckAchievements(8);
    }

    public void CheckAchievementsForHarvesterBonus() {
        CheckAchievements(9);
    }

    public void CheckAchievementsForTokenLotto() {
        CheckAchievements(10);
    }

    public void CheckAchievementsForBossKill() {
        CheckAchievements(12);
    }

    public void CheckAchievementsForAllBonusTokens() {
        CheckAchievements(16);
    }

    public void CheckAchievementsForUnitSelling() {
        CheckAchievements(17);
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
        int targetClass_C = GameEngine.GetInstance().random.Next(0, 6);
        int targetClass_B = GameEngine.GetInstance().random.Next(0, 6);

        C_Bonus cBonus = (C_Bonus)achievementsList[6];
        cBonus.targetClass = ChoiceIntToUnitClass(targetClass_C);
        GameEngine.GetInstance().achievementsPanel.C_Bonus_Description.text = "Collect 3 C units of type: " + cBonus.targetClass.ToString() + ".\n(Bonus: Harvester)";

        B_Bonus bBonus = (B_Bonus)achievementsList[7];
        bBonus.targetClass = ChoiceIntToUnitClass(targetClass_B);
        GameEngine.GetInstance().achievementsPanel.B_Bonus_Description.text = "Collect 3 B units of type: " + bBonus.targetClass.ToString() + ".\n(Bonus: 1 A Rank Token)";        
    }

    private UnitClass ChoiceIntToUnitClass(int choice) {
        UnitClass unitClass;
        switch (choice) {
            case 0:
                unitClass = UnitClass.INFANTRY;
                break;
            case 1:
                unitClass = UnitClass.MECH;
                break;
            case 2:
                unitClass = UnitClass.LASER;
                break;
            case 3:
                unitClass = UnitClass.PSIONIC;
                break;
            case 4:
                unitClass = UnitClass.ACID;
                break;
            case 5:
                unitClass = UnitClass.BLADE;
                break;
            default:
                throw new GameplayException("Invalid choice option. Cannot select a C Unit Class Bonus.");
        }
        return unitClass;
    }
}
