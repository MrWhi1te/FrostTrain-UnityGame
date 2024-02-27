using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StationScripts : MonoBehaviour
{
    public GameObject StationPan; // ������ �������
    public GameObject GamePan; //
    public Game GM; // ������ ����
    [Header("Market")]
    public GameObject MarketPan; // ������ �����
    public Slider[] SliderMarket; // �������� �����
    public Text[] ColResourceText; // ����� ���-�� �������� �����
    public Text[] ColMoneyText; // ����� �������/�������
    [Header("Train")]
    public GameObject TrainPan; // ������ ������
    public GameObject LocoBuyPan; // ������ ������� ����������
    public GameObject ViewTrainPan; // ������ ����� ���� ������
    public GameObject SpecialWagPan; // ������ ����������� �������
    public Text[] TrainViewText; //
    public Text[] LocoUpdateText; //
    [Header("QuestPass")]
    public GameObject QuestPan; // ������ �������
    public GameObject StartRewardQuestPan; //
    public Text StartRewardQuestText; //
    public Text ActiveTextQuest; //
    [Header("QuestCargo")]
    public GameObject CargoQuestPan; //
    public Text CargoWagoneTransport; //
    public Text CargoWagoneCoal; //
    public Text CargoWagoneReward; //
    int CargoTransport; //
    int CargoReward; //
    [Header("NextStation")]
    public Text NextStationText; // ����� ��������� �������
    public GameObject[] BttnNextStation; //
    public bool StationVibor; //
    [Header("AddWagone")]
    public Text CountWagon; // ������� ���-�� �������
    public Text BuyWagoneText; //
    [Header("Resource")]
    public Text MoneyText; // ����� �����
    public Text DiamondText; // ����� �������
    public Text CoalText; // ����� ����
    public Text FoodText; // ����� ���
    public Text WaterText; // ����� ����
    public Text WarmText; // ����� �����
    public Text WorkerText; // ����� �������
    [SerializeField] private Text[] plusResourceText; // 0-����� / 1-��� / 2-���� / 3-�����
    public Text PlusMoneyText; // 
    [Header("MoneyView")]
    public Text MoneyShowText; // 
    [Header("Train")]
    public GameObject[] TrainerPan; //
    public int TrainerCount; //
    [Header("ADS")]
    public GameObject ShopPan; //
    public GameObject ADSMoneyActive; //
    public Text ADSMoneyActiveText; // ����� ����� �� �������
    int ADSMoneyCol; // ������� ����� �� �������
    [Header("TASK")]
    public GameObject[] TaskPan;
    int TaskCounter;
    //
    public Text TimeInGameStatisticText; //
    public Text FoodStatisticText; //
    public Text CoalPlusStatisticText; //
    public Text WaterPlusStatisticText; //
    public Text WarmPlusStatisticText; //
    public Text MoneyPlusStatisticText; //
    public Text DistancePlusStatisticText; //
    public Text ScorePlusStatisticText; //

    public Text[] ScoreTextView; //

    private int[,] resourcesInfo; // ���������� �������, ����������� �������, ���� �������, ���� �������.

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        resourcesInfo = new int[,]
        {
            { GM.Coal, GM.CoalMax, 1, 2 },
            { GM.Food, GM.FoodMax, 9, 30 },
            { GM.Water, GM.WaterMax, 8, 25 },
            { GM.Warm, GM.WarmMax, 5, 20 }
        };
        if (GM.Trainer[1] == false)
        {
            TrainerPan[0].SetActive(true);
        }
        BttnNextStation[1].SetActive(false);
        StationVibor = false;
        TimerNextStation();
        ResourceTextUpdate();
        GM.Score+=10;
        ScoreCount();
        for (int i = 0; i < 9; i++)
        {
            ActiveSlider(i);
        }
        if (GM.CargoTransportCount >= 1)
        {
            CargoTransport = GM.CargoTransportCount;
            for (int i = 0; i < GM.CargoTransportCount; i++)
            {
                GM.CargoTransportWagone[i].SetActive(false);
            }
            GM.CargoTransportCount = 0;
            StartRewardQuestPan.SetActive(true);
            StartRewardQuestText.text = "�� ���������: " + CargoTransport + " �������. ������� ���������: " + GM.RewardCargoTransport + "$";
            if(GM.TaskCount == 4)
            {
                GM.TaskCount = 5;
            }
        }
        UpdateADSMoneyActive();
        Questioner();
    }
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    public void TimerNextStation() //
    {
        GM.NextStationTimeCount = GM.TimeForCity[GM.ChoiceCity];
        GM.NextStationTime = GM.TimeForCity[GM.ChoiceCity];
        GM.NextStationSlide.maxValue = GM.NextStationTime;
        int Min = GM.NextStationTime / 60;
        int Sec = GM.NextStationTime - (Min * 60);
        NextStationText.text = "�� " + GM.NameCity[GM.ChoiceCity] + ": " + Min + "���. " + Sec + "���." + "�����������: " + GM.TemperatureForCity[GM.ChoiceCity] + "�C";
    }
    public void StartNextStation() // ����� ��������� �������
    {
        GamePan.SetActive(true);
        for (int a = 0; a < GM.WagonCol; a++)
        {
            if (GM.WagoneData[a].Name != "")
            {
                GM.WagoneData[a].WagoneObj.SetActive(true);
            }
        }
        for (int a = 0; a < GM.WagonCol; a++)
        {
            if (GM.WagoneData[a].Name == "")
            {
                GM.WagoneData[a].WagoneObj.SetActive(true);
            }
        }
        if (GM.CargoTransportCount >= 1) // ���� ������� �������� �������
        {
            for (int i = 0; i < GM.CargoTransportCount; i++)
            {
                GM.CargoTransportWagone[i].SetActive(true);
                if (GM.TextureTrain == 0)
                {
                    GM.CargoTransportWagone[i].GetComponent<Image>().sprite = GM.SPR0[8];
                }
                else if (GM.TextureTrain == 1)
                {
                    GM.CargoTransportWagone[i].GetComponent<Image>().sprite = GM.SPR1[8];
                }
                else if (GM.TextureTrain == 2)
                {
                    GM.CargoTransportWagone[i].GetComponent<Image>().sprite = GM.SPR1[8];
                }
                else if (GM.TextureTrain == 3)
                {
                    GM.CargoTransportWagone[i].GetComponent<Image>().sprite = GM.SPR1[8];
                }
            }
            GM.StartTransportCargoWagoneQuest();
        }
        if (GM.CargoSpecTransportCount >= 1) // ���� ������� �������� SPEC �������
        {
            for (int i = 0; i < GM.CargoSpecTransportCount; i++)
            {
                GM.CargoSpecTransportWagone[i].SetActive(true);
            }
        }
        if (GM.CargoSpec1TransportCount >= 1) // ���� ������� �������� SPEC1 �������
        {
            for (int i = 0; i < GM.CargoSpec1TransportCount; i++)
            {
                GM.CargoSpec1TransportWagone[i].SetActive(true);
            }
        }
        if (GM.AirShipActive == true)
        {
            GM.AirShipObj.SetActive(true);
            GM.StartTimerAirShip();
        }
        GM.StartNextStation();
        GM.City = GM.ChoiceCity;
        GM.ChoiceCity = 0;
        GM.SpeedFon = 1;
        GM.RandomBackground();
        StationPan.SetActive(false);
        GM.Save();
        GM.TaskCounter();
        if (YandexGame.EnvironmentData.deviceType == "mobile")
        {
            YandexGame.StickyAdActivity(true);
        }
        YandexGame.FullscreenShow();
    }
    void ResourceTextUpdate() // ���������� �������
    {
        MoneyText.text = GM.Money + "$";
        DiamondText.text = GM.Diamond.ToString();
        CoalText.text = GM.Coal + "/" + GM.CoalMax;
        FoodText.text = GM.Food + "/" + GM.FoodMax;
        WaterText.text = GM.Water + "/" + GM.WaterMax;
        WarmText.text = GM.Warm + "/" + GM.WarmMax;
        WorkerText.text = GM.FreeWorker + "/" + GM.AllWorker; //
        CountWagon.text = "�������: " + GM.WagonCol + " �� " + GM.MaxWagone; //
        BuyWagoneText.text = (300 * GM.WagonCol) + "$";
    }
    public void BuyWagone() // ������� ������� / ����������
    {
        if(GM.WagonCol < GM.MaxWagone)
        {
            if(GM.Money >= (300 * GM.WagonCol))
            {
                GM.Money -= (300 * GM.WagonCol);
                StartCoroutine(PlusMoney(0 - (300 * GM.WagonCol)));
                GM.WagonCol++;
                ResourceTextUpdate();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
    }
    public void ActiveSlider(int index) // ��������� ���������
    {
        if (index < 4)
        {
            SliderMarket[index].maxValue = resourcesInfo[index % 4, 0];
            ColMoneyText[index].text = "+" + (SliderMarket[index].value * resourcesInfo[index % 4, 2]) + "$";
        }
        else
        {
            SliderMarket[index].maxValue = resourcesInfo[index % 4, 1] - resourcesInfo[index % 4, 0];
            ColMoneyText[index].text = "-" + SliderMarket[index].value * resourcesInfo[index % 4, 3] + "$";
        }
        ColResourceText[index].text = SliderMarket[index].value.ToString();
    }

    public void BuySellResource(int index) // ������� / ������� �������� �� �����
    {
        if(index < 4)
        {
            if(GM.Money >= SliderMarket[index+4].value * resourcesInfo[index % 4, 3])
            {
                GM.Money -= (int)SliderMarket[index + 4].value * resourcesInfo[index % 4, 3];
                StartCoroutine(PlusMoney(0 - ((int)SliderMarket[index + 4].value * resourcesInfo[index % 4, 3])));
                resourcesInfo[index % 4, 0] += (int)SliderMarket[index + 4].value;
                StartCoroutine(PlusResources(index % 4,(int)SliderMarket[index + 4].value));
                SliderMarket[index + 4].value = 0;
            }
        }
        else 
        {
            GM.Money += (int)SliderMarket[index-4].value * resourcesInfo[index % 4 , 2];
            GM.MoneyPlusStatistic += (int)SliderMarket[index-4].value * resourcesInfo[index % 4, 2];
            StartCoroutine(PlusMoney((int)SliderMarket[index - 4].value * resourcesInfo[index % 4, 2]));
            resourcesInfo[index % 4, 0] -= (int)SliderMarket[index - 4].value;
            StartCoroutine(PlusResources(index % 4, 0 - (int)SliderMarket[index-4].value));
            SliderMarket[index - 4].value = 0;
        }
        GM.Coal = resourcesInfo[0, 0];
        GM.Food = resourcesInfo[1, 0];
        GM.Water = resourcesInfo[2, 0];
        GM.Warm = resourcesInfo[3, 0];
        ResourceTextUpdate();
    }

    IEnumerator MoneyShow() // ����� ������� �� ������� ����� � �����
    {
        MoneyShowText.text = "�� ������� �����!";
        yield return new WaitForSeconds(2);
        MoneyShowText.text = "";
        yield break; 
    }
    public void OpenMTQPan(int index) // �������� �������� �������
    {
        if(index == 0)
        {
            MarketPan.SetActive(true);
            TrainPan.SetActive(false);
            QuestPan.SetActive(false);
        }
        else if (index == 1)
        {
            MarketPan.SetActive(false);
            TrainPan.SetActive(true);
            QuestPan.SetActive(false);
            if(GM.LevelLoco == 2 )
            {
                LocoUpdateText[0].text = "�����������";
                LocoUpdateText[1].text = "�������";
            }
            else if (GM.LevelLoco == 3)
            {
                LocoUpdateText[0].text = "�����������";
                LocoUpdateText[1].text = "�����������";
                LocoUpdateText[2].text = "�������";
            }
        }
        else if (index == 2)
        {
            MarketPan.SetActive(false);
            TrainPan.SetActive(false);
            QuestPan.SetActive(true);
        }
    }
    public void OpenTrainPan(int index) // �������� ������� ������
    {
        if (index == 0)
        {
            LocoBuyPan.SetActive(true);
            ViewTrainPan.SetActive(false);
            SpecialWagPan.SetActive(false);
        }
        else if (index == 1)
        {
            LocoBuyPan.SetActive(false);
            ViewTrainPan.SetActive(true);
            SpecialWagPan.SetActive(false);
            UpdateTextTrainView();
        }
        else if (index == 2)
        {
            LocoBuyPan.SetActive(false);
            ViewTrainPan.SetActive(false);
            SpecialWagPan.SetActive(true);
        }
    }
    private void UpdateTextTrainView()
    {
        for (int i = 0; i < TrainViewText.Length; i++)
        {
            if (i == GM.TextureTrain) TrainViewText[i].text = "�������";
            else if (GM.Texture[i]) TrainViewText[i].text = "��������";
        }
    }
    public void BuyViewTrain(int index) // ������� �������� ���� ������
    {
        // ������� ��������
        if (GM.Texture[index])
        {
            GM.TextureTrain = index;
            GM.ChoiceViewTrain(index);
            ResourceTextUpdate();
        }
        else
        {
            if (TryBuyTexture(index))
            {
                GM.TextureTrain = index;
                GM.ChoiceViewTrain(index);
                ResourceTextUpdate();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
        UpdateTextTrainView();
    }

    private bool TryBuyTexture(int index)
    {
        switch (index)
        {
            case 1:
                if (GM.Money >= 10000)
                {
                    GM.Money -= 10000;
                    StartCoroutine(PlusMoney(0 - 10000));
                    GM.Texture[index] = true;
                    return true;
                }
                break;
            case 2:
                if (GM.Money >= 30000)
                {
                    GM.Money -= 30000;
                    StartCoroutine(PlusMoney(0 - 30000));
                    GM.Texture[index] = true;
                    return true;
                }
                break;
            case 3:
                if (GM.Diamond >= 30)
                {
                    GM.Diamond -= 30;
                    GM.Texture[index] = true;
                    return true;
                }
                break;
        }
        return false;
    }

    public void BuyUpdateLoco(int index) // ������� �������� ������
    {
        if(index == 0 & GM.LevelLoco <= 1)
        {
            if(GM.Money >= 10000)
            {
                GM.Money -= 10000;
                StartCoroutine(PlusMoney(0 - 10000));
                GM.LevelLoco = 2;
                GM.LevelEngine = 1;
                GM.LevelChassis = 1;
                GM.LevelCoalStorage = 1;
                GM.LevelLocoUpdater();
                GM.TextLoco();
                ResourceTextUpdate();
                GM.ViewLoco();
                LocoUpdateText[0].text = "�����������";
                LocoUpdateText[1].text = "�������";
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
        if (index == 1 & GM.LevelLoco <= 2)
        {
            if (GM.Money >= 25000)
            {
                GM.Money -= 25000;
                StartCoroutine(PlusMoney(0 - 25000));
                GM.LevelLoco = 3;
                GM.LevelEngine = 1;
                GM.LevelChassis = 1;
                GM.LevelCoalStorage = 1;
                GM.LevelLocoUpdater();
                GM.TextLoco();
                ResourceTextUpdate();
                GM.ViewLoco();
                LocoUpdateText[0].text = "�����������";
                LocoUpdateText[1].text = "�����������";
                LocoUpdateText[2].text = "�������";
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
    }

    public void BuyAirShips()
    {
        if(GM.AirShipActive == false)
        {
            if(GM.Money >= 50000)
            {
                GM.Money -= 50000;
                StartCoroutine(PlusMoney(0 - 50000));
                ResourceTextUpdate();
                GM.AirShipActive = true;
            }
        }
    }
    



    public void EnterQuestPass() // ������� �������
    {
        if (CargoTransport >= 1)
        {
            ActiveTextQuest.text = "������� �������!" + "\n" + "�������� �������: " + CargoTransport;
        }
        else { ActiveTextQuest.text = ""; }
    }
    public void EnterRewardQuest() // ������� ������� �� ��������
    {
        GM.Money += GM.RewardCargoTransport;
        GM.MoneyPlusStatistic += GM.RewardCargoTransport;
        StartCoroutine(PlusMoney(GM.RewardCargoTransport));
        CargoTransport = 0;
        StartRewardQuestPan.SetActive(false);
        ActiveTextQuest.text = "";
        ResourceTextUpdate();
        GM.Score += GM.RewardCargoTransport/50;
        ScoreCount();
    }

    // TRANSPORT CARGO!!!
    void CargoTransportationUpdate() // ��������� ������ ��������
    {
        CargoWagoneTransport.text = CargoTransport.ToString();
        CargoWagoneCoal.text = "-" + CargoTransport * 20;
        CargoWagoneReward.text = CargoReward + "$";
    }
    public void AddCargoTransport() // �������� ����� ��������
    {
        if (StationVibor == true)
        {
            if (CargoTransport < 10)
            {
                CargoTransport++;
                CargoReward = CargoTransport * (GM.NextStationTime * 2);
                CargoTransportationUpdate();
            }
        }
        else
        {
            CargoWagoneReward.text = "��� ������ �������� ������� ����������!";
        }
    }
    public void RemoveCargoTransport() // ����� ����� ��������
    {
        if (CargoTransport > 0)
        {
            CargoTransport--;
            CargoReward = CargoTransport * (GM.NextStationTime * 2);
            CargoTransportationUpdate();
        }
    }
    public void EnterQuestCargo() // ������� �������
    {
        GM.CargoTransportCount = CargoTransport;
        GM.RewardCargoTransport = CargoReward;
        if (CargoTransport >= 1)
        {
            ActiveTextQuest.text = "������� �������!" + "\n" + "�������� �������: " + CargoTransport;
        }
        else { ActiveTextQuest.text = ""; }
    }

    public void OpenMapPan()
    {
        BttnNextStation[1].SetActive(true);
        BttnNextStation[0].SetActive(true);
        StationVibor = true;
    }

    public void ScoreCount() // ����� �����
    {
        int r = 0;
        int number = 0;
        if (GM.Score < 10000)
        {
            number = GM.Score;
        }
        else if (GM.Score > 10000)
        {
            number = 9999;
        }
        while (number > 0)
        {
            int digit = number % 10;
            number /= 10;
            ScoreTextView[r].text = digit.ToString();
            r++;
        }
    }

    // TRAINER!!!

    public void TrainerNext()
    {
        if (TrainerCount == 2)
        {
            GM.Trainer[1] = true;
            TrainerPan[0].SetActive(false);
        }
        if (TrainerCount == 1)
        {
            TrainerPan[2].SetActive(false);
            TrainerPan[3].SetActive(true);
            TrainerCount = 2;
        }
        if (TrainerCount == 0)
        {
            TrainerPan[1].SetActive(false);
            TrainerPan[2].SetActive(true);
            TrainerCount = 1;
        }
    }



    // �������� �� ������ ����������� � ��������� ��������!
    IEnumerator PlusResources(int index, int value)
    {
        if (value >= 0)
        {
            plusResourceText[index].text = "+" + value;
            plusResourceText[index].color = new Color(70, 255, 0, 255);
        }
        else
        {
            plusResourceText[index].text = value.ToString();
            plusResourceText[index].color = new Color(70, 255, 0, 255);
        }
        yield return new WaitForSeconds(2);
        plusResourceText[index].text = "";
        yield break;
    }
    IEnumerator PlusMoney(int value)
    {
        if (value > 0)
        {
            PlusMoneyText.text = "+" + value;
            PlusMoneyText.color = new Color(70, 255, 0, 255);
        }
        if (value < 0)
        {
            PlusMoneyText.text = value.ToString();
            PlusMoneyText.color = new Color(255, 0, 0, 255);
        }
        yield return new WaitForSeconds(2);
        PlusMoneyText.text = "";
        yield break;
    }


    //ADS!!!
    public void ExampleOpenRewardAd(int id)
    {
        // �������� ����� �������� ����� �������
        YandexGame.RewVideoShow(id);
    }
    void Rewarded(int id)
    {
        if (id == 4)
        {
            GM.Diamond++;
            ResourceTextUpdate();
        }
        if (id == 5)
        {
            GM.Money += ADSMoneyCol;
            PlusMoney(ADSMoneyCol);
            ADSMoneyActive.SetActive(false);
            ResourceTextUpdate();
        }
    }

    public void OpenClosedShopPan() // �������� ������ ��������
    {
        if(!ShopPan.activeInHierarchy) ShopPan.SetActive(true);
        else ShopPan.SetActive(false);
    }
    public void ExchangeDiamond() // ����� ������� �� ������
    {
        if (GM.Diamond >= 1)
        {
            GM.Money += 1500;
            GM.Diamond--;
            StartCoroutine(PlusMoney(1500));
            ResourceTextUpdate();
        }
    }
    void UpdateADSMoneyActive() //
    {
        if (GM.Money >= 500)
        {
            ADSMoneyActive.SetActive(true);
            ADSMoneyCol = Random.Range(500, GM.Money);
            ADSMoneyActiveText.text = "+" + ADSMoneyCol + "$!!!" + "\n" + "�� ��������";
        }
        else
        {
            ADSMoneyActive.SetActive(true);
            ADSMoneyCol = 500;
            ADSMoneyActiveText.text = "+" + ADSMoneyCol + "$!!!" + "\n" + "�� ��������";
        }
    }


    //
    void Questioner()
    {
        if(GM.City == 2 & GM.QuestionPoint[0] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[1].SetActive(true);
            GM.QuestionPoint[0] = true;
            TaskCounter = 0;
        }
        else if(GM.City == 9 & GM.ActiveTask == true & GM.CityTask == 9)
        {
            TaskPan[0].SetActive(true);
            TaskPan[3].SetActive(true);
            GM.ActiveTask = false;
            TaskCounter = 0;
        }
        else if (GM.City == 15 & GM.QuestionPoint[1] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[4].SetActive(true);
            GM.QuestionPoint[1] = true;
            TaskCounter = 0;
        }
        else if (GM.City == 13 & GM.QuestionPoint[2] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[7].SetActive(true);
            GM.QuestionPoint[2] = true;
            TaskCounter = 0;
        }
        else if (GM.City == 20 & GM.QuestionPoint[3] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[8].SetActive(true);
            GM.QuestionPoint[3] = true;
            TaskCounter = 0;
        }
        else if (GM.City == 19 & GM.QuestionPoint[4] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[10].SetActive(true);
            GM.QuestionPoint[4] = true;
            TaskCounter = 0;
        }
        else if (GM.City == 24 & GM.QuestionPoint[5] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[11].SetActive(true);
            GM.QuestionPoint[5] = true;
            TaskCounter = 0;
        }
        else if (GM.City == 6 & GM.ActiveTask == true & GM.CityTask == 6)
        {
            TaskPan[0].SetActive(true);
            TaskPan[13].SetActive(true);
            GM.ActiveTask = false;
            TaskCounter = 0;
        }
        else if (GM.City == 28 & GM.QuestionPoint[6] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[14].SetActive(true);
            GM.QuestionPoint[6] = true;
            TaskCounter = 0;
        }
        else if (GM.City == 32 & GM.QuestionPoint[7] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[15].SetActive(true);
            GM.QuestionPoint[7] = true;
            TaskCounter = 0;
        }
        else if (GM.City == 41 & GM.QuestionPoint[8] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[16].SetActive(true);
            GM.QuestionPoint[8] = true;
            TaskCounter = 0;
        }
        else if (GM.City == 44)
        {
            TaskPan[0].SetActive(true);
            TaskPan[18].SetActive(true);
            TaskCounter = 0;
        }
    }
    public void DismissQuestion()
    {
        if (GM.City == 2) // ����� � ������
        {
            TaskPan[0].SetActive(false);
            TaskPan[1].SetActive(false);
        }
        else if(GM.City == 15) // ����� � ������������
        {
            TaskPan[0].SetActive(false);
            TaskPan[6].SetActive(false);
        }
        else if (GM.City == 24)
        {
            TaskPan[0].SetActive(false);
            TaskPan[11].SetActive(false);
        }
    }
    public void EnterQuestion()
    {
        if(GM.City == 2) // ������� �� �������� � ������
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[2].SetActive(false);
                GM.ActiveTask = true;
                GM.CityTask = 9;
                GM.CargoSpecTransportCount = 5;
            }
            else if (TaskCounter == 0)
            {
                TaskPan[1].SetActive(false);
                TaskPan[2].SetActive(true);
                TaskCounter = 1;
            }
        }
        else if (GM.City == 9) // ������� �� �������� � ��������� �� ������
        {
            if (TaskCounter == 0)
            {
                TaskPan[0].SetActive(false);
                TaskPan[3].SetActive(false);
                GM.CargoSpecTransportCount = 0;
                GM.Money += 3000;
                StartCoroutine(PlusMoney(3000));
                ResourceTextUpdate();
            }
        }
        else if (GM.City == 15) // ������ ������������
        {
            if (TaskCounter == 2)
            {
                TaskPan[0].SetActive(false);
                TaskPan[6].SetActive(false);
            }
            else if (TaskCounter == 1)
            {
                TaskPan[5].SetActive(false);
                TaskPan[6].SetActive(true);
                TaskCounter = 2;
                //if (GM.Food >= 30)
                //{
                //    GM.Food -= 30;
                //    StartCoroutine(PlusFood(0 - 30));
                //}
                //else if (GM.Food < 30)
                //{
                //    GM.Food = 0;
                //    StartCoroutine(PlusFood(0 - GM.Food));
                //}
            }
            else if (TaskCounter == 0)
            {
                TaskPan[4].SetActive(false);
                TaskPan[5].SetActive(true);
                TaskCounter = 1;
            }
        }
        else if(GM.City == 13)
        {
            TaskPan[0].SetActive(false);
            TaskPan[7].SetActive(true);
        }
        else if (GM.City == 20)
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[9].SetActive(false);
            }
            else if (TaskCounter == 0)
            {
                TaskPan[8].SetActive(false);
                TaskPan[9].SetActive(true);
                TaskCounter = 1;
            }
        }
        else if (GM.City == 19)
        {
            TaskPan[0].SetActive(false);
            TaskPan[10].SetActive(false);
            GM.Score += 20;
            ScoreCount();
        }
        else if (GM.City == 24)
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[12].SetActive(false);
                GM.ActiveTask = true;
                GM.CityTask = 6;
                GM.CargoSpec1TransportCount = 9;
            }
            else if (TaskCounter == 0)
            {
                TaskPan[11].SetActive(false);
                TaskPan[12].SetActive(true);
                TaskCounter = 1;
            }
        }
        else if (GM.City == 6) // ������� �� �������� � ������� �� �������
        {
            if (TaskCounter == 0)
            {
                TaskPan[0].SetActive(false);
                TaskPan[13].SetActive(false);
                GM.CargoSpec1TransportCount = 0;
                GM.AirShipActive = true;
            }
        }
        else if (GM.City == 28)
        {
            TaskPan[0].SetActive(false);
            TaskPan[14].SetActive(false);
            GM.Money += 2000;
            StartCoroutine(PlusMoney(2000));
            ResourceTextUpdate();
        }
        else if (GM.City == 32)
        {
            TaskPan[0].SetActive(false);
            TaskPan[15].SetActive(false);
            GM.Score += 20;
            ScoreCount();
        }
        else if (GM.City == 41)
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[17].SetActive(false);
            }
            else if (TaskCounter == 0)
            {
                TaskPan[16].SetActive(false);
                TaskPan[17].SetActive(true);
                TaskCounter = 1;
            }
        }
        else if(GM.City == 44)
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[19].SetActive(false);
            }
            else if (TaskCounter == 0)
            {
                TaskPan[18].SetActive(false);
                TaskPan[19].SetActive(true);
                TaskCounter = 1;
                int H = GM.TimeInGameStatistic / 60;
                int M = GM.TimeInGameStatistic - (H * 60);
                TimeInGameStatisticText.text = "����� � ����: " + H + "�. " + M + "�."; //
                FoodStatisticText.text = "������� ���: " + GM.FoodPlusStatistic.ToString(); //
                CoalPlusStatisticText.text = "������� ����: " + GM.CoalPlusStatistic.ToString(); //
                WaterPlusStatisticText.text = "������� ����: " + GM.WaterPlusStatistic.ToString(); //
                WarmPlusStatisticText.text = "������� �����: " + GM.WarmPlusStatistic.ToString(); //
                MoneyPlusStatisticText.text = "���������� �����: " + GM.MoneyPlusStatistic.ToString(); //
                DistancePlusStatisticText.text = "�������� ����������: " + GM.DistancePlusStatistic * 10; //
                ScorePlusStatisticText.text = "�����: " + GM.Score.ToString(); //
            }
        }
    }
}
