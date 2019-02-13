/*
 * The ClickHandler listens for screen touches and reacts appropriately.
 */
using UnityEngine;
using System.Collections;


public class ClickHandler : MonoBehaviour {
    // ---------- Fields ----------
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

            Transform spawnedCircle = CreateClickCircle(touchedPosition);
            StartCoroutine(RemoveGameObjectAfterTime(spawnedCircle.gameObject, 0.1f)); 
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
    
    private Transform CreateClickCircle(Vector2 screenPosition) {
        clickCirclePosition.position = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector3 tempPosition = clickCirclePosition.position;
        tempPosition[2] = 0;
        clickCirclePosition.position = tempPosition;
        Transform spawnedCircle = Instantiate(clickCircle, clickCirclePosition);
        return spawnedCircle;
    }

    IEnumerator RemoveGameObjectAfterTime(GameObject obj, float time) {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}

