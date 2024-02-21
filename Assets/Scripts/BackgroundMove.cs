using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Game GM; //

    public float backgroundSpeed; // Скорость движения заднего фона
    private Vector2 startPosition; // Начальная позиция заднего фона
    public Vector3 size; 

    private void Start()
    {
        startPosition = transform.position;
        size = GetComponent<SpriteRenderer>().bounds.size;
    }

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * (backgroundSpeed * GM.SpeedFon), size.x); 
        transform.position = startPosition + Vector2.left * newPosition;
    }
}
