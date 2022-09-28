using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundSounds : MonoBehaviour
{
    public AudioMixerSnapshot snapshot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            snapshot.TransitionTo(0.5f);
        }
    }
}
