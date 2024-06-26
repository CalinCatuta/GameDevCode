using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour {

    // Check if the other game object has the icollectible interface
    // Triggers are non-solid objects that can be used to detect overlap without affecting the physics of the objects involved.
    // Triggers is used for Collective Items.
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out ICollectable collectible))
        {
            // If it does, call the collect method
            collectible.Collect();
        }
    }
}
