using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Game GM; //

    //public float length, startpos;
    //public GameObject cam;
    //public float parallaxEffect;
    public float backgroundSpeed; // —корость движени€ заднего фона
    private Vector2 startPosition; // Ќачальна€ позици€ заднего фона

    private void Start()
    {
        //startpos = transform.position.x;
        //length = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position;
    }
    private void Update()
    {
        //float temp = cam.transform.position.x * (1 - parallaxEffect);
        //float dist = cam.transform.position.x * parallaxEffect;

        //transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        //if (temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -= length;
        float newPosition = Mathf.Repeat(Time.time * backgroundSpeed, 20); // «десь 20 - ширина вашего фона в мировых координатах
        transform.position = startPosition + Vector2.left * newPosition;

    }
}
