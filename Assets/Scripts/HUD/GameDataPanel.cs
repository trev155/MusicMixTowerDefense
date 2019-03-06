using UnityEngine;
using UnityEngine.UI;


public class GameDataPanel : MonoBehaviour {
    // ---------- Fields ----------
    public Text mineralsText;
    public Text vespeneText;
    public Text killCounter;
    public Text levelTimer;
    public Text tokenCount;


    // ---------- Methods ----------
    private void Awake() {
        mineralsText.text = "Minerals: 0";
        vespeneText.text = "Vespene: 0";
        killCounter.text = "Kills: 0";
        levelTimer.text = "Level Time Left: ";
        tokenCount.text = "Tokens: 0";
    }

    public void UpdateMineralsText(int minerals) {
        mineralsText.text = "Minerals: " + minerals;
    }

    public void UpdateVespeneText(int vespene) {
        vespeneText.text = "Vespene: " + vespene;
    }

    public void UpdateKillCounter(int kills) {
        killCounter.text = "Kills: " + kills;
    }

    public void SetLevelTimer(string timeStr) {
        levelTimer.text = "Level Time Left: " + timeStr;
    }

    public void UpdateTokenCount(int tokens) {
        tokenCount.text = "Tokens: " + tokens;
    }
}
