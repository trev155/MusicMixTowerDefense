/*
 * The level manager is responsible for starting and stopping levels, as well as handling delay time between levels.
 */

using UnityEngine;
using System.Collections;


public class LevelManager : MonoBehaviour {
    // ---------- Fields ----------
    private static readonly float spawnDelay = 2.0f;
    private static readonly int numUnitsPerLevel = 5;
    private int currentLevel = 0;

    public bool levelHasStarted = false;
    public bool levelTransitionPeriod = false;
    public float timeLeftInLevel;
    

    // ---------- Methods ----------
    private void Update() {
        if (levelHasStarted) {
            timeLeftInLevel -= Time.deltaTime;
            GameEngine.GetInstance().gameDataPanel.SetLevelTimerText(ConvertTimeToString(timeLeftInLevel));
        }

        if (timeLeftInLevel < 0) {
            levelHasStarted = false;
            levelTransitionPeriod = true;
            timeLeftInLevel = 0;
        }
        
        if (levelTransitionPeriod) {
            levelTransitionPeriod = false;
            StartCoroutine(LevelTransition());
        }
    }

    public void StartLevel(int level) {
        GameEngine.GetInstance().messageQueue.PushMessage("Starting Level: [" + level + "]");
        
        levelHasStarted = true;
        currentLevel = level;
        StartCoroutine(StartLevelLoop(level));
        timeLeftInLevel = numUnitsPerLevel * spawnDelay;
        // TODO add more time to the level based on the difficulty setting
    }

    private IEnumerator StartLevelLoop(int level) {
        int numUnitsSpawned = 0;
        while (true) {
            GameEngine.GetInstance().unitSpawner.CreateEnemyUnit(level);
            numUnitsSpawned += 1;

            if (numUnitsSpawned == numUnitsPerLevel) {
                Debug.Log("Created " + numUnitsPerLevel + " units. Exiting loop.");
                break;
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private string ConvertTimeToString(float time) {
        int timeInSeconds = (int)Mathf.Round(time);
        int minutes = timeInSeconds / 60;
        int seconds = timeInSeconds % 60;

        string minutesString = minutes + "";
        string secondsString = seconds + "";
        if (seconds < 10) {
            secondsString = "0" + secondsString;
        }

        return minutesString + ":" + secondsString;
    }

    private IEnumerator LevelTransition() {
        // TODO time should depend on difficulty setting
        Debug.Log("Transitioning between levels.");
        yield return new WaitForSeconds(15.0f);
        StartLevel(currentLevel + 1);
    }

    public IEnumerator WaitBeforeStartingGame(float t) {
        Debug.Log("Waiting at game start, for time: " + t + " seconds.");
        yield return new WaitForSeconds(t);
        StartLevel(1);
    }
}