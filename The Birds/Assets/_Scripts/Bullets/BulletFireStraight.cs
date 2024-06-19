using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireStraight : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Enemy")
        {
            IDamageable damageableObject = collision.GetComponent<IDamageable>();
            if(damageableObject != null)
            {
                damageableObject.TakeDame(this.rangeBirdSO.damage);
            }
            ObjectPoolManager.instance.CoolObject(gameObject, this.rangeBirdSO.bulletType);
        }
    }
}
