using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengersScript : MonoBehaviour
{
    public Game GM;

    [Header("Passengers")]
    public List<Pass> pass = new();
    public List<PassPan> passPan = new();

    public List<Pass> passOnTrain;
    public List<Pass> passOffTrain;

    void AddList()
    {
        for(int i = 0; i < pass.Count; i++)
        {
            if (pass[i].statusPass == 1 && pass[i].statusEvent == 1)
            {
                passOnTrain.Add(pass[i]);
            }
            else if (pass[i].statusPass == 2 && pass[i].statusEvent == 2)
            {
                passOffTrain.Add(pass[i]);
            }
        }
    }

    public void CheckPassStation()
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
            }
        }
    }

    public void TakePassTrain(int index)
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

    public void ExitStation()
    {
        for(int i = 0; i < passPan.Count; i++)
        {
            if (passPan[i].passObj.activeInHierarchy)
            {
                if (passPan[i].passTakeTrain) pass[passPan[i].passNumber].statusPass = 1;
                else pass[passPan[i].passNumber].statusPass = 2;
                passPan[i].passTakeTrain = false;
                passPan[i].passObj.SetActive(false);
            }
        }
    }
}

[Serializable]
public class PassPan
{
    public GameObject passObj;
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
    public GameObject eventPass; // Объект события для пассажира
    public int statusEvent; // 1 = событие происходит если взял пассажира / 2 = событие происходит если не взял пассажира
}
