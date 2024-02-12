using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Game GM; //
    public GameObject[] Point;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Point[0].transform.position, GM.SpeedFon * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "point")
        {
            transform.position = Point[1].transform.position;
        }
    }
}
