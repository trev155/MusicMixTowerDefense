/*
 * A clickable unit is one where you can touch, and details about that unit will appear on the screen.
 */
using System.Collections.Generic;

public interface IClickableUnit {
    /*
     * Provides a list of strings to be used in the display of the selected unit's data.
     */
    List<string> GetDisplayUnitData();

}
