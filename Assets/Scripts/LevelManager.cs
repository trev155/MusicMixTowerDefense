/*
 * The level manager is responsible for starting and stopping levels, as well as handling delay time between levels.
 */

using UnityEngine;
using System.Collections;


public class LevelManager : MonoBehaviour {
    // ---------- Fields ----------
    private static readonly float spawnDelay = 1.5f;
    private static readonly int numUnitsPerLevel = 40;
    public int currentLevel = 0;

    public bool levelHasStarted = false;
    public bool levelTransitionPeriod = false;
    public float timeLeftInLevel;


    // ---------- Methods ----------
    public IEnumerator WaitBeforeStartingGame(float t) {
        Debug.Log("Waiting at game start, for time: " + t + " seconds.");
        yield return new WaitForSeconds(t);
        StartLevel(1);
    }

    private void Update() {
        if (levelHasStarted) {
            timeLeftInLevel -= Time.deltaTime;
            GameEngine.GetInstance().gameDataPanel.UpdateLevelTimeText(ConvertTimeToString(timeLeftInLevel));
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
        GameEngine.GetInstance().gameDataPanel.UpdateLevelText(level);
        StartCoroutine(StartLevelLoop(level));
        timeLeftInLevel = numUnitsPerLevel * spawnDelay;

        GameEngine.GetInstance().audioManager.AddLevelMusicToQueue(level);
    }

    private IEnumerator StartLevelLoop(int level) {
        int numUnitsSpawned = 0;
        while (true) {
            GameEngine.GetInstance().unitSpawner.CreateEnemyUnit(level);
            numUnitsSpawned += 1;

            if (numUnitsSpawned == numUnitsPerLevel) {
                Debug.Log("Created " + numUnitsPerLevel + " units. Exiting loop.");

                if (ShouldCreateBountyAtLevelEnd(level)) {
                    GameEngine.GetInstance().unitSpawner.CreateBounty();
                }
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
        float timeToWait = GetWaitTimeBetweenLevels();
        Debug.Log("Calling WaitBetweenLevels() -> " + timeToWait + " seconds");
        GameEngine.GetInstance().gameDataPanel.SetLevelTransitionTimeText();
        yield return new WaitForSeconds(timeToWait);

        StartLevel(currentLevel + 1);
    }

    private float GetWaitTimeBetweenLevels() {
        switch (GameEngine.GetInstance().gameMode) {
            case GameMode.EASY:
                return 20.0f;
            case GameMode.NORMAL:
                return 12.0f;
            case GameMode.HARD:
                return 8.0f;
            case GameMode.NONSTOP:
                return 4.0f;
            case GameMode.PRO:
                return 0.1f;
            default:
                throw new GameplayException("Unrecognized game mode");
        }
    }

    private bool ShouldCreateBountyAtLevelEnd(int level) {
        return level % 5 == 0 && level < 40;
    }
}