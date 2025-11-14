using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = (Keyboard.current.dKey.isPressed ? 1 : 0)
                    - (Keyboard.current.aKey.isPressed ? 1 : 0);

        float moveY = (Keyboard.current.wKey.isPressed ? 1 : 0)
                    - (Keyboard.current.sKey.isPressed ? 1 : 0);

        // Mantém diagonal perfeitamente
        movement = new Vector2(moveX, moveY).normalized;

        // Decide o sprite NÃO pela diagonal, mas pela direção dominante
        if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
        {
            // Horizontal domina
            sr.sprite = moveX > 0 ? spriteRight : spriteLeft;
        }
        else if (Mathf.Abs(moveY) > 0)
        {
            // Vertical domina
            sr.sprite = moveY > 0 ? spriteUp : spriteDown;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
