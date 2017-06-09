using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    float countTime = 125;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (countTime > 0)
        {
            countTime -= Time.deltaTime; //スタートしてからの秒数を格納
            GetComponent<Text>().text = countTime.ToString("F0");
        }
    }
}