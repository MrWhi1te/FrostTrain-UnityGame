using UnityEngine;
using UnityEngine.UI;

public class SwipeCardMobile : MonoBehaviour
{
    [SerializeField] private Game GM;
    [SerializeField] private PassengersScript PS;

    private Transform thisTransf;
    private Vector2 startPoint;
    private Vector2 offset;
    private Image thisImage;
    private bool isDragging;


    void Start()
    {
        thisTransf = transform;
        thisImage = GetComponent<Image>();
        startPoint = thisTransf.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            offset = thisTransf.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)); // Смещение между позицией мыши и центром объекта
            isDragging = true;
        }
        else if (Input.GetMouseButton(0))
        {
            if (isDragging)
            {
                Vector2 currentScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                Vector2 currentPosition = (Vector2)Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset; // Преобразуем эту позицию в пространство игры и применяем смещение

                thisTransf.position = currentPosition;

                if (thisTransf.position.x > startPoint.x && GM.passCount < (GM.passWagoneCount * 3))
                {
                    thisImage.color = new Color32(180, 255, 180, 255);
                }
                else
                {
                    thisImage.color = new Color32(255, 180, 180, 255);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (thisTransf.position.x > startPoint.x && GM.passCount < (GM.passWagoneCount * 3))
            {
                HandleSwipe("Right");
            }
            else
            {
                HandleSwipe("Left");
            }
        }
    }

    private void HandleSwipe(string direction)
    {
        thisImage.color = new Color32(255, 255, 255, 255);
        thisTransf.position = startPoint;
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
