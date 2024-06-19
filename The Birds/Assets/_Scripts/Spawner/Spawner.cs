using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    protected abstract void Spawn();

    public GameObject InstanceObject(PoolObjectType type)
    {
        GameObject obj = ObjectPoolManager.instance.GetPoolObject(type);
        obj.SetActive(true);
        return obj;
    }
}
