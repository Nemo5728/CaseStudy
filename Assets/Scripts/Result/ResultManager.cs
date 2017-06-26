using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GodTouches
{
    public class ResultManager : MonoBehaviour
    {
        //現ゲームのスコアテキスト
        public Text Score;

        //ランキングのスコアテキスト
        public Text[] RankingScore = new Text[5];

        //Imageのサイズと位置を設定
        void SetImage(float PosX, float PosY, float Width, float Height, GameObject go)
        {
            //GameObjectのRectTransformを取得
            RectTransform rt = go.GetComponent<RectTransform>();

            //サイズ変更
            rt.sizeDelta = new Vector2(Width, Height);
            Vector2 pos = rt.localPosition;
            //位置変更
            rt.localPosition = new Vector3(PosX, PosY, 0.0f);

        }

        // Use this for initialization
        void Start()
        {
            Vector2 pos;
            Vector2 size;

            //window1の位置設定
            pos.x = 0.0f;
            pos.y = Screen.height / 10 * 1 * 4;

            size.x = Screen.width - 90;
            size.y = Screen.height / 6;

            GameObject Window01 = GameObject.Find("Window01");
            SetImage(pos.x, pos.y, size.x, size.y, Window01);

            //2
            pos.x = 0.0f;
            pos.y = 0.0f;

            size.x = Screen.width - 50;
            size.y = Screen.height / 2 + 20;

            GameObject Window02 = GameObject.Find("Window02");
            SetImage(pos.x, pos.y, size.x, size.y, Window02);
        }

        // Update is called once per frame
        void Update()
        {
            if(GodTouch.GetPhase() == GodPhase.Began){
                Application.LoadLevel("TitleScene");
            }
        }
    }
}