using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public void PlayAudio(AudioSource audio) 
    {
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }
}
