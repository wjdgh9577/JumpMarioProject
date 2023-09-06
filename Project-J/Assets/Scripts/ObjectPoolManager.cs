using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    Dictionary<string, Queue<PoolObject>> pools = new Dictionary<string, Queue<PoolObject>>();

    public T Spawn<T>(GameObject obj) where T : PoolObject
    {
        return Spawn<T>(obj, null);
    }

    public T Spawn<T>(GameObject obj, Transform parent) where T : PoolObject
    {
        PoolObject poolObj = obj.GetComponent<PoolObject>();
        if (poolObj == null)
        {
            Debug.LogError($"{obj.name} is not PoolObject.");
            return null;
        }

        T spawnedObj = null;
        if (pools.TryGetValue(poolObj.poolKey, out var queue) && queue.Count > 0)
        {
            spawnedObj = queue.Dequeue() as T;
            spawnedObj.transform.SetParent(parent);
        }
        else
        {
            spawnedObj = Instantiate(obj, parent).GetComponent<T>();
        }

        spawnedObj.OnSpawn();

        return spawnedObj;
    }

    public void Despawn<T>(PoolObject poolObject)
    {
        string key = poolObject.poolKey;
        
        if (!pools.TryGetValue(key, out var queue))
        {
            queue = new Queue<PoolObject>();
            pools.Add(key, queue);
        }
        
        queue.Enqueue(poolObject);
        poolObject.transform.SetParent(transform);
        poolObject.OnDespawn();
    }
}
