using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ScoreDataScript : MonoBehaviour
{

    public int[] g_ScoreData;
    private string[] g_Ranking =
    {
        "hoge1\n",
        "hoge2\n",
        "hoge3\n",
        "hoge4\n",
        "hoge5\n",
    };
    public Text _socre; //今回のスコア
    void Start()
    {
        ReadScore();
        Sort(ScoreScript._score);
        SaveScore();
        _socre.text = "Your Score:" + ScoreScript._score.ToString();
    }

    void SaveScore()
    {
        for (int i = 0; i < g_ScoreData.Length; i++)
        {
            PlayerPrefs.SetInt(g_Ranking[i], g_ScoreData[i]);
        }
    }
    void ReadScore()
    {
        for (int i = 0; i < g_ScoreData.Length; i++)
        {
            g_ScoreData[i] = PlayerPrefs.GetInt(g_Ranking[i], 0);
        }
    }

    void Sort(int newScore)
    {
        int number = 5;
        for (int i = 0; i < g_ScoreData.Length; i++)
        {
            if (newScore > g_ScoreData[i])
            {
                number = i;
                break;
            }
        }
        if (number < g_ScoreData.Length)
        {
            for (int i = g_ScoreData.Length - 1; i > number; i--)
            {
                g_ScoreData[i] = g_ScoreData[i - 1];
            }
            g_ScoreData[number] = newScore;
        }
        SetRanking();
    }

    void SetRanking()
    {
        GodTouches.ResultManager Rm = GetComponent<GodTouches.ResultManager>();
        for (int i = 0; i < g_ScoreData.Length; i++)
        {
            Rm.RankingScore[i].text = g_ScoreData[i].ToString();
        }
    }
}
