// Controller for spawning units of any kind
using UnityEngine;

public class UnitSpawner : MonoBehaviour {
    public Transform playerUnitSpawnLocation;
    public Transform enemyUnitSpawnLocation;

    public Transform playerUnit;
    public Transform enemyUnit;

    public void SpawnPlayerUnit() {
        Debug.Log("Spawn Player Unit");
        PlayerUnit p = (PlayerUnit)Instantiate(playerUnit, playerUnitSpawnLocation).GetComponent<PlayerUnit>();
        p.InitializeProperties();
    }

    public void SpawnEnemyUnit() {
        Debug.Log("Spawn Enemy Unit");
        EnemyUnit e = (EnemyUnit)Instantiate(enemyUnit, enemyUnitSpawnLocation).GetComponent<EnemyUnit>();
        e.InitializeProperties();
    }
}
