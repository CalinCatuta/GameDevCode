using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectable
{
    public int experienceGranted;

 
    // Implement the function from Interface ICollectable
    public void Collect() {
        // Select the object that have the Script PlayerStats
        PlayerStats player = FindObjectOfType<PlayerStats>();
        // Invoce the function from the Script
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }
}
