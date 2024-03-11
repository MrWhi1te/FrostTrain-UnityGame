using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadioFrequency : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text valueText;
    [SerializeField] private Text checkValueText;
    private float needFrequency;

    private void OnEnable()
    {
        checkValueText.text = "";
        needFrequency = Random.Range(52, 170);
        Debug.Log(needFrequency);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        valueText.text = slider.value + " MHz";

        if(slider.value == needFrequency)
        {
            Debug.Log("WIN!");
        }
        else if(slider.value < needFrequency)
        {
            checkValueText.text = "Неверная частота. Выше";
        }
        else
        {
            checkValueText.text = "Неверная частота. Ниже";
        }
    }
}
