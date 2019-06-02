using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameDataPanel : MonoBehaviour {
    // ---------- Fields ----------
    public Text gameModeText;
    public Text mineralsText;
    public Text gasText;
    public Text levelText;
    public Text tokenCountText;
    public Text killCounterText;
    public Text globalGameTimerText;
    public Text levelTimerText;
    public Text enemyUnitCountText;

    public Text mineralHarvesterBonusText;
    public Text gasHarvesterBonusText;


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

        mineralHarvesterBonusText.text = "";
        gasHarvesterBonusText.text = "";
    }

    public void UpdateGameModeText(GameMode gameMode) {
        gameModeText.text = "Mode:\n" + Utils.CleanEnumString(gameMode.ToString());
    }

    public void UpdateMineralsText(int minerals) {
        mineralsText.text = "Minerals:\n" + minerals;
    }

    public void UpdateGasText(int gas) {
        gasText.text = "Gas:\n" + gas;
    }

    public void UpdateLevelText(int level) {
        levelText.text = "Level:\n" + level;
    }

    public void UpdateTokenCountText(int tokens) {
        tokenCountText.text = "Tokens:\n" + tokens;
    }

    public void UpdateKillCounterText(int kills) {
        killCounterText.text = "Kills:\n" + kills;
    }

    public void UpdateGlobalGameTimeText(string timeStr) {
        globalGameTimerText.text = "Game Time:\n" + timeStr;
    }

    public void UpdateLevelTimeText(string timeStr) {
        levelTimerText.text = "Level Time Left:\n" + timeStr;
    }

    public void SetLevelTransitionTimeText() {
        levelTimerText.text = "Waiting for next level...";
    }

    public void UpdateEnemyUnitCountText(int enemyCount) {
        enemyUnitCountText.text = "Enemy Units:\n" + enemyCount;
    }

    public void UpdateMineralHarvesterBonusText(int mineralIncrement) {
        StartCoroutine(Utils.DisplayAndFadeOutText(GameEngine.GetInstance().gameDataPanel.mineralHarvesterBonusText, "+ " + mineralIncrement, 3.0f));
    }

    public void UpdateGasHarvesterBonusText(int gasIncrement) {
        StartCoroutine(Utils.DisplayAndFadeOutText(GameEngine.GetInstance().gameDataPanel.gasHarvesterBonusText, "+ " + gasIncrement, 3.0f));
    }
}
