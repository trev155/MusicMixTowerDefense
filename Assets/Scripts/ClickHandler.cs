using UnityEngine;
using System.Collections.Generic;


public class ClickHandler : MonoBehaviour {
    private static readonly float CANDIDATE_DISTANCE = 50.0f;

    private void Update() {
        if (ScreenTouched()) {
            Vector2 touchedPosition = Input.GetTouch(0).position;
            List<Transform> nearbyClickableUnitTransforms = GetNearbyClickableUnitTransforms(touchedPosition);
            if (nearbyClickableUnitTransforms.Count == 0) {
                return;
            }

            IClickableUnit unitClicked = GetClosestClickableUnit(touchedPosition, nearbyClickableUnitTransforms);
            Debug.Log(unitClicked);
        }
    }

    private bool ScreenTouched() {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    private List<Transform> GetNearbyClickableUnitTransforms(Vector2 referencePosition) {
        List<Transform> allNearbyClickableUnitTransforms = new List<Transform>();
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject playerUnit in playerUnits) {
            Vector2 playerUnitScreenPosition = Camera.main.WorldToScreenPoint(playerUnit.transform.position);
            float playerUnitDistanceToReference = Vector2.Distance(referencePosition, playerUnitScreenPosition);
            if (playerUnitDistanceToReference < CANDIDATE_DISTANCE) {
                allNearbyClickableUnitTransforms.Add(playerUnit.transform);
            }
        }

        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject enemyUnit in enemyUnits) {
            Vector2 enemyUnitScreenPosition = Camera.main.WorldToScreenPoint(enemyUnit.transform.position);
            float enemyUnitDistanceToReference = Vector2.Distance(referencePosition, enemyUnitScreenPosition);
            if (enemyUnitDistanceToReference < CANDIDATE_DISTANCE) {
                allNearbyClickableUnitTransforms.Add(enemyUnit.transform);
            }
        }
        return allNearbyClickableUnitTransforms;
    }

    private IClickableUnit GetClosestClickableUnit(Vector2 touchedPosition, List<Transform> closeUnits) {
        // algorithm to get lowest distance

        // return PlayerUnit or EnemyUnit (cast to appropriate)
        
        return null;
    }
}
