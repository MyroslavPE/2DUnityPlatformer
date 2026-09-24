using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int damage = 20;
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackCooldown = 2f;
    public float hitDelay = 0.3f;

    private float lastAttackTime;
    private bool attackPending = false;
    private float damageTime;
    public float hitWindow = 0.15f;
    private float damageEndTime;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        // клик: запускаем анимацию и запоминаем, когда нанести урон
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("2_Attack");
                lastAttackTime = Time.time;

                attackPending = true;
                damageTime = Time.time + hitDelay;
                damageEndTime = damageTime + hitWindow;
            }
        }

        // время пришло: наносим урон
        if (attackPending && Time.time >= damageTime)
        {
            bool hit = DealDamage();
            if (hit || Time.time >= damageEndTime)
            {
                attackPending = false;
            }
        }

        bool DealDamage()
        {
            bool hitSomething = false;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.transform.root.CompareTag("Enemy"))
                {
                    enemy.transform.root.GetComponent<EnemyHealth>().TakeDamage(damage, transform.position);
                    hitSomething = true;
                }
            }
            return hitSomething;
        }

        void OnDrawGizmosSelected()
        {
            if (attackPoint == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}