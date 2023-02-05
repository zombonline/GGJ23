using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] AudioSource[] tracks;
    [SerializeField] AudioClip win, lose;
    bool introDone = false;

    float maxVolume;
    private void Awake()
    {
        maxVolume = PlayerPrefs.GetFloat(PlayerPrefKeys.MUSIC_VOLUME_KEY, 1f);

        GetComponent<AudioSource>().Play();

        foreach(AudioSource track in tracks)
        {
            track.volume = 0;
            
        }
        tracks[0].volume = maxVolume;
    }

    private void Update()
    {
        if(maxVolume != PlayerPrefs.GetFloat(PlayerPrefKeys.MUSIC_VOLUME_KEY, 1f))
        {
            foreach (AudioSource track in tracks)
            {
                if (track.volume != 0)
                {
                    track.volume = PlayerPrefs.GetFloat(PlayerPrefKeys.MUSIC_VOLUME_KEY, 1f);
                }
            }
        }
        maxVolume = PlayerPrefs.GetFloat(PlayerPrefKeys.MUSIC_VOLUME_KEY, 1f);

            maxVolume = PlayerPrefs.GetFloat(PlayerPrefKeys.MUSIC_VOLUME_KEY, 1f);
       
       if (!GetComponent<AudioSource>().isPlaying && !introDone)
       {
            introDone = true;
            foreach (AudioSource track in tracks)
            {
                track.Play();
            }
        }

       
    }

    public void UpdateVolume()
    {
        foreach(AudioSource track in tracks)
        {
            if(track.volume != 0 && track.volume != maxVolume)
            {
                track.volume = maxVolume;
            }
        }
    }

    public void SwitchTrack(int prevTrack, int nextTrack)
    {
        StartCoroutine(FadeVolumeDown(tracks[prevTrack]));
        StartCoroutine(FadeVolumeUp(tracks[nextTrack]));
    }

    public void FadeToWin(int clipPlaying)
    {
        StartCoroutine(FadeVolumeDown(tracks[clipPlaying]));
        GetComponent<AudioSource>().clip = win;
        GetComponent<AudioSource>().Play();
    }

    public void FadeToLose(int clipPlaying)
    {
        StartCoroutine(FadeVolumeDown(tracks[clipPlaying]));
        GetComponent<AudioSource>().clip = lose;
        GetComponent<AudioSource>().Play();
    }

    IEnumerator FadeVolumeDown(AudioSource track)
    {
        while(track.volume > 0)
        {
            track.volume -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator FadeVolumeUp(AudioSource track)
    {
        while(track.volume < maxVolume)
        {
            track.volume += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }


}
