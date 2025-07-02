using UnityEngine;

public class PlayerControlller0 : MonoBehaviour
{

    private float verticalInput, horizontalInput;
    Rigidbody2D rb;

    private float speed;
    private Vector2 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = 3.0f;
        velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        velocity.x = horizontalInput;
        velocity.y = verticalInput;

        rb.linearVelocity = velocity.normalized * speed;
    }
}
