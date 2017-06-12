using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches{
	public class CoreManager : MonoBehaviour {

		public GameObject prefab;

        //横山追記
       //public GameObject effect;               //エフェクトのオブジェクトを入れる変数
        

        // Use this for initialization
        void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			if(GodTouch.GetPhase() == GodPhase.Began)
			{
				GameObject bullets = Instantiate(prefab) as GameObject;

                //横山追記
                //GameObject go = Instantiate(effect);        //エフェクトの生成
                //go.GetComponent<boltControll>().SetParent(/* transform.position, force */ bullets);     //エフェクト生成時に親子関係形成
                                                                                                        //
            }
        }
	}
}