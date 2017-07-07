using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreScript : MonoBehaviour
{
    public Image[] g_ScoreBoard;
    public Sprite[] g_ScoreNumber;

    private int g_Score = 0;
    private int scorePool = 0;

    public static int _score;

    void Start()
    {
        _score = 0;
    }

    void Update()
    {
        if (scorePool > 0)
        {
            g_Score += 50;
            scorePool -= 50;
        }

        int[] num = new int[5];

        num[0] = g_Score / 10000 % 10;
        num[1] = g_Score / 1000 % 10;
        num[2] = g_Score / 100 % 10;
        num[3] = g_Score / 10 % 10;
        num[4] = g_Score % 10;

        for (int i = 0; i < 5; i++)
        {
            g_ScoreBoard[i].sprite = g_ScoreNumber[num[i]];

        }
        _score = g_Score;
    }

    // スコア足してね～
    public void SetScore(int number)
    {

        scorePool += number;
        //g_Score += number;
    }

    public int GetScore()
    {
        return g_Score;
    }
}
