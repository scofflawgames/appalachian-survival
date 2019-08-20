using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingSounds : MonoBehaviour
{
    private AudioSource usingAudio;
    public AudioClip[] usingAudioClips;


    void Start()
    {
        usingAudio = GetComponent<AudioSource>();
    }

    public void playAudio(int audioClipID)
    {
        usingAudio.clip = usingAudioClips[audioClipID];
        usingAudio.Play();
    }
}
