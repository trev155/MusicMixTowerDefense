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
            GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.UNIT_MIX_SOUND);
            return;
        }

        // BCD -> A
        if (CheckBCDCombo(playerUnit)) {
            GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.UNIT_MIX_SOUND);
            return;
        }

        // 6 D + 80 Gas -> A
        if (CheckAllDCombo(playerUnit)) {
            GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.UNIT_MIX_SOUND);
            return;
        }

        // 6 C + 300 Gas -> S
        if (CheckAllCCombo(playerUnit)) {
            GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.UNIT_MIX_SOUND);
            return;
        }

        // BAS -> X (one only)
        if (CheckXCombo(playerUnit)) {
            GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.UNIT_MIX_SOUND);
            return;
        }

        // B Magic + B Flame -> 2 B choosers
        if (CheckRareBCombo(playerUnit)) {
            GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.UNIT_MIX_SOUND);
            return;
        }

        // A Magic + A Flame -> 2 A choosers
        if (CheckRareACombo(playerUnit)) {
            GameEngine.GetInstance().audioManager.PlayAudio(AudioManager.UNIT_MIX_SOUND);
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
        PlayerUnit matchingPlayerUnit = null;
        matchingPlayerUnit = GetMatchingUnitType(playerUnit);
        if (matchingPlayerUnit != null) {
            PlayerUnitRank newUnitRank = GetNextTierRank(playerUnit.GetPlayerUnitData().GetRank());
            RemoveUnitSafely(playerUnit);
            RemoveUnitSafely(matchingPlayerUnit);            
            PlayerUnit newPlayerUnit = GameEngine.GetInstance().unitSpawner.CreateRandomUnitOfRank(newUnitRank);
            return true;
        }
        return false;
    }

    private PlayerUnit GetMatchingUnitType(PlayerUnit playerUnit) {
        if (playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.S || playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.X) {
            return null;
        }
        foreach (PlayerUnit p in unitsOnMixer) {
            if (playerUnit.name != p.name && 
                playerUnit.GetPlayerUnitData().GetUnitClass() == p.GetPlayerUnitData().GetUnitClass() && 
                playerUnit.GetPlayerUnitData().GetRank() == p.GetPlayerUnitData().GetRank()) {
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
        if (playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.A || playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.S || playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.X) {
            return false;
        }
        UnitClass targetUnitClass = playerUnit.GetPlayerUnitData().GetUnitClass();

        PlayerUnit dUnit = null;
        PlayerUnit cUnit = null;
        PlayerUnit bUnit = null;
        
        foreach (PlayerUnit p in unitsOnMixer) {
            PlayerUnitRank playerUnitRank = p.GetPlayerUnitData().GetRank();
            UnitClass playerUnitClass = p.GetPlayerUnitData().GetUnitClass();

            if (dUnit == null && playerUnitRank == PlayerUnitRank.D && playerUnitClass == targetUnitClass) {
                dUnit = p;
            } else if (cUnit == null && playerUnitRank == PlayerUnitRank.C && playerUnitClass == targetUnitClass) {
                cUnit = p;
            } else if (bUnit == null && playerUnitRank == PlayerUnitRank.B && playerUnitClass == targetUnitClass) {
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
            PlayerUnitRank playerUnitRank = p.GetPlayerUnitData().GetRank();
            UnitClass playerUnitClass = p.GetPlayerUnitData().GetUnitClass();

            if (playerUnitRank == PlayerUnitRank.D && playerUnitClass == UnitClass.INFANTRY) {
                infantryUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.D && playerUnitClass == UnitClass.MECH) {
                mechUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.D && playerUnitClass == UnitClass.LASER) {
                laserUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.D && playerUnitClass == UnitClass.PSIONIC) {
                psionicUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.D && playerUnitClass == UnitClass.ACID) {
                acidUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.D && playerUnitClass == UnitClass.BLADE) {
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
        if (playerUnit.GetPlayerUnitData().GetRank() != PlayerUnitRank.D) {
            return false;
        }
        return CheckAllDCombo();
    }

    private bool CheckAllCCombo() {
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
            PlayerUnitRank playerUnitRank = p.GetPlayerUnitData().GetRank();
            UnitClass playerUnitClass = p.GetPlayerUnitData().GetUnitClass();

            if (playerUnitRank == PlayerUnitRank.C && playerUnitClass == UnitClass.INFANTRY) {
                infantryUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.C && playerUnitClass == UnitClass.MECH) {
                mechUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.C && playerUnitClass == UnitClass.LASER) {
                laserUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.C && playerUnitClass == UnitClass.PSIONIC) {
                psionicUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.C && playerUnitClass == UnitClass.ACID) {
                acidUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.C && playerUnitClass == UnitClass.BLADE) {
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
        if (playerUnit.GetPlayerUnitData().GetRank() != PlayerUnitRank.C) {
            return false;
        }
        return CheckAllCCombo();
    }

    private bool CheckXCombo(PlayerUnit playerUnit) {
        if (GameEngine.GetInstance().hasXUnit) {
            return false;
        }
        if (playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.D || playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.C || playerUnit.GetPlayerUnitData().GetRank() == PlayerUnitRank.X) {
            return false;
        }
        UnitClass targetUnitClass = playerUnit.GetPlayerUnitData().GetUnitClass();

        PlayerUnit bUnit = null;
        PlayerUnit aUnit = null;
        PlayerUnit sUnit = null;

        foreach (PlayerUnit p in unitsOnMixer) {
            PlayerUnitRank playerUnitRank = p.GetPlayerUnitData().GetRank();
            UnitClass playerUnitClass = p.GetPlayerUnitData().GetUnitClass();

            if (bUnit == null && playerUnitRank == PlayerUnitRank.B && playerUnitClass == targetUnitClass) {
                bUnit = p;
            } else if (aUnit == null && playerUnitRank == PlayerUnitRank.A && playerUnitClass == targetUnitClass) {
                aUnit = p;
            } else if (sUnit == null && playerUnitRank == PlayerUnitRank.S && playerUnitClass == targetUnitClass) {
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
        if (playerUnit.GetPlayerUnitData().GetRank() != PlayerUnitRank.B) {
            return false;
        }

        PlayerUnit magicUnit = null;
        PlayerUnit flameUnit = null;

        foreach (PlayerUnit p in unitsOnMixer) {
            PlayerUnitRank playerUnitRank = p.GetPlayerUnitData().GetRank();
            UnitClass playerUnitClass = p.GetPlayerUnitData().GetUnitClass();

            if (playerUnitRank == PlayerUnitRank.B && playerUnitClass == UnitClass.MAGIC) {
                magicUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.B && playerUnitClass == UnitClass.FLAME) {
                flameUnit = p;
            }
        }

        if (magicUnit != null && flameUnit != null) {
            RemoveUnitSafely(magicUnit);
            RemoveUnitSafely(flameUnit);

            GameEngine.GetInstance().messageQueue.PushMessage("2 B Rank Choosers", MessageType.INFO);
            GameEngine.GetInstance().AddBBonusTokens(2);
            return true;
        }
        return false;
    }

    private bool CheckRareACombo(PlayerUnit playerUnit) {
        if (playerUnit.GetPlayerUnitData().GetRank() != PlayerUnitRank.A) {
            return false;
        }

        PlayerUnit magicUnit = null;
        PlayerUnit flameUnit = null;

        foreach (PlayerUnit p in unitsOnMixer) {
            PlayerUnitRank playerUnitRank = p.GetPlayerUnitData().GetRank();
            UnitClass playerUnitClass = p.GetPlayerUnitData().GetUnitClass();

            if (playerUnitRank == PlayerUnitRank.A && playerUnitClass == UnitClass.MAGIC) {
                magicUnit = p;
            } else if (playerUnitRank == PlayerUnitRank.A && playerUnitClass == UnitClass.FLAME) {
                flameUnit = p;
            }
        }

        if (magicUnit != null && flameUnit != null) {
            RemoveUnitSafely(magicUnit);
            RemoveUnitSafely(flameUnit);

            GameEngine.GetInstance().messageQueue.PushMessage("2 A Rank Choosers", MessageType.INFO);
            GameEngine.GetInstance().AddABonusTokens(2);
            return true;
        }
        return false;
    }
}
