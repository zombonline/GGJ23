using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] BoxCollider2D level1, level2, level3;
    [SerializeField] GameObject obstaclePrefab, smallObstaclePrefab, waterPocketPrefab, buffPrefab, pestPrefab;
    [SerializeField] int rocksPerLevel, smallRocksPerLevel, waterPerLevel, buffsPerLevel, pestsPerLevel;

    private void Awake()
    {
        for(int i = 0; i < waterPerLevel; i++)
        {
            GenerateWaterPocket(level1);
            GenerateWaterPocket(level2);
            GenerateWaterPocket(level3);
        }
        for (int i = 0; i < buffsPerLevel; i++)
        {
            GenerateBuff(level2);
            GenerateBuff(level3);
        }
        for (int i = 0; i < rocksPerLevel; i++)
        {
            GenerateObstacle(level2);
            GenerateObstacle(level3);
        }
        for (int i = 0; i < pestsPerLevel; i++)
        {
            GeneratePest(level2);
            GeneratePest(level3);
        }
        for (int i = 0; i < smallRocksPerLevel; i++)
        {
            GenerateSmallObstacle(level1);
            GenerateSmallObstacle(level2);
            GenerateSmallObstacle(level3);
        }
    }

    void GenerateWaterPocket(BoxCollider2D area)
    {
        var pos = new Vector2(Random.Range(area.bounds.center.x - (area.size.x / 2), area.bounds.center.x + (area.size.x / 2)),
            Random.Range(area.bounds.center.y - (area.size.y / 2), area.bounds.center.y + (area.size.y / 2)));

        var newWaterPocket =  Instantiate(waterPocketPrefab, pos, transform.rotation);
    }
    public void GenerateBuff(BoxCollider2D area)
    {
        var pos = new Vector2(Random.Range(area.bounds.center.x - (area.size.x / 2), area.bounds.center.x + (area.size.x / 2)),
            Random.Range(area.bounds.center.y - (area.size.y / 2), area.bounds.center.y + (area.size.y / 2)));

        var newWaterPocket = Instantiate(buffPrefab, pos, transform.rotation);
    }

    void GenerateObstacle(BoxCollider2D area)
    {
        var pos = new Vector2(Random.Range(area.bounds.center.x - (area.size.x / 2), area.bounds.center.x + (area.size.x / 2)),
        Random.Range(area.bounds.center.y - (area.size.y / 2), area.bounds.center.y + (area.size.y / 2)));

        var newObstacle = Instantiate(obstaclePrefab, pos, transform.rotation);
    }

    void GenerateSmallObstacle(BoxCollider2D area)
    {
        var pos = new Vector2(Random.Range(area.bounds.center.x - (area.size.x / 2), area.bounds.center.x + (area.size.x / 2)),
        Random.Range(area.bounds.center.y - (area.size.y / 2), area.bounds.center.y + (area.size.y / 2)));

        var newObstacle = Instantiate(smallObstaclePrefab, pos, transform.rotation);
    }
    void GeneratePest(BoxCollider2D area)
    {
        var pos = new Vector2(Random.Range(area.bounds.center.x - (area.size.x / 2), area.bounds.center.x + (area.size.x / 2)),
        Random.Range(area.bounds.center.y - (area.size.y / 2), area.bounds.center.y + (area.size.y / 2)));

        var newObstacle = Instantiate(pestPrefab, pos, transform.rotation);
    }

}
