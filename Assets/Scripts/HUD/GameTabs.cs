using UnityEngine;

public class GameTabs : MonoBehaviour {
    public Transform ShopPanel;
    public Transform UpgradePanel;
    public Transform HarvestersPanel;
    public Transform BonusPanel;
    public Transform AdminPanel;

    public void CloseAllGameTabs() {
        ShopPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        HarvestersPanel.gameObject.SetActive(false);
        BonusPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);
    }

    public void ToggleShowShopPanel() {
        GameEngine.GetInstance().infoTabs.CloseAllInfoTabs();
        UpgradePanel.gameObject.SetActive(false);
        HarvestersPanel.gameObject.SetActive(false);
        BonusPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = ShopPanel.gameObject.activeSelf;
        ShopPanel.gameObject.SetActive(!isActive);

        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
    }

    public void ToggleShowUpgradePanel() {
        GameEngine.GetInstance().infoTabs.CloseAllInfoTabs();
        ShopPanel.gameObject.SetActive(false);
        HarvestersPanel.gameObject.SetActive(false);
        BonusPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = UpgradePanel.gameObject.activeSelf;
        UpgradePanel.gameObject.SetActive(!isActive);

        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
    }

    public void ToggleShowHarvestersPanel() {
        GameEngine.GetInstance().infoTabs.CloseAllInfoTabs();
        ShopPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        BonusPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = HarvestersPanel.gameObject.activeSelf;
        HarvestersPanel.gameObject.SetActive(!isActive);

        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
    }

    public void ToggleShowBonusPanel() {
        GameEngine.GetInstance().infoTabs.CloseAllInfoTabs();
        ShopPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        HarvestersPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = BonusPanel.gameObject.activeSelf;
        BonusPanel.gameObject.SetActive(!isActive);

        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
    }

    public void ToggleShowAdminPanel() {
        GameEngine.GetInstance().infoTabs.CloseAllInfoTabs();
        ShopPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        HarvestersPanel.gameObject.SetActive(false);
        BonusPanel.gameObject.SetActive(false);

        bool isActive = AdminPanel.gameObject.activeSelf;
        AdminPanel.gameObject.SetActive(!isActive);

        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
    }
}
