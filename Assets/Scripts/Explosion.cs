using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _time;

    public GameObject _Flytext;

    // Use this for initialization
    void Start()
    {
        _time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if(_time >= 0.2f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //ボールだったら
        //消す処理
        if (other.transform.tag == "Ball")
        {
            BallManager.DeleteBall(other.gameObject);
            GameObject go = Instantiate(_Flytext);
            go.GetComponent<FlyText>().Create(other.transform.position, (int)300);
        }

    }
}
