using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boltControll : MonoBehaviour {

    public GameObject bullet;
    public int reverseTime;                     //テクスチャ反転の間隔
    //private Vector3 move;                        //エフェクトのスピード //AddForceのやり方に合わせているため現在は不要
    private SpriteRenderer mainRenderer;        //SpriteRendererをいじりたい
    private int timeCnt;                        //fpsカウント

    public GameObject Core;

    // Use this for initialization
    void Start () {
        //初期化
        mainRenderer = gameObject.GetComponent<SpriteRenderer>();
        timeCnt = -1;
        transform.position = Vector3.zero;
    }


   public void SetParent(GameObject bullets) {
        transform.parent = bullets.transform;
        //回転角調整   //調整必要
        Vector3 baseVec = new Vector3(1.0f, 0.0f, 0.0f);
        Vector3 force = bullet.GetComponent<GodTouches.Bullet>().force;
        float angle = Vector3.Angle(baseVec, force);
        Quaternion changeAngle = transform.rotation;
        changeAngle.z += angle;
        transform.rotation = changeAngle;
    }


    // Update is called once per frame
    void Update () {
        //時間経過におけるテクスチャの反転
        timeCnt++;
        if (timeCnt >= reverseTime)
        {
            if (mainRenderer.flipY == true)
            {
                mainRenderer.flipY = false;
            }
            else
            {
                mainRenderer.flipY = true;
            }
            timeCnt = 0;
        }
    }
}
