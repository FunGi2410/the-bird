using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyCtrl : EnemyCtrl
{
    public Transform attackPoint;

    float nextTimeToFire = 0f;

    public LayerMask playerLayer;
    Animator animator;

    [SerializeField] private bool isAttack = false;

    public bool IsAttack { get => isAttack; set => isAttack = value; }

    protected override void Start()
    {
        base.Start();
        GetComponent<EnemyMovement>().SetSpeed(this.meleeEnemy_SO.speedWalk);
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (this.CheckFireRate())
        {
            this.Attack();
        }
    }

    bool CheckFireRate()
    {
        if (Time.time >= this.nextTimeToFire)
        {
            this.nextTimeToFire = Time.time + (1f / this.meleeEnemy_SO.fireRate);
            return true;
        }
        return false;
    }

    void Attack()
    {
        // animation
        // Detect enemies in range of attack
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(this.attackPoint.position, this.meleeEnemy_SO.attackRange, this.playerLayer);
        // Damage them
        if (hitPlayers.Length <= 0)
        {
            this.IsAttack = false;
            this.animator.SetBool("IsAttack", false);
            return;
        }
        foreach (Collider2D player in hitPlayers)
        {
            IDamageable damageableObject = player.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                this.IsAttack = true;
                this.animator.SetBool("IsAttack", true);
                damageableObject.TakeDame(this.meleeEnemy_SO.damage);
            }
        }
    }

   /*public void CompletedAttackEvent(string mes)
    {
        if (mes.Equals("AttackAnimationEnded"))
        {
            this.animator.SetBool("IsAttack", false);
        }
    }*/

    private void OnDrawGizmosSelected()
    {
        if (this.attackPoint == null) return;
        Gizmos.DrawWireSphere(this.attackPoint.position, this.meleeEnemy_SO.attackRange);
    }
}
