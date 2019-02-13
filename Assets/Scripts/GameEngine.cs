/*
 * The GameEngine handles most of the main aspects of the game.
 * The GameEngine also contains references to specialized components, so that these smaller components
 * can be referenced from a centralized location.
 */
using UnityEngine;


public class GameEngine : MonoBehaviour {
    // Singleton field
    public static GameEngine Instance { get; private set; } = null;

    // References to other management objects
    public HUDManager hudManager;
    public UnitSpawner unitSpawner;

    private void Awake() {
        // singleton initialization
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            Instance = this;
        }
    }
}
