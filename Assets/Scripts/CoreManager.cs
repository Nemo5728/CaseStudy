using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches{
	public class CoreManager : MonoBehaviour {

		public GameObject prefab;
        public GameObject prefab2;
        private GameObject score;

        //石川追記
        GameObject g_SEManager;
        SeController g_SEControl;

        public int BonusStartScore;
        public int BonusStartTime;
        private float BonusTime;
        private int scorePool = 0;

        private enum BULLETSTATE
		{
            BONUS,
            NORMAL,
            NONE
        };

        private BULLETSTATE bulletstate;

        // Use this for initialization
        void Start () {

            //石川追記
            g_SEManager = GameObject.FindGameObjectWithTag("SE");
            g_SEControl = g_SEManager.GetComponent<SeController>();

            score = GameObject.Find("Score_Board");
            bulletstate = BULLETSTATE.NORMAL;
            BonusTime = 0;
        }
		
		// Update is called once per frame
		void Update () {
            if(BonusStartScore <= scorePool && bulletstate != BULLETSTATE.BONUS){
                bulletstate = BULLETSTATE.BONUS;
                BonusTime = BonusStartTime;
            }

            BonusTime -= Time.deltaTime;
            if(BonusTime < 0 && bulletstate != BULLETSTATE.NORMAL){
                bulletstate = BULLETSTATE.NORMAL;
                scorePool = 0;
            }

			if(GodTouch.GetPhase() == GodPhase.Began)
			{
                GameObject search = null;

                //石川追記
                g_SEControl.sePlayer("Shot");

                switch(bulletstate){
                    case BULLETSTATE.NORMAL:
                        {
							search = GameObject.FindGameObjectWithTag("bullet");
							if (search == null)
							{
								GameObject bullets = Instantiate(prefab) as GameObject;
								bullets.GetComponent<Bullet>().frontShot();
								GameObject bullets2 = Instantiate(prefab) as GameObject;
								bullets2.GetComponent<Bullet>().BackShot();
							}
                            break;
                        }
                    case BULLETSTATE.BONUS:
                        {
							GameObject bullets = Instantiate(prefab) as GameObject;
							bullets.GetComponent<Bullet>().frontShot();
							GameObject bullets2 = Instantiate(prefab) as GameObject;
							bullets2.GetComponent<Bullet>().BackShot();
                            break;
                        }
                    case BULLETSTATE.NONE:
                        {
                            break;
                        }
                }
            }
        }

        public void SetScore(int score){
            if(bulletstate != BULLETSTATE.BONUS)
                scorePool += score;
        }

        public int GetScore(){
            return scorePool;
        }
	}
}