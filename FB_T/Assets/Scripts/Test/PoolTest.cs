using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.Pool;
using System;

public class PoolTest : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int defaultCapacity = 2;
    [SerializeField] int maxCount = 10;
    GenericPool<GameObject> pool;

    private void Awake()
    {
        pool = new GenericPool<GameObject>(OnCreate, OnGet, OnRelease,OnPoolObjDestroy,defaultCapacity,maxCount);
    }

    private void OnPoolObjDestroy(GameObject obj) 
    {
        Destroy(obj);
    }

    private void OnRelease(GameObject obj) 
    {
        obj.SetActive(false);
    }
    private void OnGet(GameObject obj)
    {
        obj.SetActive(true);
    }
    private GameObject OnCreate()
    {
        GameObject gameObject = Instantiate(prefab);
        gameObject.SetActive(false);
        gameObject.GetComponent<IPoolable>().ActionOnRelease(Release);
        return gameObject;
    }

    public void Spawn()
    {
        pool.Get();
    }
    public void Release(GameObject go)
    {
        pool.Release(go);
    }
}
