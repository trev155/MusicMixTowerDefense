using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControlPanel : MonoBehaviour {
    public RectTransform ExitGameConfirmationPanel;
    public RectTransform ExitGameOverlay;
    public RectTransform PausedGameOverlay;

    public Slider backgroundMusicSlider;
    public Slider soundEffectsMusicSlider;

    // Exit Game
    public void ExitGameButton() {
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
        ExitGameConfirmationPanel.gameObject.SetActive(true);
        ExitGameOverlay.gameObject.SetActive(true);
    }

    public void ConfirmExitGame() {
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
        ExitGameConfirmationPanel.gameObject.SetActive(false);
        StartCoroutine(LoadMainMenuScene());
    }

    public void DenyExitGame() {
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
        ExitGameConfirmationPanel.gameObject.SetActive(false);
        ExitGameOverlay.gameObject.SetActive(false);
    }

    private IEnumerator LoadMainMenuScene() {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("MainMenu");
    }

    // Pause Game
    public void PauseGameButton() {
        Time.timeScale = 1 - Time.timeScale;
        bool isPaused = Time.timeScale == 0;
        PausedGameOverlay.gameObject.SetActive(isPaused);
    }

    // Volume Sliders
    public void BackgroundMusicSliderChange() {
        GameEngine.GetInstance().audioManager.SetBGMAudioLevel(backgroundMusicSlider.value);
    }

    public void SoundEffectsSliderChange() {
        GameEngine.GetInstance().audioManager.SetSoundEffectsVolumeLevel(soundEffectsMusicSlider.value);
    }
}
