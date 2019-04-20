using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    // ---------- Fields -----------
    public Text difficultyText;
    public Text gameModeDescriptionText;
    public int selectedGameModeIndex;

    // ---------- Methods ----------
    private void Awake() {
        selectedGameModeIndex = 1;

        UpdateDifficultyText();
        UpdateGameModeDescriptionText();
    }

    public void PlayButtonPressed() {
        SceneDataTransfer.CurrentGameMode = IndexToGameMode(selectedGameModeIndex);
        SceneManager.LoadScene("MainGame");
    }

    public void ScrollGameModeLeft() {
        if (selectedGameModeIndex > 0) {
            selectedGameModeIndex--;
        }
        UpdateDifficultyText();
        UpdateGameModeDescriptionText();
    }

    public void ScrollGameModeRight() {
        if (selectedGameModeIndex < 4) {
            selectedGameModeIndex++;
        }
        UpdateDifficultyText();
        UpdateGameModeDescriptionText();
    }

    private void UpdateDifficultyText() {
        switch (selectedGameModeIndex) {
            case 0:
                difficultyText.text = "Easy";
                break;
            case 1:
                difficultyText.text = "Normal";
                break;
            case 2:
                difficultyText.text = "Hard";
                break;
            case 3:
                difficultyText.text = "Nonstop";
                break;
            case 4:
                difficultyText.text = "Pro";
                break;
            default:
                throw new GameplayException("Unsupported index value: " + selectedGameModeIndex);
        }
    }

    private void UpdateGameModeDescriptionText() {
        switch (selectedGameModeIndex) {
            case 0:
                gameModeDescriptionText.text = "Enemy Health: 60%, Time Between Levels = 20 seconds";
                break;
            case 1:
                gameModeDescriptionText.text = "Enemy Health: 80%, Time Between Levels = 12 seconds";
                break;
            case 2:
                gameModeDescriptionText.text = "Enemy Health: 100%, Time Between Levels = 8 seconds";
                break;
            case 3:
                gameModeDescriptionText.text = "Enemy Health: 100%, Time Between Levels = 4 seconds";
                break;
            case 4:
                gameModeDescriptionText.text = "Enemy Health: 100%, Time Between Levels = 0 seconds";
                break;
            default:
                throw new GameplayException("Unsupported index value: " + selectedGameModeIndex);
        }
    }

    private GameMode IndexToGameMode(int gameModeIndex) {
        switch (gameModeIndex) {
            case 0:
                return GameMode.EASY;
            case 1:
                return GameMode.NORMAL;
            case 2:
                return GameMode.HARD;
            case 3:
                return GameMode.NONSTOP;
            case 4:
                return GameMode.PRO;
            default:
                throw new GameplayException("Unsupported index value: " + gameModeIndex);
        }
    }
}
