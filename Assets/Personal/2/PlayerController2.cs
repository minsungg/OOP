using Unity.VisualScripting;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;
    private float speed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = speed * Input.GetAxisRaw("Horizontal");
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }

    private void FixedUpdate()
    {
        if(transform.position.y<-5)
        {
            GameObject.Find("GaneManager").GetComponent<GameManager2>().GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager2>().GameOver();
        }
    }
}
