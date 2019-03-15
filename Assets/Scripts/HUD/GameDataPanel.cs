using UnityEngine;
using UnityEngine.UI;


public class GameDataPanel : MonoBehaviour {
    // ---------- Fields ----------
    public Text mineralsText;
    public Text gasText;
    public Text levelText;
    public Text tokenCountText;
    public Text killCounterText;
    public Text globalGameTimerText;
    public Text levelTimerText;


    // ---------- Methods ----------
    private void Awake() {
        mineralsText.text = "Minerals: 0";
        gasText.text = "Gas: 0";
        killCounterText.text = "Kills: 0";
        levelTimerText.text = "Level Time Left: ";
        tokenCountText.text = "Tokens: 0";
    }

    public void UpdateMineralsText(int minerals) {
        mineralsText.text = "Minerals: " + minerals;
    }

    public void UpdateGasText(int gas) {
        gasText.text = "Gas: " + gas;
    }

    public void UpdateLevelText(int level) {
        levelText.text = "Level: " + level;
    }

    public void UpdateTokenCountText(int tokens) {
        tokenCountText.text = "Tokens: " + tokens;
    }

    public void UpdateKillCounterText(int kills) {
        killCounterText.text = "Kills: " + kills;
    }

    public void UpdateGlobalGameTimeText(string timeStr) {
        globalGameTimerText.text = "Game Time: " + timeStr;
    }

    public void SetLevelTimerText(string timeStr) {
        levelTimerText.text = "Level Time Left: " + timeStr;
    }
}
