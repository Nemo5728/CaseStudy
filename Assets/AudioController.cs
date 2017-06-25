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
                    GetComponent<AudioSource>().clip = TitleBgm;
                    break;
            }
            case"TeseScene":
            {
                    GetComponent<AudioSource>().clip = GameBgm;
                    break;
            }
            case"ResultScene":
            {
                    GetComponent<AudioSource>().clip = ResultBgm;
                    break;
            }
            default:
            {
                    Debug.Log("sceneName:"+ sceneName+"_error:入っちゃいけないBGM呼び出し～");
                    break;
            }
        }
        //念のため再生処理
        GetComponent<AudioSource>().Play();
    }
}
