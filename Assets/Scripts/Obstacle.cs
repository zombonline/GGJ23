using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int waterPointsToRemove = 1;
    [SerializeField] Sprite[] possibleSprites;
    [SerializeField] float minSize = 4, maxSize = 8;
    [SerializeField] AudioClip sfx;
    [SerializeField] GameObject points;
    private void Awake()
    {
        var randomSize = Random.Range(minSize, maxSize);

        transform.localScale = new Vector2(randomSize, randomSize);
        GetComponent<SpriteRenderer>().sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
        gameObject.AddComponent<PolygonCollider2D>();
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }

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
