using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GodTouches
{
	public class BallTouch : Ball {
		bool touch = false;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			if( touch )
			{
				
			}
		}

		public override void AfterTouch()
		{
			touch = true;
			Vector3 vec = GodTouch.GetPosition();
			vec = GodTouch.GetPosition();
			//vec.z = 30.0f;
			vec = Camera.main.ScreenToWorldPoint( vec );
			Debug.Log( "x:" + vec.x + "y:" + vec.y + "z:" + vec.z );
		}

	}
}
