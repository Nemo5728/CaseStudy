using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioClip TitleBgm;
    public AudioClip GameBgm;
    public AudioClip ResultBgm;

    // Use this for initialization
    void Start ()
    {
        //ゲーム起動中は親を殺さない。
        DontDestroyOnLoad( transform.parent );
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void bgmPlayer ( string sceneName )
    {
        switch (sceneName)
        {
            case"TitleScene":
            {
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().clip = TitleBgm;
                    Debug.Log("タイトルBGM流すよ！");
                    break;
            }
            case"TeseScene":
            {
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().clip = GameBgm;
                    Debug.Log("ゲームBGM流すよ！");
                    break;
            }
            case"ResultScene":
            {
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().clip = ResultBgm;
                    Debug.Log("リザルトBGM流すよ！");
                    break;
            }
            default:
            {
                    Debug.Log("sceneName:"+ sceneName+"_error:入っちゃいけないBGM呼び出しだよ！");
                    break;
            }
        }
        //念のため再生処理
        GetComponent<AudioSource>().Play();
    }
}
