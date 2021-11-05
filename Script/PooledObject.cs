using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{

    public string poolItemName = string.Empty;
    public GameObject Prefab = null;
    public int poolCount = 0;

    [SerializeField]
    private List<GameObject> poolList = new List<GameObject>();

    public void Initialize(Transform parent)
    {
        for(int i = 0; i < poolCount; ++i)
        {
            poolList.Add(CreateItem(parent));
        }
    }

    public void PushToPool(GameObject item, Transform parent)
    {   
        item.transform.SetParent(parent);
        item.gameObject.SetActive(false);
        poolList.Add(item);
    }

    public void LPushToPool(GameObject item, Transform parent)
    {
        item.gameObject.SetActive(false);
    }

    public GameObject PopFromPool(Transform parent1, Transform parent2, Animator _animator)
    {
        if (poolList.Count == 0)
        {
            poolList.Add(CreateItem(parent1));
        }
        //GameObject item = poolList[0];
        GameObject item = parent1.GetChild(0).gameObject;
        item.gameObject.SetActive(true);
        item.transform.SetParent(parent2);
        poolList.RemoveAt(0);
        if (_animator)
        {
            if (!_animator.isInitialized)
                _animator.Rebind();
        }
        return item;
    }

    public GameObject pCPopFromPool(Transform parent1, Transform parent2, Animator _animator, int i)
    {
        if (poolList.Count == 0)
        {
            poolList.Add(CreateItem(parent1));
        }
        //GameObject item = poolList[0];
        GameObject item = parent1.GetChild(i).gameObject;
        item.gameObject.SetActive(true);
        item.transform.SetParent(parent2);
        poolList.RemoveAt(0);
        if (_animator)
        {
            if (!_animator.isInitialized)
                _animator.Rebind();
        }
        return item;
    }

    public GameObject CreateItem(Transform parent)
    {
        GameObject item = Object.Instantiate(Prefab) as GameObject;
        item.name = poolItemName;
        item.transform.SetParent(parent);
        //item.SetActive(false);

        return item;
    }
}
