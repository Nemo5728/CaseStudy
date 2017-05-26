using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    enum STATUS
    {
        STOP = 0,    //止まる
        MOVE,    //動く
        PULL,    //引っ張られる
        STICK    //くっつく（すいついてる感じなのでSTICKで）
    };

    STATUS status;
    STATUS oldStatus;


    private float speed;     //ドロップボールの速さ(今のところシューターから受け取り。)

    //あまり外部から取得するのは良くないけど、シューターから値を受け取りたいため。
    public GameObject BallSet;

    private Vector3 force;  //ボールを動かす力
    private Vector3 center = new Vector3( 0.0f , 0.0f , 0.0f ); //中心に向かわせるために。


    ////////////引っ張る時に使用する変数
    private Vector3 startPoint = Vector3.zero;    //現在の位置                    
    private Vector3 endPoint = Vector3.zero;      //どこから引っ張られるか
    private Vector3 pullVec = Vector3.zero;

    // Use this for initialization
    void Start () {

        //玉の速さをシューターから受け取り
        speed = BallSet.GetComponent<BallShooter>().ballSpeed;

        force = center - transform.position;            //発射ベクトル設定
        Vector3.Normalize(force);                               //ベクトルの正規化
        force *= speed;                                         //ドロップボールの速さ調整
        GetComponent<Rigidbody>().AddForce(force);              //ドロップボール発射

        status = STATUS.MOVE;
        oldStatus = STATUS.MOVE;
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch ( status )
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
                if( oldStatus != STATUS.MOVE )
                {
                    GetComponent<Rigidbody>().AddForce(force);              //ドロップボール発射
                }
                break;
            }
            case STATUS.PULL:
            {
                Debug.Log(oldStatus);
                //引っ張る方向へ力を加える
                if (oldStatus != STATUS.PULL)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    startPoint = transform.position;        //現在の位置を設定                     
                    endPoint = center;                      //引っ張られる目標地点を設定
                    pullVec = endPoint - startPoint;
                    float rot = Mathf.Sqrt( pullVec.x * pullVec.x + pullVec.y * pullVec.y );

                    //速度を均等にするために(やべぇ・・・眠すぎるのか・・・できねぇ・・・眠い。。次ここから・・・5/25)
                    pullVec.x = Mathf.Cos(Mathf.PI * rot) * 3;
                    pullVec.y = Mathf.Sin(Mathf.PI * rot) * 3;
                    pullVec.z = 0.0f;

                    GetComponent<Rigidbody>().AddForce(pullVec, ForceMode.Impulse);
                    Debug.Log("加速度" + GetComponent<Rigidbody>().velocity); 
                }
                break;
            }
            case STATUS.STICK:
            {
                break;
            }
            default:
            {
                break;
            }
        }
        oldStatus = status;
    }
    public void StatusChangePull()
    {
        status = STATUS.PULL;
    }
}
