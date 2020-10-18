using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{

    public GameObject objectToPool;
    [Range(0, 20)]
    public int amountToPool;
}

public class ObjectPooler : MonoBehaviour
{
    public List<ObjectPoolItem> itemsToPool;
    public static ObjectPooler SharedInstance;

    public List<GameObject> pooledObjects;

    void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            GameObject holder = new GameObject();
            holder.transform.parent = this.gameObject.transform;
            holder.name = item.objectToPool.name;
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.parent = holder.transform;
                obj.name = holder.name;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public GameObject GetPooledObjectByName(string name)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name == name)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public List<GameObject> GetActiveGameObjects(string tag)
    {
        List<GameObject> objs = new List<GameObject>();
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                objs.Add(pooledObjects[i]);
            }
        }
        return objs;
    }
}
