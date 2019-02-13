using UnityEngine;
using System.Collections.Generic;


public class ClickHandler : MonoBehaviour {
    // ---------- Fields ----------
    private static readonly float CLICK_SEARCH_RADIUS = 1.2f;
    private static readonly bool ISCLICK = true;   // temp control variable

    public Transform clickCircle;
    public Transform clickCirclePosition;

    private bool screenIsTouched = false;

    // ---------- Methods ----------
    private void Update() {
        if (ScreenTouched()) {
            if (screenIsTouched) {
                return;
            }
            screenIsTouched = true;
            Vector2 touchedPosition = GetTouchedPosition();

            IClickableUnit unitSelection = GetClosestClickableUnitToPosition(touchedPosition);

            // TODO pull up a UI panel displaying this info
            // TODO store unit selection, give ability to move
            string unit = unitSelection.GetUnitTypeString();
            Debug.Log(unit);
        } else {
            screenIsTouched = false;
        }
    }

    private bool ScreenTouched() {
        if (ISCLICK) {
            return Input.GetMouseButton(0);
        } else {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }

    private Vector2 GetTouchedPosition() {
        if (ISCLICK) {
            return Input.mousePosition;
        } else {
            return Input.GetTouch(0).position;
        }
    }

    private Transform SpawnCircleOnPosition(Vector2 touchedPosition) {
        clickCirclePosition.position = Camera.main.ScreenToWorldPoint(touchedPosition);
        Vector3 tempPosition = clickCirclePosition.position;
        tempPosition[2] = 0;
        clickCirclePosition.position = tempPosition;
        Transform spawnedCircle = Instantiate(clickCircle, clickCirclePosition);
        return spawnedCircle;
    }

    IClickableUnit GetClosestClickableUnitToPosition(Vector2 touchedPosition) {
        Transform spawnedCircle = SpawnCircleOnPosition(touchedPosition);
        Collider2D closestClickableUnitToPosition = Physics2D.OverlapCircle(spawnedCircle.position, CLICK_SEARCH_RADIUS);
        IClickableUnit unitClicked = closestClickableUnitToPosition.gameObject.GetComponent<IClickableUnit>();
        return unitClicked;
    }
}
