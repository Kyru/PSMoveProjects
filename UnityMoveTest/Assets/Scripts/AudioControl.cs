using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioClip loopClip;
    public AudioClip swingClip;
    public AudioClip activateClip;
    public AudioClip musicClip;
    public AudioSource swingAudioSource;
    public AudioSource activateAudioSource;
    public AudioSource loopAudioSource;
    public AudioSource musicAudioSource;

    private void Start()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, endGame);
        musicAudioSource.loop = true;
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }

    void Update()
    {

    }

    public void SwingAudioClip()
    {
        swingAudioSource.clip = swingClip;
        swingAudioSource.Play();
    }

    public void ActivateAudioClip()
    {
        activateAudioSource.clip = activateClip;
        activateAudioSource.Play();
    }

    public void LoopAudioClip()
    {
        loopAudioSource.loop = true;
        loopAudioSource.clip = loopClip;
        loopAudioSource.Play();
    }
    
    public void LoopAudioClipStop(){
        loopAudioSource.Stop();
    }
    void endGame()
    {
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, endGame);
    }
}
