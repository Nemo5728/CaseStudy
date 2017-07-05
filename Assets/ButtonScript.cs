using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    Color color;

    // Use this for initialization
    void Start () {
        Debug.Log(GetComponent<Image>().color);
        GetComponent<Image>().color = new Color( 0.0f , 0.0f , 0.0f , 0.0f );
        Debug.Log(GetComponent<Image>().color);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void bDispImageOn()
    {
        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Debug.Log("表示");
    }
    public void bDispImageOff()
    {
        GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        Debug.Log("非表示");
    }
    public void ButtonAction()
    {
        //自分の名前をポーズスクリプトに送って呼び出す。
        GameObject.Find("PauseManeger").GetComponent<PauseScript>().pauseMenuAction( this.gameObject.name );
    }
}
