using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private GameObject player;
    private Vector3 pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        pos = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;

        if(pos.x<-6)
        {
            pos.x = -6;
        } else if(pos.x>6)
        {
            pos.x = 6;
        }

        if (pos.y < -12)
        {
            pos.y = -12;
        }
        else if (pos.y > 12)
        {
            pos.y = 12;
        }

        transform.position = pos;
    }
}
