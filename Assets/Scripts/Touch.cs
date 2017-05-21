using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches
{
	public class Touch : MonoBehaviour {
		const float distance = 100.0f;
		Vector3 touchPos;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			touchPos = GodTouch.GetPosition();
			if( GodTouch.GetPhase() == GodPhase.Began )
			{
				Debug.Log( "x:" + touchPos.x + "y:" + touchPos.y + "z:" + touchPos.z );
				Ray ray = Camera.main.ScreenPointToRay( touchPos );
				RaycastHit hit = new RaycastHit();

				if( Physics.Raycast( ray, out hit, distance ) )
				{
					GameObject touchBall = hit.collider.gameObject;
					Ball ballComponent = touchBall.GetComponent<Ball>();
					if( ballComponent != null )
					{
						ballComponent.AfterTouch();
					}
				}
			}			
		}
	}
}
