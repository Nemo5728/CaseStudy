using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tenmetu : MonoBehaviour {

    float deltaTime;
    bool bDisp = true;

    public float dispTime;

    private RectTransform hoge;

    // Use this for initialization
    void Start () {
        hoge = GameObject.Find("Image").GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        deltaTime += Time.deltaTime;


        if( bDisp && deltaTime > dispTime * 0.3f)
        {
            hoge.localScale = new Vector3( 1.0f , 1.0f , 1.0f);
            bDisp = bDisp ? false : true;
            deltaTime = 0.0f;
        }
        else if( !bDisp && deltaTime > dispTime )
        {
            hoge.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            bDisp = bDisp ? false : true;
            deltaTime = 0.0f;
        }
    }
}
