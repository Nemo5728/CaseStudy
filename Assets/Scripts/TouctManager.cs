using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouctManager : MonoBehaviour
{

    //Input用の変数
    private Vector2 MousePos;
    private Vector2 ReleasePos;
    private Vector2 TapPos;

    public Vector3 WorldPos;

    //レイを飛ばすか
    private bool bRay;

    // Use this for initialization
    void Start()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(WorldPos);
        bRay = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();


    }

    void UpdateInput()
    {
        //マウスの位置
        MousePos = Input.mousePosition;
        //Debug.Log(Input.touchCount);
        //タッチ数が1以上
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //押された位置
                TapPos = touch.position;
                Debug.Log(TapPos);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                ReleasePos = touch.position;

                Debug.Log(TapPos);


                Debug.Log((TapPos - ReleasePos));
            }
        }
        //戻るボタン
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        }
        //MousePos = Input.mousePosition;
        //押されたら
        if (Input.GetMouseButtonDown(0))
        {
            //押された位置
            TapPos = Input.mousePosition;
            //ワールド座標に変換
            TapPos = Camera.main.ScreenToWorldPoint(TapPos);

          //  Debug.Log("ワールド座標" + WorldPos.ToString());
           // Debug.Log("押した" + TapPos.ToString());
            bRay = true;
        }
        //離されたら
        else if (Input.GetMouseButtonUp(0))
        {
            ReleasePos = Input.mousePosition;
            //ワールド座標に変換
            ReleasePos = Camera.main.ScreenToWorldPoint(ReleasePos);

            //Debug.Log("離した" + ReleasePos.ToString());


           // Debug.Log("差" + (TapPos - ReleasePos).ToString());

        }
    }
}

