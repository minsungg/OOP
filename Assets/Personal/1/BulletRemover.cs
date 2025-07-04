using UnityEngine;

public class BulletRemover : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x < -20 || transform.position.x > 20 || transform.position.y > 20 || transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
}
