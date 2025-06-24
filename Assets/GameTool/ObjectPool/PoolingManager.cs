using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : SingleTon<PoolingManager>
{
    public GameObject parent;
    private string poolName = "PoolObject";
    public List<Transform> listObject;

    protected override void Awake()
    {
        base.Awake();
        if (GameObject.Find(poolName) == null)
        {
            parent = new GameObject(poolName);
        }
        // Initialize the listObject
        listObject = new List<Transform>();
    }

    private void Start()
    {
        if (GameObject.Find(poolName) == null)
        {
            parent = new GameObject(poolName);
        }
        else
        {
            parent = GameObject.Find(poolName);
        }

        parent.transform.parent = this.transform;
    }

    public GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion quaternion)
    {
        GameObject newObject = GetFormPool(gameObject);
        newObject.transform.SetPositionAndRotation(position, quaternion);
        newObject.transform.parent = this.parent.transform;
        return newObject;
    }

    public GameObject GetFormPool(GameObject obj)
    {
        GameObject newObject = null;
        foreach (var pool in listObject)
        {
            if (pool.name == obj.name && pool != null && pool.gameObject != null)
            {
                listObject.Remove(pool);
                newObject = pool.gameObject;
                if (newObject != null && !newObject.activeInHierarchy)
                {
                    newObject.SetActive(true);
                    return newObject;
                }
            }
        }
        newObject = Instantiate(obj);
        newObject.name = obj.name;
        newObject.transform.parent = this.parent.transform;
        return newObject;
    }


    public void Despawn(GameObject gameObject)
    {
        gameObject.SetActive(false);
        listObject.Add(gameObject.transform);
    }
    
}