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

        // Use this for initialization
        void Start () {

            //石川追記
            g_SEManager = GameObject.FindGameObjectWithTag("SE");
            g_SEControl = g_SEManager.GetComponent<SeController>();

        }
		
		// Update is called once per frame
		void Update () {
			if(GodTouch.GetPhase() == GodPhase.Began)
			{
                GameObject search = null;

                //石川追記
                g_SEControl.sePlayer("Shot");

                search = GameObject.FindGameObjectWithTag("bullet");
                if(search == null){
					GameObject bullets = Instantiate(prefab) as GameObject;
					bullets.GetComponent<Bullet>().frontShot();
					GameObject bullets2 = Instantiate(prefab) as GameObject;
					bullets2.GetComponent<Bullet>().BackShot();   
                }
            }
        }
	}
}