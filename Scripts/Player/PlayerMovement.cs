using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Components
    private Rigidbody2D rb;
    private PlayerStats player;
    [HideInInspector]
    public Vector2 moveDir;

    [SerializeField]
    private string inputNameHorizontal;
    [SerializeField]
    private string inputNameVertical;


    private void Start() {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        InputManager();
    }
    private void FixedUpdate() {
        Move();
    }

    void InputManager() {

        float moveX = Input.GetAxisRaw(inputNameHorizontal);
        float moveY = Input.GetAxisRaw(inputNameVertical);

        moveDir = new Vector2(moveX, moveY).normalized;
    }
    void Move() {
        rb.velocity = new Vector2(moveDir.x * player.currentSpeed, moveDir.y * player.currentSpeed);
    }
 
}
