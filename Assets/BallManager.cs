using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

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
                            if ( _StickBall[i].color == _StickBall[j].color )
                            {
                                //Destroy(_StickBall[i].BallObject);
                                //Destroy(_StickBall[j].BallObject);
                                //ここにレイを飛ばす処理を書くi -> jに飛ばす

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
    //消した後初期化
    public void InitStickBall(int num)
    {
        _StickBall[num].BallObject = null;
        _StickBall[num].color = Ball.COLOR.NONE;
        _StickBall[num].use = false;
        _StickBall[num].put = false;
    }

    //toからfromへ例を飛ばす
    public void RayTobasu( Vector3 from, Vector3 to)
    {
        //レイを飛ばす
        Ray ray = new Ray(from, to);

        Vector3 Length = from - to;

        //Rayの飛ばせる距離
        int distance = (int)Mathf.Sqrt(Length.x * Length.x + Length.y * Length.y);
        
    }
}

