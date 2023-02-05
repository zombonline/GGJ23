using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] int waterPointsToRemove = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //stop root moving
            collision.GetComponent<Movement>().rootFinished = true;
            //disable root icon
            collision.GetComponent<SpriteRenderer>().enabled = false;
            //remove water points
            FindObjectOfType<TreeGrowth>().UpdateWaterPoints(waterPointsToRemove);
            //update root switcher so player can not control root
            FindObjectOfType<RootSwitcher>().RemoveRoot(collision.GetComponent<Movement>());
        }
    }
}
