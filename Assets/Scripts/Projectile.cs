using UnityEngine;


public class Projectile : MonoBehaviour {
    public Unit targetUnit;
    public PlayerUnit origin;
    public float movementSpeed; // this will depend on the player unit type / rank
    public float attackDamage;


    public void InitializeProperties(Unit targetUnit, PlayerUnit origin, float attackDamage) {
        this.targetUnit = targetUnit;
        this.origin = origin;
        this.attackDamage = attackDamage;
    }

    private void Update() {
        // repeatedly move toward the target

        // on contact with the enemy, inflict damage, and remove this object
    }
}
