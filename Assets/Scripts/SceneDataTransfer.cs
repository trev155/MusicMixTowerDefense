/*
 * The purpose of this class is to pass data from the main menu scene to the main game scene
 */
public static class SceneDataTransfer {
    // Game Mode Selection
    public static GameMode CurrentGameMode { get; set; } = GameMode.HARD;
}
