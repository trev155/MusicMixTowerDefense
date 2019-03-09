﻿using UnityEngine;
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