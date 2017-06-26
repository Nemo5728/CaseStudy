using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudioManager : MonoBehaviour
{
    GameObject g_BGMManager;
    AudioController g_BGMControl;

    // Use this for initialization
    void Start ()
    {
        g_BGMManager = GameObject.FindGameObjectWithTag("BGM");
        g_BGMControl = g_BGMManager.GetComponent<AudioController>();

        g_BGMControl.bgmPlayer("TitleScene");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
