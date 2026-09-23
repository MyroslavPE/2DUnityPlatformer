using UnityEngine;
using UnityEngine.Rendering;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
    }


    public void TakeDamage(int damage)
    {
        //проверка мертв ли враг, если да но выйти из метода
        if (isDead) return;
        //уменьшается текущее здоровье после получения урона
        currentHealth -= damage;

        //проверить что бы текущее здоровье не было меньше нуля
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // проигрывается анимация получения урона
        animator.SetTrigger("3_Damaged");
        //если текущее здоровье меньше или ровно нулю, то надо проиграть анимацию смерти и уничтожить объект
        if (currentHealth <= 0)
        {
            //запомнить что враг умер
            isDead = true;
            animator.SetTrigger("4_Death");
            Destroy(gameObject, 0.5f);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
