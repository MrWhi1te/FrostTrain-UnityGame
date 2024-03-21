using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private Game GM; //

    private Transform thisTransf;
    [SerializeField] private float backgroundSpeed; // Скорость движения заднего фона
    private Vector3 startPosition;

    private float spriteWidth;

    private void Start()
    {
        thisTransf = transform;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = thisTransf.position;
    }

    private void FixedUpdate()
    {
        //if (GM.SpeedFon > 0) thisTransf.position += (backgroundSpeed + GM.SpeedFon) * Time.deltaTime * Vector3.left;

        if (GM.SpeedFon > 0)
        {
            float newPosition = Mathf.Repeat(Time.time * (backgroundSpeed + GM.SpeedFon), spriteWidth);
            thisTransf.position = startPosition + Vector3.left * newPosition;
        }
        //if (thisTransf.position.x < 0)
        //{
        //    thisTransf.position += Vector3.right * spriteWidth;
        //}
    }
}
