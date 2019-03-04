using UnityEngine;

public class Admin : MonoBehaviour {
    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.Alpha1)) {
            Debug.Log("Admin Route");

            GameEngine.Instance.upgradeManager.PrintAllUpgrades();

        }
    }
}
