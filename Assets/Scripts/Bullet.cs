using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches
{
    public class Bullet : MonoBehaviour
    {
        private const float speed = 20.0f;
        private Vector3 touch;
        private Vector3 pos;
        public Vector3 force;
        private int count = 0;
        private const int deleteCnt = 50;
        private Rigidbody _rigidbody;

        //横山追記
        public GameObject effect;               //エフェクトのオブジェクトを入れる変数
        //

        // Use this for initialization
        void Start()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            touch = GodTouch.GetPosition();
            pos = Camera.main.ScreenToWorldPoint(touch);
            force = pos;
            force.z = 0.0f;
            force = force.normalized;
            force *= speed;
            GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
            _rigidbody = this.GetComponent<Rigidbody>();

            //横山追記
            GameObject go = Instantiate(effect);        //エフェクトの生成
            go.GetComponent<boltControll>().SetParent(/* transform.position, force */ this, force);     //エフェクト生成時に親子関係形成
        }

        // Update is called once per frame
        void Update()
        {
            //GetComponent<Rigidbody>().AddForce(force);
            count++;
            if (count > deleteCnt)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Ball"))
            {
                Ball ball = other.gameObject.GetComponent<Ball>();

                //Debug.Log("other:"+ other);
                //Debug.Log("-------------ball:" + ball);

                if (ball.GetStatus() == Ball.STATUS.MOVE)
                {
                    ball.StatusChangePull();
                    Destroy(gameObject);
                }
            }
        }

        //横山追記　バレットの方向ベクトル取得
        public Vector3 GetForce()
        {
            return force;
        }
        //
    }
}