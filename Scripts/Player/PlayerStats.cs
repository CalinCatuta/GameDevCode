using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   [SerializeField]
   private CharacterScriptableObject characterData;

    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public float currentRecovery;


    // Experience and level of the player
    // Creating starting values for Player Experience System
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    // Class for defining a level range and the corresponding experience cap increase for that range.
    // Creating rules from level to level to increase the ExperienceCap after Player level up
    [System.Serializable] // Fields are visble and editable in the inspector and can be saved and loaded from files. Without this they are not visible and editable.
    public class LevelRange {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }
    //I-Frames
    // Creating a sistem to stop the Player take damage infinitly every miliseconds
    // With this will make the Player take damage only after the isInvincible is false. That helps take damage after a time setted be us (example 0.5s)
    [Header("I-Frame")]
    public float invicibilityDuration;
    float invicibilityTimer;
    bool isInvincible;

    // Creating a List of LevelRange to create more Rules over longer gameplay.
    public List<LevelRange> levelRanges;

    private void Awake() {   
        currentHealth = characterData.MaxHealth;
        currentSpeed = characterData.MoveSpeed;
        currentRecovery = characterData.Recovery;

    }
    private void Update() {

        if (invicibilityTimer > 0)
        {
            invicibilityTimer -= Time.deltaTime;
        }
        // if the invincibility timer has reached 0, set the invic flag to false
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    private void LevelUpChecker() {
        // Check if experience exceeding the experienceCap
        if (experience >= experienceCap)
        {
            // Increase level by 1
            level++;
            // reset the experience
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            // loop over all levelRange rules
            foreach (LevelRange range in levelRanges)
            {
                // if Player level is >= start level from levelRanges rule and level is <= end level
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    // became the Experience Cap from levelRanges Rule
                    experienceCapIncrease = range.experienceCapIncrease;
                }
            }
            // Increase the basic player experience Cap with basicCap + Experience Cap from the LevelRanges rule
            experienceCap += experienceCapIncrease;
        }
    }
    public void TakeDamage(float amount) {
        //If the player is not currently invincible, reduce health and start invincibility
        if (!isInvincible)
        {
            currentHealth -= amount;
            invicibilityTimer = invicibilityDuration;
            // After first hit from the Enemy make the player invulnerable for next invincibiltyTimer
            isInvincible = true;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }

    }
    public void Kill() {
        Debug.Log("Player Is Dead");
    }

    // Restore Health from Potion
    public void RestoreHealth(float amount) {
        // Heal if the player HP isn't max
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;
            // Make sure health doesn't exceed their maximum hp.
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }

    }
    // Regenerate Health over time
    void Recover() {
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            // Don't Recover over max hp
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    // Increase Exp and call LevelUpChecker() to see if we make a level up.
    public void IncreaseExperience(int amout) {
        experience += amout;
        LevelUpChecker();
    }

}
