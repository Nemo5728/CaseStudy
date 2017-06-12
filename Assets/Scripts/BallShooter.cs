using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//2017/05/23
//玉の制御を全て玉に委ねたいので
//シューターでは玉を発射した時の速度と発射位置を決めるだけにする。
//玉側で受け取った後の挙動の制御をするため、もともと横山さんが設定した玉の挙動はBallManager.csに移しました。  門川

public class BallShooter : MonoBehaviour
{
    public GameObject redBall;     //ドロップボールのプレハブ
    public GameObject blueBall;     //ドロップボールのプレハブ
    public GameObject yellowBall;     //ドロップボールのプレハブ
    public GameObject greenBall;     //ドロップボールのプレハブ

    public float timeOut;       //目標時間
    public float apearPosX;    //ドロップボールの出現位置限界X
    public float apearPosY;    //ドロップボールの出現位置限界Y
    public float ballSpeed;     //ドロップボールの速さ

    private float timeElapsed;  //時間経過

    //試し
    public GameObject BallManager;

    //ボールの情報を入れておく
    public static GameObject[] _Ball = new GameObject[512];

    void Start ()
    {
	}

    void Update()
    {
        timeElapsed += Time.deltaTime;  //時間更新

        //if目標時間に到達したか否か
        if (timeElapsed >= timeOut)
        {
            timeElapsed = 0.0f;         //経過時間初期化

            //発射位置ランダム設定ここから
            float x = 0.0f, y = 0.0f;

            x = Random.Range(-3.0f, 3.0f);  //X座標を軸にランダム設定

            if (x > -apearPosX && x < apearPosX)
            {
                y = Random.Range(-7.0f, 7.0f);
                while (y > -apearPosY && y < apearPosY)  //指定範囲外の時ループする
                {
                    y = Random.Range(-7.0f, 7.0f);
                }
            }

            else
            {
                y = Random.Range(-7.0f, 7.0f);
                while (y < -apearPosY || y > apearPosY)  //指定範囲外の時ループする
                {
                    y = Random.Range(-7.0f, 7.0f);
                }
            }

            Vector3 pos = transform.position;
            pos.x = x;
            pos.y = y;
            transform.position = pos;
            //発射位置ランダム設定ここまで

            int color = Random.Range(0, 4);

            switch (color)
            {
                case 0:
                    GameObject redBalls = Instantiate( redBall ) as GameObject;     //ドロップボールの複製
                    redBalls.transform.position = transform.position;          //ドロップボール発射位置設定
                    redBalls.transform.parent = BallManager.transform;
                    redBalls.GetComponent<Ball>().SetColor(Ball.COLOR.RED);
                    break;

                case 1:
                    GameObject blueBalls = Instantiate(blueBall) as GameObject;     //ドロップボールの複製
                    blueBalls.transform.position = transform.position;          //ドロップボール発射位置設定
                    blueBalls.transform.parent = BallManager.transform;
                    blueBalls.GetComponent<Ball>().SetColor(Ball.COLOR.BLUE);
                    break;

                case 2:
                    GameObject yellowBalls = Instantiate(yellowBall) as GameObject;     //ドロップボールの複製
                    yellowBalls.transform.position = transform.position;          //ドロップボール発射位置設定
                    yellowBalls.transform.parent = BallManager.transform;
                    yellowBalls.GetComponent<Ball>().SetColor(Ball.COLOR.YELLOW);
                    break;

                case 3:
                    GameObject greenBalls = Instantiate(greenBall) as GameObject;     //ドロップボールの複製
                    greenBalls.transform.position = transform.position;          //ドロップボール発射位置設定
                    greenBalls.transform.parent = BallManager.transform;
                    greenBalls.GetComponent<Ball>().SetColor(Ball.COLOR.GREEN);
                    break;

			default:
				break;
            }
        }
    }

    float GetBallSpeed()
    {
        return ballSpeed;
    }
}
