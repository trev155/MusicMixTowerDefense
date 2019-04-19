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
    public Text enemyUnitCountText;


    // ---------- Methods ----------
    private void Start() {
        InitializeDefaults();
    }


    private void InitializeDefaults() {
        UpdateMineralsText(GameEngine.GetInstance().minerals);
        UpdateGasText(GameEngine.GetInstance().gas);
        UpdateLevelText(0);
        UpdateTokenCountText(GameEngine.GetInstance().tokenCount);
        UpdateKillCounterText(0);
        UpdateGlobalGameTimeText("");
        UpdateLevelTimeText("");
        UpdateEnemyUnitCountText(0);
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

    public void UpdateLevelTimeText(string timeStr) {
        levelTimerText.text = "Level Time Left: " + timeStr;
    }

    public void UpdateEnemyUnitCountText(int enemyCount) {
        enemyUnitCountText.text = "Enemy Units: " + enemyCount;
    }
}
