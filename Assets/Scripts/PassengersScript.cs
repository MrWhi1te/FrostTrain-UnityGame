using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengersScript : MonoBehaviour
{
    public Game GM;
    public Audio AO;

    [Header("Passengers")]
    public List<Pass> pass = new();
    [SerializeField] private List<PassPan> passPan = new();

    private List<Pass> passEventList;

    [Header("PassEvent")]
    [SerializeField] private GameObject passEventPan;
    [SerializeField] private Text passEventText;
    private int numberEvent;

    [Header("Station")]
    [SerializeField] private GameObject refugesBttn;
    [SerializeField] private GameObject refugesPan;
    [SerializeField] private Text refugesResourceText;
    [SerializeField] private Text placeFreeText;
    [SerializeField] private GameObject[] refugeRandom;

    private void Start()
    {
        StartAddList();
    }

    void StartAddList()
    {
        for(int i = 0; i < pass.Count; i++)
        {
            if (pass[i].statusPass != 0 && pass[i].statusEvent)
            {
                pass[i].numberPass = i;
                passEventList.Add(pass[i]);
            }
        }
    }

    public void EnterStation()
    {
        int r = 0;
        for (int i = 0; i < pass.Count; i++)
        {
            if (pass[i].cityLocation == GM.NameCity[GM.City] && pass[i].statusPass == 0)
            {
                passPan[r].passObj.SetActive(true);
                passPan[r].passName.text = pass[i].namePass;
                passPan[r].passDescription.text = pass[i].descriptionPass;
                passPan[r].passRequest.text = pass[i].requestPass;
                passPan[r].passTakeTrainText.text = "Взять";
                passPan[r].passNumber = i;
                r++;
                refugesBttn.SetActive(true);
            }
        }
        int rand = UnityEngine.Random.Range(0, refugeRandom.Length);
        refugeRandom[rand].SetActive(true);
        checkResource();
    }

    public void TakePassTrain(int index)
    {
        if (GM.passCount < GM.passWagoneCount * 3)
        {
            if (!passPan[index].passTakeTrain)
            {
                passPan[index].passTakeTrain = true;
                passPan[index].passTakeTrainText.text = "Убрать";
            }
            else
            {
                passPan[index].passTakeTrain = false;
                passPan[index].passTakeTrainText.text = "Взять";
            }
        }
        checkResource();
        AO.PlayAudioClickTrain();
    }

    void checkResource()
    {
        int[] r = new int[3];
        for(int i=0; i < passPan.Count; i++)
        {
            if (passPan[i].passTakeTrain)
            {
                r[0] += pass[passPan[i].passNumber].usageResources[0];
                r[1] += pass[passPan[i].passNumber].usageResources[1];
                r[2] += pass[passPan[i].passNumber].usageResources[2];
            }
        }
        placeFreeText.text = "Мест свободно: " + ((GM.passWagoneCount * 3) - GM.passCount);
        refugesResourceText.text = "Дополнительное потребление ресурсов: " + r[0] + "`Еда` " + r[1] + "`Вода` " + r[2] + "`Тепло`";
    }

    public void ExitStation()
    {
        for(int i = 0; i < passPan.Count; i++)
        {
            if (passPan[i].passObj.activeInHierarchy)
            {
                if (passPan[i].passTakeTrain) pass[passPan[i].passNumber].statusPass = 1;
                else pass[passPan[i].passNumber].statusPass = 2;
                if (pass[i].statusPass != 0 && pass[i].statusEvent)
                {
                    pass[i].numberPass = i;
                    passEventList.Add(pass[i]);
                }
                passPan[i].passTakeTrain = false;
                passPan[i].passObj.SetActive(false);
            }
        }
        for(int i = 0; i < refugeRandom.Length; i++) { refugeRandom[i].SetActive(false); }
        refugesBttn.SetActive(false);
        CheckEvent();
    }

    void CheckEvent()
    {
        for(int i = 0; i < passEventList.Count; i++)
        {
            int n = passEventList[i].statusPass;
            if (pass[passEventList[i].numberPass].passEvent[n].stationBeforeEvent <= 0)
            {
                passEventPan.SetActive(true);
                passEventText.text = passEventList[i].passEvent[n].eventPassText + "\n" + "Это событие приносит вам: " +
                    passEventList[i].passEvent[n].eventResources[0] + " Еды " + passEventList[i].passEvent[n].eventResources[1] + " Воды " + passEventList[i].passEvent[n].eventResources[0] + " Тепло";
                pass[passEventList[i].numberPass].statusEvent = false;
                numberEvent = i;
                passEventList.Remove(passEventList[i]);
                break;
            }
            else pass[passEventList[i].numberPass].passEvent[n].stationBeforeEvent--;
        }
    }

    public void TakeResourceEvent()
    {
        GM.Food += passEventList[numberEvent].passEvent[passEventList[numberEvent].statusPass].eventResources[0];
        GM.Water += passEventList[numberEvent].passEvent[passEventList[numberEvent].statusPass].eventResources[1];
        GM.Warm += passEventList[numberEvent].passEvent[passEventList[numberEvent].statusPass].eventResources[2];
        passEventPan.SetActive(false);
        CheckEvent();
        AO.PlayAudioClickTrain();
    }

    public void OpenClosedRefugesPan()
    {
        if (refugesPan.activeInHierarchy) refugesPan.SetActive(true);
        else refugesPan.SetActive(false);
        AO.PlayAudioClickBttn();
    }
}

[Serializable]
public class PassPan
{
    public GameObject passObj;
    public Image passPhoto; 
    public Text passName;
    public Text passDescription;
    public Text passRequest;
    public Text passTakeTrainText;
    public bool passTakeTrain;
    public int passNumber;
}

[Serializable]
public class Pass
{
    public Sprite photoPass;
    public string namePass;
    public string descriptionPass;
    public string requestPass;
    public string cityLocation;
    public int[] usageResources; // [0]-Еда, [1]-Вода, [2]-Тепло 
    public int statusPass; // 1 = Пассажира взяли на поезд / 2 = Пассажира не взяли на поезд
    public bool statusEvent;
    public int numberPass;
    public List<PassEvent> passEvent;
}

[Serializable]
public class PassEvent
{
    public int stationBeforeEvent;
    public string eventPassText; // Текст события для пассажира  | 0 - для взял пассажира / 1 - для не взял
    public int[] eventResources; // [0]-Еда, [1]-Вода, [2]-Тепло
}
