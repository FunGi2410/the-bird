using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Level SO", order = 1)]
public class DataLevel_SO : ScriptableObject
{
    [SerializeField] private int curLevel;
    [SerializeField] private List<EnemySpawnerLevel_SO> enemySpawnerLevelSO;

    public int CurLevel { get => curLevel; set => curLevel = value; }
    //public List<EnemySpawnerLevel_SO> EnemySpawnerLevelSO { get => enemySpawnerLevelSO; }

    public List<EnemySpawnerLevel_SO> GetEnemySpawnerLevelSO()
    {
        return enemySpawnerLevelSO;
    }
}
