using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected RangeBird_SO rangeBirdSO;

    public Vector2 direction;
    Rigidbody2D myRigidbody2D;

    float timmer = 0;

    private void Start()
    {
        this.myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        this.MoveToAttack();
        this.OnDestroy();
    }

    protected virtual void MoveToAttack()
    {
        this.myRigidbody2D.MovePosition(this.myRigidbody2D.position + this.direction * this.rangeBirdSO.speedMoveBullet * Time.fixedDeltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }

    protected virtual void OnDestroy()
    {
        this.timmer += Time.fixedDeltaTime;
        if(this.timmer >= this.rangeBirdSO.timming)
        {
            ObjectPoolManager.instance.CoolObject(gameObject, this.rangeBirdSO.bulletType);
        }
    }

}
