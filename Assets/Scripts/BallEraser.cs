using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEraser : MonoBehaviour
{

    public GameObject _go;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //自分を飛ばす    力　標的
    public void Shot( float rad, GameObject go )
    {
        Vector3 force;

        force.x = Mathf.Sin(rad) * 3.0f;
        force.y = Mathf.Cos(rad) * 3.0f;
        force.z = 0.0f;

        GetComponent<Rigidbody>().velocity = force;

        _go = go;
    }
    private void OnTriggerEnter(Collider other)
    {
        //ボールに当たったら消す
        if (other.gameObject == _go)
        {
            BallManager.DeleteBall(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Ball"))
        {
            BallManager.DeleteBall(other.gameObject);
        }

        if (other.gameObject.CompareTag("Frame"))
        {
            BallManager.DeleteBall(other.gameObject);
        }
    }

}
