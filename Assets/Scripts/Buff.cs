using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] BoxCollider2D area;
    [SerializeField] AudioClip sfx;
    [SerializeField] GameObject points;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Level"))
        {
            area = collision.GetComponent<BoxCollider2D>();
        }
        if (collision.GetComponent<WaterPocket>() || collision.GetComponent<Obstacle>() || collision.GetComponent<Buff>())
        {
            if (collision.transform != this.transform)
            {
                Debug.Log("Repositioning");
                var pos = new Vector2(Random.Range(area.bounds.center.x - (area.size.x / 2), area.bounds.center.x + (area.size.x / 2)),
                Random.Range(area.bounds.center.y - (area.size.y / 2), area.bounds.center.y + (area.size.y / 2)));

                transform.position = pos;
            }
        }

        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position, PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY));
            var newPoints = Instantiate(points.gameObject, collision.transform.position, Quaternion.identity);
            newPoints.GetComponentInChildren<TextMeshPro>().color = Color.green;
            newPoints.GetComponentInChildren<TextMeshPro>().text = "x2";
            Destroy(newPoints, 1f);

            FindObjectOfType<TreeGrowth>().StartBuff();
            //get rid of buff
            Destroy(gameObject);
        }
    }



}
