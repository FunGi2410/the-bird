using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Range Bird SO", order = 1)]
public class RangeBird_SO : ScriptableObject
{
    public string birdName;
    public float timming;
    public int price;

    public float fireRate;
    public float damage;
    public float health;

    // Bullet Info
    [SerializeField]
    public GameObject bulletPrefab;
    public float speedMoveBullet;
    public PoolObjectType bulletType;
}

