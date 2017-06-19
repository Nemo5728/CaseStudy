using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches{
	public class CoreManager : MonoBehaviour {

		public GameObject prefab;
        public GameObject prefab2;

        // Use this for initialization
        void Start () {
			
		}
		
		// Update is called once per frame
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