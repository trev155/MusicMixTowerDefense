using UnityEngine;

public class InfoPanel : MonoBehaviour {
    public Transform AchievementsPanel;

    public void ToggleAchievementsPanel() {
        bool isActive = AchievementsPanel.gameObject.activeSelf;
        AchievementsPanel.gameObject.SetActive(!isActive);
    }
}
