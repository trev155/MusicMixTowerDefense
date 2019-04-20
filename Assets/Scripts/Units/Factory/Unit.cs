using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public abstract class Unit : MonoBehaviour, IClickableUnit, IPointerClickHandler {
    public string displayName;
    public float movementSpeed;

    public abstract List<string> GetDisplayUnitData();
    public abstract void OnPointerClick(PointerEventData eventData);
}
