using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expControll : MonoBehaviour
{

    private int timeCnt;

    // Use this for initialization
    void Start()
    {
        timeCnt = -1;
    }

    public void Set(Vector3 pos)
    {
        transform.position = pos;

    }

    // Update is called once per frame
    void Update()
    {
        timeCnt++;
        if (timeCnt > 60)
        {
            Destroy(gameObject);
        }

    }
}
