using UnityEngine;

public class PlayerMovement : MonoBehaviour
{     //Movement 
    [Header("Movement")]
    public float moveSpeed = 1f;
    public float jumpForce = 1f;
    public int jumpsCount = 0;
    public Transform unitRoot;
    private Animator animator;

    //Ground Check
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    public bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = unitRoot.GetComponent<Animator>();
    }

    
    void Update()
    {   //Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            if (isGrounded){
                jumpsCount = 0;
        }



        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpsCount < 1){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsCount++;
        }
        
    }   

        void FixedUpdate()
    {   //Movement left and right
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        //Flip the player
        if (horizontalInput > 0)
        {
            unitRoot.localScale = new Vector3(-1, 1, 1);
        } 
        else if (horizontalInput < 0)
        {
            unitRoot.localScale = new Vector3(1, 1, 1);
        }

        //Animation
        animator.SetBool("1_Move", Mathf.Abs(rb.linearVelocity.x) > 0.5f);
        Debug.Log(horizontalInput);
        

    }


}
