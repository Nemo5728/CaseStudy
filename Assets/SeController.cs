using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeController : MonoBehaviour
{

    public AudioClip ShotSe;
    public AudioClip BallDeleteSe;
    public AudioClip StartSe;
    public AudioClip HitSe;
    // Use this for initialization
    void Start()
    {
        //ゲーム起動中は親を殺さない。
        DontDestroyOnLoad(transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void sePlayer(string SeChange)
    {
        switch (SeChange)
        {
            case "Shot":
            {
                    GetComponent<AudioSource>().PlayOneShot(ShotSe,0.5f);
                    Debug.Log("弾を撃ったよ！");
                    break;
            }
            case "BallDelete":
            {
                    GetComponent<AudioSource>().PlayOneShot(BallDeleteSe,0.5f);
                    Debug.Log("弾が消えるよ！");
                    break;
            }
            case "Hit":
            {
                    GetComponent<AudioSource>().PlayOneShot(HitSe);
                    Debug.Log("弾が当たったよ！");
                    break;
            }
            case "Start":
            {
                    GetComponent<AudioSource>().PlayOneShot(StartSe);
                    Debug.Log("ゲームが始まるよ！");
                    break;
            }
            default:
            {
                    Debug.Log("SeChange:" + SeChange + "_error:入っちゃいけないSE呼び出しだよ！");
                    break;
            }
        }
    }
}
