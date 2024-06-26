using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    private EnemyStats enemyStats;
    private void Start() {
        enemyStats = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.currentSpeed * Time.deltaTime);
    }
}
