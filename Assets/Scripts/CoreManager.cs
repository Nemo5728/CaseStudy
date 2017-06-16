using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches{
	public class CoreManager : MonoBehaviour {

		public GameObject prefab;

        // Use this for initialization
        void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			if(GodTouch.GetPhase() == GodPhase.Began)
			{
				GameObject bullets = Instantiate(prefab) as GameObject;
            }
        }
	}
}