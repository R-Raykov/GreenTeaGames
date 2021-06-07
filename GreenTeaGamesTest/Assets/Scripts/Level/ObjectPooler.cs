using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectPoolItem
{
    // Struct containing the basic object information
    public GameObject ObjectToPool;
    public int AmountToPool;
    public bool ShouldExpand;
}

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler _instance;
    public static ObjectPooler Instance { get => _instance; }

    [SerializeField] private List<ObjectPoolItem> itemsToPool;

    [SerializeField] private List<GameObject> _pooledObjects;

    void Awake()
    {
        _instance = this;

        _pooledObjects = new List<GameObject>();

        // Instantiate the gameobject we will use
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.AmountToPool; i++)
            {
                GameObject obj = Instantiate(item.ObjectToPool);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }
    }

    /// <summary>
    /// Returns an available gameobject from the pool with the given tag
    /// </summary>
    /// <param name="pTag"> The tag of the gameobject we want </param>
    /// <returns></returns>
    public GameObject GetPooledObject(string pTag)
    {
        // Checks for any available objects and returns the first one
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy && _pooledObjects[i].CompareTag(pTag))
            {
                return _pooledObjects[i];
            }
        }

        // Checks if any of the objects are allowed to expand from their predetermined size,
        // we dont really use this as the call this method in recursive functions which would cause a crash, but its good to have 
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.ObjectToPool.CompareTag(pTag))
            {
                if (item.ShouldExpand)
                {
                    GameObject obj = Instantiate(item.ObjectToPool);
                    obj.SetActive(false);
                    _pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

}
