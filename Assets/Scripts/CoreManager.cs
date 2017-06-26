using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches{
	public class CoreManager : MonoBehaviour {

		public GameObject prefab;
        public GameObject prefab2;

        //石川追記
        GameObject g_SEManager;
        SeController g_SEControl;
        GameObject g_BGMManager;
        AudioController g_BGMControl;

        // Use this for initialization
        void Start () {

            //石川追記
            g_BGMManager = GameObject.FindGameObjectWithTag("BGM");
            g_BGMControl = g_BGMManager.GetComponent<AudioController>();

            g_BGMControl.bgmPlayer("TeseScene");
        }

        // Update is called once per frame

        private void Awake()
        {

        }
        void Update () {
			if(GodTouch.GetPhase() == GodPhase.Began)
			{
                GameObject bullets = Instantiate(prefab) as GameObject;
                bullets.GetComponent<Bullet>().frontShot();
                GameObject bullets2 = Instantiate(prefab) as GameObject;
                bullets2.GetComponent<Bullet>().BackShot();
            }
        }
	}
}