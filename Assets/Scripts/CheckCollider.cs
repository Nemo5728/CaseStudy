﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//挟まれたどうか判別させて、中間にコアがなければ、次はBallEraserを飛ばして消す。
public class CheckCollider : MonoBehaviour
{
    public GameObject _Erase;     //プレハブ

    public GameObject _toObj;           //目的位置（同じ色のぼーる）
    public GameObject _fromObj;       //スタートの位置（自分）
    public GameObject _viaObj;          //中間経由したぼーる

    Vector3 _from;  //自分
    Vector3 _to;    //目的地

    [SerializeField]
    Vector3 _via;   //中間経由地

    //当たったボールを入れておく配列
    public GameObject[] _BallObj = new GameObject[128];

    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < 128; i++ )
        {
            _BallObj[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //自分を飛ばす    力　標的
    public void Shot(float rad, GameObject fromobj, GameObject toobj)
    {
        Vector3 force;

        force.x = Mathf.Sin(rad) * 10.0f;
        force.y = Mathf.Cos(rad) * 10.0f;
        force.z = 0.0f;

        GetComponent<Rigidbody>().velocity = force;

        _fromObj = fromobj;
        _toObj = toobj;

        _from = _fromObj.transform.position;
        _to = _toObj.transform.position;

        //中間経由地に自分自身を入れておく。
        _viaObj = fromobj;
        _via = _from;

        if (_fromObj == null)
        {
            Time.timeScale = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //他の玉に当たった時にその玉が自分とくっついているかどうか、簡易計算させる(自分自身と触れ合った時は避ける）
        if( other.gameObject.tag == "Ball" && other.gameObject != _viaObj && _viaObj != null)
        {
            /*
            Debug.Log("this:" + this);
            Debug.Log("from:" + _fromObj);
            Debug.Log("to:" + _toObj);
            Debug.Log("via:" + _viaObj);
            Debug.Log("other:" + other);
            Debug.Log("-------------------------");
            //*/
            float BallDistance = Vector3.Distance( _viaObj.transform.position , other.gameObject.transform.position );

            //ボール同士の半径よりもDistanceの方が遠かったら確実につながってないので判定をやめる。
            if( BallDistance <= (_viaObj.gameObject.GetComponent<SphereCollider>().radius * transform.localScale.x) + (other.gameObject.GetComponent<SphereCollider>().radius * transform.localScale.x))
            {
                //経由した物を記憶
                _viaObj = other.gameObject;
                //経由した場所記憶（これ必要？）
                _via = other.gameObject.transform.position;
                for (int i = 0; i < 128; i++)
                {
                    if(_BallObj[i] == null)
                    {
                        _BallObj[i] = other.gameObject;
                        break;
                    }
                }
            }
            else
            {
                //ボール同士が繋がってないから消えてやめる。
                Destroy(this.gameObject);
            }
        }

        //同じ色の玉に当たったら消える。(目的地）
        if (other.gameObject == _toObj)
        {
            //距離
            Vector3 Difference = _to - _from;

            //角度
            float fRad = Mathf.Atan2(Difference.x, Difference.y);

            //インスタンス生成
            GameObject go = Instantiate(_Erase) as GameObject;
            go.transform.position = _from;

            //消すためのコライダー発生
            go.GetComponent<BallEraser>().Shot(fRad, _fromObj, _toObj);

            //目的地までついたので消える
            Destroy(this.gameObject);
            ////ボールを消す処理
            //for(int i = 0; i < 128; i++)
            //{
            //    if(_BallObj[i] != null)
            //    {
            //        BallManager.DeleteBall(_BallObj[i]);
            //    }
            //}
            //BallManager.DeleteBall(_toObj);

            //BallManager.DeleteBall(_fromObj);

            //BallManager.AllStickBallPull();
        }

        //壁に当たったら消える。
        else if (other.gameObject.CompareTag("Frame"))
        {
            Destroy(this.gameObject);
        }

        //コアに当たったら判別をやめて消える。
        else if (other.gameObject.name == "Core")
        {
            Destroy(this.gameObject);
        }
        
    }
}
