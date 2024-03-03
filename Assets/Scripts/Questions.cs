using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    [SerializeField] private Game GM;
    [SerializeField] private StationScripts ST;
    [SerializeField] private Audio AO;

    public List<Quest> quest = new();

    [HideInInspector] public string targetCity;

    [SerializeField] private GameObject questPan;
    [SerializeField] private Text namePersonQuest;
    [SerializeField] private Text textQuest;
    private bool activeTask;
    private int numberQuest;


    public void CheckQuest()
    {
        for (int i=0; i < quest.Count; i++)
        {
            if(quest[i].cityPerson == GM.NameCity[GM.City] && !quest[i].doneTask)
            {
                if(i > 0 && !quest[i - 1].doneTask)
                {
                    textQuest.text = "Вам нужно посетить станцию " + quest[i-1].cityPerson + ". Там вас ожидает человек";
                    questPan.SetActive(true);
                    namePersonQuest.text = quest[i].namePerson;
                }
                else
                {
                    questPan.SetActive(true);
                    namePersonQuest.text = quest[i].namePerson;
                    textQuest.text = quest[i].textQuest;
                    numberQuest = i;
                    activeTask = true;
                }
                break;
            }
        }
    }

    public void AcceptTask()
    {
        if (activeTask)
        {
            quest[numberQuest].doneTask = true;
            targetCity = quest[numberQuest].targetCity;
            if(quest[numberQuest].wagonTask[0] > 0 || quest[numberQuest].wagonTask[1] > 0)
            {
                GM.CargoSpecTransportCount = quest[numberQuest].wagonTask[0];
                GM.CargoSpec1TransportCount = quest[numberQuest].wagonTask[1];
            }
            if(numberQuest > 0)
            {
                GM.Money += quest[numberQuest].rewardTask;
                ST.ResourceTextUpdate();
                AO.PlayAudioTakeResource();
            }
        }
        questPan.SetActive(false);
    }
}

[Serializable]
public class Quest
{
    public string namePerson;
    [TextArea] public string textQuest;
    public string cityPerson;
    public bool doneTask;
    public string targetCity;
    public int rewardTask;
    public int[] wagonTask;
}
