using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    // References to the players
    public Transform player1;
    public Transform player2;
    // Offset for the camera position
    public Vector3 offset;
    // Minimum size of the camera's orthographic view
    public float minSizeY = 5f;
    // Factor to increase the camera size
    public float sizeFactor = 1.5f;

    // Update is called once per frame
    void Update() {
        // Calculate the midpoint between the two players
        Vector3 midpoint = (player1.position + player2.position) / 2;
        // Update the camera position to follow the midpoint with the offset
        transform.position = midpoint + offset;

        // Calculate the distance between the two players
        float distance = Vector3.Distance(player1.position, player2.position);

        // Adjust the camera size based on the distance between players
        Camera.main.orthographicSize = Mathf.Max(minSizeY, distance * sizeFactor);
    }
}
