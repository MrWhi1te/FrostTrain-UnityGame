using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private Game GM; //

    private Transform thisTransf;
    [SerializeField] private float backgroundSpeed; // Скорость движения заднего фона

    private float spriteWidth;

    private void Start()
    {
        thisTransf = transform;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        if(GM.SpeedFon > 0) thisTransf.position += (backgroundSpeed + GM.SpeedFon) * Time.fixedDeltaTime * Vector3.left;

        if (thisTransf.position.x < 0)
        {
            thisTransf.position += Vector3.right * spriteWidth;
        }
    }
}
