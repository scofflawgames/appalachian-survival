using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSounds : MonoBehaviour
{
    private AudioSource pickUpAudio;
    public AudioClip[] pickUpAudioClips;


    void Start()
    {
        pickUpAudio = GetComponent<AudioSource>();
    }

    public void playAudio(int audioClipID)
    {
        pickUpAudio.clip = pickUpAudioClips[audioClipID];
        pickUpAudio.Play();
    }
}
