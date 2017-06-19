﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyText : MonoBehaviour {

    private int score;
    private int count;          //諸々の基準となるカウンタ
    private float alpha;

    public float upRate;
    public int deleteTime;
    public int fadeInTime;      //フェードインし終わる時間
    public int fadeOutTime;     //フェードアウトを始める時間

    public GameObject _ScoreCanvas;

    private enum FADE
    {
        NONE,
        IN,
        OUT
    };
    private FADE fade;

    public GameObject _hund;
    public GameObject _ten;
    public GameObject _one;

	// Use this for initialization
	void Start () {
        count = 0;
        alpha = 0.0f;
        fade = FADE.IN;

        //エラーチェック
        if (deleteTime < fadeInTime || deleteTime < fadeOutTime){
            Debug.LogError("エラー!!deleteTimeよりもfade関係のパラメタは少なくしてね！！");
        }
	}
	
	// Update is called once per frame
	void Update () {
        //ゆっくりフェードで上がってファッと消える
        transform.position += new Vector3(0.0f, upRate, 0.0f);

        //フェード制御 インスペクタで微調整してね
        if (count < fadeInTime){
            fade = FADE.IN;
        }
        else if (count > fadeOutTime){
            fade = FADE.OUT;
        }
        else{
            fade = FADE.NONE;
        }

        switch (fade){
            case FADE.IN:
                {
                    alpha += 1.0f / (float)fadeInTime;
                    break;
                }
            case FADE.OUT:
                {
                    alpha -= 1.0f / (float)fadeOutTime;
                    break;
                }
            case FADE.NONE:
                {
                    alpha = 1.0f;
                    break;
                }
            default:
                {
                    break;
                }
        };

        _hund.GetComponent<number>().SetAlpha(alpha);
        _ten.GetComponent<number>().SetAlpha(alpha);
        _one.GetComponent<number>().SetAlpha(alpha);

        //デリートタイマ
        count++;
        if(count > deleteTime){
            Destroy(gameObject);
        }
	}

    public void Create(Vector3 pos, int score){
        int hund, ten, one;
        transform.position = new Vector3(pos.x, pos.y, -5);

		hund = score / 100;
		ten = score / 10 % 10;
		one = score % 10;

		_hund.GetComponent<number>().Create(hund);
		_ten.GetComponent<number>().Create(ten);
		_one.GetComponent<number>().Create(one);

        _ScoreCanvas = GameObject.Find("Score_Board");
		_ScoreCanvas.GetComponent<ScoreScript>().SetScore(score);
    }
}
