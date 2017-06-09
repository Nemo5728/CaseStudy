using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int AddScore = 0;
    bool ScoreChecker = true;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ScoreChecker == true)
        {
            AddScore += 1000;
            GetComponent<Text>().text = AddScore.ToString("SCORE:000000");
            if (AddScore >= 999999)
            {
                AddScore = 999999;
                ScoreChecker = false;
                GetComponent<Text>().text = AddScore.ToString("SCORE:000000");
            }
        }
    }
}

