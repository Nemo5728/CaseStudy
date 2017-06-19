using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class BallManager : MonoBehaviour
{
    public GameObject _Check;     //プレハブ(チェック用のコライダー)
    
    /// ////////////////////////テスト
    List<GameObject> colList = new List<GameObject>();

    public static Transform _Trans;
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
            InitStickBall(i);
        }
        _Trans = transform;
	}

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            //記憶されているボール（基本的にくっついた物のみ記憶しているはず）
            if (_StickBall[i].use && _StickBall[i].color != Ball.COLOR.OJAMA)
            {
                //くっついたばかりのボールかどうか？
                if (_StickBall[i].put == true)
                {
                    for (int j = 0; j < 512; j++)
                    {
                        if (_StickBall[j].use && i != j)
                        {
                            //くっついたのと他のと色を比較して同じなら
                            if (_StickBall[i].color == _StickBall[j].color)
                            {
                                //Destroy(_StickBall[i].BallObject);
                                //Destroy(_StickBall[j].BallObject);
                                //コライダーを飛ばす。
                                RayTobasu(_StickBall[i].BallObject, _StickBall[j].BallObject);
                            }
                        }
                    }
                    //くっついたばかりじゃない状態にする
                    _StickBall[i].put = false;
                }
            }
        }
        _Trans = transform;
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
                DeleteStickBall(i);
                break;
            }
        }
    }

    //くっついていた玉を再度引っ張られる状態に
    public static void AllStickBallPull()
    {
        int count = 0;
        bool init = true;
        //子供を検索
        foreach (Transform child in _Trans)
        {
            //子供もプルにする
            if( child.GetComponent<Ball>().GetStatus() == Ball.STATUS.STICK )
            {
                child.GetComponent<Ball>().StatusChangePull();
            }
            //離れたから全部初期化
            if(init)
            {
                for (int i = 0; i < 512; i++)
                {
                    InitStickBall(i);
                }
                init = false;
            }
            count++;
        }
    }


    //消した後初期化
    public static void InitStickBall(int num)
    {
        _StickBall[num].color = Ball.COLOR.NONE;
        _StickBall[num].use = false;
        _StickBall[num].put = false;
        //Destroy(_StickBall[num].BallObject);
        //_StickBall[num].BallObject.GetComponent<SphereCollider>().isTrigger = true;
    }

    //消す予約。
    public static void DeleteStickBall(int num)
    {
        _StickBall[num].color = Ball.COLOR.NONE;
        _StickBall[num].use = false;
        _StickBall[num].put = false;

        //消える予約
        _StickBall[num].BallObject.GetComponent<Ball>().StatusChangeDelete();
        //Istrrigerで判定だけなくす（本来の使い方とは違う。istrrigerはトリガー判定を使うときのみ）
        _StickBall[num].BallObject.GetComponent<SphereCollider>().isTrigger = true;
        //ボールを見えなくする(ボールの3Dと2D）
        _StickBall[num].BallObject.GetComponent<MeshRenderer>().enabled = false;
        _StickBall[num].BallObject.GetComponent<MeshRenderer>().GetComponentInChildren<SpriteRenderer>().enabled = false;
	}

	//toからfromへコリジョンを飛ばす
	public void RayTobasu(GameObject from, GameObject to)
    {
        //Debug.Log("from:" + from);
        //Debug.Log("to:" + to);

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