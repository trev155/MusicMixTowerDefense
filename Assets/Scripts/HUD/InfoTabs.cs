using UnityEngine;

public class InfoTabs : MonoBehaviour {
    public Transform GuidancePanel;
    public Transform AchievementsPanel;

    public void CloseAllInfoTabs() {
        GuidancePanel.gameObject.SetActive(false);
        AchievementsPanel.gameObject.SetActive(false);
    }

    public void ToggleGuidancePanel() {
        GameEngine.GetInstance().gameTabs.CloseAllGameTabs();
        AchievementsPanel.gameObject.SetActive(false);

        bool isActive = GuidancePanel.gameObject.activeSelf;
        GuidancePanel.gameObject.SetActive(!isActive);

        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
    }

    public void ToggleAchievementsPanel() {
        GameEngine.GetInstance().gameTabs.CloseAllGameTabs();
        GuidancePanel.gameObject.SetActive(false);

        bool isActive = AchievementsPanel.gameObject.activeSelf;
        AchievementsPanel.gameObject.SetActive(!isActive);

        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
    }
}
