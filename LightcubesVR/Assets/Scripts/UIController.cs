using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text scoreTextP1;
    [SerializeField] Text lifesTextP1;
    [SerializeField] Text scoreTextP2;
    [SerializeField] Text lifesTextP2;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject calibrationText;

    public int scoreVariable;
    public int lifeVariable;
    public int scoreP1;
    public int lifeP1;
    public int scoreP2;
    public int lifeP2;

    void Awake()
    {
        Messenger.AddListener(GameEvent.ONLY_ONE, onlyOneController);
        Messenger<int>.AddListener(GameEvent.ADD_SCORE, addScore);
        Messenger<int>.AddListener(GameEvent.MINUS_LIFE, minusLife);
        Messenger.AddListener(GameEvent.START_GAME, startGame);
    }

    void Start()
    {
        lifesTextP1.text = "Lifes: " + lifeP1;
        scoreTextP1.text = "Score: " + scoreP1;
        lifesTextP2.text = "Lifes: " + lifeP2;
        scoreTextP2.text = "Score: " + scoreP2;
        calibrationText.SetActive(true);
        gameOver.SetActive(false);
    }

    public void addScore(int player)
    {
        if (player == 1)
        {
            scoreP1 = scoreP1 + scoreVariable;
            scoreTextP1.text = "Score: " + scoreP1;
        }
        else
        {
            scoreP2 = scoreP2 + scoreVariable;
            scoreTextP2.text = "Score: " + scoreP2;
        }
    }

    public void minusLife(int player)
    {
        if (player == 1)
        {
            lifeP1 = lifeP1 - lifeVariable;
            if (lifeP1 < 0) lifeP1 = 0;
            lifesTextP1.text = "Lifes: " + lifeP1;
            if (lifeP1 == 0) endGame();
        }
        else
        {
            lifeP2 = lifeP2 - lifeVariable;
            if (lifeP2 < 0) lifeP2 = 0;
            lifesTextP2.text = "Lifes: " + lifeP2;
            if (lifeP2 == 0) endGame();
        }
    }

    void endGame()
    {
        if (SceneManager.GetActiveScene().name == "SableLaserPrototipoP2")
        {
            calibrationText.SetActive(true);
            calibrationText.GetComponent<Text>().fontSize = 45;
            if (scoreP1 > scoreP2)
            {
                calibrationText.GetComponent<Text>().text = "Player 1 Wins!";
            }
            else if (scoreP1 < scoreP2)
            {
                calibrationText.GetComponent<Text>().text = "Player 2 Wins!";
            }
            else
            {
                calibrationText.GetComponent<Text>().text = "Draw";
            }
        }
        gameOver.GetComponent<Text>().text = "Game Over";
        gameOver.SetActive(true);

        Time.timeScale = 0f;
        Messenger.Broadcast(GameEvent.GAME_OVER);
    }

    void startGame()
    {
        calibrationText.SetActive(false);
    }

    void onlyOneController()
    {
        calibrationText.SetActive(true);
        calibrationText.GetComponent<Text>().fontSize = 30;
        calibrationText.GetComponent<Text>().text = "Only one controller connected, to play this game-mode please use 2 controller";

        Time.timeScale = 0f;
    }

    void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.ADD_SCORE, addScore);
        Messenger<int>.RemoveListener(GameEvent.MINUS_LIFE, minusLife);
        Messenger.RemoveListener(GameEvent.START_GAME, startGame);
        Messenger.RemoveListener(GameEvent.ONLY_ONE, onlyOneController);
    }
}
