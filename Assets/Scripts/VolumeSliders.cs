using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    [SerializeField] Slider musicSlider, sfxSlider;
    private void Awake()
    {
        musicSlider.value = PlayerPrefs.GetFloat(PlayerPrefKeys.MUSIC_VOLUME_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_VOLUME_KEY, 1f);
    }

    public void UpdateMusicVolume()
    {
        PlayerPrefs.SetFloat(PlayerPrefKeys.MUSIC_VOLUME_KEY, musicSlider.value);
    }
    public void UpdateSFXVolume()
    {
        PlayerPrefs.SetFloat(PlayerPrefKeys.SFX_VOLUME_KEY, sfxSlider.value);
    }
}
