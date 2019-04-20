using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {
    public Text statsText;

    public void EndGame() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SetEndGameStats() {
        statsText.text = "Stats:\n" + "Kills: " + GameEngine.GetInstance().kills + "\n" + "Level: " + GameEngine.GetInstance().levelManager.currentLevel;
    }
}