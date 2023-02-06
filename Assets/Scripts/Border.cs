using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] int waterPointsToRemove = 1;
    [SerializeField] AudioClip sfx;
    [SerializeField] GameObject points;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position, PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY));

        if (collision.CompareTag("Player"))
        {
            //stop root moving
            collision.GetComponent<Movement>().rootFinished = true;
            //disable root icon
            collision.GetComponent<SpriteRenderer>().enabled = false;
            //remove water points
            var newPoints = Instantiate(points.gameObject, collision.transform.position, Quaternion.identity);
            newPoints.GetComponentInChildren<TextMeshPro>().color = Color.red;
            newPoints.GetComponentInChildren<TextMeshPro>().text = "-1";
            Destroy(newPoints, 1f);


            FindObjectOfType<TreeGrowth>().UpdateWaterPoints(waterPointsToRemove);
            //update root switcher so player can not control root
            FindObjectOfType<RootSwitcher>().RemoveRoot(collision.GetComponent<Movement>());
        }
    }
}
