using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengersScript : MonoBehaviour
{
    [SerializeField] private Game GM;
    [SerializeField] private StationScripts ST;
    [SerializeField] private Audio AO;

    private List<Pass> passStationList = new();

    [Header("Station")]
    [SerializeField] private GameObject passPan;
    [SerializeField] private GameObject swipeCard;
    [SerializeField] private Image passPhoto;
    [SerializeField] private Text passName;
    [SerializeField] private Text passRequest;
    [SerializeField] private Text[] passResource;
    [SerializeField] private Text placeFreeText;
    [SerializeField] private GameObject helpPan;

    [Header("Passengers")]
    public List<Pass> pass = new();

    public void EnterStation()
    {
        for (int i = 0; i < pass.Count; i++)
        {
            if (pass[i].cityLocation == GM.NameCity[GM.City] && !pass[i].statusPass)
            {
                pass[i].numberPass = i;
                passStationList.Add(pass[i]);
            }
        }
    }

    void SwipeCardCheck()
    {
        if (passStationList.Count > 0)
        {
            passName.text = passStationList[0].namePass;
            passRequest.text = passStationList[0].requestPass;
            for (int i = 0; i < passResource.Length; i++) passResource[i].text = passStationList[0].usageResources[i].ToString();
            swipeCard.SetActive(true);
        }
        else
        {
            passPan.SetActive(false);
            ST.StartNextStation();
        }
        placeFreeText.text = "Мест свободно: " + ((GM.passWagoneCount * 3) - GM.passCount);
    }

    public void TakePassTrain()
    {
        AO.PlayAudioTakePass();
        pass[passStationList[0].numberPass].statusPass = true;
        passStationList.Remove(passStationList[0]);
        GM.passCount++;
        Debug.Log("Кол-во в листе: " + passStationList.Count);
        for (int i = 0; i < GM.PassResourceUse.Length; i++)
        {
            Debug.Log("Индекс: " + i);
            Debug.Log("Значение в GM: " + GM.PassResourceUse[i]);
            Debug.Log("Значение в пасс: " + passStationList[0].usageResources[i]);
            GM.PassResourceUse[i] += passStationList[0].usageResources[i];
        }
        swipeCard.SetActive(false);
        SwipeCardCheck();
    }

    public void NotTakePassTrain()
    {
        AO.PlayAudioNotTakePass();
        passStationList.Remove(passStationList[0]);
        swipeCard.SetActive(false);
        SwipeCardCheck();
    }

    public void ExitStation()
    {
        if (passStationList.Count > 0)
        {
            passPan.SetActive(true);
            SwipeCardCheck();
            if (!GM.helpPass)
            {
                helpPan.SetActive(true);
                AO.PlayAudioEnterPanel();
            }
        }
        else ST.StartNextStation();
    }

    public void ClosedHelpPan()
    {
        GM.helpPass = true;
        helpPan.SetActive(false);
        swipeCard.SetActive(false); swipeCard.SetActive(true);
        AO.PlayAudioClickBttn();
    }
}

[Serializable]
public class Pass
{
    public string namePass;
    public Sprite photoPass;
    [TextArea]public string requestPass;
    public string cityLocation;
    public int[] usageResources; // [0]-Еда, [1]-Вода, [2]-Тепло 
    [HideInInspector] public bool statusPass; // true = Пассажира взяли на поезд 
    [HideInInspector] public int numberPass;
}

