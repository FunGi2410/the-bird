using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Spawner Level SO", order = 1)]
public class EnemySpawnerLevel_SO : ScriptableObject
{
    public List<EnemyWaves> enemyWaves;
    //public List<EnemyWaves> EnemyWaves { get => enemyWaves; }
}
