using UnityEngine;

public class Waypoint : MonoBehaviour {
    public Transform nextWaypoint;

    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag == "EnemyUnit") {
            EnemyUnit enemy = collider.gameObject.GetComponent<EnemyUnit>();
            enemy.UpdateWaypointDestination(nextWaypoint);
        }
    }
}
