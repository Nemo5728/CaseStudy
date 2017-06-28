using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour {
    private GameObject gameobject;

	// Use this for initialization
	void Start () {
        gameobject = GameObject.Find("Tutorial");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonPush(){
        gameobject.GetComponent<GodTouches.TutorialManager>().PrevButton();
        Debug.Log("prev");
    }
}
