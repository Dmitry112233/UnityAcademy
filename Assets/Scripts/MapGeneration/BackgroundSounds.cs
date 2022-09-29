using Assets.Scripts;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundSounds : MonoBehaviour
{
    public AudioMixerSnapshot snapshot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(MyTags.Tags.Player)) 
        {
            snapshot.TransitionTo(0.5f);
        }
    }
}
