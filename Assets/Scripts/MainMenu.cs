using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip confirm, cancel;

    [SerializeField] GameObject introObject, menuObject, settingsObject;
    [SerializeField] RectTransform offScreenPos, onScreenPos;

    private void Awake()
    {
    }

    public void IntroButton()
    {
        StartCoroutine(IntroCoroutine());
    }

    IEnumerator IntroCoroutine()
    {
        AudioSource.PlayClipAtPoint(confirm, Camera.main.transform.position, PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY));
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocal(introObject, offScreenPos.localPosition, 0.25f);
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocal(menuObject, onScreenPos.localPosition, 0.25f);

    }

    public void SettingsButton()
    {
        StartCoroutine(SettingsCoroutine());
    }

    public void MenuButton()
    {
        StartCoroutine(MenuCoroutine());
    }

    IEnumerator MenuCoroutine()
    {
        AudioSource.PlayClipAtPoint(confirm, Camera.main.transform.position, PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY));
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocal(settingsObject, offScreenPos.localPosition, 0.25f);
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocal(menuObject, onScreenPos.localPosition, 0.25f);

    }

    IEnumerator SettingsCoroutine()
    {
        AudioSource.PlayClipAtPoint(confirm, Camera.main.transform.position, PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY));
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocal(menuObject, offScreenPos.localPosition, 0.25f);
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocal(settingsObject, onScreenPos.localPosition, 0.25f);

    }

    public void Playbutton()
    {
        StartCoroutine(PlayCoroutine());
    }

    public void Quitbutton()
    {
        StartCoroutine(QuitCoroutine());
    }



    private IEnumerator QuitCoroutine()
    {
        AudioSource.PlayClipAtPoint(confirm, Camera.main.transform.position, PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY));
        yield return new WaitForSeconds(0.7f);
        Application.Quit();
    }
    private IEnumerator PlayCoroutine()
    {
        AudioSource.PlayClipAtPoint(confirm, Camera.main.transform.position, PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY));
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Game");
    }


}
