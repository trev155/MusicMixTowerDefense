using UnityEngine;

public class InfoPanel : MonoBehaviour {
    public Transform GuidancePanel;
    public Transform AchievementsPanel;

    public void ToggleGuidancePanel() {
        AchievementsPanel.gameObject.SetActive(false);

        bool isActive = GuidancePanel.gameObject.activeSelf;
        GuidancePanel.gameObject.SetActive(!isActive);
    }

    public void ToggleAchievementsPanel() {
        GuidancePanel.gameObject.SetActive(false);

        bool isActive = AchievementsPanel.gameObject.activeSelf;
        AchievementsPanel.gameObject.SetActive(!isActive);
    }
}
