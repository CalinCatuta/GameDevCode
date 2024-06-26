// Projectile.cs
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 4f;
    public float lifeTime = 2f;
    public float currentDamage = 5f;

    private Vector3 direction;

    public void SetTarget(Vector3 target) {
        // Calculate the direction to move the projectile in
        direction = (target - transform.position).normalized;
        Destroy(gameObject, lifeTime);  // Destroy the projectile after its lifetime
    }

    void Update() {
        // Move in the calculated direction
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Reference the script from the collided collider and deal damage using TakeDamage().
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage); //Make sure to use currentDamage instead of weaponData.damage in case any damage multipliers in the future.
            Destroy(gameObject);
        }
    }
}
