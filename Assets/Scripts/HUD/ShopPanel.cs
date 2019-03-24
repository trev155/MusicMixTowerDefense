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
            GameEngine.GetInstance().messageQueue.PushMessage("Could not purchase a D unit. Requires 1 token.");            
            return;
        }
        PlayerUnit p = GameEngine.GetInstance().unitSpawner.CreateRandomDUnit();
        GameEngine.GetInstance().DecreaseTokenCount(1);
    }

    public void PurchaseGas() {
        if (GameEngine.GetInstance().tokenCount == 0) {
            GameEngine.GetInstance().messageQueue.PushMessage("Could not purchase gas. Requires 1 token.");
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

        GameEngine.GetInstance().messageQueue.PushMessage("[+" + gasIncrement + " Gas]");
    }

    public void PurchaseHarvester() {
        if (GameEngine.GetInstance().tokenCount < 5) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot Purchase item: [Harvester]. Requires (5) token.");
            return;
        }

        GameEngine.GetInstance().DecreaseTokenCount(5);
        GameEngine.GetInstance().AddHarvester();

        GameEngine.GetInstance().messageQueue.PushMessage("Purchased Harvester");
    }

    public void PurchasePiano() {
        if (GameEngine.GetInstance().hasPiano) {
            GameEngine.GetInstance().messageQueue.PushMessage("You can only purchase the Piano once.");
            return;
        }
        if (GameEngine.GetInstance().tokenCount < 2) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot Purchase item: [Piano]. Requires (2) tokens.");
            return;
        }
        GameEngine.GetInstance().DecreaseTokenCount(2);
        GameEngine.GetInstance().hasPiano = true;

        GameEngine.GetInstance().messageQueue.PushMessage("Purchased Piano");
    }

    public void PurchaseDrums() {
        if (GameEngine.GetInstance().hasDrum) {
            GameEngine.GetInstance().messageQueue.PushMessage("You can only purchase the Drums once.");
            return;
        }
        if (GameEngine.GetInstance().tokenCount < 3) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot Purchase item: [Drums]. Requires (3) tokens.");
            return;
        }
        GameEngine.GetInstance().DecreaseTokenCount(3);
        GameEngine.GetInstance().hasDrum = true;

        GameEngine.GetInstance().messageQueue.PushMessage("Purchased Drums");
    }

    public void PurchaseTokenLotto() {
        if (GameEngine.GetInstance().tokenCount < 1) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot Purchase item: [Lotto]. Requires (1) token.");
            return;
        }
        GameEngine.GetInstance().DecreaseTokenCount(1);

        int option = random.Next(1, 100);
        if (option <= 55) {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: No Luck - 0 Token");
        } else if (option <= 75) {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: 1 Token");
            GameEngine.GetInstance().IncreaseTokenCount(1);
        } else if (option <= 90) {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: 2 Tokens");
            GameEngine.GetInstance().IncreaseTokenCount(2);
        } else {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: 4 Tokens");
            GameEngine.GetInstance().IncreaseTokenCount(4);
        }
    }

    public void PurchaseGasLotto() {
        if (GameEngine.GetInstance().gas < 15) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot Purchase item: [Gas Lotto]. Requires (15 gas)");
            return;
        }
        GameEngine.GetInstance().DecreaseGas(15);

        int option = random.Next(1, 100);
        if (option <= 60) {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: No unit. Unlucky.");
        } else if (option <= 80) {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: Random D Unit");
            GameEngine.GetInstance().unitSpawner.CreateRandomDUnit();
        } else if (option <= 93) {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: Random C Unit");
            GameEngine.GetInstance().unitSpawner.CreateRandomCUnit();
        } else if (option <= 97) {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: Random B Unit");
            GameEngine.GetInstance().unitSpawner.CreateRandomBUnit();
        } else if (option <= 99) {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: Random A Unit");
            GameEngine.GetInstance().unitSpawner.CreateRandomAUnit();
        } else {
            GameEngine.GetInstance().messageQueue.PushMessage("Lotto: Random S Unit");
            GameEngine.GetInstance().unitSpawner.CreateRandomSUnit();
        }
    }
}