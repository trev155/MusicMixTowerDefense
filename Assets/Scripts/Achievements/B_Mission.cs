﻿using UnityEngine;

public class B_Mission : Achievement {
    public B_Mission() : base() {
        this.achievementName = "B Mission";
    }

    public override bool CheckCondition() {
        bool hasInfantry = false;
        bool hasMech = false;
        bool hasLaser = false;
        bool hasPsionic = false;
        bool hasAcid = false;
        bool hasBlade = false;
        bool hasMagic = false;
        bool hasFlame = false;

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject g in playerUnits) {
            PlayerUnit playerUnit = g.GetComponent<PlayerUnit>();

            if (playerUnit.rank != PlayerUnitRank.B) {
                continue;
            }

            UnitClass unitClass = playerUnit.unitClass;
            if (unitClass == UnitClass.INFANTRY) {
                hasInfantry = true;
            } else if (unitClass == UnitClass.MECH) {
                hasMech = true;
            } else if (unitClass == UnitClass.LASER) {
                hasLaser = true;
            } else if (unitClass == UnitClass.PSIONIC) {
                hasPsionic = true;
            } else if (unitClass == UnitClass.ACID) {
                hasAcid = true;
            } else if (unitClass == UnitClass.BLADE) {
                hasBlade = true;
            } else if (unitClass == UnitClass.MAGIC) {
                hasMagic = true;
            } else if (unitClass == UnitClass.FLAME) {
                hasFlame = true;
            }
        }
        
        int uniqueBTypes = (hasInfantry ? 1 : 0) + (hasMech ? 1 : 0) + (hasLaser ? 1 : 0) + (hasPsionic ? 1 : 0) 
            + (hasAcid ? 1 : 0) + (hasBlade ? 1 : 0) + (hasMagic ? 1 : 0) + (hasFlame ? 1 : 0);
        return uniqueBTypes >= 7;
    }

    public override void GiveReward() {
        GameEngine.GetInstance().messageQueue.PushMessage("Bonus: 1 A Rank Token", MessageType.POSITIVE);
        GameEngine.GetInstance().messageQueue.PushMessage(this.achievementName + " Complete", MessageType.POSITIVE);
        GameEngine.GetInstance().AddABonusTokens(1);
    }
}
