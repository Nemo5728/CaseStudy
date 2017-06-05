using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollider : MonoBehaviour
{

    public GameObject _Erase;     //プレハブ

    public GameObject _toObj;
    public GameObject _fromObj;

    Vector3 _from;
    Vector3 _to;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //自分を飛ばす    力　標的
    public void Shot(float rad, GameObject fromobj, GameObject toobj)
    {
        Vector3 force;

        force.x = Mathf.Sin(rad) * 3.0f;
        force.y = Mathf.Cos(rad) * 3.0f;
        force.z = 0.0f;

        GetComponent<Rigidbody>().velocity = force;

        _fromObj = fromobj;
        _toObj = toobj;

        _from = _fromObj.transform.position;
        _to = _toObj.transform.position;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _toObj)
        {
            //距離
            Vector3 Difference = _to - _from;

            //角度
            float fRad = Mathf.Atan2(Difference.x, Difference.y);

            //インスタンス生成
            GameObject go = Instantiate(_Erase) as GameObject;
            go.transform.position = _from;

            go.GetComponent<BallEraser>().Shot(fRad, _fromObj, _toObj);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Frame"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.name == "Core")
        {
            Destroy(this.gameObject);
        }
    }
}
