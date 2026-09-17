using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public bool moveToPointB = true;
    public Transform player;
    public float radius = 0.5f;


    private Animator animator;
    public Transform unitRoot;

    private Rigidbody2D rb;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        if (player == null) return;

        if (Vector2.Distance(transform.position, player.position) < radius)
        {
            if (player.position.x > transform.position.x)
            {
                rb.linearVelocity = new Vector2(1, rb.linearVelocity.y);
                unitRoot.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                rb.linearVelocity = new Vector2(-1, rb.linearVelocity.y);
                unitRoot.localScale = new Vector3(1, 1, 1);
            }
        }

        else
        {

            if (Vector2.Distance(transform.position, pointA.position) < 1f)
            {
                moveToPointB = true;
            }
            else if (Vector2.Distance(transform.position, pointB.position) < 1f)
            {
                moveToPointB = false;
            }
        }

            animator.SetBool("1_Move", Mathf.Abs(rb.linearVelocity.x) > 0.5f);

          
    }
}

