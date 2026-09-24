using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 25;
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackCooldown = 2f;
    public float hitDelay = 0.3f;

    private float lastAttackTime;
    private bool attackPending = false;
    private float damageTime;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // игрок в круге и кулдаун прошёл: начинаем замах
        if (!attackPending && Time.time - lastAttackTime >= attackCooldown && PlayerInRange())
        {
            animator.SetTrigger("2_Attack");
            lastAttackTime = Time.time;

            attackPending = true;
            damageTime = Time.time + hitDelay;
        }

        // время пришло: наносим урон, если игрок всё ещё в круге
        if (attackPending && Time.time >= damageTime)
        {
            DealDamage();
            attackPending = false;
        }
    }

    bool PlayerInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.transform.root.CompareTag("Player")) return true;
        }
        return false;
    }

    void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.transform.root.CompareTag("Player"))
            {
                hit.transform.root.GetComponent<PlayerHealth>().TakeDamage(damage, transform.position);
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}