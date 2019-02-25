using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class AdminPanel : MonoBehaviour {
    private Dictionary<PlayerUnitRank, int> rankToNumberMappings = new Dictionary<PlayerUnitRank, int>();
    private int curNum = 0;
    private PlayerUnitRank curRank;
    private int enemyLevel = 1;
    private const int MAX_LEVELS = 40;
    private int nextLevel = 1;
    private int upgradeNum = 0;

    public Text rankText;
    public Text numberText;
    public Text enemyLevelText;
    public Text nextLevelText;
    public Text upgradeText;
    

    private void Awake() {
        rankToNumberMappings.Add(PlayerUnitRank.D, 6);
        rankToNumberMappings.Add(PlayerUnitRank.C, 6);
        rankToNumberMappings.Add(PlayerUnitRank.B, 8);
        rankToNumberMappings.Add(PlayerUnitRank.A, 8);
        rankToNumberMappings.Add(PlayerUnitRank.S, 8);
        rankToNumberMappings.Add(PlayerUnitRank.X, 8);

        rankText.text = curRank.ToString();
        numberText.text = curNum + "";
        enemyLevelText.text = enemyLevel + "";
        nextLevelText.text = nextLevel + "";
        upgradeText.text = GetUpgradeText();
    }

    // Creating Player Units
    public void CreatePlayerUnit() {
        GameEngine.Instance.unitSpawner.CreatePlayerUnit(curRank, curNum);
    }

    public void ScrollRankLeft() {
        if (curRank == PlayerUnitRank.C) {
            curRank = PlayerUnitRank.D;
        } else if (curRank == PlayerUnitRank.B) {
            curRank = PlayerUnitRank.C;
        } else if (curRank == PlayerUnitRank.A) {
            curRank = PlayerUnitRank.B;
        } else if (curRank == PlayerUnitRank.S) {
            curRank = PlayerUnitRank.A;
        } else if (curRank == PlayerUnitRank.X) {
            curRank = PlayerUnitRank.S;
        }

        rankText.text = curRank.ToString();
    }

    public void ScrollRankRight() {
        if (curRank == PlayerUnitRank.D) {
            curRank = PlayerUnitRank.C;
        } else if (curRank == PlayerUnitRank.C) {
            curRank = PlayerUnitRank.B;
        } else if (curRank == PlayerUnitRank.B) {
            curRank = PlayerUnitRank.A;
        } else if (curRank == PlayerUnitRank.A) {
            curRank = PlayerUnitRank.S;
        } else if (curRank == PlayerUnitRank.S) {
            curRank = PlayerUnitRank.X;
        }

        rankText.text = curRank.ToString();
    }

    public void ScrollNumLeft() {
        if (curNum > 0) {
            curNum -= 1;
        }

        numberText.text = curNum + "";
    }

    public void ScrollNumRight() {
        if (curNum < rankToNumberMappings[curRank] - 1) {
            curNum += 1;
        }

        numberText.text = curNum + "";
    }

    // Creating enemy units
    public void CreateEnemyUnit() {
        GameEngine.Instance.unitSpawner.CreateEnemyUnit(enemyLevel);
    }

    public void ScrollLevelLeft() {
        if (enemyLevel > 1) {
            enemyLevel -= 1;
        }

        enemyLevelText.text = enemyLevel + "";
    }

    public void ScrollLevelRight() {
        if (enemyLevel < MAX_LEVELS) {
            enemyLevel += 1;
        }

        enemyLevelText.text = enemyLevel + "";
    }

    // Levels
    public void ActivateNextLevel() {
        GameEngine.Instance.levelManager.StartLevel(nextLevel);
    }

    public void ScrollNextLevelLeft() {
        if (nextLevel > 1) {
            nextLevel -= 1;
        }

        nextLevelText.text = nextLevel + "";
    }

    public void ScrollNextLevelRight() {
        if (nextLevel < MAX_LEVELS) {
            nextLevel += 1;
        }

        nextLevelText.text = nextLevel + "";
    }

    // Upgrades
    public void ActivateUpgrade() {
        if (upgradeNum == 0) {
            GameEngine.Instance.upgradeManager.IncrementUpgradeClass(UnitClass.INFANTRY);
        } else if (upgradeNum == 1) {
            GameEngine.Instance.upgradeManager.IncrementUpgradeClass(UnitClass.MECH);
        } else if (upgradeNum == 2) {
            GameEngine.Instance.upgradeManager.IncrementUpgradeClass(UnitClass.LASER);
        } else if (upgradeNum == 3) {
            GameEngine.Instance.upgradeManager.IncrementUpgradeClass(UnitClass.PSIONIC);
        } else if (upgradeNum == 4) {
            GameEngine.Instance.upgradeManager.IncrementUpgradeClass(UnitClass.ACID);
        } else if (upgradeNum == 5) {
            GameEngine.Instance.upgradeManager.IncrementUpgradeClass(UnitClass.BLADE);
        } else if (upgradeNum == 6) {
            GameEngine.Instance.upgradeManager.IncrementUpgradeClass(UnitClass.MAGIC);
        } else if (upgradeNum == 7) {
            GameEngine.Instance.upgradeManager.IncrementUpgradeClass(UnitClass.FLAME);
        } else {
            Debug.Log("Invalid upgrade number value. Did not upgrade.");
        }
    }

    public void ScrollUpgradeLeft() {
        if (upgradeNum > 0) {
            upgradeNum -= 1;
        }

        upgradeText.text = GetUpgradeText();
    }

    public void ScrollUpgradeRight() {
        if (upgradeNum < 7) {
            upgradeNum += 1;
        }

        upgradeText.text = GetUpgradeText();
    }

    private string GetUpgradeText() {
        if (upgradeNum == 0) {
            return "Infantry";
        } else if (upgradeNum == 1) {
            return "Mech";
        } else if (upgradeNum == 2) {
            return "Laser";
        } else if (upgradeNum == 3) {
            return "Psionic";
        } else if (upgradeNum == 4) {
            return "Acid";
        } else if (upgradeNum == 5) {
            return "Blade";
        } else if (upgradeNum == 6) {
            return "Magic";
        } else if (upgradeNum == 7) {
            return "Flame";
        } else {
            return "";
        }
    }
}
