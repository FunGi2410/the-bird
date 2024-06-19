using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "List Object Data", order = 1)]
public class Objects_SO : ScriptableObject
{
    [SerializeField] private List<GameObject> allObjectData;

    public List<GameObject> GetAllObjectsData()
    {
        return this.allObjectData;
    }

    public GameObject GetObjectInData(int index)
    {
        return this.allObjectData[index];
    }

    public int GetLength()
    {
        return this.allObjectData.Count;
    }
}
