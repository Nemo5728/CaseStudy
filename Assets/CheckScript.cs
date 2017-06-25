using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScript : MonoBehaviour {

    public GameObject AudioManager;

	// Use this for initialization
	void Start () {
        if (gameObject.transform.Find("/AudioManager"))
        {
            return ;
        }
        else
        {
            Instantiate(AudioManager);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
