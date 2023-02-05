using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    int waterPoints = 0;
    [SerializeField] int[] waterPointsNeeded;
    public int currentLevel = 0;

    SpriteRenderer treeSpriteRenderer;
    [SerializeField] Sprite deadTreeSprite;
    [SerializeField] Sprite[] treeSprites;

    bool buffActive = false;
    [SerializeField] float buffTIme;

    private void Awake()
    {
        treeSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartBuff()
    {
        buffActive = true;
        foreach (Movement root in FindObjectsOfType<Movement>())
        {
            StartCoroutine(BuffSprite(root.GetComponent<SpriteRenderer>()));
        }
        Invoke(nameof(DisableBuff),buffTIme);
    }
    IEnumerator BuffSprite(SpriteRenderer sprite)
    {
        while(buffActive)
        {
            sprite.color = Color.green;
            yield return new WaitForSeconds(0.25f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
    }
    void DisableBuff()
    {
        buffActive = false;

    }


    public void UpdateWaterPoints(int amount)
    {
        if(buffActive)
        {
            waterPoints += amount;
        }
        waterPoints += amount;
        if (waterPoints > waterPointsNeeded[currentLevel])
        {
            LevelUpTree();
        }
    }

    public void KillTree()
    {
        treeSpriteRenderer.sprite = deadTreeSprite;
    }

    void LevelUpTree()
    {
        waterPoints = 0;
        currentLevel++;
        FindObjectOfType<BGMManager>().SwitchTrack(currentLevel - 1, currentLevel);
        treeSpriteRenderer.sprite = treeSprites[currentLevel];
        if(currentLevel == 5)
        {
            FindObjectOfType<CanvasController>().GameWin();
            FindObjectOfType<BGMManager>().FadeToWin(currentLevel);
        }
        else
        {
            FindObjectOfType<BGMManager>().SwitchTrack(currentLevel - 1, currentLevel);
        }
    }

    public float GetWaterPoints()
    {
        return waterPoints;
    }

    public float GetWaterPointsNeeded()
    {
        return waterPointsNeeded[currentLevel];
    }

   

}
