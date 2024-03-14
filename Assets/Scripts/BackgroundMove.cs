using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private Game GM; //

    private Transform thisTransf;
    [SerializeField] private float backgroundSpeed; // Скорость движения заднего фона
    private float currentSpeed;
    private float smoothTime = 0.1f;
    private Vector2 startPosition; // Начальная позиция заднего фона
    private Vector3 size;

    private float spriteWidth;
    private void Start()
    {
        thisTransf = transform;
        startPosition = transform.position;
        //size = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        //if (currentSpeed > GM.SpeedFon) currentSpeed = 0;
        //if (GM.SpeedFon > 0)
        //{
        //    currentSpeed = Mathf.Lerp(currentSpeed, GM.SpeedFon, smoothTime * Time.fixedDeltaTime);
        //    float newPosition = Mathf.Repeat(Time.time * (backgroundSpeed + currentSpeed), size.x);
        //    thisTransf.position = startPosition + Vector2.left * newPosition;
        //}
        //else currentSpeed = 0;
        transform.position += Vector3.left * (currentSpeed + GM.SpeedFon) * Time.deltaTime;

        // Если спрайт вышел за пределы экрана, перемещаем его обратно в начало
        if (transform.position.x + spriteWidth / 2 < -spriteWidth / 2)
        {
            transform.position += Vector3.right * spriteWidth;
        }
    }
}
