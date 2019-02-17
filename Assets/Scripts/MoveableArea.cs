using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class MoveableArea : MonoBehaviour, IPointerClickHandler {
    //---------- Fields ----------
    private static readonly float CLICK_FADE_TIME = 0.4f;

    public Transform ClickCircle;
    public Transform ClickCirclePosition;


    //---------- Methods ----------
    public void OnPointerClick(PointerEventData eventData) {
        if (GameEngine.Instance.playerUnitMovementAllowed) {
            Vector2 touchedPosition = Camera.main.ScreenToWorldPoint(eventData.position);

            UpdatePlayerDestination(touchedPosition);
            CreateClickSpriteAtTouchedLocation(touchedPosition);
        }
    }

    private void UpdatePlayerDestination(Vector2 destination) {
        PlayerUnit playerUnit = GameEngine.Instance.playerUnitSelected;
        playerUnit.movementEnabled = true;
        playerUnit.movementDestination = destination;
    }

    private void CreateClickSpriteAtTouchedLocation(Vector2 touchedPosition) {
        ClickCirclePosition.position = touchedPosition;
        Transform spawnedCircle = Instantiate(ClickCircle, ClickCirclePosition);
        StartCoroutine(RemoveGameObjectAfterTime(spawnedCircle.gameObject, CLICK_FADE_TIME));
    }

    IEnumerator RemoveGameObjectAfterTime(GameObject obj, float time) {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}