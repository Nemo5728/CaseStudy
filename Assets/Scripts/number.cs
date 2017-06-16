using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class number : MonoBehaviour {
    public Sprite[] sprite;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        //spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create(int num){
        spriteRenderer = GetComponent<SpriteRenderer>();
        //num番目のスプライトを貼る インスペクタ上で設定
        spriteRenderer.sprite = sprite[num];
    }

    public void SetAlpha(float alpha){
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
