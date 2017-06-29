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

        public float _Time;

        //石川追記
        GameObject g_BGMManager;
        AudioController g_BGMControl;

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
            


            //石川追記
            g_BGMManager = GameObject.FindGameObjectWithTag("BGM");
            g_BGMControl = g_BGMManager.GetComponent<AudioController>();

            g_BGMControl.bgmPlayer("ResultScene");
            _Time = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {

            _Time += Time.deltaTime;
            if (GodTouch.GetPhase() == GodPhase.Began && _Time >= 3.0f)
            {
                Application.LoadLevel("TitleScene");
            }
        }
    }
}