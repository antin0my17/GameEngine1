using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 3f;

    private float baseSpeed;
    private bool facingRight = true;

    void OnEnable()
    {
        baseSpeed = moveSpeed;
    }

    void Update()
    {
        float input = 0f;
        if (Input.GetKey(KeyCode.A)) input -= 1f;
        if (Input.GetKey(KeyCode.D)) input += 1f;

     
        float speed = baseSpeed;
        if (Input.GetKey(KeyCode.LeftShift))                speed = baseSpeed * 1.8f;   
        else if (Input.GetKey(KeyCode.LeftControl) || 
                 Input.GetKey(KeyCode.RightControl))        speed = baseSpeed * 0.5f;   

        Vector3 delta = new Vector3(input * speed * Time.deltaTime, 0f, 0f);
        transform.position += delta;

        if (input > 0 && !facingRight)          Flip(true);
        else if (input < 0 && facingRight)      Flip(false);

        if (animator != null)
        {
            float normalized = Mathf.Abs(input) * (speed / baseSpeed); 
            animator.SetFloat("Speed", Mathf.Clamp01(normalized));     
        }
    }

    void Flip(bool toRight)
    {
        facingRight = toRight;
        var s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (toRight ? 1f : -1f);
        transform.localScale = s;
    }
}
