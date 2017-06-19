using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BackGroundScript : MonoBehaviour {

	public Image[] g_BackGroundImage;
	public float g_ScrollSpeed;
	// Use this for initialization
	void Start () {
		// 光れ我が右手
	}
	
	// Update is called once per frame
	void Update () {
		for( int o = 0 ; o < g_BackGroundImage.Length ; o++ ){
			g_BackGroundImage [o].rectTransform.anchoredPosition = new Vector2( g_BackGroundImage [o].rectTransform.anchoredPosition.x , g_BackGroundImage [o].rectTransform.anchoredPosition.y + g_BackGroundImage [o].rectTransform.sizeDelta.y / ( g_ScrollSpeed * 60 ) ) ;
			if( g_BackGroundImage [o].rectTransform.anchoredPosition.y > g_BackGroundImage [o].rectTransform.sizeDelta.y ){
				g_BackGroundImage [o].rectTransform.anchoredPosition = new Vector2( g_BackGroundImage [o].rectTransform.anchoredPosition.x ,  -g_BackGroundImage [o].rectTransform.sizeDelta.y );
			}
		}
	}
}
