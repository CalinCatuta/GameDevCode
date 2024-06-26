using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    EnemySpawner enemySpawner;
    [SerializeField]
    private EnemyScriptableObject enemyStats;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public float currentDamage;

    Animator animator;
    Enemy enemy;

    private void Awake() {
        animator = GetComponent<Animator>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        enemy = GetComponent<Enemy>();
        currentHealth = enemyStats.MaxHealth;
        currentSpeed = enemyStats.MoveSpeed;
        currentDamage = enemyStats.Damage;
    }
    //  Collisions involve a physical impact, meaning the objects will bounce off each other or otherwise react based on their physics properties.
    // Collisions is used for physical logic like Hitting.
    private void OnCollisionEnter2D(Collision2D collision) {
        // Reference the script from the collided collider and deal damage using TakeDamage().
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage); //Make sure to use currentDamage instead of weaponData.damage in case any damage multipliers in the future.
        }
    }
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            enemy.StopMovement(); // Stop enemy movement
            //animator.SetTrigger("Die");

            if (HasAnimatorParameter(animator, "Die"))
            {
                animator.SetTrigger("Die");
            }
            else
            {
                Kill();
            }
        }
    }
    private bool HasAnimatorParameter(Animator animator, string parameterName) {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == parameterName)
            {
                return true;
            }
        }
        return false;
    }
    void Kill() {
        enemySpawner.OnEnemyDeath();
        Destroy(gameObject);
    }
}
