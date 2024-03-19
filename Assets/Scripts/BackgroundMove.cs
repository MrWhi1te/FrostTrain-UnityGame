using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private Game GM; //

    private Transform thisTransf;
    [SerializeField] private float backgroundSpeed; // Скорость движения заднего фона
    private Vector2 startPosition;

    private float spriteWidth;

    private void Start()
    {
        thisTransf = transform;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (GM.SpeedFon > 0) thisTransf.position += (backgroundSpeed + GM.SpeedFon) * Time.deltaTime * Vector3.left;

        if (thisTransf.position.x < 0)
        {
            thisTransf.position += Vector3.right * spriteWidth;
        }
    }
}
