using UnityEngine;
using System.Collections.Generic;

public class EnemyUnit : Unit, IClickableUnit {
    //---------- Fields ----------
    public float health;
    public float armor;
    public int level;
    public EnemyAbilities abilities;

    private List<Transform> waypoints = new List<Transform>();
    private Transform currentWaypointDestination; 

    //---------- Methods ----------
    public string GetUnitTypeString() {
        return "Enemy Unit";
    }

    public void InitializeProperties(EnemyUnitData enemyUnitData) {
        this.DisplayName = enemyUnitData.GetDisplayName();
        this.MovementSpeed = enemyUnitData.GetMovementSpeed();
        this.health = enemyUnitData.GetHealth();
        this.armor = enemyUnitData.GetArmor();
        this.level = enemyUnitData.GetLevel();
        this.abilities = enemyUnitData.GetEnemyAbilities();
        InitializeWaypoints();
    }

    private void InitializeWaypoints() {
        GameObject mapWaypoints = GameObject.Find("MapWaypoints");
        if (mapWaypoints == null) {
            Debug.LogWarning("No waypoints found. This unit will not move.");
        } else {
            foreach (Transform waypoint in mapWaypoints.transform) {
                waypoints.Add(waypoint);
            }
            if (waypoints.Count < 2) {
                Debug.LogWarning("Less than 2 waypoints found. This unit will not move.");
            } else {
                currentWaypointDestination = waypoints[0];
            }
        }
    }

    private void Update() {
        WaypointHandler();
    }

    private void WaypointHandler() {
        if (!WaypointsExist()) {
            return;
        }
        MoveToNextWaypoint();
    }

    private bool WaypointsExist() {
        return waypoints.Count > 0;
    }

    public void UpdateWaypointDestination(Transform destination) {
        currentWaypointDestination = destination;
    }

    private void MoveToNextWaypoint() {
        transform.position = Vector2.MoveTowards(transform.position, currentWaypointDestination.position, MovementSpeed * Time.deltaTime);
    }
}
