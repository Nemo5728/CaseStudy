using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreScript : MonoBehaviour {
	public Image[] g_ScoreBoard;
	public Sprite[] g_ScoreNumber;

	int g_timer = 0;
	int g_Score = 0;

	void Start(){
	}

	void Update(){
		// サンプル
		g_timer++;
		if( g_timer > 123 ){
			g_Score += Random.Range ( 0 , 10000 );

			if (g_Score == 0) {
				for (int i = 0; i < g_ScoreBoard.Length; i++) {
					g_ScoreBoard [i].sprite = g_ScoreNumber [0];
				}
			} else if (g_Score > 99999) {
				for (int i = 0; i < g_ScoreBoard.Length; i++) {
					g_ScoreBoard [i].sprite = g_ScoreNumber [9];
				}
			} else {
				int score = g_Score;
				g_ScoreBoard[0].sprite = g_ScoreNumber[ ( score / 10000 ) ];
				score %= 10000;
				if (score != 0) {
					g_ScoreBoard [1].sprite = g_ScoreNumber [(score / 1000)];
					score %= 1000;
					if (score != 0) {
						g_ScoreBoard [2].sprite = g_ScoreNumber [(score / 100)];
						score %= 100;
						if (score != 0) {
							g_ScoreBoard [3].sprite = g_ScoreNumber [(score / 10)];
							score %= 10;
							if (score != 0) {
								g_ScoreBoard [4].sprite = g_ScoreNumber [(score / 1)];
							} else {
								g_ScoreBoard [4].sprite = g_ScoreNumber [0];
							}
						} else {
							g_ScoreBoard [3].sprite = g_ScoreNumber [0];
							g_ScoreBoard [4].sprite = g_ScoreNumber [0];
						}
					} else {
						g_ScoreBoard [2].sprite = g_ScoreNumber [0];
						g_ScoreBoard [3].sprite = g_ScoreNumber [0];
						g_ScoreBoard [4].sprite = g_ScoreNumber [0];
					}
				} else {
					g_ScoreBoard [1].sprite = g_ScoreNumber [0];
					g_ScoreBoard [2].sprite = g_ScoreNumber [0];
					g_ScoreBoard [3].sprite = g_ScoreNumber [0];
					g_ScoreBoard [4].sprite = g_ScoreNumber [0];
				}
			}
			g_timer = 0;
		}
	}

	// スコア足してね～
	public void SetScore( int number ){
		g_Score += number;
	}
} 
