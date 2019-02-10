using UnityEngine;
using System.Collections.Generic;

public class EnemyUnit : Unit, IClickableUnit {
    //---------- Fields ----------
    public float health;
    public float armor;
    public EnemyAbilities abilities;

    private List<Transform> waypoints = new List<Transform>();


    //---------- Methods ----------
    public void GetUnitDetails() {
        // this should probably return a string, or a map of strings
        Debug.Log("Enemy Unit Details");
    }

    public void InitializeProperties() {
        this.DisplayName = "Test";
        this.MovementSpeed = 1.5f;
        this.health = 100.0f;
        this.armor = 0;
        this.abilities = EnemyAbilities.NONE;

        GameObject mapWaypoints = GameObject.Find("MapWaypoints");
        Debug.Log(mapWaypoints);
        foreach (Transform waypoint in mapWaypoints.transform) {
            waypoints.Add(waypoint);
        }
        
    }
}
