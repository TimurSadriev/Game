using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded=false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }
 void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, на чем мы стоим
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
     void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Обнуляем вертикальную скорость
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
}
