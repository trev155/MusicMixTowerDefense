using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text globalTimer;
    public float globalTime;
    
    void Update() {
        globalTime += Time.deltaTime;
        globalTimer.text = "Game Time: " + ConvertTimeToString(globalTime);
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
}
