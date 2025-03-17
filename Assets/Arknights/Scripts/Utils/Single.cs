using System;
using UnityEngine;

namespace Arknights.Utils
{
    public abstract class Single<T> where T : Single<T>, new()
    {
        private static readonly Lazy<T> instance = new(() =>
        {
            var instance = new T();
            instance.Init();
            return instance;
        });

        public static T GetInstance() => instance.Value;

        protected Single() { } // 防止外部 new

        protected virtual void Init() { } // 允许子类初始化
    }

    public abstract class MonoSingle<T> : MonoBehaviour where T : MonoSingle<T>
    {
        private static T instance;
        private static readonly object lockObj = new();

        public static T Inst()
        {
            if (instance) return instance;

            lock (lockObj)
            {
                if (instance) return instance;

                instance = FindObjectOfType<T>(); // 先查找场景中的对象
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }
                instance.Initialization();
            }
            return instance;
        }

        protected virtual void Initialization() { }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                Initialization();
            }
            else if (instance != this)
            {
                Destroy(gameObject); // 确保只保留一个实例
            }
        }
    }

}