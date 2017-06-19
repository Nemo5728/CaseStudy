using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boltControll : MonoBehaviour {

    public GameObject bullet;
    public GameObject effect;
    private ParticleSystem par01;
    private ParticleSystem par02;
    private ParticleSystem par03;
    private ParticleSystem par04;
    public int reverseTime;                     //テクスチャ反転の間隔
    private SpriteRenderer mainRenderer;        //SpriteRendererをいじりたい
    private int timeCnt1, timeCnt2;             //fpsカウント
    private bool deleteFlag;                    //エフェクトの消滅判定
    private GameObject go;


    // Use this for initialization
    void Start () {
        //初期化
        mainRenderer = gameObject.GetComponent<SpriteRenderer>();
        timeCnt1 = timeCnt2 = -1;
        transform.position = Vector3.zero;
        deleteFlag = false;
    }


   public void SetParent(GodTouches.Bullet bullets, Vector3 force) {
        transform.parent = bullets.transform;

        //回転角調整
        Vector3 baseVec = new Vector3(1.0f, 0.0f, 0.0f);
        Vector3.Normalize(force);

        float x = force.x - baseVec.x;
        float y = force.y - baseVec.y;

        float rad = Mathf.Atan2(y, x);
        float deg = rad * Mathf.Rad2Deg;

        float changeRot = transform.rotation.z;
        changeRot += deg;
        this.transform.Rotate(new Vector3(0.0f, 0.0f, changeRot));
    }


    // Update is called once per frame
    void Update () {


        //時間経過におけるテクスチャの反転
        timeCnt1++;
        if (timeCnt1 >= reverseTime)
        {
            if (mainRenderer.flipY == true)
            {
                mainRenderer.flipY = false;
            }
            else
            {
                mainRenderer.flipY = true;
            }
            timeCnt1 = -1;
        }

        //余韻を持たせて消滅
        if (deleteFlag == true)
        {
            timeCnt2++;

            if (timeCnt2 > 60)
            {
                
                Destroy(gameObject);
                timeCnt2 = -1;
            }
        }
    }


    //消滅時の必要な値設定
    public void SetDelete()
    {
		par01 = transform.Find("par_tracks").GetComponent<ParticleSystem>();
		par01.Stop();
		par02 = transform.Find("par_spark_core").GetComponent<ParticleSystem>();
		par02.Stop();
		par03 = transform.Find("par_spark_L").GetComponent<ParticleSystem>();
		par03.Stop();
		par04 = transform.Find("par_spark_R").GetComponent<ParticleSystem>();
		par04.Stop();

		go = Instantiate(effect);
		go.GetComponent<hitControll>().SetPosition(transform.position);

		//transform.DetachChildren();	//光の護封剣発動！！かっこいいから消さないで！！


		deleteFlag = true;
        mainRenderer.enabled = false;   
    }
}
