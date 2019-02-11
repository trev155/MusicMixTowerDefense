using UnityEngine;

public class HUDManager : MonoBehaviour {
    public UnitSpawner unitSpawner;

    public void SpawnPlayerUnit() {
        unitSpawner.CreateRandomDUnit();
    }

    public void SpawnEnemyUnit() {
        unitSpawner.CreateEnemyUnit();
    }
}
