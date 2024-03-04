using UnityEngine;
using UnityEngine.EventSystems;

public class BttnRetention : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Game GM;

    [SerializeField] private float holdTime;
    [SerializeField] private string nameType;
    private AudioSource thisAudio;
    private float currentTime;
    private UnityEngine.UI.Image thisImage;

    void Start()
    {
        thisImage = GetComponent<UnityEngine.UI.Image>();
        thisAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            currentTime += Time.deltaTime;
            thisImage.fillAmount = currentTime/10;

            if (currentTime >= holdTime)
            {
                CheckType();
            }
        }
        else currentTime = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        thisAudio.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        thisAudio.Stop();
    }

    void CheckType()
    {
        if(nameType == "Repair")
        {
            GM.repairTrainObj.SetActive(false);
            if (!GM.helpRepair)
            {
                GM.helpRepair = true;
                GM.helpTrainRepairObj.SetActive(false);
            }
        }
        else if (nameType == "Barrier")
        {
            GM.barrierObj.SetActive(false);
            if (!GM.helpRepair)
            {
                GM.helpRepair = true;
                GM.helpTrainRepairObj.SetActive(false);
            }
        }
        GM.StartEngineLoco();
    }
}
