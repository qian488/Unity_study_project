using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TreasureHunt
{
    public class EffectControl : MonoBehaviour
    {
        public GameObject EffectPre;
        public Transform player;

        [SerializeField]
        public float spawnDelay = 1f; // ÿ��������Ч���ӳ�
        public float destroyDelay = 8f; // һ�����ٵ��ӳ�

        void Start()
        {
            StartEffectSequence();
        }

        public void StartEffectSequence()
        {
            StartCoroutine(SpawnAndDestroyEffects());
        }

        IEnumerator SpawnAndDestroyEffects()
        {
            List<GameObject> effects = new List<GameObject>();

            for (int i = 0; i < 5; i++)
            {
                GameObject effect = Instantiate(EffectPre, player.position, player.rotation);
                effects.Add(effect);
                yield return new WaitForSeconds(spawnDelay);
            }
            Debug.Log(effects.Count);
            yield return new WaitForSeconds(destroyDelay);
            foreach (GameObject effect in effects)
            {
                Destroy(effect);
            }
        }
    }
}

