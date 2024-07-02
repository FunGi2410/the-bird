using System;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    wheat,
    /*----------- Enemy type -----------*/
    redBullet,
    blueBullet,
    /*----------- Bullet type -----------*/
    orrangeEnemy,
    pinkEnemy,
    purpleEnemy,
    blackEnemy
}

[Serializable]
public class PoolInfo
{
    public PoolInfo(PoolObjectType type, int amount, GameObject prefab)
    {
        this.type = type;
        this.amount = amount;
        this.prefab = prefab;
    }

    public PoolObjectType type;
    public int amount;
    public GameObject prefab;
    public Queue<GameObject> pools = new Queue<GameObject>();
}

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField]
    public List<PoolInfo> objToPools = new List<PoolInfo>();

    [SerializeField] PoolObject_SO poolObject_SO;

    public PoolObject_SO PoolObject_SO { get => poolObject_SO; }

    private void Awake()
    {
        instance = this;

        this.objToPools = PoolObject_SO.ObjToPools;
    }

    private void Start()
    {
        foreach (PoolInfo info in objToPools)
        {
            info.pools = new Queue<GameObject>();
            PooledObject(info);
        }
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo poolSelected = GetPoolByType(type);
        Queue<GameObject> pools = poolSelected.pools;
        GameObject objInstance = null;
        if(pools.Count > 0)
        {
            objInstance = pools.Dequeue();
        }
        else // expand obj
            objInstance = Instantiate(poolSelected.prefab, transform);
        return objInstance;
    }

    public void CoolObject(GameObject obj, PoolObjectType type)
    {
        obj.SetActive(false);

        PoolInfo poolSelected = GetPoolByType(type);
        Queue<GameObject> pools = poolSelected.pools;
        
        if (!pools.Contains(obj))
            pools.Enqueue(obj);
    }

    PoolInfo GetPoolByType(PoolObjectType type)
    {
        Debug.Log("Type 2: " + type);
        foreach(PoolInfo info in objToPools)
            if (type == info.type)
            {
                //Debug.Log("Info Type: " + info.type);
                return info;
            }
                
        return null;
    }

    public void PooledObject(PoolInfo objInfo)
    {
        for (int i = 0; i < objInfo.amount; ++i)
            objInfo.pools.Enqueue(CreateGobject(objInfo.prefab, transform));
    }

    GameObject CreateGobject(GameObject obj, Transform parrent)
    {
        GameObject gameObj = Instantiate(obj, parrent);
        gameObj.SetActive(false);
        return gameObj;
    }
}
