using UnityEngine;
using System.Collections.Generic;


public class ClickHandler : MonoBehaviour {
    // ---------- Fields ----------
    private static readonly float CANDIDATE_DISTANCE = 100.0f;

    public Transform clickCircle;
    public Transform clickCirclePosition;

    // ---------- Methods ----------
    private void Update() {
        if (ScreenTouched()) {
            Vector2 touchedPosition = Input.GetTouch(0).position;

            // display a circle where the user clicked
            clickCirclePosition.position = Camera.main.ScreenToWorldPoint(touchedPosition);
            Vector3 tempPosition = clickCirclePosition.position;
            tempPosition[2] = 0;
            clickCirclePosition.position = tempPosition;
            Transform spawnedCircleObject = Instantiate(clickCircle, clickCirclePosition);

            
            List<Transform> nearbyClickableUnitTransforms = GetNearbyClickableUnitTransforms(touchedPosition);
            if (nearbyClickableUnitTransforms.Count == 0) {
                return;
            }
            IClickableUnit unitClicked = GetClosestClickableUnit(touchedPosition, nearbyClickableUnitTransforms);

            // Decide what to do based on the type of the unit clicked
            string unit = unitClicked.GetUnitTypeString();

            
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
        if (closeUnits.Count == 0) {
            return null;
        }
        
        int closestUnitIndex = 0;
        float closestUnitDistance = float.MaxValue;
        for (int unitIndex = 0; unitIndex < closeUnits.Count; unitIndex++) {
            Transform unit = closeUnits[unitIndex];
            Vector2 unitScreenPosition = Camera.main.WorldToScreenPoint(unit.position);
            float unitDistanceToReference = Vector2.Distance(touchedPosition, unitScreenPosition);
            Debug.Log("Distance of unit [" + unit.gameObject.name + "] = " + unitDistanceToReference);
            if (unitDistanceToReference < closestUnitDistance) {
                closestUnitIndex = unitIndex;
                closestUnitDistance = unitDistanceToReference;
            }
        }

        Transform closestUnit = closeUnits[closestUnitIndex];
        IClickableUnit closestUnitComponent = closestUnit.gameObject.GetComponent<IClickableUnit>();
        return closestUnitComponent;
    }
}
