using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int damage = 20;
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        lastAttackTime = -attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("2_Attack");
                lastAttackTime = Time.time;

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.transform.root.CompareTag("Enemy"))
                    {
                        enemy.transform.root.GetComponent<EnemyHealth>().TakeDamage(damage);
                    }
                }
            }
        }
    }
}