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
        //レイを飛ばす
        if (bRay == true)
        {
            //レイをタップされた場所に飛ばす
            Ray ray = new Ray(new Vector3(0.0f, 0.0f, 0.0f), TapPos);

            //Rayの飛ばせる距離
            int distance = (int)Mathf.Sqrt(TapPos.x * TapPos.x + TapPos.y * TapPos.y);

            //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
            Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

            //当たった分の箱
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

            //当たったやつの処理
            foreach (var obj in hits)
            {
                if (Physics.Raycast(ray.origin, ray.direction, (float)distance))
                {
                    if (obj.collider.gameObject.tag == "Ball")
                    {
                        //コメントアウト（門川)
                        //Destroy(obj.collider.gameObject);

                        //追加（門川）
                        BallManager ballmanager = obj.collider.gameObject.GetComponent<BallManager>();
                        ballmanager.StatusChangePull();

                        Debug.Log("レイが玉に当たった");
                    }
                }
            }

            bRay = false;
        }


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

