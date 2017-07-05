using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CregitScript : MonoBehaviour {

    GameObject Cregit;
    GameObject StartButton;

    // Use this for initialization
    void Start () {
        Cregit = GameObject.Find("Cregit");
        StartButton = GameObject.Find("StartButton");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CregitOff()
    {
        Cregit.SetActive(false);
        StartButton.SetActive(true);
    }
}
