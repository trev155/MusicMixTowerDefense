using UnityEngine;
using System.Collections;

public class GlobalTimer : MonoBehaviour {
    private bool globalTimerStarted = false;
    public float globalTime = 0;

    // Begin global timer
    public void BeginGlobalGameTimer() {
        this.globalTimerStarted = true;
    }

    // Continually update global timer
    private void Update() {
        if (globalTimerStarted) {
            globalTime += Time.deltaTime;
            GameEngine.GetInstance().gameDataPanel.UpdateGlobalGameTimeText(ConvertTimeToString(globalTime));
        }
    }
    
    private string ConvertTimeToString(float time) {
        int timeInSeconds = (int)Mathf.Round(time);
        int minutes = timeInSeconds / 60;
        int seconds = timeInSeconds % 60;

        string minutesString = minutes + "";
        if (minutes < 10) {
            minutesString = "0" + minutesString;
        }
        string secondsString = seconds + "";
        if (seconds < 10) {
            secondsString = "0" + secondsString;
        }

        return minutesString + ":" + secondsString;
    }

    // Harvester Bonuses
    private void Awake() {
        StartCoroutine(CheckForGasHarvesterBonus());
        StartCoroutine(CheckForMineralHarvesterBonus());
    }

    private IEnumerator CheckForGasHarvesterBonus() {
        while (true) {
            yield return new WaitForSeconds(5.0f);

            int gasIncrement = 2 * GameEngine.GetInstance().gasHarvesters;
            if (gasIncrement > 0) {
                GameEngine.GetInstance().IncreaseGas(gasIncrement);
                GameEngine.GetInstance().gameDataPanel.UpdateGasHarvesterBonusText(gasIncrement);
            }
        }
    }

    private IEnumerator CheckForMineralHarvesterBonus() {
        while (true) {
            yield return new WaitForSeconds(10.0f);

            int mineralIncrement = 4 * GameEngine.GetInstance().mineralHarvesters;
            if (mineralIncrement > 0) {
                GameEngine.GetInstance().IncreaseMinerals(mineralIncrement);
                GameEngine.GetInstance().gameDataPanel.UpdateMineralHarvesterBonusText(mineralIncrement);
            }
        }
    }
}
