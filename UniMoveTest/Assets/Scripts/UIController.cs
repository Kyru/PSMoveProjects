using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private List<GameObject> lifesP1;
    [SerializeField] private List<GameObject> lifesP2;
    private int lifeP1;
    private int lifeP2;
    [SerializeField] private Text endGameTextWhiteP1;
    [SerializeField] private Text endGameTextBlackP1;

    [SerializeField] private Text endGameTextWhiteP2;
    [SerializeField] private Text endGameTextBlackP2;


    void Awake()
    {
        Messenger<int>.AddListener(GameEvent.MINUS_LIFE, minusLife);
    }

    void Start()
    {
        lifeP1 = lifeP2 = 3;       // both player have 3 lifes

        endGameTextBlackP1.enabled = false;
        endGameTextBlackP2.enabled = false;
        endGameTextWhiteP1.enabled = false;
        endGameTextWhiteP2.enabled = false;
    }

    void minusLife(int player)
    {
        if (player == 1)
        {
            lifesP1[lifeP1 - 1].SetActive(false);       // le restamos 1 porque empieza en 0
            lifeP1--;

            if (lifeP1 == 0) gameOver(1);

        }
        else if (player == 2)
        {
            lifesP2[lifeP2 - 1].SetActive(false);
            lifeP2--;

            if (lifeP2 == 0) gameOver(2);
        }
    }

    void gameOver(int looser)
    {
        endGameTextBlackP1.enabled = true;
        endGameTextBlackP2.enabled = true;
        endGameTextWhiteP1.enabled = true;
        endGameTextWhiteP2.enabled = true;
        if (looser == 1)
        {
            endGameTextWhiteP1.text = "You Lose!";
            endGameTextBlackP1.text = "You Lose!";
            endGameTextWhiteP2.text = "You Win!";
            endGameTextBlackP2.text = "You Win!";
        }
        else if (looser == 2)
        {
            endGameTextWhiteP1.text = "You Win!";
            endGameTextBlackP1.text = "You Win!";
            endGameTextWhiteP2.text = "You Lose!";
            endGameTextBlackP2.text = "You Lose!";
        }
    }

    void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.MINUS_LIFE, minusLife);
    }
}
