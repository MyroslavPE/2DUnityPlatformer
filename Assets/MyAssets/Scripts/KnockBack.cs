using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float knockBackForce = 10f;
    public float knockBackUpForce = 5f;
    public float knockBackDuration = 0.5f;
    private Rigidbody2D rb;
    public bool isKnockedBack = false;
    private float knockBackEndTimer;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockBack(Vector3 attackerPosition)
    {
        Vector2 knockBackDirection = (transform.position - attackerPosition).normalized;
        rb.AddForce(new Vector2(knockBackDirection.x * knockBackForce, knockBackUpForce), ForceMode2D.Impulse);
        isKnockedBack = true;
        knockBackEndTimer = Time.time + knockBackDuration;

    }

    void Update()
    {
        if (isKnockedBack && Time.time >= knockBackEndTimer)
        {
            isKnockedBack = false;
        }
    }

}
