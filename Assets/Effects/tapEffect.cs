using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches
{
	public class tapEffect : MonoBehaviour
	{
		public GameObject effect;
		private GameObject tapEff;  

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
				tapEff = Instantiate(effect);
				Vector3 pos = GodTouch.GetPosition();
				tapEff.transform.position = Camera.main.ScreenToWorldPoint(pos);
				Vector3 pos2 = new Vector3(tapEff.transform.position.x, tapEff.transform.position.y, 0.0f);
				tapEff.transform.position = pos2;
				Destroy(tapEff, 1.0f);
			}
			else if (phase == GodPhase.Moved)
			{
			}

			else if (phase == GodPhase.Ended)
			{
				//リリース
			}
		}
	}
}
