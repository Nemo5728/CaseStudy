using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodTouches
{
    public class Bullet : MonoBehaviour
    {
        private const float speed = 5.0f;
        private Vector3 touch;
        private Vector3 pos;
        private Vector3 force;
        private Vector3 direction;
        private int count = 0;
        private const int deleteCnt = 50;
        private Rigidbody _rigidbody;

        // Use this for initialization
        void Start()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            touch = GodTouch.GetPosition();
            pos = Camera.main.ScreenToWorldPoint(touch);
            force = pos;
            force.z = 0.0f;
            Vector3.Normalize(force);
            force *= speed;
            GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
            _rigidbody = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            //GetComponent<Rigidbody>().AddForce(force);
            Debug.Log("速度" + _rigidbody.velocity.magnitude);
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

                if(ball.GetStatus() == Ball.STATUS.MOVE)
                {
                    ball.StatusChangePull();
                    Destroy(gameObject);

                }
            }
        }
    }
}