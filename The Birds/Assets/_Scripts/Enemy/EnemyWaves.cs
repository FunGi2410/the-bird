using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWaves
{
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] float nextSpawnTime = 0.5f;

    public List<Enemy> Enemies { get => enemies; set => enemies = value; }
    public float NextSpawnTime { get => nextSpawnTime; set => nextSpawnTime = value; }
}
