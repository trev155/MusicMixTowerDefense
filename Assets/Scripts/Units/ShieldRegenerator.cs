using UnityEngine;
using System.Collections;

public class ShieldRegenerator : MonoBehaviour {
    private EnemyUnit enemyUnit;
    private bool isRegenerating = false;

    private void Awake() {
        enemyUnit = GetComponentInParent<EnemyUnit>();   
    }

    private void Update() {
        if (ShouldStartShieldRegenLoop()) {
            StartCoroutine(ShieldRegenLoop());
        }
    }

    private bool ShouldStartShieldRegenLoop() {
        return enemyUnit.GetCurrentShields() < enemyUnit.GetEnemyUnitData().GetMaxShields() && !isRegenerating;
    }

    private IEnumerator ShieldRegenLoop() {
        while (true) {
            isRegenerating = true;
            yield return new WaitForSeconds(5 / enemyUnit.GetEnemyUnitData().GetShieldRegenerationRate());
            if (enemyUnit.GetCurrentShields() < enemyUnit.GetEnemyUnitData().GetMaxShields()) {
                enemyUnit.SetCurrentShields(enemyUnit.GetEnemyUnitData().GetMaxShields());
                if (GameEngine.GetInstance().enemyUnitSelected == enemyUnit) {
                    GameEngine.GetInstance().unitSelectionPanel.UpdateSelectedUnitDataPanel(enemyUnit);
                }
            }
            if (enemyUnit.GetCurrentHealth() == enemyUnit.GetEnemyUnitData().GetMaxHealth()) {
                break;
            }
        }
        isRegenerating = false;
    }
}
