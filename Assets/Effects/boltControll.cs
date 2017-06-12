using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boltControll : MonoBehaviour {

    public GameObject bullet;
    public int reverseTime;                     //テクスチャ反転の間隔
    private SpriteRenderer mainRenderer;        //SpriteRendererをいじりたい
    private int timeCnt;                        //fpsカウント


    // Use this for initialization
    void Start () {
        //初期化
        mainRenderer = gameObject.GetComponent<SpriteRenderer>();
        timeCnt = -1;
        transform.position = Vector3.zero;
    }


   public void SetParent(GodTouches.Bullet bullets, Vector3 force) {
        transform.parent = bullets.transform;

        //回転角調整
        Vector3 baseVec = new Vector3(1.0f, 0.0f, 0.0f);
        //Vector3 force = bullet.GetComponent<GodTouches.Bullet>().force;
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
