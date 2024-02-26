using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Game GM; //

    private Transform thisTransf;
    [SerializeField] private float backgroundSpeed; // Скорость движения заднего фона
    private Vector2 startPosition; // Начальная позиция заднего фона
    private Vector3 size; 

    private void Start()
    {
        thisTransf = transform;
        startPosition = transform.position;
        size = GetComponent<SpriteRenderer>().bounds.size;
    }

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * (backgroundSpeed * GM.SpeedFon), size.x);
        thisTransf.position = startPosition + Vector2.left * newPosition;
    }
}
