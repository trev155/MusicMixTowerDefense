using UnityEngine;

public class RevealHidePanel : MonoBehaviour {
    public Transform ShopPanel;
    public Transform UpgradePanel;
    public Transform HarvestersPanel;
    public Transform BonusPanel;
    public Transform AdminPanel;

    public void ToggleShowShopPanel() {
        UpgradePanel.gameObject.SetActive(false);
        HarvestersPanel.gameObject.SetActive(false);
        BonusPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = ShopPanel.gameObject.activeSelf;
        ShopPanel.gameObject.SetActive(!isActive);
    }

    public void ToggleShowUpgradePanel() {
        ShopPanel.gameObject.SetActive(false);
        HarvestersPanel.gameObject.SetActive(false);
        BonusPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = UpgradePanel.gameObject.activeSelf;
        UpgradePanel.gameObject.SetActive(!isActive);
    }

    public void ToggleShowHarvestersPanel() {
        ShopPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        BonusPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = HarvestersPanel.gameObject.activeSelf;
        HarvestersPanel.gameObject.SetActive(!isActive);
    }

    public void ToggleShowBonusPanel() {
        ShopPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        HarvestersPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = BonusPanel.gameObject.activeSelf;
        BonusPanel.gameObject.SetActive(!isActive);
    }

    public void ToggleShowAdminPanel() {
        ShopPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        HarvestersPanel.gameObject.SetActive(false);
        BonusPanel.gameObject.SetActive(false);

        bool isActive = AdminPanel.gameObject.activeSelf;
        AdminPanel.gameObject.SetActive(!isActive);
    }
}
