using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestRPG
{
    public class PlayerControl : MonoBehaviour
    {
        private Animator ani;
        private RectTransform rectTransform;

        // Start is called before the first frame update
        void Start()
        {
            ani = GetComponent<Animator>();
            rectTransform = GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal != 0)
            {
                ani.SetFloat("Horizontal", horizontal);
                ani.SetFloat("Vertical", 0);
            }

            if (vertical != 0)
            {
                ani.SetFloat("Horizontal", 0);
                ani.SetFloat("Vertical", vertical);
            }

            Vector2 dir = new Vector2(horizontal, vertical);
            ani.SetFloat("Speed", dir.magnitude);
            rectTransform.anchoredPosition += dir * 0.5f;

            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }
}

