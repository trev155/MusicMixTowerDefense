using UnityEngine;

public class PlayerUnit : Unit, IClickableUnit {
    //---------- Fields ----------
    public PlayerUnitRank rank;
    public float attackDamage;
    public float attackSpeed;
    public AttackType attackType;
    // TODO reference to the projectile prefab


    //---------- Methods ----------
    public void GetUnitDetails() {
        // this should probably return a string, or a map of strings
        Debug.Log("Player Unit Details");
    }

    public void InitializeProperties(PlayerUnitData playerUnitData) {
        this.DisplayName = playerUnitData.GetDisplayName();
        this.MovementSpeed = playerUnitData.GetMovementSpeed();
        this.rank = playerUnitData.GetRank();
        this.attackDamage = playerUnitData.GetAttackDamage();
        this.attackSpeed = playerUnitData.GetAttackSpeed();
        this.attackType = playerUnitData.GetAttackType();
    }
}
