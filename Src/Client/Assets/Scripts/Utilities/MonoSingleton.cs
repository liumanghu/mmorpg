using System;
using UnityEngine;

namespace MMORPG
{
    /// <summary>
    /// MonoBehaviour单例类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool global = true;
        
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }

        private void Start()
        {
            if (global)
            {
                if (_instance != null && _instance != gameObject.GetComponent<T>())
                {
                    Destroy(gameObject);
                    return;
                }
                
                DontDestroyOnLoad(gameObject);
                _instance = gameObject.GetComponent<T>();
            }
        }

        protected abstract void OnStart();
    }
}