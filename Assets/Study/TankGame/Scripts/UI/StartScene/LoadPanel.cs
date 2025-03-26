using System.Collections;
using System.Collections.Generic;
using TankGame.AGUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace TankGame.GamePlay 
{
    public class LoadPanel : BasePanel<LoadPanel>
    {
        public CustomGUITexture bg;
        public CustomGUITexture bar;
        public CustomGUITexture load;

        public float offsetTime = 0.5f;
        public float nowTime = 0;

        private float temp;
        private float orign;

        void Start()
        {
            orign = load.guiPos.width;
            temp = load.guiPos.width * 2;
            Hide();
        }

        void Update()
        {
            nowTime += Time.deltaTime;
            if (nowTime > offsetTime)
            {
                load.guiPos.width = temp;
                load.guiPos.height = temp;
                nowTime = 0;
            }
            load.guiPos.width = orign;
            load.guiPos.height = orign;
        }
    }

}

