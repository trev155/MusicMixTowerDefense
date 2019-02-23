using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class AdminPanel : MonoBehaviour {
    private Dictionary<PlayerUnitRank, int> rankToNumberMappings = new Dictionary<PlayerUnitRank, int>();
    private int curNum = 0;
    private PlayerUnitRank curRank;
    private int enemyLevel = 1;

    public Text rankText;
    public Text numberText;


    private void Awake() {
        rankToNumberMappings.Add(PlayerUnitRank.D, 6);
        rankToNumberMappings.Add(PlayerUnitRank.C, 6);
        rankToNumberMappings.Add(PlayerUnitRank.B, 8);
        rankToNumberMappings.Add(PlayerUnitRank.A, 8);
        rankToNumberMappings.Add(PlayerUnitRank.S, 8);
        rankToNumberMappings.Add(PlayerUnitRank.X, 8);

        rankText.text = curRank.ToString();
        numberText.text = curNum + "";
    }

    public void CreatePlayerUnit() {
        GameEngine.Instance.unitSpawner.CreatePlayerUnit(curRank, curNum);
    }

    public void CreateEnemyUnit() {
        GameEngine.Instance.unitSpawner.CreateEnemyUnit(enemyLevel);
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
}
