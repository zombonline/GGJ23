using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    [SerializeField] int waterPoints = 0;
    [SerializeField] int[] waterPointsNeeded;
    public int currentLevel = 0;

    SpriteRenderer treeSpriteRenderer;
    [SerializeField] Sprite deadTreeSprite;
    [SerializeField] Sprite[] treeSprites;

    float buffActiveTimer = 0f;
    [SerializeField] float buffTime;
    [SerializeField] AudioClip sfx;

    private void Awake()
    {
        treeSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartBuff()
    {
        buffActiveTimer = buffTime;
        StartCoroutine(BuffSprite());
    }
    IEnumerator BuffSprite()
    {
        while(buffActiveTimer > 0f)
        {
            buffActiveTimer -= 0.5f;
            foreach (Movement root in FindObjectsOfType<Movement>())
            {
                if (root.GetComponent<SpriteRenderer>() != null)
                {
                    root.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }

            yield return new WaitForSeconds(0.25f);

            foreach (Movement root in FindObjectsOfType<Movement>())
            {
                if (root.GetComponent<SpriteRenderer>() != null)
                {
                    root.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }



    public void UpdateWaterPoints(int amount)
    {
        if(buffActiveTimer > 0f)
        {
            waterPoints += amount;
        }
        waterPoints += amount;
        if(waterPoints < 0)
        {
            waterPoints= 0; 
        }

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
        AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position, PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY));
        waterPoints = 0;
        FindObjectOfType<CanvasController>().treeProgress.value = 0;
        currentLevel++;
        StartCoroutine(TreeChange());
        if(currentLevel == 5)
        {
            FindObjectOfType<CanvasController>().GameWin();
            FindObjectOfType<BGMManager>().FadeToWin(currentLevel - 1 );
        }
        else
        {
            FindObjectOfType<BGMManager>().SwitchTrack(currentLevel - 1, currentLevel);
        }
    }

    IEnumerator TreeChange()
    {
        float timer = 1.5f;
        yield return new WaitForSeconds(.75f);
        FindObjectOfType<CanvasController>().TreeCamButton();
        yield return new WaitForSeconds(2f);
        while (timer > 0)
        {
            timer -= 0.05f;
            treeSpriteRenderer.sprite = treeSprites[currentLevel - 1];
            yield return new WaitForSeconds(0.05f);
            timer -= 0.05f;
            treeSpriteRenderer.sprite = treeSprites[currentLevel];
            yield return new WaitForSeconds(0.05f);
        }
    }

    public float GetWaterPoints()
    {
        return waterPoints;
    }

    public float GetWaterPointsNeeded()
    {
        if(currentLevel > 4)
        {
            return 4;
        }
        else
        {
            return waterPointsNeeded[currentLevel];
        }
    }

   

}
