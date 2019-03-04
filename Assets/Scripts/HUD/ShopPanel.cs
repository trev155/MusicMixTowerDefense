using UnityEngine;
using UnityEngine.UI;


public class ShopPanel : MonoBehaviour {
    // ---------- Fields ----------
    public Text shopItemText;

    private int shopItemOption;


    // ---------- Methods ----------
    private void Awake() {
        shopItemText.text = "Piano (2 Token)";
        shopItemOption = 0;
    }

    public void CreateRandomDUnit() {
        if (GameEngine.Instance.tokenCount == 0) {
            Debug.Log("Could not purchase a D unit. Requires 1 token.");
            return;
        }

        GameEngine.Instance.unitSpawner.CreateRandomDUnit();
        GameEngine.Instance.tokenCount--;
        GameEngine.Instance.gameDataPanel.UpdateTokenCount(GameEngine.Instance.tokenCount);
    }

    public void ScrollItemLeft() {
        if (shopItemOption > 0) {
            shopItemOption--;
        }
        shopItemText.text = GetShopItemText(shopItemOption);
    }

    public void ScrollItemRight() {
        if (shopItemOption < 3) {
            shopItemOption++;
        }
        shopItemText.text = GetShopItemText(shopItemOption);
    }

    public void PurchaseItem() {
        // TODO 
        // implement shop items
        // deduct token count
        // detect if already purchased
    }

    private string GetShopItemText(int option) {
        switch (option) {
            case 0:
                return "Piano (2 Token)";
            case 1:
                return "Drum (3 Token)";
            case 2:
                return "Lotto (1 Token)";
            case 3:
                return "Harvester (5 Token)";
            default:
                throw new GameplayException("Invalid Option: " + option + ". Could not get option text.");
        }
    }
}