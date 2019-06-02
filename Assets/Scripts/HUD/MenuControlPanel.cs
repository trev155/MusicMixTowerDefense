using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControlPanel : MonoBehaviour {
    public RectTransform ExitGameConfirmationPanel;

    public void ExitGameButton() {
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
        ExitGameConfirmationPanel.gameObject.SetActive(true);
    }

    public void ConfirmExitGame() {
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
        ExitGameConfirmationPanel.gameObject.SetActive(false);
        StartCoroutine(LoadMainMenuScene());
    }

    public void DenyExitGame() {
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
        ExitGameConfirmationPanel.gameObject.SetActive(false);
    }

    private IEnumerator LoadMainMenuScene() {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("MainMenu");
    }
}
