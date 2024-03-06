using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    [SerializeField] private Game GM;
    [SerializeField] private StationScripts ST;
    [SerializeField] private Audio AO;

    [SerializeField] private GameObject endPan;
    [SerializeField] private Text endText;
    [SerializeField] private Text statisticText;
    [SerializeField] [TextArea] private string[] end;
    [SerializeField] private GameObject questPan;
    [SerializeField] private Text namePersonQuest;
    [SerializeField] private Text textQuest;
    private bool activeTask;
    private int numberQuest;

    public List<Quest> quest = new();


    public void CheckQuest()
    {
        int done = 0;
        for (int i=0; i < quest.Count; i++)
        {
            if(quest[i].cityPerson == GM.NameCity[GM.City] && !quest[i].doneTask)
            {
                if(i > 0 && !quest[i - 1].doneTask)
                {
                    textQuest.text = "Вам нужно посетить станцию " + quest[i-1].cityPerson + ". Там вас ожидает человек";
                    questPan.SetActive(true);
                    namePersonQuest.text = quest[i].namePerson;
                    activeTask = false;
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
            if (quest[i].doneTask) done++;
        }
        if (done == quest.Count)
        {
            if(GM.NameCity[GM.City] == "Владивосток")
            {
                endText.text = end[0];
            }
            else if(GM.NameCity[GM.City] == "Магнитогорск")
            {
                endText.text = end[1];
            }
            endPan.SetActive(true);
            statisticText.text = "Статистика вашей игры:" + "\n" + "Собрано угля: " + GM.CoalPlusStatistic + "\n" + "Собрано еды: " + GM.FoodPlusStatistic + "\n" + "Собрано воды:" + GM.WaterPlusStatistic 
                + "\n" + "Собрано тепла: " + GM.WarmPlusStatistic + "\n" + "Заработано денег: " + GM.MoneyPlusStatistic + "\n" + "Пройдено: " + GM.DistancePlusStatistic + "\n" + "Очков: " + GM.Score;
        }
    }

    public void AcceptTask()
    {
        if (activeTask)
        {
            quest[numberQuest].doneTask = true;
            if(numberQuest > 0)
            {
                GM.Money += quest[numberQuest].rewardTask;
                ST.ResourceTextUpdate();
                GM.CargoSpecTransportCount = 0;
                GM.CargoSpec1TransportCount = 0;
                AO.PlayAudioTakeResource();
            }
            if (quest[numberQuest].wagonTask[0] > 0 || quest[numberQuest].wagonTask[1] > 0)
            {
                GM.CargoSpecTransportCount = quest[numberQuest].wagonTask[0];
                GM.CargoSpec1TransportCount = quest[numberQuest].wagonTask[1];
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
    public int targetCity;
    public int rewardTask;
    public int[] wagonTask;
}
