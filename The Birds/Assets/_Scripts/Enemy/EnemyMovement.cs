using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    Vector2 moveInput;
    float speed;
    float originalSpeed;
    MeleeEnemyCtrl meleeEnemyCtrl;
    bool isSlow;
    float timerSlow = 0f;

    private void Start()
    {
        this.originalSpeed = this.speed;
        this.myRigidbody2D = GetComponent<Rigidbody2D>();
        this.meleeEnemyCtrl = GetComponent<MeleeEnemyCtrl>();
    }

    private void FixedUpdate()
    {
        if (this.meleeEnemyCtrl.IsAttack) return;
        if (isSlow) this.MoveSlow();
        else this.Move();
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void MakeSlowMove()
    {
        this.isSlow = true;
        this.timerSlow = 0f;
        SetSpeed(0.3f);
    }

    public void Move()
    {
        this.moveInput = Vector2.left;
        Vector2 moveVelocity = this.moveInput.normalized * this.speed;
        this.myRigidbody2D.MovePosition(myRigidbody2D.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void MoveSlow()
    {
        if (this.timerSlow > 2f)
        {
            SetSpeed(this.originalSpeed);
            this.timerSlow = 0f;
            this.isSlow = false;
        }
        this.timerSlow += Time.fixedDeltaTime;
        Move();
    }
}
