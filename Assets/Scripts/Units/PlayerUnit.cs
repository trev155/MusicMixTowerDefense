using UnityEngine;

public class PlayerUnit : Unit, IClickableUnit {
    //---------- Fields ----------
    public PlayerUnitRank rank;
    public float attackDamage;
    public float attackSpeed;
    // TODO reference to the projectile prefab
    // TODO attack type field


    //---------- Methods ----------
    public void GetUnitDetails() {
        // this should probably return a string, or a map of strings
        Debug.Log("Player Unit Details");
    }

    public void InitializeProperties() {
        this.DisplayName = "Test";
        this.MovementSpeed = 1.5f;
        this.rank = PlayerUnitRank.D;
        this.attackDamage = 5.0f;
        this.attackSpeed = 1.0f;
    }

}
