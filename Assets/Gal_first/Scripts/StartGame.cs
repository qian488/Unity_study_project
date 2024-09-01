using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gal_first
{
    public class StartGame : MonoBehaviour
    {
        public GameObject EmpTitle;//标题父对象
        public Button button;
        // Start is called before the first frame update
        void Start()
        {
            button.onClick.AddListener(GameStart);
        }
        void GameStart()
        {
            CloseTitle();
            Gal_first_GameManger.Instance.Start();
        }

        void CloseTitle()
        {
            EmpTitle.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }
}

