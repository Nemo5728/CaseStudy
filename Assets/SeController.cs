using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeController : MonoBehaviour
{

    public AudioClip ShotSe;
    public AudioClip BallDeleteSe;
    public AudioClip StartSe;
    public AudioClip HitSe;
    public AudioClip PowerUpSe;
    public AudioClip PowerDownSe;

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
                    break;
            }
            case "BallDelete":
            {
                    GetComponent<AudioSource>().PlayOneShot(BallDeleteSe,0.5f);
                    break;
            }
            case "Hit":
            {
                    GetComponent<AudioSource>().PlayOneShot(HitSe);
                    break;
            }
            case "Start":
            {
                    GetComponent<AudioSource>().PlayOneShot(StartSe);
                    break;
            }
            case "PowerUpSe":
            {
                    GetComponent<AudioSource>().PlayOneShot(PowerUpSe);
                    break;
            }
            case "PowerDownSe":
            {
                    GetComponent<AudioSource>().PlayOneShot(PowerDownSe);
                    break;
            }   
        }
    }
}
