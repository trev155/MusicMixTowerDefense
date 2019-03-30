﻿using UnityEngine;

public class InfoPanel : MonoBehaviour {
    public Transform AchievementsPanel;
    public Transform GuidancePanel;

    public void ToggleAchievementsPanel() {
        GuidancePanel.gameObject.SetActive(false);

        bool isActive = AchievementsPanel.gameObject.activeSelf;
        AchievementsPanel.gameObject.SetActive(!isActive);
    }

    public void ToggleGuidancePanel() {
        AchievementsPanel.gameObject.SetActive(false);

        bool isActive = GuidancePanel.gameObject.activeSelf;
        GuidancePanel.gameObject.SetActive(!isActive);
    }
}
