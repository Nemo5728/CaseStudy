using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TimerScript : MonoBehaviour {
	public Image[] g_TimerBoard;
	public Sprite[] g_TimerNumber;

    float g_timer = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int[] num = new int[3];

        g_timer -= Time.deltaTime;
        if(g_timer <= 0){
            //画面遷移
        }

		num[0] = (int)g_timer / 100 % 10;
		num[1] = (int)g_timer / 10 % 10;
		num[2] = (int)g_timer % 10;

		for (int i = 0; i < 3; i++)
		{
			g_TimerBoard[i].sprite = g_TimerNumber[num[i]];
		}
	}
}
