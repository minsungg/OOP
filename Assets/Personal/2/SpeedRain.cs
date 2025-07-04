using UnityEngine;

public class SpeedRain : RainStandard
{
    private Rigidbody2D rb;
    private float randomForce = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        randomForce = Random.Range(-1, 2);
        rb.AddForceY(randomForce, ForceMode2D.Impulse);
    }
}
