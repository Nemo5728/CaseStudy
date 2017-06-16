using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitControll : MonoBehaviour {

	private int timeCnt;

	// Use this for initialization
	void Start () {
		timeCnt = -1;
	}
	
	// Update is called once per frame
	void Update () {
		timeCnt++;
		if(timeCnt > 60)
		{
			Destroy(gameObject);
		}
	}

	public void SetPosition(Vector3 pos)
	{
		transform.position = pos;
	}
}
