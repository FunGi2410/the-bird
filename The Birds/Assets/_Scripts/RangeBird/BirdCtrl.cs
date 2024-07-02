using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCtrl : LivingEntity
{
    [SerializeField]
    protected Bird_SO birdSO;

    protected virtual void Start()
    {
        this.SetHealth();
    }

    protected void SetHealth()
    {
        this.startingHealth = this.birdSO.health;
        this.health = this.startingHealth;
    }

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
