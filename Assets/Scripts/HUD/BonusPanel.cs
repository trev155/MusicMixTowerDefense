using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : MonoBehaviour {
    public Text bTokens;
    public Text aTokens;
    public Text sTokens;
    public Transform sWall;

    private void Awake() {
        bTokens.text = "Tokens: 0";
        aTokens.text = "Tokens: 0";
        sTokens.text = "Tokens: 0";
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
        if (GameEngine.GetInstance().bChoosers < 1) {
            Debug.Log("Cannot purhcase unit. No B Choosers available.");
            return;
        }
        GameEngine.GetInstance().IncreaseGas(200);

        GameEngine.GetInstance().bChoosers -= 1;
        UpdateBTokenCount(GameEngine.GetInstance().bChoosers);
    }

    private void PurchaseBUnit(UnitClass unitClass) {
        if (GameEngine.GetInstance().bChoosers < 1) {
            Debug.Log("Cannot purhcase unit. No B Choosers available.");
            return;
        }
        GameEngine.GetInstance().unitSpawner.CreatePlayerUnit(PlayerUnitRank.B, unitClass);
        GameEngine.GetInstance().bChoosers -= 1;
        UpdateBTokenCount(GameEngine.GetInstance().bChoosers);
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
        if (GameEngine.GetInstance().aChoosers < 1) {
            Debug.Log("Cannot purhcase unit. No A Choosers available.");
            return;
        }
        GameEngine.GetInstance().IncreaseGas(350);

        GameEngine.GetInstance().aChoosers -= 1;
        UpdateATokenCount(GameEngine.GetInstance().aChoosers);
    }

    private void PurchaseAUnit(UnitClass unitClass) {
        if (GameEngine.GetInstance().aChoosers < 1) {
            Debug.Log("Cannot purhcase unit. No A Choosers available.");
            return;
        }
        GameEngine.GetInstance().unitSpawner.CreatePlayerUnit(PlayerUnitRank.A, unitClass);
        GameEngine.GetInstance().aChoosers -= 1;
        UpdateATokenCount(GameEngine.GetInstance().aChoosers);
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
        if (GameEngine.GetInstance().sChoosers < 1) {
            Debug.Log("Cannot purhcase unit. No S Choosers available.");
            return;
        }
        GameEngine.GetInstance().IncreaseGas(800);

        GameEngine.GetInstance().sChoosers -= 1;
        UpdateSTokenCount(GameEngine.GetInstance().sChoosers);
    }

    public void PurchaseWall_S() {
        if (GameEngine.GetInstance().sChoosers < 1) {
            Debug.Log("Cannot purhcase unit. No S Choosers available.");
            return;
        }
        if (GameEngine.GetInstance().hasWall) {
            Debug.Log("Already purchased wall.");
            return;
        }

        Debug.Log("Purchased Wall");
        sWall.gameObject.SetActive(true);
        GameEngine.GetInstance().hasWall = true;
        GameEngine.GetInstance().sChoosers -= 1;
        UpdateSTokenCount(GameEngine.GetInstance().sChoosers);
    }

    private void PurchaseSUnit(UnitClass unitClass) {
        if (GameEngine.GetInstance().sChoosers < 1) {
            Debug.Log("Cannot purhcase unit. No S Choosers available.");
            return;
        }
        GameEngine.GetInstance().unitSpawner.CreatePlayerUnit(PlayerUnitRank.S, unitClass);
        GameEngine.GetInstance().sChoosers -= 1;
        UpdateSTokenCount(GameEngine.GetInstance().sChoosers);
    }    
}
