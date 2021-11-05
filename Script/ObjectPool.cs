using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool> {

    public List<PooledObject> objectpool = new List<PooledObject>();

    private void Awake()
    {
        for (int i = 0; i < objectpool.Count; ++i)
        {
            objectpool[i].Initialize(transform);
        }
    }

    public bool PushToPool(string itemName, GameObject item, Transform parent = null)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return false;
        if(itemName == "LongCNote" || itemName == "LongPNote")
        {
            pool.LPushToPool(item, parent == null ? transform : parent);
        }
        else
            pool.PushToPool(item, parent == null ? transform : parent);
        return true;
    }

    public GameObject PopFromPool(string itemName, Transform parent1 = null, Transform parent2 = null, Animator _animator = null)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return null;


        return pool.PopFromPool(parent1, parent2, _animator);
    }

    public GameObject pCPopFromPool(string itemName, int i, Transform parent1 = null, Transform parent2 = null, Animator _animator = null)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return null;

        return pool.pCPopFromPool(parent1, parent2, _animator, i);
    }

    PooledObject GetPoolItem(string itemName)
    {
        for (int i = 0; i < objectpool.Count; ++i)
        {
            if (objectpool[i].poolItemName.Equals(itemName))
                return objectpool[i];
        }

        return null;
    }
}
