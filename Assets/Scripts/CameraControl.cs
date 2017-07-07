using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public bool _shake;         //揺れるフラグ
    public float _timeCnt;      //経過時間
    private Vector3 _posBak;    //ポジションのバックアップ
    public float _shakeVol;     //揺れる大きさ
    // Use this for initialization
    void Start()
    {
        _shake = false;
        _timeCnt = 0.0f;
        _posBak = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (_shake)
        {
            switch ((int)Random.Range(0, 4))
            {
                case 0:
                    transform.localPosition = new Vector3(0.0f, _shakeVol, _posBak.z);
                    break;
                case 1:
                    transform.localPosition = new Vector3(0.0f, -_shakeVol, _posBak.z);
                    break;
                case 2:
                    transform.localPosition = new Vector3(_shakeVol, 0.0f, _posBak.z);
                    break;
                case 3:
                    transform.localPosition = new Vector3(-_shakeVol, 0.0f, _posBak.z);
                    break;
                case 4:
                    transform.localPosition = new Vector3(0.0f, 0.0f, _posBak.z);
                    break;
                default:
                    break;

            }
            //経過時間加算
            _timeCnt += Time.deltaTime;

            //一定時間
            if (_timeCnt >= 0.1f)
            {
                _shake = false;
                transform.localPosition = _posBak;
            }
        }
    }

    public void ShakeCamera()
    {
        _shake = true;
        _timeCnt = 0.0f;
    }
}
