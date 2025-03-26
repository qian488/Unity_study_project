using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class Player : TankBase
    {
        public GameObject moveEffect;

        public Weapon nowWeapon;
        public Transform weaponPos;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(this.atk + " " + this.def + " " + this.nowHP + " " + this.nowWeapon.name);
            CheckMove();
            CheckRotate();
            if (Input.GetMouseButtonDown(0)) Fire();
            if (nowHP <= 0) Dead();
        }

        public override void Fire()
        {
            if(nowWeapon!=null) nowWeapon.Fire();
        }

        public override void Wound(TankBase other)
        {
            base.Wound(other);
            GamePanel.Instance.UpdateHP(maxHP,nowHP);
        }

        public override void Dead()
        {
            Time.timeScale = 0;
            LosePanel.Instance.Show();
        }

        private void CheckMove()
        {
            this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime);
        }

        private void CheckRotate()
        {
            this.transform.Rotate(Input.GetAxis("Horizontal") * Vector3.up * rotateSpeed * Time.deltaTime);
            head.transform.Rotate(Input.GetAxis("Mouse X") * Vector3.up * headRotateSpeed * Time.deltaTime);
        }

        public void ChangeWeapon(GameObject obj)
        {
            if(nowWeapon != null)
            {
                Destroy(nowWeapon.gameObject);
                nowWeapon = null;
            }

            GameObject gameObject = Instantiate(obj,weaponPos,false);
            nowWeapon = gameObject.GetComponent<Weapon>();
            nowWeapon.SetFater(this);
        }
    }
}

