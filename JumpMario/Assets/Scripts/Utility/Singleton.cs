using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Utility
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance { get; private set; }
        protected bool destroyed = false;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                destroyed = true;
                Debug.LogError("Instance already created.");

                return;
            }

            if (this is T ins)
            {
                instance = ins;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogError("Formal parameter error.");
            }
        }
    }
}