using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] BoxCollider2D area;
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
            FindObjectOfType<TreeGrowth>().StartBuff();
            //get rid of buff
            Destroy(gameObject);
        }
    }



}
