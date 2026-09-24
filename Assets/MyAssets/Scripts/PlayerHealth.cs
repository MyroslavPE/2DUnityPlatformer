using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    [SerializeField] private Image healthFill;
    private bool isDead = false;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image statusImage;
    [SerializeField] private Sprite[] statusSprites;
    [SerializeField] private float drainSpeed = 0.5f;
    private float targetFill;
    private float displayedFill;
    private KnockBack knockBack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        animator = GetComponentInChildren<Animator>();
        knockBack = GetComponent<KnockBack>();
        displayedFill = 1f;
    }
    public void TakeDamage(int damage, Vector3 attackerPosition)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        animator.SetTrigger("3_Damaged");
        knockBack.ApplyKnockBack(attackerPosition);
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
        targetFill = (float)currentHealth / maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;
        
        if (targetFill <= 0f) statusImage.sprite = statusSprites[4];
        else if (targetFill <= 0.25f) statusImage.sprite = statusSprites[3];
        else if (targetFill <= 0.5f) statusImage.sprite = statusSprites[2];
        else if (targetFill <= 0.75f) statusImage.sprite = statusSprites[1];
        else statusImage.sprite = statusSprites[0];
    }

    void Update()
    {
        if (displayedFill > targetFill)
        {
            displayedFill = displayedFill - drainSpeed * Time.deltaTime;
        }

        healthFill.fillAmount = displayedFill; 

    }
}
