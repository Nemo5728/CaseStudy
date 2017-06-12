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
        MAX
    };
    //自分のステータス
    public STATUS status;
    public STATUS oldStatus;

    //自分の色
    public COLOR color;

    private float speed;     //ドロップボールの速さ(今のところシューターから受け取り。)

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

    // Use this for initialization
    void Start()
    {
        //玉の速さをシューターから受け取り
        speed = BallSet.GetComponent<BallShooter>().ballSpeed;

        force = center - transform.position;            //発射ベクトル設定
        Vector3.Normalize(force);                               //ベクトルの正規化
        force *= speed;                                         //ドロップボールの速さ調整
        GetComponent<Rigidbody>().AddForce(force);              //ドロップボール発射

        status = STATUS.MOVE;
        oldStatus = status;
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
                    break;
                }
            case STATUS.PULL:
                {
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
                    //5秒後に消える
                    Destroy( this.gameObject, 5.0f);




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
        status = STATUS.DELETE;
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
                //状態を変更　くっついている
                status = STATUS.STICK;
                //力を０に
                GetComponent<Rigidbody>().velocity = Vector3.zero;

                ////めり込んだ分の計算
                float sinkValue = (GetComponent<SphereCollider>().radius * transform.localScale.x) + ( col.gameObject.GetComponent<SphereCollider>().radius * transform.localScale.x) -
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
                GetComponent<Rigidbody>().mass = 500.0f;
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
