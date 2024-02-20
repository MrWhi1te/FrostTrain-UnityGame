using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Game GM; //

    public float backgroundSpeed; // �������� �������� ������� ����
    private Vector2 startPosition; // ��������� ������� ������� ����

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * (backgroundSpeed * GM.SpeedFon), 20); 
        transform.position = startPosition + Vector2.left * newPosition;
    }
}
