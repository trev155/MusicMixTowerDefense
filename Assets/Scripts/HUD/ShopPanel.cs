using UnityEngine;
using UnityEngine.UI;


public class ShopPanel : MonoBehaviour {
    // ---------- Fields ----------
    public Text shopItemText;

    private int shopItemOption;

    private System.Random random;

    // ---------- Methods ----------
    private void Awake() {
        random = new System.Random();

        shopItemText.text = "Piano (2 Token)";
        shopItemOption = 0;
    }

    public void CreateRandomDUnit() {
        if (GameEngine.GetInstance().tokenCount == 0) {
            Debug.Log("Could not purchase a D unit. Requires 1 token.");
            return;
        }

        PlayerUnit p = GameEngine.GetInstance().unitSpawner.CreateRandomDUnit();
        GameEngine.GetInstance().tokenCount--;
        GameEngine.GetInstance().gameDataPanel.UpdateTokenCountText(GameEngine.GetInstance().tokenCount);

        Debug.Log("[D Unit - " + p.displayName + "]");
    }

    public void PurchaseGas() {
        if (GameEngine.GetInstance().tokenCount == 0) {
            Debug.Log("Could not purchase gas. Requires 1 token.");
            return;
        }

        int gasIncrement;
        int option = random.Next(1, 10);
        if (option <= 4) {
            gasIncrement = 40;
        } else if (option <= 7) {
            gasIncrement = 60;
        } else if (option <= 9) {
            gasIncrement = 80;
        } else {
            gasIncrement = 100;
        }
        
        GameEngine.GetInstance().tokenCount--;
        GameEngine.GetInstance().gameDataPanel.UpdateTokenCountText(GameEngine.GetInstance().tokenCount);

        GameEngine.GetInstance().IncreaseGas(gasIncrement);
        GameEngine.GetInstance().gameDataPanel.UpdateGasText(GameEngine.GetInstance().gas);

        Debug.Log("[+" + gasIncrement + " Gas]");
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
        switch (this.shopItemOption) {
            case 0:
                PurchasePiano();
                break;
            case 1:
                PurchaseDrums();
                break;
            case 2:
                PurchaseTokenLotto();
                break;
            case 3:
                break;
            default:
                throw new GameplayException("Invalid shop item option. Could not purchase item.");
        }
    }

    private void PurchasePiano() {
        if (GameEngine.GetInstance().hasPiano) {
            Debug.Log("You can only purchase the Piano once.");
            return;
        }
        if (GameEngine.GetInstance().tokenCount < 2) {
            Debug.Log("Cannot Purchase item: [Piano]. Requires (2) tokens.");
            return;
        }
        GameEngine.GetInstance().tokenCount -= 2;
        GameEngine.GetInstance().hasPiano = true;

        GameEngine.GetInstance().gameDataPanel.UpdateTokenCountText(GameEngine.GetInstance().tokenCount);
    }

    private void PurchaseDrums() {
        if (GameEngine.GetInstance().hasDrum) {
            Debug.Log("You can only purchase the Drums once.");
            return;
        }
        if (GameEngine.GetInstance().tokenCount < 3) {
            Debug.Log("Cannot Purchase item: [Drums]. Requires (3) tokens.");
            return;
        }
        GameEngine.GetInstance().tokenCount -= 3;
        GameEngine.GetInstance().hasDrum = true;

        GameEngine.GetInstance().gameDataPanel.UpdateTokenCountText(GameEngine.GetInstance().tokenCount);
    }

    private void PurchaseTokenLotto() {
        if (GameEngine.GetInstance().tokenCount < 1) {
            Debug.Log("Cannot Purchase item: [Lotto]. Requires (1) token.");
            return;
        }
        GameEngine.GetInstance().tokenCount -= 1;

        int option = random.Next(1, 100);
        if (option <= 55) {
            Debug.Log("Lotto: No Luck - 0 Token");
        } else if (option <= 75) {
            Debug.Log("Lotto: 1 Token");
            GameEngine.GetInstance().tokenCount += 1;
        } else if (option <= 90) {
            Debug.Log("Lotto: 2 Tokens");
            GameEngine.GetInstance().tokenCount += 2;
        } else {
            Debug.Log("Lotto: 4 Tokens");
            GameEngine.GetInstance().tokenCount += 4;
        }

        GameEngine.GetInstance().gameDataPanel.UpdateTokenCountText(GameEngine.GetInstance().tokenCount);
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