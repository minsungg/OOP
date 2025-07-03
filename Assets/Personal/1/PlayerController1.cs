using Combat.Enemies;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private Vector2 velocity;
    private Rigidbody2D rb;

    [SerializeField] private int hp = 3;

    public GameObject ProjectilePrefab;

    private Vector3 mouseWorldPos;
    private Vector2 direction;
    private GameObject bullet;

    private Slider HealthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = speed * velocity.normalized;

        if (Input.GetMouseButtonDown(0))
        {
            mouseWorldPos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0.0f;

            direction.x = (mouseWorldPos - transform.position).x;
            direction.y = (mouseWorldPos - transform.position).y;

            bullet = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;
        }

        HealthBar.value = hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            hp -= 1;
        }

        if (hp <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager1>().GameOver();
        }
    }
}
