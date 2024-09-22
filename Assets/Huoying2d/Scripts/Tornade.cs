using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace huoying2d
{
    public class Tornade : MonoBehaviour
    {
        private RectTransform rectTransform;
        public float speed = 2f;
        // Start is called before the first frame update
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            Destroy(gameObject,2f);
        }

        // Update is called once per frame
        void Update()
        {
            rectTransform.anchoredPosition += new Vector2(rectTransform.localScale.x, 0) * speed;
        }
    }
}

