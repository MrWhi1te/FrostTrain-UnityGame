using UnityEngine;

public class SwipeCardPC : MonoBehaviour
{
    [SerializeField] private Game GM;
    [SerializeField] private PassengersScript PS;

    private Vector3 startPoint;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float dragThreshold = 0.2f;
    private UnityEngine.UI.Image thisImage;

    private void Start()
    {
        startPoint = transform.position;
        thisImage = GetComponent<UnityEngine.UI.Image>();
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); // ������� ������� ���� � ������������ ������

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)); // �������� ����� �������� ���� � ������� �������
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint;

        currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset; // ����������� ��� ������� � ������������ ���� � ��������� ��������

        transform.position = currentPosition;

        if (Vector3.Distance(transform.position, startPoint) > dragThreshold)
        {
            if (transform.position.x > startPoint.x && GM.passCount < (GM.passWagoneCount * 3))
                thisImage.color = new Color32(180, 255, 180, 255);
            else thisImage.color = new Color32(255, 180, 180, 255);
        }
    }

    void OnMouseUp()
    {
        if (Vector3.Distance(transform.position, startPoint) > dragThreshold) // ���� �������� ���� ���������� ������ ��� dragThreshold, ������� ��� �������
        {
            if (transform.position.x > startPoint.x && GM.passCount < (GM.passWagoneCount * 3))
                HandleSwipe("Right");
            else HandleSwipe("Left");
        }
        else transform.position = startPoint; // ���� �������� �� ���� ����������, ���������� �� �� �������� �������
    }

    private void HandleSwipe(string direction)
    {
        thisImage.color = new Color32(255, 255, 255, 255);
        transform.position = startPoint;
        if (direction == "Right")
        {
            PS.TakePassTrain();
        }
        else
        {
            PS.NotTakePassTrain();
        }
    }
}
