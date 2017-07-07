﻿using System.Collections;
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
        private int scorePool = 5000;
        private float timeleft;
        private float timer = 0.1f;

        public enum BULLETSTATE
        {
            BONUS,
            NORMAL,
            NONE
        };

        private BULLETSTATE bulletstate;
        public static BULLETSTATE _state;

		//揺らすやつ
		private float rightRange;
		private float leftRange;
		private float upRange;
		private float bottomRange;
		public float shakeRange;

		private bool shakeEnable = false;
		public float shakeTime;
		private float shakeCount;

		private Vector3 defaultPos;
		private float shakeX;
		private float shakeY;

        public GameObject ballManager;

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

            SetShakeRange();

            timeleft = timer;
        }

        // Update is called once per frame
        void Update()
        {
            //ボーナススタート判定
            if (BonusStartScore <= scorePool && bulletstate != BULLETSTATE.BONUS && bulletstate != BULLETSTATE.NONE )
            {
                bulletstate = BULLETSTATE.BONUS;
                BonusTime = BonusStartTime;

                //石川追記
                g_SEControl.sePlayer("PowerUpSe");

                transform.Find("CoreImage").gameObject.SetActive(false);
                transform.Find("CoreImage2").gameObject.SetActive(true);

                //トランザム生成
                GameObject TAgo = Instantiate(_transAm) as GameObject;
                TAgo.transform.position = new Vector3(0.0f,0.0f,-0.5f);
            }

            //ボーナス終了判定
            BonusTime -= Time.deltaTime;
            if (scorePool <= 0 && bulletstate != BULLETSTATE.NORMAL && bulletstate != BULLETSTATE.NONE )
            {
                bulletstate = BULLETSTATE.NORMAL;
                scorePool = 0;

                transform.Find("CoreImage").gameObject.SetActive(true);
                transform.Find("CoreImage2").gameObject.SetActive(false);

                //石川追記
                g_SEControl.sePlayer("PowerDownSe");
            }

            //ボーナス中処理
            timeleft -= Time.deltaTime;
            if (timeleft <= 0.0f && bulletstate == BULLETSTATE.BONUS)
            {
                timeleft = timer;
                scorePool -= 50;
            }

            //揺らします
			if (shakeEnable)
			{
				shakeCount -= Time.deltaTime;
				if (shakeCount <= 0)
				{
					shakeEnable = false;
					transform.position = defaultPos;
					shakeX = 0.0f;
					shakeY = 0.0f;
				}

				shakeX = Random.Range(leftRange, rightRange);
				shakeY = Random.Range(bottomRange, upRange);
				transform.position = new Vector3(shakeX, shakeY, transform.position.z);
			}

            if (GodTouch.GetPhase() == GodPhase.Began && StartManager._b)
            {
                GameObject search = null;

                switch (bulletstate)
                {
                    case BULLETSTATE.NORMAL:
                        {
                            search = GameObject.FindGameObjectWithTag("bullet");
                            if (search == null)
                            {
                                //石川追記
                                g_SEControl.sePlayer("Shot");
                                GameObject bullets = Instantiate(prefab1) as GameObject;
                                bullets.GetComponent<Bullet>().frontShot();
                                GameObject bullets2 = Instantiate(prefab1) as GameObject;
                                bullets2.GetComponent<Bullet>().BackShot();
                            }
                            break;
                        }
                    case BULLETSTATE.BONUS:
                        {
                            //石川追記
                            g_SEControl.sePlayer("Shot");
                            GameObject bullets = Instantiate(prefab1Bonus) as GameObject;
                            bullets.GetComponent<Bullet>().frontShot();
                            GameObject bullets2 = Instantiate(prefab1Bonus) as GameObject;
                            bullets2.GetComponent<Bullet>().BackShot();
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
            {
                scorePool += score;
                if(scorePool >= BonusStartScore)
                {
                    scorePool = BonusStartScore;
                }
            }
        }

        public int GetScore()
        {
            return scorePool;
        }
        public static BULLETSTATE GetState()
        {
            return _state;
        }

        //揺らすやつ
		public void ShakeCore()
		{
			//shakeEnable = true;
			shakeCount = shakeTime;
			defaultPos = transform.position;
		}

		private void SetShakeRange()
		{
			Vector3 pos = transform.position;
			rightRange = pos.x + shakeRange;
			leftRange = pos.x - shakeRange;
			upRange = pos.y + shakeRange;
			bottomRange = pos.y - shakeRange;
		}

        public bool GetShake()
        {
            return shakeEnable;
        }

		public float GetShakeX()
		{
			return shakeX;
		}

		public float GetShakeY()
		{
			return shakeY;
		}
    }
}