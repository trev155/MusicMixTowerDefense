using UnityEngine;

public class ShopPanel : MonoBehaviour {
    // ---------- Fields ----------
    private System.Random random;

    // ---------- Methods ----------
    private void Awake() {
        random = new System.Random();
    }

    public void CreateRandomDUnit() {
        if (GameEngine.GetInstance().tokenCount == 0) {
            Debug.Log("Could not purchase a D unit. Requires 1 token.");
            return;
        }
        PlayerUnit p = GameEngine.GetInstance().unitSpawner.CreateRandomDUnit();
        GameEngine.GetInstance().DecreaseTokenCount(1);

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

        GameEngine.GetInstance().DecreaseTokenCount(1);
        GameEngine.GetInstance().IncreaseGas(gasIncrement);
        GameEngine.GetInstance().gameDataPanel.UpdateGasText(GameEngine.GetInstance().gas);

        Debug.Log("[+" + gasIncrement + " Gas]");
    }

    public void PurchasePiano() {
        if (GameEngine.GetInstance().hasPiano) {
            Debug.Log("You can only purchase the Piano once.");
            return;
        }
        if (GameEngine.GetInstance().tokenCount < 2) {
            Debug.Log("Cannot Purchase item: [Piano]. Requires (2) tokens.");
            return;
        }
        GameEngine.GetInstance().DecreaseTokenCount(2);
        GameEngine.GetInstance().hasPiano = true;

        Debug.Log("Purchased Piano");
    }

    public void PurchaseDrums() {
        if (GameEngine.GetInstance().hasDrum) {
            Debug.Log("You can only purchase the Drums once.");
            return;
        }
        if (GameEngine.GetInstance().tokenCount < 3) {
            Debug.Log("Cannot Purchase item: [Drums]. Requires (3) tokens.");
            return;
        }
        GameEngine.GetInstance().DecreaseTokenCount(3);
        GameEngine.GetInstance().hasDrum = true;

        Debug.Log("Purchased Drums");
    }

    public void PurchaseTokenLotto() {
        if (GameEngine.GetInstance().tokenCount < 1) {
            Debug.Log("Cannot Purchase item: [Lotto]. Requires (1) token.");
            return;
        }
        GameEngine.GetInstance().DecreaseTokenCount(1);

        int option = random.Next(1, 100);
        if (option <= 55) {
            Debug.Log("Lotto: No Luck - 0 Token");
        } else if (option <= 75) {
            Debug.Log("Lotto: 1 Token");
            GameEngine.GetInstance().IncreaseTokenCount(1);
        } else if (option <= 90) {
            Debug.Log("Lotto: 2 Tokens");
            GameEngine.GetInstance().IncreaseTokenCount(2);
        } else {
            Debug.Log("Lotto: 4 Tokens");
            GameEngine.GetInstance().IncreaseTokenCount(4);
        }
    }
}