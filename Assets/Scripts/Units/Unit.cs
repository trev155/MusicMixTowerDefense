using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Unit : MonoBehaviour, IClickableUnit, IPointerClickHandler {
    public string DisplayName;
    public float MovementSpeed;

    public abstract string GetTitleData();
    public abstract string GetBasicUnitData();
    public abstract string GetAdvancedUnitData();

    public abstract void OnPointerClick(PointerEventData eventData);
}
