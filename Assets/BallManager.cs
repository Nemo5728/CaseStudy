using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallManager : MonoBehaviour
{
    public GameObject _Check;     //プレハブ

    public struct BALL
    {
        public GameObject BallObject;   //ゲームオブジェクト
        public Ball.COLOR color;        //色
        public bool use;                //使ってるか
        public bool put;                //くっついた
    }

    //くっついているボールの情報を入れる
    public static BALL[] _StickBall = new BALL[512];

    // Use this for initialization
    void Start()
    {
        //初期化
        for (int i = 0; i < 512; i++)
        {
            _StickBall[i].BallObject = null;
            _StickBall[i].use = false;
            _StickBall[i].put = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            //くっついているボール
            if (_StickBall[i].use)
            {
                //くっついたばかりなら
                if (_StickBall[i].put == true)
                {
                    for (int j = 0; j < 512; j++)
                    {
                        if (_StickBall[i].use && i != j)
                        {
                            //くっついたのと他のと色を比較して同じなら
                            if (_StickBall[i].color == _StickBall[j].color)
                            {
                                //Destroy(_StickBall[i].BallObject);
                                //Destroy(_StickBall[j].BallObject);
                                //ここにレイを飛ばす処理を書くi -> jに飛ばす
                                RayTobasu(_StickBall[i].BallObject, _StickBall[j].BallObject);

                            }
                        }
                    }
                    //くっついたばかりじゃない状態にする
                    _StickBall[i].put = false;
                }
            }

        }
    }

    //くっついた弾の情報をもらってくる
    public static void SetStickBall(GameObject go, Ball.COLOR col)
    {
        for (int i = 0; i < 512; i++)
        {
            if (_StickBall[i].use == false)
            {
                _StickBall[i].BallObject = go;
                _StickBall[i].color = col;
                _StickBall[i].use = true;
                _StickBall[i].put = true;
                break;
            }

        }


    }

    public static void DeleteBall(GameObject go)
    {
        for (int i = 0; i < 512; i++)
        {
            if (go == _StickBall[i].BallObject)
            {
                InitStickBall(i);
                break;
            }
        }
    }
    //消した後初期化
    public static void InitStickBall(int num)
    {
        _StickBall[num].color = Ball.COLOR.NONE;
        _StickBall[num].use = false;
        _StickBall[num].put = false;
        Destroy(_StickBall[num].BallObject);
    }

    //toからfromへコリジョンを飛ばす
    public void RayTobasu(GameObject from, GameObject to)
    {
        //距離
        Vector3 Difference = to.transform.position - from.transform.position;

        //角度
        float fRad = Mathf.Atan2(Difference.x, Difference.y);

        //インスタンス生成
        GameObject go = Instantiate(_Check) as GameObject;
        go.transform.position = from.transform.position;

        go.GetComponent<CheckCollider>().Shot(fRad, from, to);
        
    }
}

