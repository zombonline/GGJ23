using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] BoxCollider2D level1, level2, level3;
    [SerializeField] GameObject obstaclePrefab, waterPocketPrefab;

    private void Awake()
    {
        for(int i = 0; i < 30; i++)
        {
            GenerateWaterPockets(level1,level3);
        }
        for (int i = 0; i < 10; i++)
        {
            GenerateObstacle(level2,level3);
        }
    }

    void GenerateWaterPockets(BoxCollider2D highestLevel, BoxCollider2D lowestLevel)
    {
        var pos = new Vector2(Random.Range(level2.bounds.center.x - (level2.size.x / 2), level2.bounds.center.x + (level2.size.x / 2)),
            Random.Range(lowestLevel.bounds.center.y - (lowestLevel.size.y / 2), highestLevel.bounds.center.y + (highestLevel.size.y / 2)));

        Instantiate(waterPocketPrefab, pos, transform.rotation);
    }

    void GenerateObstacle(BoxCollider2D highestLevel, BoxCollider2D lowestLevel)
    {
        var pos = new Vector2(Random.Range(level2.bounds.center.x - (level2.size.x / 2), level2.bounds.center.x + (level2.size.x / 2)),
        Random.Range(lowestLevel.bounds.center.y - (lowestLevel.size.y / 2), highestLevel.bounds.center.y + (highestLevel.size.y / 2)));

        Instantiate(obstaclePrefab, pos, transform.rotation);
    }

}
