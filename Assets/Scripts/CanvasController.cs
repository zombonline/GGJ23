using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Slider treeProgress;
    [SerializeField] Animator cameraState;
    public GameObject rootCamButton;
    bool gamePaused = false;

    [SerializeField] RectTransform onScreen, offScreen, resultPos;

    [SerializeField] GameObject settingsMenu, gameOverMenu, gameWinMenu, gameUI;

    [SerializeField] TextMeshProUGUI timerText, metreText;
    int mins;
    float seconds;

    public float metres;

    [SerializeField] GameObject gameOverText, gameWinText;


    private void Update()
    {
        seconds += Time.deltaTime;
        if(seconds >= 59.95f)
        {
            mins++;
            seconds = 0;
        }
        timerText.text = mins.ToString("00") + ":" + seconds.ToString("00");

        metreText.text = metres.ToString("00.00") + "m";

        treeProgress.value = FindObjectOfType<TreeGrowth>().GetWaterPoints() / FindObjectOfType<TreeGrowth>().GetWaterPointsNeeded();
    }

    public void SettingsButton()
    {
        LeanTween.move(settingsMenu, onScreen.localPosition, 0.25f);
        rootCamButton.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        LeanTween.move(settingsMenu, offScreen.localPosition, 0.25f);
        rootCamButton.SetActive(true);

    }


    public void TreeCamButton()
    {
        gamePaused = true;
        //change camera
        cameraState.SetInteger("Level", 1);

        Invoke(nameof(EnableRootCamButton), 2f);
    }
    public void RootCamButton()
    {
        cameraState.SetInteger("Level", 0);
        Invoke(nameof(DisablePauseState), 2f);
        Invoke(nameof(DisableRootCamButton), 2f);
    }

    public void GameOver()
    {
        gamePaused = true;
        //change camera
        cameraState.SetInteger("Level", 1);
        DisplayResult(gameOverText);
        gameUI.SetActive(false);
        Invoke(nameof(EnableGameOverMenu), 5f);
        
        FindObjectOfType<TreeGrowth>().KillTree();
    }

    void DisplayResult(GameObject result)
    {
        LeanTween.move(result, resultPos.localPosition, 0.25f);
    }

    public void GameWin()
    {
        gamePaused = true;
        //change camera
        cameraState.SetInteger("Level", 1);
        DisplayResult(gameWinText);
        gameUI.SetActive(false);


        Invoke(nameof(EnableGameWinMenu), 5f);
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("Game");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void EnableGameOverMenu()
    {
        LeanTween.move(gameOverMenu, onScreen.localPosition, 0.25f);
        
    }
    void EnableGameWinMenu()
    {
        LeanTween.move(gameWinMenu, onScreen.localPosition, 0.25f);
    }
    void EnableRootCamButton()
    {
        rootCamButton.SetActive(true);
    }

    void DisableRootCamButton()
    {
        rootCamButton.SetActive(false);
    }

    void DisablePauseState()
    {
        gamePaused = false;
    }

    public bool GetGamePaused()
    {
        return gamePaused;
    }

}
