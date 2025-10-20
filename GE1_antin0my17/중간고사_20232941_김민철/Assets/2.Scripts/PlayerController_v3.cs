using UnityEngine;

public class PlayerController_v3 : MonoBehaviour
{
    [Header("Move / Jump")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;          
    public Transform goal;                
    public int totalCoins = 3;

    Rigidbody2D rb;
    float moveInput;
    bool facingRight = true;

    bool isGrounded = false;
    int groundContacts = 0;

    float baseJumpForce;                  

    int collected = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        baseJumpForce = jumpForce;       
        if (rb != null) rb.freezeRotation = true;  
    }

    void Update()
    {
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) moveInput -= 1f;
        if (Input.GetKey(KeyCode.D)) moveInput += 1f;

        if (moveInput > 0 && !facingRight) Flip(true);
        else if (moveInput < 0 && facingRight) Flip(false);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            DoJump();
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
        if (col.collider.CompareTag("Ground") || col.collider.CompareTag("JumpBoostPlatform"))
        {
            groundContacts++;
            isGrounded = true;

            if (col.collider.CompareTag("JumpBoostPlatform"))
                jumpForce = baseJumpForce * 2f;
            else
                jumpForce = baseJumpForce;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground") || col.collider.CompareTag("JumpBoostPlatform"))
        {
            groundContacts = Mathf.Max(0, groundContacts - 1);
            isGrounded = groundContacts > 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            collected++;
            Destroy(other.gameObject);

            if (collected >= totalCoins && goal != null)
            {
                rb.linearVelocity = Vector2.zero;
                transform.position = goal.position;
            }
        }
    }
}
