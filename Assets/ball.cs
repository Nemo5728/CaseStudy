using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(4.0f,3.0f,0.0f,ForceMode.Impulse);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
