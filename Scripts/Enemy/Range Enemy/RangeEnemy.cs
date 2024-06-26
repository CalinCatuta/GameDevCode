using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour, IDamageable
{
    RangeEnemySpawner rangeEnemySpawner;
    [SerializeField]
    private RangeEnemyScriptableObject rangeEnemyStats;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentShootRange;
    [HideInInspector]
    public float currentShootInterval;


    public GameObject projectilePrefab;

    private Transform player;
    private float shootTimer;


    private void Awake() {
        rangeEnemySpawner = FindObjectOfType<RangeEnemySpawner>();
        currentHealth = rangeEnemyStats.MaxHealth;
        currentMoveSpeed = rangeEnemyStats.MoveSpeed;
        currentShootRange = rangeEnemyStats.ShootRange;
        currentShootInterval = rangeEnemyStats.ShootInterval;
    }
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootTimer = currentShootInterval;
    }

    void Update() {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < currentShootRange)
        {
            // Move away from the player if too close
            Vector3 direction = (transform.position - player.position).normalized;
            transform.position += direction * currentMoveSpeed * Time.deltaTime;
        }
        else if (distanceToPlayer > currentShootRange)
        {
            // Move towards the player if too far
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * currentMoveSpeed * Time.deltaTime;
        }

        // Shooting logic
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = currentShootInterval;
        }
    }

    void Shoot() {
        // Stop moving while shooting
        Vector3 targetPosition = player.position;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(targetPosition);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }
    void Kill() {
        // We need to call this to subtract from enemySpawn value.
        rangeEnemySpawner.OnEnemyDeath();
        Destroy(gameObject);
    }
}
