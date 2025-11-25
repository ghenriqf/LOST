using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();

        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);

        if (context.performed) 
        {
            animator.SetBool("IsMoving", true);
        }

        if (context.canceled) 
        {
            animator.SetBool("IsMoving", false);
            animator.SetFloat("LastInputX", movement.x);
            animator.SetFloat("LastInputY", movement.y);
            movement = Vector2.zero;
        }
    }
}
