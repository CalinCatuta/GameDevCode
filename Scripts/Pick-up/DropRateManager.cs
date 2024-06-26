using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropRateManager : MonoBehaviour
{

    // Making a class to specify what type of drop an Object can have.
    [System.Serializable]
   public class Drops {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }
    // Creating a list of drops where we can add more types of Drop for an Object.
    public List<Drops> drops;
    bool isQuitting = false;

    void OnApplicationQuit() { isQuitting = true; }
    // Special function OnDestroy run if the Object that have this script is destroy this will run.
    private void OnDestroy() {
        if (isQuitting) return;
        // Create a Random Runmber from 0 to 100 and when the enemy die we know the random dropRate he give us.
        float dropRoll = UnityEngine.Random.Range(0f, 100f);
        // Making a new List that contain the possible drops we can get from the Destroyed Object.
        List<Drops> possibleDrops = new List<Drops>();
        // We loop over each drop in the List.
        foreach (Drops drop in drops)
        {
            // basic drop logic if dropRoll is smaller than dropRate needet the item will be added to possibleDrops
            // problem is if dropRoll is 1 we will have all the drops in possibleDrops and we can get the basic drop not only the rare one.
            if(dropRoll <= drop.dropRate)
            {
                // If the drop pass we add them in the possibleDrops List to chose only one of them.
                // This stop the Object droping more than 1 item.
                possibleDrops.Add(drop);
            }
        }
        // if posible drops list inst't empty
        if (possibleDrops.Count > 0) {
            // Getting the drop that pass and chose to give the player only one.
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            // Spawn the drop
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
       
    }
}
