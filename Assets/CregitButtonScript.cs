using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CregitButtonScript : MonoBehaviour {

    GameObject Cregit;
    GameObject StartButton;

    // Use this for initialization
    void Start () {
        Cregit = GameObject.Find("Cregit");
        StartButton = GameObject.Find("StartButton");
        Cregit.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void CregitButton()
    {
        Cregit.SetActive(true);
        StartButton.SetActive(false);
    }
}
