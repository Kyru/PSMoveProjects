using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    bool isGameOver;

    void Awake(){
        Messenger.AddListener(GameEvent.GAME_OVER, gameOver);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isGameOver = false;
    }

    public void PlayAudioClip(int clipToPlay)
    {
        audioSource.clip = audioClips[clipToPlay];
        audioSource.Play();

        if(isGameOver) Invoke("DisableAudioSource", 2f);
    }

    void DisableAudioSource(){
        audioSource.enabled = false;
    }

    public void SetLoop(bool isActive)
    {
        audioSource.loop = isActive;
    }

    public bool GetLoop()
    {
        return audioSource.loop;
    }

    void gameOver(){
        isGameOver = true;
    }

    void OnDestroy(){
        Messenger.RemoveListener(GameEvent.GAME_OVER, gameOver);
    }
}