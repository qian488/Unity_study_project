using UnityEngine;

namespace Bomberman
{
    public class PlayerControl : MonoBehaviour
    {
        public GameObject BombPre;
        public float cd = 2;
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(horizontal, 0, vertical);

            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(dir);
                transform.Translate(Vector3.forward * 10 * Time.deltaTime);
            }

            cd += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                if (cd > 2)
                {
                    cd = 0;
                    Instantiate(BombPre, transform.position, transform.rotation);
                }
            }
        }
    }
}

