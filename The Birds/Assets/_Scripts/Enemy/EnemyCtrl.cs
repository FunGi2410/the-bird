using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCtrl : LivingEntity
{
    public static UnityAction OnEnemyDead;

    [SerializeField]
    protected MeleeEnemy_SO meleeEnemy_SO;

    protected override void Die()
    {
        base.Die();
        OnEnemyDead?.Invoke();
        ObjectPoolManager.instance.CoolObject(gameObject, meleeEnemy_SO.type);
    }

    protected virtual void Start()
    {
        this.startingHealth = meleeEnemy_SO.health;
        this.health = this.startingHealth;
    }
}
