using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Spawner Level SO", order = 1)]
public class EnemySpawnerLevel_SO : ScriptableObject
{
    [SerializeField] private List<EnemyWaves> enemyWaves;

    public List<EnemyWaves> EnemyWaves { get => enemyWaves; }
}
