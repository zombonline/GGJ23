using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    int waterPoints = 0;
    [SerializeField] int[] waterPointsNeeded;
    int currentLevel = 0;

    SpriteRenderer treeSpriteRenderer;
    [SerializeField] Sprite[] treeSprites;

    private void Awake()
    {
        treeSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateWaterPoints(int amount)
    {
        waterPoints += waterPoints;
        if (waterPoints > waterPointsNeeded[currentLevel])
        {
            LevelUpTree();
        }
    }

    void LevelUpTree()
    {
        waterPoints = 0;
        currentLevel++;
        treeSpriteRenderer.sprite = treeSprites[currentLevel];
    }

    public int GetWaterPoints()
    {
        return waterPoints;

    }

    public int GetWaterPointsNeeded()
    {
        return waterPointsNeeded[currentLevel];
    }

   

}
