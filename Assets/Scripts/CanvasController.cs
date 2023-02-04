using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Slider treeProgress;

    bool gamePaused = false;

    private void Update()
    {
        treeProgress.value = FindObjectOfType<TreeGrowth>().GetWaterPoints() / FindObjectOfType<TreeGrowth>().GetWaterPointsNeeded();
    }

    public void CheckTreeCamButton()
    {
        gamePaused = true;
        //change camera
    }

    public bool GetGamePaused()
    {
        return gamePaused;
    }

}
