using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{

    //石川追記
    GameObject g_SEManager;
    SeController g_SEControl;

    public float fadeRate = 0.2f;

    public void LoadSceneGame()
    {

        //石川追記
        g_SEManager = GameObject.FindGameObjectWithTag("SE");
        g_SEControl = g_SEManager.GetComponent<SeController>();

        //石川追記
        g_SEControl.sePlayer("Start");

        //Application.LoadLevel("TutorialScene");
        FadeManager.Instance.LoadLevel("TutorialScene", fadeRate);
    }
}
