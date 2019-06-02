using UnityEngine;

public class MenuPanel : MonoBehaviour {
    public MenuControlPanel menuControlPanel;

    public void MenuButton() {
        bool isActive = menuControlPanel.gameObject.activeSelf;
        menuControlPanel.gameObject.SetActive(!isActive);

        GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.BUTTON_CLICK_SOUND);
    }
}
