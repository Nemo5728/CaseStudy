using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボールに当たったら消す用。主に挟んだボール同士で使う。

public class BallEraser : MonoBehaviour
{
    public GameObject _toObj;
    
    public bool _ok;

    public GameObject _ball;

    int count = 0;

    // Use this for initialization
    void Start()
    {
        _ok = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //自分を飛ばす    力　標的
    public void Shot(float rad, GameObject formobj, GameObject toobj)
    {
        Vector3 force;

        force.x = Mathf.Sin(rad) * 3.0f;
        force.y = Mathf.Cos(rad) * 3.0f;
        force.z = 0.0f;

        GetComponent<Rigidbody>().velocity = force;
        
        _toObj = toobj;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //目的地についた。
        if (other.gameObject == _toObj)
        {
            BallManager.DeleteBall(other.gameObject);
            Destroy(this.gameObject);
        }
        //途中のボールに触れた(吸い付いてるボールのみ)
        if (other.gameObject.CompareTag("Ball") && other.gameObject.GetComponent<Ball>().GetStatus() == Ball.STATUS.STICK )
        {
            BallManager.DeleteBall(other.gameObject);
        }

        if (other.gameObject.CompareTag("Frame"))
        {
            Destroy(this.gameObject);
        }
    }
    void OnDestroy()
    {
        //消える寸前にオブジェクトの数が1以下なら全てのドロップからStick状態のものだけPull状態にする。
        int ObjCount = this.transform.childCount;
        Debug.Log("objCount:" + ObjCount);

        //再度引っ張る処理
        if ( count <= 1 )
        {
            Debug.Log("引っ張る");
            BallManager.AllStickBallPull();
        }
    }
}
