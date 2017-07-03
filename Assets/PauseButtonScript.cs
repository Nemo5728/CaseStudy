using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickButton()
    {
        GameObject.Find("PauseManeger").GetComponent<PauseScript>().bPauseChange();
    }
}
