using UnityEngine;
using UnityEngine.Rendering;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

   void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("3_Damaged");

        if (currentHealth <= 0)
        {
            animator.SetTrigger("4_Death");
            Destroy(gameObject, 0.5f);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
