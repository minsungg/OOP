using UnityEngine;

public class RainMoving : RainStandard
{
    private Rigidbody2D rb;
    private Vector2 direction;

    private GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction.x = Player.transform.position.x - transform.position.x;
        direction.y = rb.linearVelocityY;
        rb.linearVelocity = direction;
    }
}
