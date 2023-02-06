using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAudio : MonoBehaviour
{
    
    public void ParentAudio()
    {
        foreach(AudioSource source in FindObjectsOfType<AudioSource>())
        {
            if(source.gameObject.name == "One shot audio" && source.transform.parent != this.transform)
            {
                source.transform.parent = this.transform;
            }

        }
    }
}
