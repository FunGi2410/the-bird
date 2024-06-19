using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool Object SO In Level", order = 1)]
public class PoolObject_SO : ScriptableObject
{
    [SerializeField]
    private List<PoolInfo> objToPools;

    public List<PoolInfo> ObjToPools { get => objToPools; }
}
