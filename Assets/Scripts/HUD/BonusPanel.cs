using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : MonoBehaviour {
    public Text bTokens;
    public Text aTokens;
    public Text sTokens;
    public Transform sWall;

    private void Start() {
        bTokens.text = "Tokens: " + GameEngine.GetInstance().bChoosers;
        aTokens.text = "Tokens: " + GameEngine.GetInstance().aChoosers;
        sTokens.text = "Tokens: " + GameEngine.GetInstance().sChoosers;
    }

    // UI Methods
    public void UpdateBTokenCount(int tokens) {
        bTokens.text = "Tokens: " + tokens;
    }

    public void UpdateATokenCount(int tokens) {
        aTokens.text = "Tokens: " + tokens;
    }

    public void UpdateSTokenCount(int tokens) {
        sTokens.text = "Tokens: " + tokens;
    }

    // Button Handlers
    // B
    public void PurchaseInfantry_B() {
        PurchaseBUnit(UnitClass.INFANTRY);
    }

    public void PurchaseMech_B() {
        PurchaseBUnit(UnitClass.MECH);
    }

    public void PurchaseLaser_B() {
        PurchaseBUnit(UnitClass.LASER);
    }

    public void PurchasePsionic_B() {
        PurchaseBUnit(UnitClass.PSIONIC);
    }

    public void PurchaseAcid_B() {
        PurchaseBUnit(UnitClass.ACID);
    }

    public void PurchaseBlade_B() {
        PurchaseBUnit(UnitClass.BLADE);
    }

    public void PurchaseGas_B() {
        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
        if (GameEngine.GetInstance().bChoosers < 1) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot purchase. No B Choosers available", MessageType.NEGATIVE);
            return;
        }
        GameEngine.GetInstance().DecrementBBonusTokenCount();
        GameEngine.GetInstance().IncreaseGas(200);
        GameEngine.GetInstance().messageQueue.PushMessage("Purchased: 200 Gas", MessageType.POSITIVE);
    }

    private void PurchaseBUnit(UnitClass unitClass) {
        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
        if (GameEngine.GetInstance().bChoosers < 1) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot purhcase unit. No B Choosers available", MessageType.NEGATIVE);
            return;
        }

        GameEngine.GetInstance().DecrementBBonusTokenCount();
        GameEngine.GetInstance().unitSpawner.CreatePlayerUnit(PlayerUnitRank.B, unitClass);
    }

    // A
    public void PurchaseInfantry_A() {
        PurchaseAUnit(UnitClass.INFANTRY);
    }

    public void PurchaseMech_A() {
        PurchaseAUnit(UnitClass.MECH);
    }

    public void PurchaseLaser_A() {
        PurchaseAUnit(UnitClass.LASER);
    }

    public void PurchasePsionic_A() {
        PurchaseAUnit(UnitClass.PSIONIC);
    }

    public void PurchaseAcid_A() {
        PurchaseAUnit(UnitClass.ACID);
    }

    public void PurchaseBlade_A() {
        PurchaseAUnit(UnitClass.BLADE);
    }

    public void PurchaseGas_A() {
        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
        if (GameEngine.GetInstance().aChoosers < 1) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot purchase. No A Choosers available", MessageType.NEGATIVE);
            return;
        }
        GameEngine.GetInstance().DecrementABonusTokenCount();
        GameEngine.GetInstance().IncreaseGas(350);
        GameEngine.GetInstance().messageQueue.PushMessage("Purchased: 350 Gas", MessageType.POSITIVE);
    }

    private void PurchaseAUnit(UnitClass unitClass) {
        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
        if (GameEngine.GetInstance().aChoosers < 1) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot purhcase unit. No A Choosers available", MessageType.NEGATIVE);
            return;
        }
        GameEngine.GetInstance().DecrementABonusTokenCount();
        GameEngine.GetInstance().unitSpawner.CreatePlayerUnit(PlayerUnitRank.A, unitClass);
    }

    // S
    public void PurchaseInfantry_S() {
        PurchaseSUnit(UnitClass.INFANTRY);
    }

    public void PurchaseMech_S() {
        PurchaseSUnit(UnitClass.MECH);
    }

    public void PurchaseLaser_S() {
        PurchaseSUnit(UnitClass.LASER);
    }

    public void PurchasePsionic_S() {
        PurchaseSUnit(UnitClass.PSIONIC);
    }

    public void PurchaseAcid_S() {
        PurchaseSUnit(UnitClass.ACID);
    }

    public void PurchaseBlade_S() {
        PurchaseSUnit(UnitClass.BLADE);
    }

    public void PurchaseGas_S() {
        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
        if (GameEngine.GetInstance().sChoosers < 1) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot purchase. No S Choosers available", MessageType.NEGATIVE);
            return;
        }
        GameEngine.GetInstance().DecrementSBonusTokenCount();
        GameEngine.GetInstance().IncreaseGas(800);
        GameEngine.GetInstance().messageQueue.PushMessage("Purchased: 800 Gas", MessageType.POSITIVE);
    }

    public void PurchaseWall_S() {
        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
        if (GameEngine.GetInstance().sChoosers < 1) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot purchase. No S Choosers available", MessageType.NEGATIVE);
            return;
        }
        if (GameEngine.GetInstance().hasWall) {
            GameEngine.GetInstance().messageQueue.PushMessage("Already purchased Wall", MessageType.INFO);
            return;
        }

        sWall.gameObject.SetActive(true);
        GameEngine.GetInstance().hasWall = true;
        
        GameEngine.GetInstance().DecrementSBonusTokenCount();
        GameEngine.GetInstance().messageQueue.PushMessage("Purchased Wall", MessageType.POSITIVE);
    }

    private void PurchaseSUnit(UnitClass unitClass) {
        GameEngine.GetInstance().audioManager.PlaySound(AudioManager.BUTTON_CLICK_SOUND);
        if (GameEngine.GetInstance().sChoosers < 1) {
            GameEngine.GetInstance().messageQueue.PushMessage("Cannot purhcase unit. No S Choosers available", MessageType.NEGATIVE);
            return;
        }
        GameEngine.GetInstance().unitSpawner.CreatePlayerUnit(PlayerUnitRank.S, unitClass);
        GameEngine.GetInstance().DecrementSBonusTokenCount();
    }    
}
