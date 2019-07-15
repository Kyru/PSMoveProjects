using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerYoga : MonoBehaviour
{
    [SerializeField] GameObject calibrationText;
    [SerializeField] GameObject scoreText;

    void Awake()
    {
        Messenger.AddListener(GameEvent.START_GAME, startGame);
        Messenger<float>.AddListener(GameEvent.SEND_SCORE, gameOver);
    }
    void Start()
    {
        calibrationText.SetActive(true);
        scoreText.SetActive(false);
    }
    void startGame()
    {
        calibrationText.SetActive(false);
    }
    void gameOver(float playerScore)
    {
        scoreText.SetActive(true);
        scoreText.GetComponent<Text>().text = "Your movements were " + (int) playerScore + "% correct";
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SEND_SCORE, gameOver);
        Messenger.RemoveListener(GameEvent.START_GAME, startGame);
    }
}
