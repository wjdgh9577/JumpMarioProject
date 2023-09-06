using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string poolKey;

    public virtual void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public virtual void Despawn<T>()
    {
        ObjectPoolManager.instance.Despawn<T>(this);
    }
}
