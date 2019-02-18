using UnityEngine;


public class Projectile : MonoBehaviour {
    public Unit targetUnit;
    public PlayerUnit origin;
    public float movementSpeed;
    public float attackDamage;


    public void InitializeProperties(Unit targetUnit, PlayerUnit origin, float attackDamage) {
        this.targetUnit = targetUnit;
        this.origin = origin;
        movementSpeed = 5.0f;
        this.attackDamage = attackDamage;
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, targetUnit.transform.position, Time.deltaTime * movementSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == targetUnit.gameObject) {
            // TODO inflict damage to the target unit
            Destroy(this.gameObject);
        }
    }
}
