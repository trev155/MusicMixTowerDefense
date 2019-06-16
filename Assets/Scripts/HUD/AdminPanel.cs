using UnityEngine;
using UnityEngine.UI;

/*
 * The AdminPanel is for debugging purposes.
 * Allows me to create any unit or enemy unit. Allows me to upgrade for free.
 */
public class AdminPanel : MonoBehaviour {
    private PlayerUnitRank selectedUnitRank;
    private UnitClass selectedUnitClass;
    private int enemyLevel = 1;
    private const int MAX_LEVELS = 40;
    
    public Text selectionText;
    public Text enemyLevelText;

    private void Awake() {
        selectedUnitRank = PlayerUnitRank.D;
        selectedUnitClass = UnitClass.INFANTRY;
    }

    // Creating Player Units
    public void CreatePlayerUnit() {
        if (selectedUnitClass == UnitClass.MAGIC || selectedUnitClass == UnitClass.FLAME) {
            if (selectedUnitRank == PlayerUnitRank.D || selectedUnitRank == PlayerUnitRank.C) {
                Debug.Log("Invalid combination for unit class and rank.");
                return;
            }
        }

        GameEngine.GetInstance().unitSpawner.CreatePlayerUnit(selectedUnitRank, selectedUnitClass);
    }

    public void RankSelectionButton(string rankStr) {
        selectedUnitRank = ParsePlayerUnitRankString(rankStr);
        UpdateSelectionText();
    }

    private PlayerUnitRank ParsePlayerUnitRankString(string s) {
        switch (s) {
            case "D":
                return PlayerUnitRank.D;
            case "C":
                return PlayerUnitRank.C;
            case "B":
                return PlayerUnitRank.B;
            case "A":
                return PlayerUnitRank.A;
            case "S":
                return PlayerUnitRank.S;
            case "X":
                return PlayerUnitRank.X;
            default:
                Debug.Log("Unrecognized rank string value: " + s + ". Returning PlayerUnitRank.D");
                return PlayerUnitRank.D;
        }
    }

    public void UnitClassSelectionButton(string unitClassStr) {
        selectedUnitClass = ParseUnitClassString(unitClassStr);
        UpdateSelectionText();
    }

    private UnitClass ParseUnitClassString(string s) {
        switch (s) {
            case "Infantry":
                return UnitClass.INFANTRY;
            case "Mech":
                return UnitClass.MECH;
            case "Laser":
                return UnitClass.LASER;
            case "Psionic":
                return UnitClass.PSIONIC;
            case "Acid":
                return UnitClass.ACID;
            case "Blade":
                return UnitClass.BLADE;
            case "Magic":
                return UnitClass.MAGIC;
            case "Flame":
                return UnitClass.FLAME;
            default:
                Debug.Log("Unrecognized unit class string value: " + s + ". Returning UnitClass.INFANTRY");
                return UnitClass.INFANTRY;
        }
    }

    private void UpdateSelectionText() {
        selectionText.text = selectedUnitRank.ToString() + " " + selectedUnitClass.ToString();
    }

    // Creating enemy units
    public void CreateEnemyUnit() {
        GameEngine.GetInstance().unitSpawner.CreateEnemyUnit(enemyLevel);
    }

    public void ScrollLevelLeftOne() {    
        enemyLevel -= 1;
        if (enemyLevel < 1) {
            enemyLevel = 1;
        }
        UpdateEnemyLevelText();
    }

    public void ScrollLevelLeftFive() {
        enemyLevel -= 5;
        if (enemyLevel < 1) {
            enemyLevel = 1;
        }
        UpdateEnemyLevelText();
    }

    public void ScrollLevelRightOne() {
        enemyLevel += 1;
        if (enemyLevel > MAX_LEVELS) {
            enemyLevel = MAX_LEVELS;
        }
        UpdateEnemyLevelText();
    }

    public void ScrollLevelRightFive() {
        enemyLevel += 5;
        if (enemyLevel > MAX_LEVELS) {
            enemyLevel = MAX_LEVELS;
        }
        UpdateEnemyLevelText();
    }

    private void UpdateEnemyLevelText() {
        enemyLevelText.text = "Level: " + enemyLevel;
    }

    // Creating special enemy units
    public void CreateSpecialEnemyUnit(string type) {
        switch (type) {
            case "Bounty":
                GameEngine.GetInstance().unitSpawner.CreateBounty();
                break;
            default:
                throw new GameplayException("Unrecognized value for type: " + type);
        }
    }

    // Other
    public void AddFiveTokens() {
        GameEngine.GetInstance().IncreaseTokenCount(5);
    }

}
