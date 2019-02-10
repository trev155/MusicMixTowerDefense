/*
 * A clickable unit is one where you can touch, and details about that unit will appear on the screen.
 */

public interface IClickableUnit {
    // When we detect a click on the screen, if this unit was clicked, call this function
    void GetUnitDetails();
}
