using UnityEngine;
using System.Collections;

public class HealthRegenerator : MonoBehaviour {
    private EnemyUnit enemyUnit;
    private bool isRegenerating = false;

    private void Awake() {
        enemyUnit = GetComponentInParent<EnemyUnit>();
    }

    private void Update() {
        if (ShouldStartHealthRegenLoop()) {
            StartCoroutine(HealthRegenLoop());
        }
    }

    private bool ShouldStartHealthRegenLoop() {
        return enemyUnit != null && enemyUnit.GetCurrentHealth() < enemyUnit.GetEnemyUnitData().GetMaxHealth() && !isRegenerating;
    }

    private IEnumerator HealthRegenLoop() {
        float regenerationRate = GetRegenerationRate(enemyUnit.GetEnemyUnitData().GetEnemyType());
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

    private float GetRegenerationRate(EnemyType enemyType) {
        switch (enemyType) {
            case EnemyType.BOUNTY:
                return 0.4f;
            default:
                throw new GameplayException("Enemy type not supported: " + enemyType.ToString());
        }
    }
}