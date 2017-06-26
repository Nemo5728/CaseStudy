using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreVarScript : MonoBehaviour {

    private const int widthMax = 359;
    private const int Bonus = 5000;

    private GameObject _score;
    private RectTransform rt;

	// Use this for initialization
	void Start () {
        rt = GetComponent<RectTransform>();
        _score = GameObject.Find("Core");
	}
	
	// Update is called once per frame
	void Update () {
        int score = _score.GetComponent<GodTouches.CoreManager>().GetScore();
        rt.sizeDelta = new Vector2((float)widthMax / (float)Bonus * (float)score, rt.sizeDelta.y);
	}
}
