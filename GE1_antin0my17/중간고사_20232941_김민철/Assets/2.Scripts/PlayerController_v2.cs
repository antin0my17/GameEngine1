using UnityEngine;

public class PlayerController_v2 : MonoBehaviour
{
    [Header("Move/Jump")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    Rigidbody2D rb;
    float moveInput;
    bool facingRight = true;

    bool isGrounded = false;
    int groundContacts = 0;         

    bool doubleJumpUnlocked = false;
    int extraJumpsAvailable = 0;    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        if (rb != null)
        {
            rb.freezeRotation = true;              
        }
    }

    void Update()
    {
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) moveInput -= 1f;
        if (Input.GetKey(KeyCode.D)) moveInput += 1f;

        if (moveInput > 0f && !facingRight) Flip(true);
        else if (moveInput < 0f && facingRight) Flip(false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)                     DoJump();
            else if (doubleJumpUnlocked && extraJumpsAvailable > 0)
            {
                DoJump();
                extraJumpsAvailable--;
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void DoJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    void Flip(bool toRight)
    {
        facingRight = toRight;
        var s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (toRight ? 1f : -1f);
        transform.localScale = s;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            groundContacts++;
            isGrounded = true;
            if (doubleJumpUnlocked) extraJumpsAvailable = 1; 
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            groundContacts = Mathf.Max(0, groundContacts - 1);
            isGrounded = groundContacts > 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DoubleJumpItem"))
        {
            doubleJumpUnlocked = true;
            extraJumpsAvailable = 1;     
            Destroy(other.gameObject);   
        }
    }
}
