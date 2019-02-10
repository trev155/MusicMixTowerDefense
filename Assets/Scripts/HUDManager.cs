using UnityEngine;

public class HUDManager : MonoBehaviour {
    public UnitSpawner unitSpawner;

    public void SpawnPlayerUnit() {
        unitSpawner.SpawnPlayerUnit();
    }

    public void SpawnEnemyUnit() {
        unitSpawner.SpawnEnemyUnit();
    }
}
