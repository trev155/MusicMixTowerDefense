using UnityEngine;

public class GuidancePanel : MonoBehaviour {
    public Transform firstPage;
    public Transform secondPage;

    private int currentPage;

    //---------- Pages and Navigation ----------
    private void Awake() {
        this.currentPage = 1;
        ShowHidePages();
    }

    public void ScrollPageLeft() {
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
        if (currentPage == 1) {
            return;
        }
        currentPage -= 1;
        ShowHidePages();
    }

    public void ScrollPageRight() {
        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
        if (currentPage == 2) {
            return;
        }
        currentPage += 1;
        ShowHidePages();
    }

    private void ShowHidePages() {
        if (currentPage == 1) {
            ToggleFirstPage(true);
            ToggleSecondPage(false);
        } else if (currentPage == 2) {
            ToggleFirstPage(false);
            ToggleSecondPage(true);
        }
    }

    private void ToggleFirstPage(bool state) {
        firstPage.gameObject.SetActive(state);
    }

    private void ToggleSecondPage(bool state) {
        secondPage.gameObject.SetActive(state);
    }
}
