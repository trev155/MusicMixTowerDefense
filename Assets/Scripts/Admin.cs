﻿using UnityEngine;

public class Admin : MonoBehaviour {
    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.Alpha1)) {
            Debug.Log("Admin Route");

            GameEngine.GetInstance().audioManager.AddLevelMusicToQueue(1);
            GameEngine.GetInstance().audioManager.AddLevelMusicToQueue(2);
            
        }
    }
}
