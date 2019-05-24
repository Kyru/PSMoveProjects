﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text lifesText;

    public int scoreVariable;
    public int lifeVariable;
    public int score;
    public int life;

    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener(GameEvent.ADD_SCORE, addScore);
        Messenger.AddListener(GameEvent.MINUS_LIFE, minusLife);

        lifesText.text = "Lifes: " + life;
        scoreText.text = "Score: " + score; 
    }

    public void addScore(){
        score = score + scoreVariable;
        scoreText.text = "Score: " + score; 
    }

    public void minusLife(){
        life = life - lifeVariable;
        if(life < 0) life = 0;
        lifesText.text = "Lifes: " + life;
    }
    
    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.ADD_SCORE, addScore);
        Messenger.RemoveListener(GameEvent.MINUS_LIFE, minusLife);
    }
}
