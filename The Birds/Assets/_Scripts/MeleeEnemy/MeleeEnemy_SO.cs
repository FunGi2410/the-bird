using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Melee Enemy SO", order = 1)]
public class MeleeEnemy_SO : ScriptableObject
{
    public string enemyName;
    public float timming;
    public EnemyID enemyID;
    public PoolObjectType type;

    public float attackRange;
    public float speedWalk;
    public float fireRate;
    public float damage;
    public float health;
}
