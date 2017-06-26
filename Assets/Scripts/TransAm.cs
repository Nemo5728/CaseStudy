using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransAm : MonoBehaviour
{   
    //アニメーションの長さ
    private float _length;
    //経過時間
    private float _cur;

    // Use this for initialization
    void Start()
    {
        //自分のアニメーション
        Animator anim = GetComponent<Animator>();

        //アニメーションの情報
        AnimatorStateInfo infAnim = anim.GetCurrentAnimatorStateInfo(0);

        //長さ取得
        _length = infAnim.length;
        _cur = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //経過時間
        _cur += Time.deltaTime;

        //再生してから立った時間が長さ以上なら自分を削除
        if (_cur > _length)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
