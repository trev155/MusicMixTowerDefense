using UnityEngine;
using UnityEngine.EventSystems;


public class MoveableArea : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        if (GameEngine.Instance.playerUnitMovementAllowed) {
            Vector2 touchedPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            UpdatePlayerDestination(touchedPosition);
        }
    }

    private void UpdatePlayerDestination(Vector2 destination) {
        PlayerUnit playerUnit = GameEngine.Instance.playerUnitSelected;
        playerUnit.movementEnabled = true;
        playerUnit.movementDestination = destination;
    }
}