using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum STATUS
    {
        STOP = 0,    //止まる
        MOVE,    //動く
        PULL,    //引っ張られる
        STICK,    //くっつく（すいついてる感じなのでSTICKで）
        DELETE,
    };

    public enum COLOR
    {
        NONE = 0,
        RED,
        BLUE,
        YELLOW,
        GREEN,
        PURPLE,
        OJAMA,
        BOMB,
        MAX
    };

    public float speed;
    public static int _moveBallCnt = 0;

    //自分のステータス
    public STATUS status;
    public STATUS oldStatus;
    private static float _LastDelTime = 0.0f;
    //自分の色
    public COLOR color;

    public float _magnitude;     //ドロップボールの速さ(今のところシューターから受け取り。)

    //あまり外部から取得するのは良くないけど、シューターから値を受け取りたいため。
    public GameObject BallSet;//（Setと書いてあるがシューターのスクリプト

    private Vector3 force;  //ボールを動かす力
    private Vector3 center = new Vector3(0.0f, 0.0f, 0.0f); //中心に向かわせるために。


    ////////////引っ張る時に使用する変数
    private Vector3 startPoint = Vector3.zero;    //現在の位置                    
    private Vector3 endPoint = Vector3.zero;      //どこから引っ張られるか
    private Vector3 pullVec = Vector3.zero;

    //重力
    public float gravity1 = 50;     //くっつく前の引っ張る重力
    public float gravity2 = 5000;   //後の引っ張る重力

    //横山追記
    public GameObject effect;

    public GameObject score;
    private int scoreValue = 100;

    //石川追記
    GameObject g_SEManager;
    SeController g_SEControl;

    bool Delete = false;

    float deleteCnt = 0;

    //一回だけ実行させるための糞コード
    bool bOne = true;

    GameObject ballImage;

    // Use this for initialization
    void Start()
    {
        //子のイメージを探す
        foreach (Transform child in transform)
        {
            if( child.tag == "BallImage" )
            {
                ballImage = child.gameObject;
            }
        }


        //玉の速さをシューターから受け取り
        speed = BallSet.GetComponent<BallShooter>().ballSpeed;

        force = center - transform.position;                   //発射ベクトル設定
        Vector3.Normalize(force);                               //ベクトルの正規化
        force *= speed;                                         //ドロップボールの速さ調整
        GetComponent<Rigidbody>().AddForce(force);              //ドロップボール発射

        status = STATUS.MOVE;
        oldStatus = status;
        //石川追記
        g_SEManager = GameObject.FindGameObjectWithTag("SE");
        g_SEControl = g_SEManager.GetComponent<SeController>();
        _moveBallCnt++;
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case STATUS.STOP:   //強制的に止める場合。（多分使わない）
                {
                    if (oldStatus != STATUS.STOP)
                    {
                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                    break;
                }
            case STATUS.MOVE:
                {
                    if (oldStatus != STATUS.MOVE)
                    {
                        GetComponent<Rigidbody>().AddForce(force);              //ドロップボール発射
                    }
                    //Debug.Log(GetComponent<Rigidbody>().velocity.magnitude.ToString());
                    _magnitude = GetComponent<Rigidbody>().velocity.magnitude;
                    if (GetComponent<Rigidbody>().velocity.magnitude < 5.0f && GetComponent<Rigidbody>().velocity.magnitude > 1.0f)
                    {
                        Vector3 vec = GetComponent<Rigidbody>().velocity;
                        vec.x = vec.x * 1.01f;
                        vec.y = vec.y * 1.01f;
                        vec.z = vec.z * 1.01f;
                        GetComponent<Rigidbody>().velocity = vec;
                    }
                    else if (GetComponent<Rigidbody>().velocity.magnitude > 10.0f)
                    {
                        Vector3 vec = GetComponent<Rigidbody>().velocity;
                        vec.x = vec.x * 0.99f;
                        vec.y = vec.y * 0.99f;
                        vec.z = vec.z * 0.99f;
                        GetComponent<Rigidbody>().velocity = vec;
                    }
                    if (GetComponent<Rigidbody>().velocity.magnitude < 1.0f)
                    {
                        force = new Vector3( Random.Range(0.0f,5.0f), Random.Range(0.0f, 5.0f), 0.0f);                   //発射ベクトル設定
                        Vector3.Normalize(force);                               //ベクトルの正規化
                        force *= speed;                                         //ドロップボールの速さ調整
                        GetComponent<Rigidbody>().AddForce(force);
                    }

                    //イメージの回転(イメージオブジェクトはスタートで取得)
                    ballImage.transform.Rotate(new Vector3(0, 0, 5));

                    //ボールが止まらないようにする処理をこんな感じで。
                    //if(GetComponent<Rigidbody>().velocity.x < 0.02f || GetComponent<Rigidbody>().velocity.y < 0.02f)
                    //{
                    //    Vector3 selfVelocity = GetComponent<Rigidbody>().velocity;

                    //    selfVelocity.x = 0.2f;
                    //    selfVelocity.y = 0.2f;
                    //    selfVelocity.z = 0.0f;

                    //    GetComponent<Rigidbody>().AddForce(selfVelocity , ForceMode.VelocityChange);
                    //}
                    //if (GetComponent<Rigidbody>().velocity.x < -0.02f || GetComponent<Rigidbody>().velocity.y < -0.02f)
                    //{
                    //    Vector3 selfVelocity = GetComponent<Rigidbody>().velocity;

                    //    selfVelocity.x = 0.2f;
                    //    selfVelocity.y = 0.2f;
                    //    selfVelocity.z = 0.0f;

                    //    GetComponent<Rigidbody>().AddForce(selfVelocity, ForceMode.VelocityChange);
                    //}


                    break;
                }
            case STATUS.PULL:
                {
                    if (oldStatus == STATUS.MOVE)
                    {
                        _moveBallCnt--;
                    }
                    //Debug.Log(oldStatus);
                    //引っ張る方向へ力を加える

                    //                    if (oldStatus != STATUS.PULL)
                    //                    {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    startPoint = transform.position;        //現在の位置を設定                     
                    endPoint = center;                      //引っ張られる目標地点を設定
                    pullVec = endPoint - startPoint;

                    pullVec = MoveBall(startPoint, endPoint, 10.0f);

                    GetComponent<Rigidbody>().AddForce(pullVec, ForceMode.VelocityChange);

                    //GetComponent<Rigidbody>().GetComponent<Collider>().material = (PhysicMaterial)Resources.Load("Ball Physic Material2");
                    //                    }

                    //float distance;
                    //Vector3 t1Angle;

                    //distance = Vector3.Distance(center, transform.position);
                    //t1Angle = center - transform.position;
                    //GetComponent<Rigidbody>().AddForce(t1Angle.normalized * (gravity1 / Mathf.Pow(distance, 2)));

                    break;
                }
            case STATUS.STICK:
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                   
                    //爆弾はくっついたら爆発
                    if( color == COLOR.BOMB )
                    {
                        BallManager bm = GameObject.Find("BallManager").GetComponent<BallManager>();
                        bm.SetExplosion(transform.position);
                    }
                    //フリーズ状態中も力だけは加えておく
                    //GetComponent<Rigidbody>().velocity = Vector3.zero;
                    //startPoint = transform.position;        //現在の位置を設定                     
                    //endPoint = center;                      //引っ張られる目標地点を設定
                    //pullVec = endPoint - startPoint;

                    //pullVec = MoveBall(startPoint, endPoint, 10.0f);

                    //GetComponent<Rigidbody>().AddForce(pullVec, ForceMode.VelocityChange);
                    break;
                }
            case STATUS.DELETE:
                {
                    deleteCnt += Time.deltaTime;

                    if (deleteCnt > 0.5f)
                    {
                        if (bOne)
                        {

                            GameObject.Find("Main Camera").GetComponent<CameraControl>().ShakeCamera();
                            //5秒後に消える
                            Destroy(this.gameObject);

                            //Istrrigerで判定だけなくす（本来の使い方とは違う。istrrigerはトリガー判定を使うときのみ）
                            GetComponent<SphereCollider>().isTrigger = true;

                            //ボールを見えなくする(ボールの3Dと2D）
                            GetComponent<MeshRenderer>().enabled = false;
                            GetComponent<MeshRenderer>().GetComponentInChildren<SpriteRenderer>().enabled = false;

                            //輪郭の削除
                            transform.Find("ballFixed").gameObject.SetActive(false);
                            transform.Find("ballMag").gameObject.SetActive(false);

                            //横山追記
                            GameObject go = Instantiate(effect);
                            go.GetComponent<expControll>().Set(transform.position);

                            //GameObject gobj = Instantiate(score);
                            //gobj.GetComponent<FlyText>().Create(transform.position, scoreValue);

                            //石川追記
                            g_SEControl.sePlayer("BallDelete");

                            bOne = false;

                            BallManager.bAllStickBallPull();
                        }
                    }



                    /*
                    float distance;
                    Vector3 t1Angle;

                    distance = Vector3.Distance(center, transform.position);
                    t1Angle = center - transform.position;
                    GetComponent<Rigidbody>().AddForce(t1Angle.normalized * (gravity2 / Mathf.Pow(distance, 2)));
                    //*/

                    //Destroy( this.gameObject , 5.0f);
                    break;
                }
            default:
                {
                    break;
                }
        }
        oldStatus = status;
    }

    //ステータス操作関連
    public void StatusChangePull()
    {
        //フリーズポジションを消してZだけ固定を再度実行。
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        status = STATUS.PULL;
    }

    public void StatusChangeDelete()
    {
        if (status != STATUS.DELETE)
        {
            status = STATUS.DELETE;

            ////横山追記
            //GameObject go = Instantiate(effect);
            //go.GetComponent<expControll>().Set(transform.position);

            //if( BallManager._LastDeleteTime < 0.5f )
            //{
            //    GameObject gobj = Instantiate(score);
            //    gobj.GetComponent<FlyText>().Create(transform.position, (int)((float)scoreValue * 1.5f - BallManager._LastDeleteTime));
            //}
            //else
            //{
            //    GameObject gobj = Instantiate(score);
            //    gobj.GetComponent<FlyText>().Create(transform.position, scoreValue);

            //}
            //BallManager._LastDeleteTime = 0;
            //Debug.Log("Fly");
            //石川追記
            g_SEControl.sePlayer("BallDelete");
        }
    }
    public STATUS GetStatus()
    {
        return status;
    }

    public void FreezePositionMove()
    {
        //止める
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    //fromからtoの方向へ移動する　強さ
    public Vector3 MoveBall(Vector3 from, Vector3 to, float pow)
    {
        //変数定義
        //返す値　　力
        Vector3 _force = new Vector3(0.0f, 0.0f, 0.0f);

        //距離
        Vector3 Difference = to - from;

        //角度
        float fRad = Mathf.Atan2(Difference.x, Difference.y);

        //題した角度の方向に移動する力
        _force.x = Mathf.Sin(fRad) * pow;
        _force.y = Mathf.Cos(fRad) * pow;
        _force.z = 0.0f;

        return _force;
    }

    //当たった時に呼ばれる
    void OnCollisionEnter(Collision col)
    {
        if (status == STATUS.PULL)
        {
            if (col.gameObject.name == "Core" || ColBall(col) == true)
            {
                //輪郭の表示
                transform.Find("ballFixed").gameObject.SetActive(true);
                transform.Find("ballMag").gameObject.SetActive(false);

                //状態を変更　くっついている
                status = STATUS.STICK;
                //力を０に
                GetComponent<Rigidbody>().velocity = Vector3.zero;

                ////めり込んだ分の計算
                float sinkValue = (GetComponent<SphereCollider>().radius * transform.localScale.x) + (col.gameObject.GetComponent<SphereCollider>().radius * transform.localScale.x) -
                                  (Vector3.Distance(col.transform.position, transform.position));

                float db__kakunin = GetComponent<SphereCollider>().radius * 2;

                float db__kakunin2 = Vector3.Distance(col.transform.position, transform.position);


                //方向ベクトルを求める。
                Vector3 dirVec = transform.position - col.transform.position;
                Vector3.Normalize(dirVec);        //正規化

                //現在の位置から正しいところへ
                transform.position += new Vector3(dirVec.x * sinkValue, dirVec.y * sinkValue, dirVec.z * sinkValue);

                //止める
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

                //ボールを記憶しておく配列を用意（色の数だけ）
                //各ボールのボール自身と位置を記憶
                BallManager.SetStickBall(this.gameObject, color);
				//GetComponent<Rigidbody>().mass = 500.0f;

				//コアを揺らす
				GameObject gameobject = GameObject.Find("Core");
				gameobject.GetComponent<GodTouches.CoreManager>().ShakeCore();
            }
        }
    }

    public bool ColBall(Collision col)
    {
        if (col.gameObject.CompareTag("Ball") && col.gameObject.GetComponent<Ball>().GetStatus() == STATUS.STICK)
        {
            return true;
        }
        return false;
    }

    public void SetColor(COLOR col)
    {
        color = col;
    }
}
