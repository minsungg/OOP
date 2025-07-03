using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerControlller0 : MonoBehaviour
{
    private Rigidbody2D rb;

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
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = velocity.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("TopDoor"))
        {
            SceneManager.LoadScene("TopDown");
        } else if(other.CompareTag("ExitDoor"))
        {
            SceneManager.LoadScene("Main");
        } else
        {
            SceneManager.LoadScene("Platformer");
        }
    }
}
