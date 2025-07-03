using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private float speed = 1.0f;
    private Vector2 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position)<3)
        {
            OnComing();
        } else
        {
            rb.linearVelocity *= 0;
        }
    }

    private void OnComing()
    {
        velocity.x = player.transform.position.x - transform.position.x;
        velocity.y = player.transform.position.y - transform.position.y;

        rb.linearVelocity = speed * velocity.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("bullet"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager1>().score += 1;
            Destroy(gameObject);
        }
    }
}
