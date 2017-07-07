using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches
{
	public class trailEffControll : MonoBehaviour
	{
		public GameObject effect;
		private GameObject trailEff;

		// Use this for initialization
		void Start()
		{
			
		}

		// Update is called once per frame
		void Update()
		{
			// タッチを検出して動かす
			var phase = GodTouch.GetPhase();
			if (phase == GodPhase.Began)
			{
				//トリガー
				trailEff = Instantiate(effect);
				Vector3 pos = GodTouch.GetPosition();
				trailEff.transform.position = Camera.main.ScreenToWorldPoint(pos);
				Vector3 pos2 = new Vector3(trailEff.transform.position.x, trailEff.transform.position.y, 0.0f);
				trailEff.transform.position = pos2;
			}
			else if (phase == GodPhase.Moved)
			{
				Vector3 pos = GodTouch.GetPosition();
				trailEff.transform.position = Camera.main.ScreenToWorldPoint(pos);
				Vector3 pos2 = new Vector3(trailEff.transform.position.x, trailEff.transform.position.y, 0.0f);
				trailEff.transform.position = pos2;
			}
			else if (phase == GodPhase.Ended)
			{
				//リリース
				Destroy(trailEff, 3.0f);
			}
		}
	}
}
