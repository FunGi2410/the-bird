using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    [SerializeField] protected float health;

    public event System.Action OnDeath;
    public event System.Action OnTakeDame;

    public void TakeDame(float dame)
    {
        this.health -= dame;

        if (this.OnTakeDame != null)
        {
            this.OnTakeDame();
        }

        if (this.health <= 0)
        {
            this.Die();
        }
    }

    [ContextMenu("Self Detruct")]
    protected virtual void Die()
    {
        if (this.OnDeath != null)
        {
            this.OnDeath();
        }
    }
}
