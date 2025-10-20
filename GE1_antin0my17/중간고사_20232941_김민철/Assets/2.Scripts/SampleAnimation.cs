using UnityEngine;

public class SampleAnimation : MonoBehaviour
{
    private Animator animator;
    [Range(0f, 1f)]
    public float Speed;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
         if (animator != null)
         {
            animator.SetFloat("Speed", Speed);
         }
    }
}
