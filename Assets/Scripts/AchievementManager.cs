using UnityEngine;
using System.Collections.Generic;


public class AchievementManager : MonoBehaviour {
    public List<Achievement> achievementsList;

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
            }
        }
    }
}
