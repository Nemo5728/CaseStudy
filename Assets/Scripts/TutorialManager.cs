using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GodTouches{
    public class TutorialManager : MonoBehaviour{
        public Sprite[] sprite;

        private Image image;
        private GameObject gameobject;
        private int spriteCnt = 0;
        private bool touch;
        private float updateTexture;

        // Use this for initialization
        void Start(){
            gameobject = GameObject.Find("Tutorial").gameObject as GameObject;
            image = gameobject.GetComponent<Image>();
            image.sprite = sprite[spriteCnt];
            touch = false;
        }

        // Update is called once per frame
        void Update(){
            updateTexture += Time.deltaTime;
            if (updateTexture >= 0.2f)
            {
                if (spriteCnt < 9 && spriteCnt > 0)
                {
                    image.sprite = sprite[spriteCnt];
                }
                else if (spriteCnt <= 0)
                {
                    spriteCnt = 0;
                    image.sprite = sprite[0];
                }
                else if (spriteCnt >= 9)
                {
                    Application.LoadLevel("TeseScene");
                }

                touch = false;
            }

            if(GodTouch.GetPhase() == GodPhase.Began && !touch)
            {
                spriteCnt++;
                updateTexture = 0.0f;
                touch = true;
            }
        }

        public void PrevButton()
        {
            if (!touch)
            {
	            if (spriteCnt <= 1)
	            {
	                spriteCnt = -1;
	            }
	            else
	            {
	                spriteCnt -= 2;
	            }
                touch = true;
            }
        }

        public void SkipButton(){
            Application.LoadLevel("TeseScene");
        }
    }
}