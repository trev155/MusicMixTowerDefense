using UnityEngine;
using UnityEngine.UI;


public class GameDataPanel : MonoBehaviour {
    // ---------- Fields ----------
    public Text killCounter;
    public Text levelTimer;
    public Text tokenCount;


    // ---------- Methods ----------
    private void Awake() {
        levelTimer.text = "";
    }

    public void UpdateKillCounter(int kills) {
        killCounter.text = "Enemy Units Killed: " + kills;
    }

    public void SetLevelTimer(string timeStr) {
        levelTimer.text = "Time left in level: " + timeStr;
    }

    public void UpdateTokenCount(int tokens) {
        tokenCount.text = "Tokens: " + tokens;
    }
}
