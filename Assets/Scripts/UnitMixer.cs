using UnityEngine;
using System.Collections.Generic;

public class UnitMixer : MonoBehaviour {
    public List<PlayerUnit> unitsOnMixer = new List<PlayerUnit>();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "PlayerUnit") {
            return;
        }
        PlayerUnit playerUnit = collision.GetComponent<PlayerUnit>();
        unitsOnMixer.Add(playerUnit);
        
        // 2 of same unit
        if (CheckMatchingUnit(playerUnit)) {
            return;
        }

        // BCD -> A
        if (CheckBCDCombo(playerUnit)) {
            return;
        }

        // 6 D + 80 Gas -> A
        if (CheckAllDCombo(playerUnit)) {
            return;
        }

        // 6 C + 300 Gas -> S
        if (CheckAllCCombo(playerUnit)) {
            return;
        }

        // BAS -> X (one only)
        if (CheckXCombo(playerUnit)) {
            return;
        }

        // B Magic + B Flame -> 2 B choosers
        if (CheckRareBCombo(playerUnit)) {
            return;
        }

        // A Magic + A Flame -> 2 A choosers
        if (CheckRareACombo(playerUnit)) {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag != "PlayerUnit") {
            return;
        }
        PlayerUnit playerUnit = collision.GetComponent<PlayerUnit>();
        unitsOnMixer.Remove(playerUnit);
    }

    public void CheckGasCombos() {
        if (CheckAllDCombo()) {
            return;
        }
        if (CheckAllCCombo()) {
            return;
        }
    }

    private void RemoveUnitSafely(PlayerUnit playerUnit) {
        unitsOnMixer.Remove(playerUnit);
        Destroy(playerUnit.gameObject);
        if (playerUnit == GameEngine.GetInstance().playerUnitSelected) {
            GameEngine.GetInstance().unitSelectionPanel.CloseUnitSelectionPanelButton();
            GameEngine.GetInstance().playerUnitSelected = null;
        }
    }

    //---------- Unit Mixing Methods ----------
    private bool CheckMatchingUnit(PlayerUnit playerUnit) {
        Debug.Log("Check Matching Unit");

        PlayerUnit matchingPlayerUnit = null;
        matchingPlayerUnit = GetMatchingUnitType(playerUnit);
        if (matchingPlayerUnit != null) {
            PlayerUnitRank newUnitRank = GetNextTierRank(playerUnit.rank);
            RemoveUnitSafely(playerUnit);
            RemoveUnitSafely(matchingPlayerUnit);            
            PlayerUnit newPlayerUnit = GameEngine.GetInstance().unitSpawner.CreateRandomUnitOfRank(newUnitRank);
            return true;
        }
        return false;
    }

    private PlayerUnit GetMatchingUnitType(PlayerUnit playerUnit) {
        if (playerUnit.rank == PlayerUnitRank.S || playerUnit.rank == PlayerUnitRank.X) {
            return null;
        }
        foreach (PlayerUnit p in unitsOnMixer) {
            if (playerUnit.name != p.name && playerUnit.unitClass == p.unitClass && playerUnit.rank == p.rank) {
                return p;
            }
        }
        return null;
    }

    private PlayerUnitRank GetNextTierRank(PlayerUnitRank rank) {
        if (rank == PlayerUnitRank.D) {
            return PlayerUnitRank.C;
        } else if (rank == PlayerUnitRank.C) {
            return PlayerUnitRank.B;
        } else if (rank == PlayerUnitRank.B) {
            return PlayerUnitRank.A;
        } else if (rank == PlayerUnitRank.A) {
            return PlayerUnitRank.S;
        } else {
            throw new GameplayException("Unsupported rank value: " + rank.ToString());
        }
    }

    private bool CheckBCDCombo(PlayerUnit playerUnit) {
        Debug.Log("Check BCD Combo");

        if (playerUnit.rank == PlayerUnitRank.A || playerUnit.rank == PlayerUnitRank.S || playerUnit.rank == PlayerUnitRank.X) {
            return false;
        }
        UnitClass targetUnitClass = playerUnit.unitClass;

        PlayerUnit dUnit = null;
        PlayerUnit cUnit = null;
        PlayerUnit bUnit = null;
        
        foreach (PlayerUnit p in unitsOnMixer) {
            if (dUnit == null && p.rank == PlayerUnitRank.D && p.unitClass == targetUnitClass) {
                dUnit = p;
            } else if (cUnit == null && p.rank == PlayerUnitRank.C && p.unitClass == targetUnitClass) {
                cUnit = p;
            } else if (bUnit == null && p.rank == PlayerUnitRank.B && p.unitClass == targetUnitClass) {
                bUnit = p;
            }
        }
        if (dUnit != null && cUnit != null && bUnit != null) {
            RemoveUnitSafely(dUnit);
            RemoveUnitSafely(cUnit);
            RemoveUnitSafely(bUnit);
            PlayerUnit newPlayerUnit = GameEngine.GetInstance().unitSpawner.CreateRandomAUnit();
            return true;
        }
        return false;
    }
    
    private bool CheckAllDCombo() {
        Debug.Log("All D Combo");

        if (GameEngine.GetInstance().gas < 80) {
            return false;
        }

        PlayerUnit infantryUnit = null;
        PlayerUnit mechUnit = null;
        PlayerUnit laserUnit = null;
        PlayerUnit psionicUnit = null;
        PlayerUnit acidUnit = null;
        PlayerUnit bladeUnit = null;
        foreach (PlayerUnit p in unitsOnMixer) {
            if (p.rank == PlayerUnitRank.D && p.unitClass == UnitClass.INFANTRY) {
                infantryUnit = p;
            } else if (p.rank == PlayerUnitRank.D && p.unitClass == UnitClass.MECH) {
                mechUnit = p;
            } else if (p.rank == PlayerUnitRank.D && p.unitClass == UnitClass.LASER) {
                laserUnit = p;
            } else if (p.rank == PlayerUnitRank.D && p.unitClass == UnitClass.PSIONIC) {
                psionicUnit = p;
            } else if (p.rank == PlayerUnitRank.D && p.unitClass == UnitClass.ACID) {
                acidUnit = p;
            } else if (p.rank == PlayerUnitRank.D && p.unitClass == UnitClass.BLADE) {
                bladeUnit = p;
            }
        }

        if (infantryUnit != null && mechUnit != null && laserUnit != null && psionicUnit != null && acidUnit != null && bladeUnit != null) {
            RemoveUnitSafely(infantryUnit);
            RemoveUnitSafely(mechUnit);
            RemoveUnitSafely(laserUnit);
            RemoveUnitSafely(psionicUnit);
            RemoveUnitSafely(acidUnit);
            RemoveUnitSafely(bladeUnit);

            GameEngine.GetInstance().DecreaseGas(80);
            PlayerUnit newPlayerUnit = GameEngine.GetInstance().unitSpawner.CreateRandomUnitOfRank(PlayerUnitRank.A);
            return true;
        }
        return false;
    }

    private bool CheckAllDCombo(PlayerUnit playerUnit) {
        if (playerUnit.rank != PlayerUnitRank.D) {
            return false;
        }
        return CheckAllDCombo();
    }

    private bool CheckAllCCombo() {
        Debug.Log("All C Combo");

        if (GameEngine.GetInstance().gas < 300) {
            return false;
        }

        PlayerUnit infantryUnit = null;
        PlayerUnit mechUnit = null;
        PlayerUnit laserUnit = null;
        PlayerUnit psionicUnit = null;
        PlayerUnit acidUnit = null;
        PlayerUnit bladeUnit = null;
        foreach (PlayerUnit p in unitsOnMixer) {
            if (p.rank == PlayerUnitRank.C && p.unitClass == UnitClass.INFANTRY) {
                infantryUnit = p;
            } else if (p.rank == PlayerUnitRank.C && p.unitClass == UnitClass.MECH) {
                mechUnit = p;
            } else if (p.rank == PlayerUnitRank.C && p.unitClass == UnitClass.LASER) {
                laserUnit = p;
            } else if (p.rank == PlayerUnitRank.C && p.unitClass == UnitClass.PSIONIC) {
                psionicUnit = p;
            } else if (p.rank == PlayerUnitRank.C && p.unitClass == UnitClass.ACID) {
                acidUnit = p;
            } else if (p.rank == PlayerUnitRank.C && p.unitClass == UnitClass.BLADE) {
                bladeUnit = p;
            }
        }

        if (infantryUnit != null && mechUnit != null && laserUnit != null && psionicUnit != null && acidUnit != null && bladeUnit != null) {
            RemoveUnitSafely(infantryUnit);
            RemoveUnitSafely(mechUnit);
            RemoveUnitSafely(laserUnit);
            RemoveUnitSafely(psionicUnit);
            RemoveUnitSafely(acidUnit);
            RemoveUnitSafely(bladeUnit);

            GameEngine.GetInstance().DecreaseGas(300);
            PlayerUnit newPlayerUnit = GameEngine.GetInstance().unitSpawner.CreateRandomUnitOfRank(PlayerUnitRank.S);
            return true;
        }
        return false;
    }

    private bool CheckAllCCombo(PlayerUnit playerUnit) {
        if (playerUnit.rank != PlayerUnitRank.C) {
            return false;
        }
        return CheckAllCCombo();
    }

    private bool CheckXCombo(PlayerUnit playerUnit) {
        Debug.Log("Check X Combo");

        if (GameEngine.GetInstance().hasXUnit) {
            return false;
        }
        if (playerUnit.rank == PlayerUnitRank.D || playerUnit.rank == PlayerUnitRank.C || playerUnit.rank == PlayerUnitRank.X) {
            return false;
        }
        UnitClass targetUnitClass = playerUnit.unitClass;

        PlayerUnit bUnit = null;
        PlayerUnit aUnit = null;
        PlayerUnit sUnit = null;

        foreach (PlayerUnit p in unitsOnMixer) {
            if (bUnit == null && p.rank == PlayerUnitRank.B && p.unitClass == targetUnitClass) {
                bUnit = p;
            } else if (aUnit == null && p.rank == PlayerUnitRank.A && p.unitClass == targetUnitClass) {
                aUnit = p;
            } else if (sUnit == null && p.rank == PlayerUnitRank.S && p.unitClass == targetUnitClass) {
                sUnit = p;
            }
        }
        if (bUnit != null && aUnit != null && sUnit != null) {
            RemoveUnitSafely(bUnit);
            RemoveUnitSafely(aUnit);
            RemoveUnitSafely(sUnit);

            PlayerUnit newPlayerUnit = GameEngine.GetInstance().unitSpawner.CreatePlayerUnit(PlayerUnitRank.X, targetUnitClass);
            GameEngine.GetInstance().hasXUnit = true;
            return true;
        }
        return false;
    }

    private bool CheckRareBCombo(PlayerUnit playerUnit) {
        Debug.Log("Check Rare B Combo");

        if (playerUnit.rank != PlayerUnitRank.B) {
            return false;
        }

        PlayerUnit magicUnit = null;
        PlayerUnit flameUnit = null;

        foreach (PlayerUnit p in unitsOnMixer) {
            if (p.rank == PlayerUnitRank.B && p.unitClass == UnitClass.MAGIC) {
                magicUnit = p;
            } else if (p.rank == PlayerUnitRank.B && p.unitClass == UnitClass.FLAME) {
                flameUnit = p;
            }
        }

        if (magicUnit != null && flameUnit != null) {
            RemoveUnitSafely(magicUnit);
            RemoveUnitSafely(flameUnit);

            // TODO create 2 B choosers

            return true;
        }
        return false;
    }

    private bool CheckRareACombo(PlayerUnit playerUnit) {
        Debug.Log("Check Rare A Combo");

        if (playerUnit.rank != PlayerUnitRank.A) {
            return false;
        }

        PlayerUnit magicUnit = null;
        PlayerUnit flameUnit = null;

        foreach (PlayerUnit p in unitsOnMixer) {
            if (p.rank == PlayerUnitRank.A && p.unitClass == UnitClass.MAGIC) {
                magicUnit = p;
            } else if (p.rank == PlayerUnitRank.A && p.unitClass == UnitClass.FLAME) {
                flameUnit = p;
            }
        }

        if (magicUnit != null && flameUnit != null) {
            RemoveUnitSafely(magicUnit);
            RemoveUnitSafely(flameUnit);

            // TODO create 2 A choosers

            return true;
        }
        return false;
    }
}
