using UnityEngine;
using UnityEngine.UI;


public class MenuPanel : MonoBehaviour {
    public Text mineralsText;
    public Text vespeneText;

    public void UpdateMineralsText(int minerals) {
        mineralsText.text = minerals + "";
    }

    public void UpdateVespeneText(int vespene) {
        vespeneText.text = vespene + "";
    }
}
