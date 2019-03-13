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
        PlayerUnit matchingPlayerUnit = GetMatchingUnitType(playerUnit);
        if (matchingPlayerUnit != null) {
            PlayerUnitRank newUnitRank = GetNextTierRank(playerUnit.rank);
            PlayerUnit newPlayerUnit = GameEngine.GetInstance().unitSpawner.CreateRandomUnitOfRank(newUnitRank);

            unitsOnMixer.Remove(playerUnit);
            unitsOnMixer.Remove(matchingPlayerUnit);

            Destroy(playerUnit.gameObject);
            Destroy(matchingPlayerUnit.gameObject);
        }

        // BCD -> A

        // 6 D + 80 Gas -> A

        // 6 C + 300 Gas -> S

        // BAS -> X (one only)

        // B Magic + B Flame -> 2 B choosers

        // A Magic + A Flame -> 2 A choosers
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag != "PlayerUnit") {
            return;
        }
        PlayerUnit playerUnit = collision.GetComponent<PlayerUnit>();
        unitsOnMixer.Remove(playerUnit);
    }

    private PlayerUnit GetMatchingUnitType(PlayerUnit playerUnit) {
        if (playerUnit.rank == PlayerUnitRank.S) {
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
}
