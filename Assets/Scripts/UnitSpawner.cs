// Controller for spawning units of any kind
using UnityEngine;


public class UnitSpawner : MonoBehaviour {
    // ---------- Fields ----------
    public Transform playerUnitSpawnLocation;
    public Transform enemyUnitSpawnLocation;
    public Transform playerUnit;
    public Transform enemyUnit;

    private static int uid = 0;
    private UnitFactory unitFactory;
    private System.Random random;


    // ---------- Methods ----------
    private void Awake() {
        unitFactory = new UnitFactory();
        random = new System.Random();
    }

    // Player Unit Creation Functions
    public void CreateRandomDUnit() {
        int selection = random.Next(0, 6);
        PlayerUnitData playerUnitData = unitFactory.CreatePlayerUnitData(PlayerUnitRank.D, selection);
        // TODO the prefab to initialize should vary based on the playerunitdata
        PlayerUnit p = (PlayerUnit)Instantiate(playerUnit, playerUnitSpawnLocation).GetComponent<PlayerUnit>();
        p.InitializeProperties(playerUnitData);
        SetObjectName(p.gameObject);
    }        
    
    public void CreateRandomCUnit() {

    }

    public void CreateRandomBUnit() {

    }

    public void CreateRandomAUnit() {

    }

    public void CreateRandomSUnit() {

    }

    // Enemy Unit Creation Functions
    public void CreateEnemyUnit() {
        // TODO level should be a parameter passed in from the game engine
        int level = 1;
        EnemyUnitData enemyUnitData = unitFactory.CreateEnemyUnitData(level);
        // TODO the prefab to initialize should depend on the level
        EnemyUnit e = (EnemyUnit)Instantiate(enemyUnit, enemyUnitSpawnLocation).GetComponent<EnemyUnit>();
        e.InitializeProperties(enemyUnitData);
        SetObjectName(e.gameObject);
    }

    // Other
    private void SetObjectName(GameObject obj) {
        obj.name = uid + "";
        uid += 1;
    }
}
