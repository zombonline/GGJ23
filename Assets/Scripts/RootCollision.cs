using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class RootCollision : MonoBehaviour
{
    [SerializeField] int waterPointsToRemove = 1;
    [SerializeField] AudioClip sfx;
    [SerializeField] GameObject points;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position, PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY));

            var newPoints = Instantiate(points.gameObject, collision.transform.position, Quaternion.identity);
            newPoints.GetComponentInChildren<TextMeshPro>().color = Color.red;
            newPoints.GetComponentInChildren<TextMeshPro>().text = "-1";
            Destroy(newPoints, 1f);

            //stop root moving
            collision.GetComponent<Movement>().rootFinished = true;
            //disable root icon
            Destroy(collision.GetComponent<SpriteRenderer>());
            Destroy(collision.GetComponent<CircleCollider2D>());
            //remove water points
            FindObjectOfType<TreeGrowth>().UpdateWaterPoints(-waterPointsToRemove);
            //update root switcher so player can not control root
            FindObjectOfType<RootSwitcher>().RemoveRoot(collision.GetComponent<Movement>());
        }
    }
}
