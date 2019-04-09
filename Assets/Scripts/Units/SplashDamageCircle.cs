using UnityEngine;

public class SplashDamageCircle : MonoBehaviour {
    public Projectile projectile;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyUnit") {
            Debug.Log(collision.gameObject.name);
            EnemyUnit enemyUnit = collision.gameObject.GetComponent<EnemyUnit>();
            projectile.InflictDamage(enemyUnit, damage);
        }
    }
}