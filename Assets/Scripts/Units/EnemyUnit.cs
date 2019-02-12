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
    private int currentWaypointDestinationIndex = -1;

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
                currentWaypointDestinationIndex = 0;
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
        if (WaypointDestinationReached()) {
            UpdateToNextWaypointDestination();
        }
        MoveToNextWaypoint();
    }

    private bool WaypointsExist() {
        return currentWaypointDestinationIndex >= 0;
    }

    private bool WaypointDestinationReached() {
        return transform.position == currentWaypointDestination.position;
    }

    private void UpdateToNextWaypointDestination() {
        if (currentWaypointDestinationIndex == waypoints.Count - 1) {
            currentWaypointDestinationIndex = 0;
        } else {
            currentWaypointDestinationIndex += 1;
        }
        currentWaypointDestination = waypoints[currentWaypointDestinationIndex];
    }

    private void MoveToNextWaypoint() {
        transform.position = Vector2.MoveTowards(transform.position, currentWaypointDestination.position, MovementSpeed * Time.deltaTime);
    }
}
