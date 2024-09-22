using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace huoying2d
{
    public class Player : MonoBehaviour
    {
        private Animator ani;
        private RectTransform rectTransform;

        public float speed = 0.05f;
        public GameObject tornadePre;

        private bool isAttacking;
        private bool isCasting;

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

            if(isAttacking||isCasting) ResetMove();

            if (!isAttacking && !isCasting)
            {
                if (horizontal != 0)
                {
                    rectTransform.localScale = new Vector3(horizontal > 0 ? -1 : 1, 1, 1);
                    rectTransform.Translate(Vector3.right * horizontal * speed * Time.deltaTime); // ?这个怎么这么慢

                    ani.SetBool("IsRun", true);
                }
                else
                {
                    ani.SetBool("IsRun", false);
                }

                Vector2 dir = new Vector2(horizontal, vertical);
                rectTransform.anchoredPosition += dir * speed;

            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                ani.SetTrigger("Attack");
                isAttacking = true;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                ani.SetTrigger("Skill");
                isCasting = true;
                GameObject tornade = Instantiate(tornadePre, rectTransform);
                tornade.transform.SetParent(rectTransform, false);
            }
        }

        public void ResetMove()
        {
            isAttacking = false;
            isCasting = false;
        }
    }
}

