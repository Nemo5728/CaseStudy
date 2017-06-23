using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches
{
    public class Bullet : MonoBehaviour
    {
        private const float speed = 40.0f;
        private Vector3 touch;
        private Vector3 pos;
        public Vector3 force;
        private int count = 0;
        public int deleteCnt = 50;
        private Rigidbody _rigidbody;
        private bool used;

        //横山追記
        public GameObject effect;               //エフェクトのオブジェクトを入れる変数
        private GameObject go;
        //

        //石川追記
        GameObject g_SEManager;
        SeController g_SEControl;

        // Use this for initialization
        void Start()
        {
            //石川追記
            g_SEManager = GameObject.FindGameObjectWithTag("SE");
            g_SEControl = g_SEManager.GetComponent<SeController>();
        }

        // Update is called once per frame
        void Update()
        {
            //GetComponent<Rigidbody>().AddForce(force);

			//横山追記
            count++;
            if (count > deleteCnt)
            {
                Destroy(gameObject);
            }
			//
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

                    //横山変更　当たり判定を消し、見かけ上はまだ存在しているように見せる
                    go.GetComponent<boltControll>().SetDelete();
                    GetComponent<MeshCollider>().enabled = false;
                    //Destroy(gameObject);      //変更のためここで弾を削除させると困るのでコメントアウト
                    //

                    //石川追記
                    g_SEControl.sePlayer("Hit");
                }
            }
        }

        //横山追記　バレットの方向ベクトル取得
        public Vector3 GetForce()
        {
            return force;
        }
        //正面に発射
        public void frontShot()
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
            go = Instantiate(effect);        //エフェクトの生成
            go.GetComponent<boltControll>().SetParent(this, force);     //エフェクト生成時に親子関係形成、方向ベクトル取得
        }
        //後ろに発射
        public void BackShot()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            touch = GodTouch.GetPosition();
            pos = Camera.main.ScreenToWorldPoint(touch);
            force = pos;

            //適当に反転（横山追記
            force.x = -force.x;
            force.y = -force.y;

            force.z = 0.0f;
            force = force.normalized;
            force *= speed;

            GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);

            _rigidbody = this.GetComponent<Rigidbody>();

            //横山追記
            go = Instantiate(effect);        //エフェクトの生成
            go.GetComponent<boltControll>().SetParent(this, force);     //エフェクト生成時に親子関係形成、方向ベクトル取得
        }
    }
}