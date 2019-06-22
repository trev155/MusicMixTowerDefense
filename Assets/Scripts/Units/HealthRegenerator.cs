using UnityEngine;
using System.Collections;

public class HealthRegenerator : MonoBehaviour {
    public EnemyUnit enemyUnit;
    private bool isRegenerating = false;
    private float regenerationRate;
    
    private void Update() {
        if (ShouldStartHealthRegenLoop()) {
            StartCoroutine(HealthRegenLoop());
        }
    }

    private bool ShouldStartHealthRegenLoop() {
        return enemyUnit != null && enemyUnit.GetCurrentHealth() < enemyUnit.GetEnemyUnitData().GetMaxHealth() && !isRegenerating;
    }

    private IEnumerator HealthRegenLoop() {
        isRegenerating = true;
        while (true) {
            yield return new WaitForSeconds(regenerationRate);
            if (enemyUnit.GetCurrentHealth() < enemyUnit.GetEnemyUnitData().GetMaxHealth()) {
                enemyUnit.SetCurrentHealth(enemyUnit.GetCurrentHealth() + 1);
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

    public void SetRegenerationRate(string unitType) {
        switch (unitType) {
            case "Bounty":
                regenerationRate = 0.5f;
                break;
            default:
                regenerationRate = 0.2f;
                break;
        }
    }
}