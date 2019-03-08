using UnityEngine;

public class RevealHidePanel : MonoBehaviour {
    public Transform ShopPanel;
    public Transform UpgradePanel;
    public Transform AdminPanel;

    public void ToggleShowShopPanel() {
        UpgradePanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = ShopPanel.gameObject.activeSelf;
        ShopPanel.gameObject.SetActive(!isActive);
    }

    public void ToggleShowUpgradePanel() {
        ShopPanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = UpgradePanel.gameObject.activeSelf;
        UpgradePanel.gameObject.SetActive(!isActive);
    }

    public void ToggleShowAdminPanel() {
        UpgradePanel.gameObject.SetActive(false);
        AdminPanel.gameObject.SetActive(false);

        bool isActive = AdminPanel.gameObject.activeSelf;
        AdminPanel.gameObject.SetActive(!isActive);
    }
}
