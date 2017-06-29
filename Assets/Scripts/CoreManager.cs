using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches
{
    public class CoreManager : MonoBehaviour
    {

        public GameObject prefab1;       //正面
        public GameObject prefab2;      //後ろ

        public GameObject prefab1Bonus;       //正面
        public GameObject prefab2Bonus;      //後ろ

        public GameObject _transAm; //トランザムプレハブ

        private GameObject score;

        //石川追記
        GameObject g_SEManager;
        SeController g_SEControl;

        GameObject g_BGMManager;
        AudioController g_BGMControl;

        public int BonusStartScore;
        public int BonusStartTime;
        private float BonusTime;
        private int scorePool = 0;
        private float timeleft = 1.0f;

        public enum BULLETSTATE
        {
            BONUS,
            NORMAL,
            NONE
        };

        private BULLETSTATE bulletstate;
        public static BULLETSTATE _state;
        // Use this for initialization
        void Start()
        {

            //石川追記
            g_SEManager = GameObject.FindGameObjectWithTag("SE");
            g_SEControl = g_SEManager.GetComponent<SeController>();

            g_BGMManager = GameObject.FindGameObjectWithTag("BGM");
            g_BGMControl = g_BGMManager.GetComponent<AudioController>();

            g_BGMControl.bgmPlayer("TeseScene");

            score = GameObject.Find("Score_Board");
            bulletstate = BULLETSTATE.NORMAL;
            BonusTime = 0;
            _state = bulletstate;
        }

        // Update is called once per frame
        void Update()
        {
            //ボーナススタート判定
            if (BonusStartScore <= scorePool && bulletstate != BULLETSTATE.BONUS)
            {
                bulletstate = BULLETSTATE.BONUS;
                BonusTime = BonusStartTime;

                //石川追記
                g_SEControl.sePlayer("PowerUpSe");

                //トランザム生成
                GameObject TAgo = Instantiate(_transAm) as GameObject;
                TAgo.transform.position = new Vector3(0.0f,0.0f,-0.5f);
            }

            //ボーナス終了判定
            BonusTime -= Time.deltaTime;
            if (BonusTime < 0 && bulletstate != BULLETSTATE.NORMAL)
            {
                bulletstate = BULLETSTATE.NORMAL;
                scorePool = 0;

                //石川追記
                g_SEControl.sePlayer("PowerDownSe");
            }

            //ボーナス中処理
            timeleft -= Time.deltaTime;
            if (timeleft <= 0.0f && bulletstate == BULLETSTATE.BONUS)
            {
                timeleft = 1.0f;
                scorePool -= BonusStartScore / BonusStartTime;
            }

            if (GodTouch.GetPhase() == GodPhase.Began)
            {
                GameObject search = null;

                switch (bulletstate)
                {
                    case BULLETSTATE.NORMAL:
                        {
                            transform.Find("CoreImage").gameObject.SetActive(true);
                            transform.Find("CoreImage2").gameObject.SetActive(false);
                            search = GameObject.FindGameObjectWithTag("bullet");
                            if (search == null)
                            {
                                GameObject bullets = Instantiate(prefab1) as GameObject;
                                bullets.GetComponent<Bullet>().frontShot();
                                GameObject bullets2 = Instantiate(prefab1) as GameObject;
                                bullets2.GetComponent<Bullet>().BackShot();


                                //石川追記
                                g_SEControl.sePlayer("Shot");
                            }
                            break;
                        }
                    case BULLETSTATE.BONUS:
                        {
                            transform.Find("CoreImage").gameObject.SetActive(false);
                            transform.Find("CoreImage2").gameObject.SetActive(true);
                            GameObject bullets = Instantiate(prefab1Bonus) as GameObject;
                            bullets.GetComponent<Bullet>().frontShot();
                            GameObject bullets2 = Instantiate(prefab1Bonus) as GameObject;
                            bullets2.GetComponent<Bullet>().BackShot();
                            //石川追記
                            g_SEControl.sePlayer("Shot");

                            break;
                        }
                    case BULLETSTATE.NONE:
                        {
                            break;
                        }
                }
            }
            _state = bulletstate;
        }

        public void SetScore(int score)
        {
            if (bulletstate != BULLETSTATE.BONUS)
                scorePool += score;
        }

        public int GetScore()
        {
            return scorePool;
        }
        public static BULLETSTATE GetState()
        {
            return _state;
        }
    }
}