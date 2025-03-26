using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.GamePlay
{
    public class BasePanel<T> : MonoBehaviour where T : BasePanel<T>
    {
        private static T instance;
        public static T Instance => instance;

        protected virtual void Awake()
        {
            instance = this as T;
        }

        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
