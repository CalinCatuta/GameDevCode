using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Objects
    [SerializeField]
    private GameObject swordArea;
    // Components
    private PlayerMovement pm;
    private SpriteRenderer sp;
    private Animator an;
    private bool attacking = false;
    

    private void Start() {
        swordArea.SetActive(false); // Ensure AttackArea is inactive at start
        pm = GetComponent<PlayerMovement>();
        sp = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
    }
    private void Update() {
        // isRunning
        if(pm.moveDir.x != 0 ||  pm.moveDir.y != 0)
        {
            an.SetBool("isRunning", true);
            CheckDirection();
        }
        else
        {
            an.SetBool("isRunning", false);
        }

        // Attacking
        if (Input.GetKey(KeyCode.Space)) {
            Attack();
        }

    }

    // Flip the player if he is moving to Left
    void CheckDirection() {
        if(pm.moveDir.x < 0)
        {
            sp.flipX = true;
            swordArea.transform.localPosition = new Vector3(0.25f, 0, 0);
            swordArea.transform.rotation = Quaternion.Euler(0, 0, -150); // Set the desired rotation
        }
        else
        {
            sp.flipX = false;
            swordArea.transform.localPosition = new Vector3(-0.30f, -0.1f, 0);
            swordArea.transform.rotation = Quaternion.Euler(0, 0, 0); // Set the desired rotation
        }

        if (Input.GetKey(KeyCode.S)) {
            swordArea.transform.localPosition = new Vector3(-0.1f, 0.3f, 0);
            swordArea.transform.rotation = Quaternion.Euler(0, 0, -76); // Set the desired rotation
        }
        else if(Input.GetKey(KeyCode.W))
        {
            swordArea.transform.localPosition = new Vector3(0.22f, -0.3f, 0);
            swordArea.transform.rotation = Quaternion.Euler(0, 0, 100); // Set the desired rotation
        }
    }

    void Attack() {
        if (!attacking)
        {
            attacking = true;
            an.SetTrigger("Attack");
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack() {
        swordArea.SetActive(true);
        yield return new WaitForSeconds(0.5f); // Adjust the duration to match your attack animation
        swordArea.SetActive(false);
        attacking = false;
    }

}
