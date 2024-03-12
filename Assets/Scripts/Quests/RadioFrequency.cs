using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadioFrequency : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private Audio AO;

    [SerializeField] private GameObject pan;
    [SerializeField] private Slider slider;
    [SerializeField] private Text valueText;
    [SerializeField] private Text checkValueText;
    private float needFrequency;

    private AudioSource thisAudio;
    [SerializeField] private AudioClip[] music;

    private void Start()
    {
        thisAudio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        checkValueText.text = "";
        needFrequency = Random.Range(52, 170);
        thisAudio.clip = music[0];
        thisAudio.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        valueText.text = slider.value + " MHz";

        if(slider.value == needFrequency)
        {
            AO.PlayAudioSuccess();
            pan.SetActive(false);
        }
        else if(slider.value < needFrequency)
        {
            checkValueText.text = "Неверная частота. Выше";
            CheckAudio();
        }
        else
        {
            checkValueText.text = "Неверная частота. Ниже";
            CheckAudio();
        }
    }

    void CheckAudio()
    {
        if (needFrequency - slider.value < 10 && needFrequency - slider.value > -10)
        {
            thisAudio.Stop();
            thisAudio.clip = music[1];
            thisAudio.Play();
        }
        else
        {
            thisAudio.Stop();
            thisAudio.clip = music[0];
            thisAudio.Play();
        }
    }
}
