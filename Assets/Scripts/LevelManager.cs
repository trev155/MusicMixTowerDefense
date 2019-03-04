/*
 * The level manager is responsible for starting and stopping levels, as well as handling delay time between levels.
 */

using UnityEngine;
using System.Collections;


public class LevelManager : MonoBehaviour {
    private float spawnDelay = 2.0f;
    private int numUnitsSpawned = 0;
    private bool levelHasStarted = false;
    private int numUnitsPerLevel = 20;

    private float timeLeftInLevel;


    private void Update() {
        if (levelHasStarted) {
            timeLeftInLevel -= Time.deltaTime;
            GameEngine.Instance.gameDataPanel.SetLevelTimer(ConvertTimeToString(timeLeftInLevel));
        }

        if (timeLeftInLevel <= 0) {
            levelHasStarted = false;
            timeLeftInLevel = 0;
        }
    }

    public void StartLevel(int level) {
        if (levelHasStarted) {
            throw new GameplayException("There is a level in progress, cannot start new level right now.");
        }

        levelHasStarted = true;
        StartCoroutine(StartLevelLoop(level));
        timeLeftInLevel = 50.0f;
    }

    IEnumerator StartLevelLoop(int level) {
        while (true) {
            GameEngine.Instance.unitSpawner.CreateEnemyUnit(level);
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
}