using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] Animator ybotAnimator;
    int animationIterations;

    void Awake()
    {
        Messenger.AddListener(GameEvent.START_GAME, starGame);
    }

    void Start() {
        animationIterations = 0;
        GetComponent<Animator>().enabled = false;
        ybotAnimator.enabled = false;
    }

    void starGame()
    {
        GetComponent<Animator>().enabled = true;
        ybotAnimator.enabled = true;
    }

    void finishAnimation()
    {
        Debug.Log("finish animation");
        animationIterations++;
        if (animationIterations > 1)
        {
            GetComponent<Animator>().enabled = false;
            ybotAnimator.enabled = false;
            Messenger.Broadcast(GameEvent.GAME_OVER);
        }
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.START_GAME, starGame);
    }
}
