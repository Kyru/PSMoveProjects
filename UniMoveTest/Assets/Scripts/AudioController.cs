using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(int clipToPlay)
    {
        audioSource.clip = audioClips[clipToPlay];
        audioSource.Play();
    }

    public void SetLoop(bool isActive)
    {
        audioSource.loop = isActive;
    }

    public bool GetLoop()
    {
        return audioSource.loop;
    }

}