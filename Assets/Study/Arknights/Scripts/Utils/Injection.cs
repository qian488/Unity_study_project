using System;
using UnityEngine;

namespace Arknights.Utils
{
    [Serializable]
    public class Injection
    {
        public enum InjectionType
        {
            Component,
            ScriptableObjects,
            Resources,
            Assets
        }

        public Injection() { }

        public Injection(InjectionType type, UnityEngine.Object value)
        {
            this.type = type;
            this.value = value;
        }

        public string name;
        public UnityEngine.Object value;
        public InjectionType type;

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(name) && value != null;
        }
    }
}
