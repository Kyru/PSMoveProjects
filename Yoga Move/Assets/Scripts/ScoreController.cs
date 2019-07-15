using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject cubePerfect;
    Vector3 sphereCenter;
    Vector3 cubePerfectCenter;
    float totalScore;
    float playerScore;
    bool endgame;

    void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, gameOver);
    }
    void Start()
    {
        totalScore = 0;
        playerScore = 0;
        endgame = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!endgame)
        {
            totalScore = totalScore + 3;           // la puntuacion maxima para sacar le porcentaje
            Debug.Log("totalScore " + totalScore);
            sphereCenter = sphere.GetComponent<Renderer>().bounds.center;
            cubePerfectCenter = cubePerfect.GetComponent<Renderer>().bounds.center;

            Debug.Log("sphereCenter " + sphereCenter + "cubeCenter " + cubePerfectCenter);

            if (((cubePerfectCenter.x - 0.3f) < sphereCenter.x && sphereCenter.x < (cubePerfectCenter.x + 0.3f)) &&
                ((cubePerfectCenter.y - 0.3f) < sphereCenter.y && sphereCenter.x < (cubePerfectCenter.y + 0.3f)))
            {
                if (((cubePerfectCenter.x - 0.2f) < sphereCenter.x && sphereCenter.x < (cubePerfectCenter.x + 0.2f)) &&
                    ((cubePerfectCenter.y - 0.2f) < sphereCenter.y && sphereCenter.x < (cubePerfectCenter.y + 0.2f)))
                {
                    if (((cubePerfectCenter.x - 0.1f) < sphereCenter.x && sphereCenter.x < (cubePerfectCenter.x + 0.1f)) &&
                        ((cubePerfectCenter.y - 0.1f) < sphereCenter.y && sphereCenter.x < (cubePerfectCenter.y + 0.1f)))
                    {
                        playerScore = playerScore + 3;
                        Debug.Log("playerScore " + playerScore);
                    }
                    else    // condicion del 0.2
                    {
                        playerScore = playerScore + 2;
                        Debug.Log("playerScore " + playerScore);
                    }
                }
                else  // condicion del 0.3
                {
                    playerScore = playerScore + 1;
                    Debug.Log("playerScore " + playerScore);
                }
            }
            else { }  // no cumple ninguna condicion
        }
    }

    void obtainScore()
    {
        Debug.Log("playerScore " + playerScore + "totalScore " + totalScore);
        playerScore = (playerScore / totalScore) * 100;
        Debug.Log(playerScore);
        Messenger<float>.Broadcast(GameEvent.SEND_SCORE, playerScore);
    }

    void gameOver()
    {
        endgame = true;
        obtainScore();
    }

    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.GAME_OVER, gameOver);
    }
}
