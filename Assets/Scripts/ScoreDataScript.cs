using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ScoreDataScript : MonoBehaviour
{

    public int[] g_ScoreData;
    public TextAsset g_ScoreList;
    public Text _socre; //今回のスコア
    void Start()
    {
        ReadScoreData();
        Sort(Random.Range(0, 10000));
        WriteScoreData();
    }

    public void ReadScoreData()
    {
        //ReadScoreData
        for (int i = 0; i < g_ScoreData.Length; i++)
        {
            g_ScoreData[i] = 0;
        }

        char[] kugiri = { '\n' };

        g_ScoreList = Resources.Load("score1") as TextAsset;

        string[] scoreData = g_ScoreList.text.Split(kugiri);

        for (int i = 0; (i < g_ScoreData.Length) && (i < scoreData.Length); i++)
        {
            g_ScoreData[i] = int.Parse(scoreData[i]);
        }
    }

    public void WriteScoreData()
    {
        TextAsset csv = Resources.Load("score1") as TextAsset;
        StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources/" + csv.name + ".csv", false);
        sw.WriteLine(g_ScoreData[0]);
        sw.WriteLine(g_ScoreData[1]);
        sw.WriteLine(g_ScoreData[2]);
        sw.WriteLine(g_ScoreData[3]);
        sw.WriteLine(g_ScoreData[4]);
        sw.Flush();
        sw.Close();
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
