using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCtrl : LivingEntity
{
    [SerializeField]
    protected RangeBird_SO rangeBirdSO;

    protected virtual void Start()
    {
        this.startingHealth = this.rangeBirdSO.health;
        this.health = this.startingHealth;
    }

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
