using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 25;
    private Animator animator; 
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
            foreach (Collider2D player in hitPlayers)
            {
                if (player.transform.root.CompareTag("Player"))
                {
                    animator.SetTrigger("2_Attack");
                    player.transform.root.GetComponent<PlayerHealth>().TakeDamage(damage);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}
