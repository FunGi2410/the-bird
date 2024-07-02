using UnityEngine;

public class RangeBirdCtrl : BirdCtrl
{
    [SerializeField] Transform muzzle;

    float nextTimeToFire = 0f;
    public Transform objectContainer;

    [SerializeField] bool isAttack = false;
    Animator animator;

    public PoolObjectType bulletType;
    public GameObject bulletPrefab;

    protected override void Start()
    {
        this.PoolBullet();
        base.Start();
        this.animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        this.Attack();

        // Raycast to Enemy to shoot
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.GetChild(0).position, Vector2.right * UIManager.instance.HalfWidthOfCanvas);

        bool isHaveEnemy = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                isHaveEnemy = true;
                this.isAttack = true;
                Debug.DrawRay(transform.position, hit.transform.position - transform.position, Color.green);
            }
        }
        if (!isHaveEnemy) this.isAttack = false;
    }

    void PoolBullet()
    {
        PoolInfo bulletInfo = new PoolInfo(this.bulletType, 1, this.bulletPrefab);
        ObjectPoolManager.instance.objToPools.Add(bulletInfo);
        ObjectPoolManager.instance.PooledObject(bulletInfo);
    }

    public void CompletedAttackEvent(string mes)
    {
        if (mes.Equals("AttackAnimationEnded"))
        {
            this.animator.SetBool("IsAttack", false);
            this.InstanceBullet(this.muzzle);
            AudioManager.Instance.Play("BirdFire");
        }
    }

    bool CheckFireRate()
    {
        if (Time.time >= this.nextTimeToFire)
        {
            this.nextTimeToFire = Time.time + (1f / this.birdSO.fireRate);
            return true;
        }
        return false;
    }

    protected void Attack()
    {
        if (this.CheckFireRate() && this.isAttack)
        {
            this.animator.SetBool("IsAttack", true);
        }
    }

    public GameObject InstanceBullet(Transform origin)
    {
        GameObject projectile = ObjectPoolManager.instance.GetPoolObject(this.bulletType);
        //projectile.transform.SetParent(origin.transform.parent.parent);
        projectile.transform.position = origin.position;
        projectile.SetActive(true);
        
        return projectile;
    }
}
