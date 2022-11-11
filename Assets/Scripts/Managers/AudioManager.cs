using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource shot;
    public AudioSource bullet;
    public AudioSource grenade;
    public AudioSource tennis;

    private Dictionary<string, AudioSource> audioMap;
    List<GameObject> audioGameObjects;

    private void Awake()
    {
        audioMap = new Dictionary<string, AudioSource> 
        { 
            { MyTags.AudioSourceNames.Shot, shot },
            { MyTags.AudioSourceNames.Bullet, bullet },
            { MyTags.AudioSourceNames.Grenade, grenade },
            { MyTags.AudioSourceNames.Tennis, tennis }
        };

        audioGameObjects = new List<GameObject>();
    }

    private void Update()
    {
        if(audioGameObjects.Count > 0) 
        {
            for (int i = audioGameObjects.Count - 1; i >= 0; i--) 
            {
                var go = audioGameObjects[i];
                if (!go.GetComponent<AudioSource>().isPlaying)
                {
                    audioGameObjects.RemoveAt(i);
                    Destroy(go);
                }
            }
        }
    }

    public void PlayAudio(string audioSourceName)
    {
        var audioSource = audioMap[audioSourceName];
        audioSource.Play();
    }

    public void Play3DAudio(Transform transform, string initialAudioSourceName)
    {
        var go = new GameObject("Game Object for sound");
        go.transform.position = transform.position;

        go.transform.SetParent(this.transform);

        /*var audioSource = go.AddComponent<AudioSource>().GetCopyOf(audioMap[initialAudioSourceName]);
        audioSource.Play();*/
        
        //opportunity to add component and customize via script

        UnityEditorInternal.ComponentUtility.CopyComponent(audioMap[initialAudioSourceName]);
        UnityEditorInternal.ComponentUtility.PasteComponentAsNew(go);

        var audioSource = go.GetComponent<AudioSource>();
        audioSource.Play();

        audioGameObjects.Add(go);
    }
}