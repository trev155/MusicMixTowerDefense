using UnityEngine;

public class GlobalTimer : MonoBehaviour {
    private bool globalTimerStarted = false;
    public float globalTime;
    
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

    public void BeginGlobalGameTimer() {
        this.globalTimerStarted = true;
    }
}
