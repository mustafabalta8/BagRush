using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
     
    [SerializeField] private int amountToPool;

    [Header("Objects")]
    [SerializeField] private GameObject bag;
    [SerializeField] private GameObject convertor;
    
    [Header("Object Pools")]
    [SerializeField] private List<GameObject> pooledBags;
    [SerializeField] private List<GameObject> pooledConvertor;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CreatePool(bag, out pooledBags, amountToPool);

    }

    private void CreatePool(GameObject objectToPool, out List<GameObject> pooledObjects, int amountToPool)//Transform parent
    {
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(transform); // set as children of Spawn Manager
        }
    }

    public GameObject GetPooledObject(GameObjects pooledObjectName)
    {
        List<GameObject> pooledObjects = null;
        switch (pooledObjectName)
        {
            case GameObjects.Bag:
                pooledObjects = pooledBags;
                break;
            case GameObjects.Convertor:
                pooledObjects = pooledConvertor;
                break;
        }
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // otherwise, return null   
        return null;
    }

    public void DeactivateThePool(List<GameObject> pool)
    {
        foreach(var obj in pool)
        {
            obj.SetActive(false);           
        }
    }
    public void DeactivateAllPools()
    {
        DeactivateThePool(pooledBags);
        //DeactivateThePool(pooledConvertor);
    }
}
