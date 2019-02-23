using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public void PlayButtonPressed() {
        Debug.Log("Play Button Pressed");
        SceneManager.LoadScene("MainGame");
    }
}
