using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    [SerializeField] private Image healthFill;
    private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        animator = GetComponentInChildren<Animator>();
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        animator.SetTrigger("3_Damaged");
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("4_Death");
            Destroy(gameObject, 0.5f);

        }
    }

    void UpdateHealthBar()          
    {
        healthFill.fillAmount = (float)currentHealth / maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
    }
}
