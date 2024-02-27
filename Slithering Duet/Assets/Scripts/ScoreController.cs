using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score = 0;
    public bool IsPlayer1;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        RefreshUI();
    }

    public void IncreaseScore(int increaseScore)
    {
        score += increaseScore;
        RefreshUI();
    }

    public void DecreaseScore(int decreaseScore)
    {
        if (score <= 0)
        {
            score = 0;
        }
        else
        {
            score -= decreaseScore;
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        if(IsPlayer1)
        {
            scoreText.text = "Player1 Score: " + score;
        }
        else if(!IsPlayer1)
        {
            scoreText.text = "Player2 Score: " + score;
        }  
    }
}
