using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] private List<GameObject> lifesP1;
    [SerializeField] private List<GameObject> lifesP2;
    private int lifeP1;
    private int lifeP2;
    void Awake()
    {
        Messenger<int>.AddListener(GameEvent.MINUS_LIFE, minusLife);
    }

    void Start()
    {
        lifeP1 = lifeP2 = 3;       // both player have 3 lifes
        Debug.Log("lifeP1 " + lifeP1 + " lifeP2 " + lifeP2);
    }

    void minusLife(int player)
    {
        if (player == 1)
        {
            lifesP1[lifeP1 - 1].SetActive(false);       // le restamos 1 porque empieza en 0
            lifeP1--;
        }
        else if (player == 2)
        {
            lifesP2[lifeP2 - 1].SetActive(false);
            lifeP2--;
        }
    }

    void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.MINUS_LIFE, minusLife);
    }
}
