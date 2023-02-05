using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPocket : MonoBehaviour
{
    [SerializeField] Movement rootPointPrefab;
    [SerializeField] int waterPointsEarned = 2;
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

            //add water points (multiplier is how many active roots / 2)
            FindObjectOfType<TreeGrowth>().UpdateWaterPoints(waterPointsEarned);


            //add a new root
            if (FindObjectOfType<RootSwitcher>().roots.Count < FindObjectOfType<RootSwitcher>().maxRoots)
            {
                var randomChance = Random.Range(0, 1);
                var rotation = 0f;
                if (randomChance == 0)
                {
                    rotation = collision.transform.rotation.z + 0.45f;
                }
                else
                {
                    rotation = collision.transform.rotation.z - 0.45f;
                }
                var newRoot = Instantiate(rootPointPrefab, collision.transform.position, new Quaternion(0, 0, rotation, transform.rotation.w));
                FindObjectOfType<RootSwitcher>().AddNewRoot(newRoot);
            }

            //get rid of water pocket
            Destroy(gameObject);
        }
    }



}
