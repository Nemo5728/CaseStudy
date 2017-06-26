﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour {

    public float startTime = 1.0f;
    public GameObject start;

    private bool startCount = false;
    private GameObject readySprite;
    private GameObject startSprite;
    private GameObject timeupSprite;

	// Use this for initialization
	void Start () {
        readySprite = GameObject.Find("ready");
        startSprite = this.transform.Find("start").gameObject;
        timeupSprite = this.transform.Find("timeup").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        startTime -= Time.deltaTime;
        if (startTime <= 0.0f && !startCount){
            readySprite.SetActive(false);
            startSprite.SetActive(true);
            startCount = true;
            startTime = 1.0f;
        }

        if (startTime <= 0.0f && startCount){
            startSprite.SetActive(false);
        }
	}

    public void SetTimeupActive(){
        timeupSprite.SetActive(true);
    }
}
