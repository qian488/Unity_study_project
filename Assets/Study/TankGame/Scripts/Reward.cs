using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankGame.Data;
using UnityEngine.Rendering;
using Unity.VisualScripting;

namespace TankGame.GamePlay
{
    enum E_Reward_Type
    {
        Weapon,
        Bullet,
        State
    }
    public class Reward : MonoBehaviour
    {
        public GameObject[] weapons;
        public GameObject[] bullets;

        public GameObject rewardEff;

        private E_Reward_Type type;

        void Start()
        {
            InitType();
            InitWeaponBullet();
            if (rewardEff == null)
            { 
                Debug.Log("缺失特效,尝试进行加载");
                PrefabManager.Instance.LoadPrefab("Study/TankGame/CltEffect.prefab", (prefab) =>
                {
                    if (prefab != null)
                    {
                        rewardEff = prefab;
                        Debug.Log("特效加载成功");
                    }
                    else
                    {
                        Debug.LogError("Failed to load reward effect prefab.");
                    }
                });
            }
        }

        public void InitType()
        {
            int idx = UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_Reward_Type)).Length);
            switch (idx)
            {
                case 0:
                    type = E_Reward_Type.Weapon;
                    break;
                case 1:
                    type = E_Reward_Type.Bullet;
                    break;
                case 2:
                    type = E_Reward_Type.State;
                    break;
            }
        }

        public void InitWeaponBullet()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].GetComponent<Bullet>() == null)
                {
                    bullets[i].AddComponent<Bullet>();

                }

                Bullet bullet = bullets[i].GetComponent<Bullet>();
                if (bullet.eff == null)
                {
                    StartCoroutine(LoadAndUsePrefab(bullet));
                }

            }

            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].GetComponent<Weapon>() == null)
                {
                    weapons[i].AddComponent<Weapon>();
                }

                Weapon weapon = weapons[i].GetComponent<Weapon>();
                Transform[] shootPoints = FindShootPoints(weapons[i].transform);
                if (shootPoints.Length > 0)
                {
                    // 设置射击位置数组
                    weapon.shootPos = new Transform[shootPoints.Length];
                    for (int j = 0; j < shootPoints.Length; j++)
                    {
                        weapon.shootPos[j] = shootPoints[j];
                    }
                }
                else
                {
                    Debug.Log("未正确设置射击位置");
                }

                if (weapon.bullet == null)
                {
                    int r = UnityEngine.Random.Range(0, bullets.Length);
                    weapon.bullet = bullets[r];
                }
                else
                {
                    Debug.Log("已经有子弹了");
                }

            }
        }

        IEnumerator LoadAndUsePrefab(Bullet bullet)
        {
            string prefabRelativePath = "Study/TankGame/Boomeff.prefab";
            // 调用预制体管理器的加载方法 加载子弹特效预制体
            yield return PrefabManager.Instance.LoadPrefabCoroutine(prefabRelativePath, (prefab) => OnPrefabLoaded(bullet, prefab));
        }

        void OnPrefabLoaded(Bullet bullet,GameObject prefab)
        {
            if (prefab != null)
            {
                bullet.eff = prefab;
            }
            else
            {
                Debug.LogError("Failed to load and instantiate prefab.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {

                Player player = other.GetComponent<Player>();
                if (type == E_Reward_Type.Weapon)
                {
                    int idx = UnityEngine.Random.Range(0, weapons.Length);
                    player.ChangeWeapon(weapons[idx]);
                    Debug.Log("更换武器");
                }
                else if (type == E_Reward_Type.Bullet)
                {
                    int idx = UnityEngine.Random.Range(0, bullets.Length);
                    player.nowWeapon.ChangeBullet(bullets[idx]);
                    Debug.Log("更换子弹");
                }
                else if (type == E_Reward_Type.State)
                {
                    int idx = UnityEngine.Random.Range(0, 3);
                    switch (idx) 
                    { 
                        case 0:
                            player.nowHP += 10;
                            if (player.nowHP > player.maxHP) player.nowHP = player.maxHP;
                            GamePanel.Instance.UpdateHP(player.maxHP, player.nowHP);
                            Debug.Log("更新血量");
                            break;
                        case 1:
                            player.atk += 5;
                            Debug.Log("更新攻击力");
                            break;
                        case 2:
                            player.def += 20;
                            Debug.Log("更新防御力");
                            break;
                    }
                    // 这里仅测试，其实数值应该用变量存
                }
                else
                {
                    // 处理其他未知类型的情况
                    Debug.Log("未知类型");
                }

                GameObject eff = Instantiate(rewardEff,player.transform.position,player.transform.rotation);
                AudioSource audioSource = eff.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.volume = GameDataManager.Instance.musicData.soundVolume;
                }
                else
                {
                    Debug.Log("该特效不含音效");
                }
                    Destroy(this.gameObject);
            }
        }

        private Transform[] FindShootPoints(Transform parent)
        {
            List<Transform> shootPoints = new List<Transform>();
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                if (child.name == "ShootPoint")
                {
                    shootPoints.Add(child);
                }
                else
                {
                    // 递归查找子物体的子物体
                    Transform[] nestedShootPoints = FindShootPoints(child);
                    foreach (Transform nestedShootPoint in nestedShootPoints)
                    {
                        shootPoints.Add(nestedShootPoint);
                    }
                }
            }

            return shootPoints.ToArray();
        }
    }
}

