using UnityEngine;

public abstract class Achievement {
    public Achievement() {
        this.isCompleted = false;
    }

    public string achievementName;
    public bool isCompleted;

    public Transform completedText;
    
    public abstract void GiveReward();
    public abstract bool CheckCondition();        
}
