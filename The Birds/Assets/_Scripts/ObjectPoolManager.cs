using System;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    redBullet,
    wheat,
    /*----------- Enemy type -----------*/
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
    public List<GameObject> pools = new List<GameObject>();
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
            PooledObject(info);
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo poolSelected = GetPoolByType(type);
        List<GameObject> pools = poolSelected.pools;
        GameObject objInstance = null;
        if(pools.Count > 0)
        {
            int index = pools.Count - 1;
            objInstance = pools[index];
            pools.RemoveAt(index);
        }
        else // expand obj
            objInstance = Instantiate(poolSelected.prefab, transform);
        return objInstance;
    }

    public void CoolObject(GameObject obj, PoolObjectType type)
    {
        obj.SetActive(false);

        PoolInfo poolSelected = GetPoolByType(type);
        List<GameObject> pools = poolSelected.pools;
        if (!pools.Contains(obj))
            pools.Add(obj);
    }

    PoolInfo GetPoolByType(PoolObjectType type)
    {
        foreach(PoolInfo info in objToPools)
            if (type == info.type)
                return info;
        return null;
    }

    public void PooledObject(PoolInfo objInfo)
    {
        for (int i = 0; i < objInfo.amount; ++i)
            objInfo.pools.Add(CreateGobject(objInfo.prefab, transform));
    }

    GameObject CreateGobject(GameObject obj, Transform parrent)
    {
        GameObject gameObj = Instantiate(obj, parrent);
        gameObj.SetActive(false);
        return gameObj;
    }
}
