using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Start()
    {
        float targetAspect = 9f / 16f; // �������� ����������� ������ (������ / ������)
        float currentAspect = (float)Screen.height / Screen.width; // ������� ����������� ������

        // ������ ������ ������� ������, ����� ������� ���������� �������� �� ���� �����������
        float differenceInSize = currentAspect / targetAspect;
        Camera.main.orthographicSize *= differenceInSize;
    }
}
