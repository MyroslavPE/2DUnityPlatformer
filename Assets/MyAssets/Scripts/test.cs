using NUnit.Framework;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public bool moveToPointB = true;

    private Rigidbody2D rb;

    public Animator animator;
    public Transform unitRoot;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        if (moveToPointB)
        {
            rb.linearVelocity = new Vector2(1, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-1, rb.linearVelocity.y);
        }
        if (Vector2.Distance(transform.position, pointA.position) < 1f)
        {
            moveToPointB = true;
        }
        else if (Vector2.Distance(transform.position, pointB.position) < 1f)
        {
            moveToPointB = false;
        }

        animator.SetBool("1_Move", Mathf.Abs(rb.linearVelocity.x) > 0.5f);

        if (moveToPointB)
            unitRoot.localScale = new Vector3(-1, 1, 1);

        else
            unitRoot.localScale = new Vector3(1, 1, 1);
    }
}
