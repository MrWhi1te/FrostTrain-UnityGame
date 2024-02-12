using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StationScripts : MonoBehaviour
{
    public GameObject StationPan; // Панель станции
    public GameObject GamePan; //
    public Game GM; // Скрипт Гейм
    [Header("Market")]
    public GameObject MarketPan; // Панель рынка
    public Slider[] SliderMarket; // Слайдеры рынка
    public Text[] ColResourceText; // Выбор Кол-во ресурсов текст
    public Text[] ColMoneyText; // Сумма покупки/продажи
    [Header("Train")]
    public GameObject TrainPan; // Панель поезда
    public GameObject LocoBuyPan; // Панель покупки локомотива
    public GameObject ViewTrainPan; // Панель Смены вида поезда
    public GameObject SpecialWagPan; // Панель Специальных вагонов
    public Text[] TrainViewText; //
    public Text[] LocoUpdateText; //
    [Header("QuestPass")]
    public GameObject QuestPan; // Панель заданий
    public GameObject PassQuestPan; //
    public GameObject StartRewardQuestPan; //
    public Text StartRewardQuestText; //
    public Text PWagoneTransport; //
    public Text PWagoneFood; //
    public Text PWagoneWater; //
    public Text PWagoneWarm; //
    public Text PWagoneReward; //
    public Text ActiveTextQuest; //
    int PTransport; //
    int PReward; //
    [Header("QuestCargo")]
    public GameObject CargoQuestPan; //
    public Text CargoWagoneTransport; //
    public Text CargoWagoneCoal; //
    public Text CargoWagoneReward; //
    int CargoTransport; //
    int CargoReward; //
    [Header("NextStation")]
    public Text NextStationText; // Текст следующей станции
    public GameObject[] BttnNextStation; //
    public bool StationVibor; //
    [Header("AddWagone")]
    public Text CountWagon; // Подсчет кол-ва вагонов
    public Text BuyWagoneText; //
    [Header("Resource")]
    public Text MoneyText; // Текст денег
    public Text DiamondText; // Текст Роскошь
    public Text CoalText; // Текст угля
    public Text FoodText; // Текст еды
    public Text WaterText; // Текст воды
    public Text WarmText; // Текст тепла
    public Text WorkerText; // Текст рабочих
    public Text PlusFoodText; // 
    public Text PlusWaterText; //
    public Text PlusWarmText; //
    public Text PlusCoalText; //
    public Text PlusWorkerText; //
    public Text PlusMoneyText; // 
    [Header("MoneyView")]
    public Text MoneyShowText; // 
    [Header("Train")]
    public GameObject[] TrainerPan; //
    public int TrainerCount; //
    [Header("ADS")]
    public GameObject ShopPan; //
    public GameObject ADSMoneyActive; //
    public Text ADSMoneyActiveText; // текст денег за рекламу
    int ADSMoneyCol; // награда денег за рекламу
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

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
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
        if (GM.PTransportCount >= 1 || GM.CargoTransportCount >= 1)
        {
            PTransport = GM.PTransportCount;
            CargoTransport = GM.CargoTransportCount;
            for (int i = 0; i < GM.PTransportCount; i++)
            {
                GM.PTransportWagone[i].SetActive(false);
            }
            for (int i = 0; i < GM.CargoTransportCount; i++)
            {
                GM.CargoTransportWagone[i].SetActive(false);
            }
            GM.PTransportCount = 0;
            GM.CargoTransportCount = 0;
            StartRewardQuestPan.SetActive(true);
            StartRewardQuestText.text = "Вы доставили: " + (PTransport + CargoTransport) + " вагонов. Награда составила: " + (GM.RewardPTransport + GM.RewardCargoTransport) + "$";
            if(GM.TaskCount == 4)
            {
                GM.TaskCount = 5;
            }
        }
        UpdateADSMoneyActive();
        Questioner();
    }
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimerNextStation() //
    {
        GM.NextStationTimeCount = GM.TimeForCity[GM.ChoiceCity];
        GM.NextStationTime = GM.TimeForCity[GM.ChoiceCity];
        GM.NextStationSlide.maxValue = GM.NextStationTime;
        int Min = GM.NextStationTime / 60;
        int Sec = GM.NextStationTime - (Min * 60);
        NextStationText.text = "До " + GM.NameCity[GM.ChoiceCity] + ": " + Min + "мин. " + Sec + "сек." + "Температура: " + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
    }
    public void StartNextStation() // Старт следующей станции
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
        if (GM.PTransportCount >= 1) // Если активно Пассажирское задание
        {
            for (int i = 0; i < GM.PTransportCount; i++)
            {
                GM.PTransportWagone[i].SetActive(true);
                if (GM.TextureTrain == 0)
                {
                    GM.PTransportWagone[i].GetComponent<Image>().sprite = GM.SPR0[4];
                }
                if (GM.TextureTrain == 1)
                {
                    GM.PTransportWagone[i].GetComponent<Image>().sprite = GM.SPR1[4];
                }
                if (GM.TextureTrain == 2)
                {
                    GM.PTransportWagone[i].GetComponent<Image>().sprite = GM.SPR2[4];
                }
                if (GM.TextureTrain == 3)
                {
                    GM.PTransportWagone[i].GetComponent<Image>().sprite = GM.SPR3[4];
                }
            }
            GM.StartTransportWagoneQuest();
        }
        if (GM.CargoTransportCount >= 1) // Если активно грузовое задание
        {
            for (int i = 0; i < GM.CargoTransportCount; i++)
            {
                GM.CargoTransportWagone[i].SetActive(true);
                if (GM.TextureTrain == 0)
                {
                    GM.CargoTransportWagone[i].GetComponent<Image>().sprite = GM.SPR0[8];
                }
                if (GM.TextureTrain == 1)
                {
                    GM.CargoTransportWagone[i].GetComponent<Image>().sprite = GM.SPR1[8];
                }
                if (GM.TextureTrain == 2)
                {
                    GM.CargoTransportWagone[i].GetComponent<Image>().sprite = GM.SPR1[8];
                }
                if (GM.TextureTrain == 3)
                {
                    GM.CargoTransportWagone[i].GetComponent<Image>().sprite = GM.SPR1[8];
                }
            }
            GM.StartTransportCargoWagoneQuest();
        }
        if (GM.CargoSpecTransportCount >= 1) // Если активно грузовое SPEC задание
        {
            for (int i = 0; i < GM.CargoSpecTransportCount; i++)
            {
                GM.CargoSpecTransportWagone[i].SetActive(true);
            }
        }
        if (GM.CargoSpec1TransportCount >= 1) // Если активно грузовое SPEC1 задание
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
        GM.PostWagonePeople();
        GM.City = GM.ChoiceCity;
        GM.ChoiceCity = 0;
        GM.SpeedFon = 1;
        GM.RandomBackground();
        StationPan.SetActive(false);
        GM.Save();
        GM.TaskCounter();
        GM.PostWagonePeople();
        if (YandexGame.EnvironmentData.deviceType == "mobile")
        {
            YandexGame.StickyAdActivity(true);
        }
        YandexGame.FullscreenShow();
    }
    void ResourceTextUpdate() // Обновление текстов
    {
        MoneyText.text = GM.Money + "$";
        DiamondText.text = GM.Diamond.ToString();
        CoalText.text = GM.Coal + "/" + GM.CoalMax;
        FoodText.text = GM.Food + "/" + GM.FoodMax;
        WaterText.text = GM.Water + "/" + GM.WaterMax;
        WarmText.text = GM.Warm + "/" + GM.WarmMax;
        WorkerText.text = GM.Worker + "/" + GM.AllPeople + "(" + GM.WorkerMax + ")"; //
        CountWagon.text = GM.WagonCol + " из " + GM.MaxWagone; //
        BuyWagoneText.text = (300 * GM.WagonCol) + "$";
    }
    public void BuyWagone() // Покупка вагонов / Добавление
    {
        if(GM.WagonCol < GM.MaxWagone)
        {
            if(GM.Money >= (300 * GM.WagonCol))
            {
                GM.Money -= (300 * GM.WagonCol);
                StartCoroutine(PlusMoney(0 - (300 * GM.WagonCol)));
                //GM.BuyAddWagone();
                GM.WagonCol++;
                ResourceTextUpdate();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
        else
        {
            //
        }
    }
    public void ActiveSlider(int index) // Активация слайдеров
    {
        if(index == 0)
        {
            SliderMarket[0].maxValue = GM.Coal; 
            ColResourceText[0].text = SliderMarket[0].value.ToString();
            ColMoneyText[0].text = "+" + (SliderMarket[0].value * 1) + "$";
        }
        if (index == 1)
        {
            SliderMarket[1].maxValue = GM.Food;
            ColResourceText[1].text = SliderMarket[1].value.ToString();
            ColMoneyText[1].text = "+" + (SliderMarket[1].value * 18) + "$";
        }
        if (index == 2)
        {
            SliderMarket[2].maxValue = GM.Water;
            ColResourceText[2].text = SliderMarket[2].value.ToString();
            ColMoneyText[2].text = "+" + (SliderMarket[2].value * 15) + "$";
        }
        if (index == 3)
        {
            SliderMarket[3].maxValue = GM.Warm;
            ColResourceText[3].text = SliderMarket[3].value.ToString();
            ColMoneyText[3].text = "+" + (SliderMarket[3].value * 8) + "$";
        }
        if (index == 4)
        {
            SliderMarket[4].maxValue = GM.Worker;
            ColResourceText[4].text = SliderMarket[4].value.ToString();
            ColMoneyText[4].text = "+" + (SliderMarket[4].value * 40) + "$";
        }
        if (index == 5)
        {
            SliderMarket[5].maxValue = GM.CoalMax - GM.Coal;
            ColResourceText[5].text = SliderMarket[5].value.ToString();
            ColMoneyText[5].text = "-" + (SliderMarket[5].value * 2) + "$";
        }
        if (index == 6)
        {
            SliderMarket[6].maxValue = GM.FoodMax - GM.Food;
            ColResourceText[6].text = SliderMarket[6].value.ToString();
            ColMoneyText[6].text = "-" + (SliderMarket[6].value * 30) + "$";
        }
        if (index == 7)
        {
            SliderMarket[7].maxValue = GM.WaterMax - GM.Water;
            ColResourceText[7].text = SliderMarket[7].value.ToString();
            ColMoneyText[7].text = "-" + (SliderMarket[7].value * 25) + "$";
        }
        if (index == 8)
        {
            SliderMarket[8].maxValue = GM.WarmMax - GM.Warm;
            ColResourceText[8].text = SliderMarket[8].value.ToString();
            ColMoneyText[8].text = "-" + (SliderMarket[8].value * 20) + "$";
        }
        if (index == 9)
        {
            SliderMarket[9].maxValue = GM.WorkerMax - GM.AllPeople;
            ColResourceText[9].text = SliderMarket[9].value.ToString();
            ColMoneyText[9].text = "-" + (SliderMarket[9].value * 100) + "$";
        }
    }
    public void BuySellResource(int index) // Покупка / Продажа Ресурсов на рынке
    {
        if(index == 0)
        {
            if(GM.Money >= SliderMarket[5].value * 2)
            {
                GM.Money -= (int)SliderMarket[5].value * 2;
                StartCoroutine(PlusMoney(0 - ((int)SliderMarket[5].value * 2)));
                GM.Coal += (int)SliderMarket[5].value;
                StartCoroutine(PlusCoal((int)SliderMarket[5].value));
                SliderMarket[5].value = 0;
                ResourceTextUpdate();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
        if (index == 1)
        {
            if (GM.Money >= SliderMarket[6].value * 30)
            {
                GM.Money -= (int)SliderMarket[6].value * 30;
                StartCoroutine(PlusMoney(0 - ((int)SliderMarket[6].value * 30)));
                GM.Food += (int)SliderMarket[6].value;
                StartCoroutine(PlusFood((int)SliderMarket[6].value));
                SliderMarket[6].value = 0;
                ResourceTextUpdate();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
        if (index == 2)
        {
            if (GM.Money >= SliderMarket[7].value * 25)
            {
                GM.Money -= (int)SliderMarket[7].value * 25;
                StartCoroutine(PlusMoney(0 - ((int)SliderMarket[7].value * 25)));
                GM.Water += (int)SliderMarket[7].value;
                StartCoroutine(PlusWater((int)SliderMarket[7].value));
                SliderMarket[7].value = 0;
                ResourceTextUpdate();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
        if (index == 3)
        {
            if (GM.Money >= SliderMarket[8].value * 20)
            {
                GM.Money -= (int)SliderMarket[8].value * 20;
                StartCoroutine(PlusMoney(0 - ((int)SliderMarket[8].value * 20)));
                GM.Warm += (int)SliderMarket[8].value;
                StartCoroutine(PlusWarm((int)SliderMarket[8].value));
                SliderMarket[8].value = 0;
                ResourceTextUpdate();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
        if (index == 4)
        {
            if (GM.Money >= SliderMarket[9].value * 100)
            {
                GM.Money -= (int)SliderMarket[9].value * 100;
                StartCoroutine(PlusMoney(0 - ((int)SliderMarket[9].value * 100)));
                GM.Worker += (int)SliderMarket[9].value;
                StartCoroutine(PlusWorker((int)SliderMarket[9].value));
                GM.AllPeople += (int)SliderMarket[9].value;
                GM.DontPeopleWagone += (int)SliderMarket[9].value;
                SliderMarket[9].value = 0;
                ResourceTextUpdate();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
        if (index == 5)
        {
            GM.Money += (int)SliderMarket[0].value * 1;
            GM.MoneyPlusStatistic += (int)SliderMarket[0].value * 1;
            StartCoroutine(PlusMoney((int)SliderMarket[0].value * 1));
            GM.Coal -= (int)SliderMarket[0].value;
            StartCoroutine(PlusCoal(0 - (int)SliderMarket[0].value));
            SliderMarket[0].value = 0;
            ResourceTextUpdate();
        }
        if (index == 6)
        {
            GM.Money += (int)SliderMarket[1].value * 18;
            GM.MoneyPlusStatistic += (int)SliderMarket[1].value * 18;
            StartCoroutine(PlusMoney((int)SliderMarket[1].value * 18));
            GM.Food -= (int)SliderMarket[1].value;
            StartCoroutine(PlusFood(0 - (int)SliderMarket[1].value));
            SliderMarket[1].value = 0;
            ResourceTextUpdate();
        }
        if (index == 7)
        {
            GM.Money += (int)SliderMarket[2].value * 15;
            GM.MoneyPlusStatistic += (int)SliderMarket[2].value * 15;
            StartCoroutine(PlusMoney((int)SliderMarket[2].value * 15));
            GM.Water -= (int)SliderMarket[2].value;
            StartCoroutine(PlusWater(0 - (int)SliderMarket[2].value));
            SliderMarket[2].value = 0;
            ResourceTextUpdate();
        }
        if (index == 8)
        {
            GM.Money += (int)SliderMarket[3].value * 8;
            GM.MoneyPlusStatistic += (int)SliderMarket[3].value * 8;
            StartCoroutine(PlusMoney((int)SliderMarket[3].value * 8));
            GM.Warm -= (int)SliderMarket[3].value;
            StartCoroutine(PlusWarm(0 - (int)SliderMarket[3].value));
            SliderMarket[3].value = 0;
            ResourceTextUpdate();
        }
        if (index == 9)
        {
            GM.Money += (int)SliderMarket[4].value * 40;
            GM.MoneyPlusStatistic += (int)SliderMarket[4].value * 40;
            StartCoroutine(PlusMoney((int)SliderMarket[4].value * 40));
            GM.Worker -= (int)SliderMarket[4].value;
            GM.AllPeople -= (int)SliderMarket[4].value;
            StartCoroutine(PlusWorker(0 -(int)SliderMarket[4].value));
            SliderMarket[4].value = 0;
            ResourceTextUpdate();
        }
    }
    IEnumerator MoneyShow() // Вывод надписи не хватает денег с рынка
    {
        MoneyShowText.text = "Не хватает денег!";
        yield return new WaitForSeconds(2);
        MoneyShowText.text = "";
        yield break; 
    }
    public void OpenMTQPan(int index) // Открытие основных панелей
    {
        if(index == 0)
        {
            MarketPan.SetActive(true);
            TrainPan.SetActive(false);
            QuestPan.SetActive(false);
        }
        if (index == 1)
        {
            MarketPan.SetActive(false);
            TrainPan.SetActive(true);
            QuestPan.SetActive(false);
            if(GM.LevelLoco == 2 )
            {
                LocoUpdateText[0].text = "Приобретено";
                LocoUpdateText[1].text = "Текущий";
            }
            if (GM.LevelLoco == 3)
            {
                LocoUpdateText[0].text = "Приобретено";
                LocoUpdateText[1].text = "Приобретено";
                LocoUpdateText[2].text = "Текущий";
            }
        }
        if (index == 2)
        {
            MarketPan.SetActive(false);
            TrainPan.SetActive(false);
            QuestPan.SetActive(true);
        }
    }
    public void OpenTrainPan(int index) // Открытие панелей поезда
    {
        if (index == 0)
        {
            LocoBuyPan.SetActive(true);
            ViewTrainPan.SetActive(false);
            SpecialWagPan.SetActive(false);
        }
        if (index == 1)
        {
            LocoBuyPan.SetActive(false);
            ViewTrainPan.SetActive(true);
            SpecialWagPan.SetActive(false);
            if(GM.TextureTrain == 0)
            {
                TrainViewText[0].text = "Текущий";
            }
            if(GM.Texture1 == true)
            {
                TrainViewText[1].text = "Доступен";
            }
            if (GM.Texture2 == true)
            {
                TrainViewText[2].text = "Доступен";
            }
            if (GM.Texture3 == true)
            {
                TrainViewText[3].text = "Доступен";
            }
            if (GM.TextureTrain == 1)
            {
                TrainViewText[1].text = "Текущий";
                TrainViewText[0].text = "Доступен";
            }
            if (GM.TextureTrain == 2)
            {
                TrainViewText[2].text = "Текущий";
                TrainViewText[0].text = "Доступен";
            }
            if (GM.TextureTrain == 3)
            {
                TrainViewText[3].text = "Текущий";
                TrainViewText[0].text = "Доступен";
            }
        }
        if (index == 2)
        {
            LocoBuyPan.SetActive(false);
            ViewTrainPan.SetActive(false);
            SpecialWagPan.SetActive(true);
        }
    }

    public void BuyViewTrain(int index) // Покупка внешнего вида поезда
    {
        if(index == 0)
        {
            TrainViewText[0].text = "Текущий";
            GM.TextureTrain = 0;
            GM.ChoiceViewTrain(0);
            if(GM.Texture1 == true)
            {
                TrainViewText[1].text = "Доступен";
            }
            if (GM.Texture2 == true)
            {
                TrainViewText[2].text = "Доступен";
            }
            if (GM.Texture3 == true)
            {
                TrainViewText[3].text = "Доступен";
            }
        }
        if(index == 1)
        {
            if (GM.Texture1 == true)
            {
                TrainViewText[1].text = "Текущий";
                GM.TextureTrain = 1;
                GM.ChoiceViewTrain(1);
                TrainViewText[0].text = "Доступен";
            }
            if (GM.Texture2 == true)
            {
                TrainViewText[2].text = "Доступен";
            }
            if (GM.Texture3 == true)
            {
                TrainViewText[3].text = "Доступен";
            }
            if (GM.Texture1 == false & GM.Money >= 10000)
            {
                GM.Money -= 10000;
                StartCoroutine(PlusMoney(0 - 10000));
                GM.Texture1 = true;
                TrainViewText[1].text = "Текущий";
                GM.TextureTrain = 1;
                GM.ChoiceViewTrain(1);
                TrainViewText[0].text = "Доступен";
                ResourceTextUpdate();
            }
            if (GM.Texture1 == false & GM.Money < 10000)
            {
                StartCoroutine(MoneyShow());
            }
        }
        if (index == 2)
        {
            if (GM.Texture2 == true)
            {
                TrainViewText[2].text = "Текущий";
                GM.TextureTrain = 2;
                GM.ChoiceViewTrain(2);
                TrainViewText[0].text = "Доступен";
            }
            if (GM.Texture1 == true)
            {
                TrainViewText[1].text = "Доступен";
            }
            if (GM.Texture3 == true)
            {
                TrainViewText[3].text = "Доступен";
            }
            if (GM.Texture2 == false & GM.Money >= 30000)
            {
                GM.Money -= 30000;
                StartCoroutine(PlusMoney(0 - 30000));
                GM.Texture2 = true;
                TrainViewText[2].text = "Текущий";
                GM.TextureTrain = 2;
                GM.ChoiceViewTrain(2);
                TrainViewText[0].text = "Доступен";
                ResourceTextUpdate();
            }
            if (GM.Texture2 == false & GM.Money < 30000)
            {
                StartCoroutine(MoneyShow());
            }
        }
        if (index == 3)
        {
            if (GM.Texture3 == true)
            {
                TrainViewText[3].text = "Текущий";
                GM.TextureTrain = 3;
                GM.ChoiceViewTrain(3);
                TrainViewText[0].text = "Доступен";
            }
            if (GM.Texture1 == true)
            {
                TrainViewText[1].text = "Доступен";
            }
            if (GM.Texture2 == true)
            {
                TrainViewText[2].text = "Доступен";
            }
            if (GM.Texture3 == false & GM.Diamond >= 15) // 30 АЛМАЗОВ . ЗАМЕНА на 15! СКИДКА!                                                  АКЦИЯ!!!
            {
                GM.Diamond -= 15;
                GM.Texture3 = true;
                TrainViewText[3].text = "Текущий";
                GM.TextureTrain = 3;
                GM.ChoiceViewTrain(3);
                TrainViewText[0].text = "Доступен";
                ResourceTextUpdate();
            }
            if (GM.Texture3 == false & GM.Diamond >= 15) 
            {
                StartCoroutine(MoneyShow());
            }
        }
    }

    public void BuyUpdateLoco(int index) // Покупка улушение поезда
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
                LocoUpdateText[0].text = "Приобретено";
                LocoUpdateText[1].text = "Текущий";
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
                LocoUpdateText[0].text = "Приобретено";
                LocoUpdateText[1].text = "Приобретено";
                LocoUpdateText[2].text = "Текущий";
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
    
    // TRANSPORT PASS!!!

    public void OpenTransportPan(int index)
    {
        if(index == 0)
        {
            PassQuestPan.SetActive(true);
            CargoQuestPan.SetActive(false);
        }
        if (index == 1)
        {
            PassQuestPan.SetActive(false);
            CargoQuestPan.SetActive(true);
        }
    }

    void PassTransportationUpdate() // Обнвление панели доставки
    {
        PWagoneTransport.text = PTransport.ToString();
        PWagoneFood.text = "-" + PTransport * 10;
        PWagoneWater.text = "-" + PTransport * 10;
        PWagoneWarm.text = "-" + PTransport * 10;
        PWagoneReward.text = PReward + "$";
    }
    public void AddPTransport() // Добавить вагон доставки
    {
        if(StationVibor == true)
        {
            if (PTransport < 25)
            {
                PTransport++;
                PReward = PTransport * (GM.NextStationTime * 2) * 2; // Награда увеличина в 2 раза для акции!                                                   АКЦИЯ!!
                PassTransportationUpdate();
            }
        }
        else
        {
            PWagoneReward.text = "Для начала выберите станцию назначения!";
        }
    }
    public void RemovePTransport() // Минус вагон доставки
    {
        if (PTransport > 0)
        {
            PTransport--;
            PReward = PTransport * (GM.NextStationTime * 2) * 2; // Награда увеличина в 2 раза для акции!
            PassTransportationUpdate();
        }
    }
    public void EnterQuestPass() // Принять задание
    {
        GM.PTransportCount = PTransport;
        GM.RewardPTransport = PReward;
        if (PTransport >= 1 || CargoTransport >= 1)
        {
            ActiveTextQuest.text = "Активно задание!" + "\n" + "Доставка вагонов: " + (PTransport + CargoTransport);
        }
        else { ActiveTextQuest.text = ""; }
    }
    public void EnterRewardQuest() // Принять награду за доставку
    {
        GM.Money += GM.RewardPTransport + GM.RewardCargoTransport;
        GM.MoneyPlusStatistic += GM.RewardPTransport + GM.RewardCargoTransport;
        StartCoroutine(PlusMoney(GM.RewardPTransport + GM.RewardCargoTransport));
        PTransport = 0;
        CargoTransport = 0;
        StartRewardQuestPan.SetActive(false);
        ActiveTextQuest.text = "";
        ResourceTextUpdate();
        GM.Score += (GM.RewardPTransport/50) + (GM.RewardCargoTransport/50);
        ScoreCount();
    }

    // TRANSPORT CARGO!!!
    void CargoTransportationUpdate() // Обнвление панели доставки
    {
        CargoWagoneTransport.text = CargoTransport.ToString();
        CargoWagoneCoal.text = "-" + CargoTransport * 20;
        CargoWagoneReward.text = CargoReward + "$";
    }
    public void AddCargoTransport() // Добавить вагон доставки
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
            CargoWagoneReward.text = "Для начала выберите станцию назначения!";
        }
    }
    public void RemoveCargoTransport() // Минус вагон доставки
    {
        if (CargoTransport > 0)
        {
            CargoTransport--;
            CargoReward = CargoTransport * (GM.NextStationTime * 2);
            CargoTransportationUpdate();
        }
    }
    public void EnterQuestCargo() // Принять задание
    {
        GM.CargoTransportCount = CargoTransport;
        GM.RewardCargoTransport = CargoReward;
        if (CargoTransport >= 1 || PTransport >= 1)
        {
            ActiveTextQuest.text = "Активно задание!" + "\n" + "Доставка вагонов: " + (PTransport + CargoTransport);
        }
        else { ActiveTextQuest.text = ""; }
    }

    public void OpenMapPan()
    {
        BttnNextStation[1].SetActive(true);
        BttnNextStation[0].SetActive(true);
        StationVibor = true;
    }

    public void ScoreCount() // Вывод очков
    {
        int r = 0;
        int number = 0;
        if (GM.Score < 10000)
        {
            number = GM.Score;
        }
        if (GM.Score > 10000)
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

    // Корутины на тексты прибавления и отнимания ресурсов!
    IEnumerator PlusFood(int F)
    {
        if (F > 0)
        {
            PlusFoodText.text = "+" + F;
            PlusFoodText.color = new Color(70, 255, 0, 255);
        }
        if (F < 0)
        {
            PlusFoodText.text = F.ToString();
            PlusFoodText.color = new Color(255, 0, 0, 255);
        }
        yield return new WaitForSeconds(2);
        PlusFoodText.text = "";
        yield break;
    }
    IEnumerator PlusWater(int F)
    {
        if (F > 0)
        {
            PlusWaterText.text = "+" + F;
            PlusWaterText.color = new Color(70, 255, 0, 255);
        }
        if (F < 0)
        {
            PlusWaterText.text = F.ToString();
            PlusWaterText.color = new Color(255, 0, 0, 255);
        }
        yield return new WaitForSeconds(2);
        PlusWaterText.text = "";
        yield break;
    }
    IEnumerator PlusWarm(int F)
    {
        if (F > 0)
        {
            PlusWarmText.text = "+" + F;
            PlusWarmText.color = new Color(70, 255, 0, 255);
        }
        if (F < 0)
        {
            PlusWarmText.text = F.ToString();
            PlusWarmText.color = new Color(255, 0, 0, 255);
        }
        yield return new WaitForSeconds(2);
        PlusWarmText.text = "";
        yield break;
    }
    IEnumerator PlusCoal(int F)
    {
        if (F > 0)
        {
            PlusCoalText.text = "+" + F;
            PlusCoalText.color = new Color(70, 255, 0, 255);
        }
        if (F < 0)
        {
            PlusCoalText.text = F.ToString();
            PlusCoalText.color = new Color(255, 0, 0, 255);
        }
        yield return new WaitForSeconds(2);
        PlusCoalText.text = "";
        yield break;
    }
    IEnumerator PlusWorker(int F)
    {
        if (F > 0)
        {
            PlusWorkerText.text = "+" + F;
            PlusWorkerText.color = new Color(70, 255, 0, 255);
        }
        if (F < 0)
        {
            PlusWorkerText.text = F.ToString();
            PlusWorkerText.color = new Color(255, 0, 0, 255);
        }
        yield return new WaitForSeconds(2);
        PlusWorkerText.text = "";
        yield break;
    }
    IEnumerator PlusMoney(int F)
    {
        if (F > 0)
        {
            PlusMoneyText.text = "+" + F;
            PlusMoneyText.color = new Color(70, 255, 0, 255);
        }
        if (F < 0)
        {
            PlusMoneyText.text = F.ToString();
            PlusMoneyText.color = new Color(255, 0, 0, 255);
        }
        yield return new WaitForSeconds(2);
        PlusMoneyText.text = "";
        yield break;
    }


    //ADS!!!
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
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

    public void OpenShopPan() // Открытие панели магазина
    {
        ShopPan.SetActive(true);
    }
    public void ClosedShopPan() // Закрытие панели магазина
    {
        ShopPan.SetActive(false);
    }
    public void ExchangeDiamond() // Обмен Роскоши на деньги
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
            ADSMoneyActiveText.text = "+" + ADSMoneyCol + "$!!!" + "\n" + "За просмотр";
        }
        else
        {
            ADSMoneyActive.SetActive(true);
            ADSMoneyCol = 500;
            ADSMoneyActiveText.text = "+" + ADSMoneyCol + "$!!!" + "\n" + "За просмотр";
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
        if(GM.City == 9 & GM.ActiveTask == true & GM.CityTask == 9)
        {
            TaskPan[0].SetActive(true);
            TaskPan[3].SetActive(true);
            GM.ActiveTask = false;
            TaskCounter = 0;
        }
        if (GM.City == 15 & GM.QuestionPoint[1] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[4].SetActive(true);
            GM.QuestionPoint[1] = true;
            TaskCounter = 0;
        }
        if (GM.City == 13 & GM.QuestionPoint[2] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[7].SetActive(true);
            GM.QuestionPoint[2] = true;
            TaskCounter = 0;
        }
        if (GM.City == 20 & GM.QuestionPoint[3] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[8].SetActive(true);
            GM.QuestionPoint[3] = true;
            TaskCounter = 0;
        }
        if (GM.City == 19 & GM.QuestionPoint[4] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[10].SetActive(true);
            GM.QuestionPoint[4] = true;
            TaskCounter = 0;
        }
        if (GM.City == 24 & GM.QuestionPoint[5] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[11].SetActive(true);
            GM.QuestionPoint[5] = true;
            TaskCounter = 0;
        }
        if (GM.City == 6 & GM.ActiveTask == true & GM.CityTask == 6)
        {
            TaskPan[0].SetActive(true);
            TaskPan[13].SetActive(true);
            GM.ActiveTask = false;
            TaskCounter = 0;
        }
        if (GM.City == 28 & GM.QuestionPoint[6] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[14].SetActive(true);
            GM.QuestionPoint[6] = true;
            TaskCounter = 0;
        }
        if (GM.City == 32 & GM.QuestionPoint[7] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[15].SetActive(true);
            GM.QuestionPoint[7] = true;
            TaskCounter = 0;
        }
        if (GM.City == 41 & GM.QuestionPoint[8] == false)
        {
            TaskPan[0].SetActive(true);
            TaskPan[16].SetActive(true);
            GM.QuestionPoint[8] = true;
            TaskCounter = 0;
        }
        if (GM.City == 44)
        {
            TaskPan[0].SetActive(true);
            TaskPan[18].SetActive(true);
            TaskCounter = 0;
        }
    }
    public void DismissQuestion()
    {
        if(GM.City == 2) // Отказ в Москве
        {
            TaskPan[0].SetActive(false);
            TaskPan[1].SetActive(false);
        }
        if(GM.City == 15) // Отказ в Новороссийск
        {
            TaskPan[0].SetActive(false);
            TaskPan[6].SetActive(false);
        }
        if (GM.City == 24)
        {
            TaskPan[0].SetActive(false);
            TaskPan[11].SetActive(false);
        }
    }
    public void EnterQuestion()
    {
        if(GM.City == 2) // Задание на доставку в Москве
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[2].SetActive(false);
                GM.ActiveTask = true;
                GM.CityTask = 9;
                GM.CargoSpecTransportCount = 5;
            }
            if (TaskCounter == 0)
            {
                TaskPan[1].SetActive(false);
                TaskPan[2].SetActive(true);
                TaskCounter = 1;
            }
        }
        if (GM.City == 9) // Награда за доставку в Краснодар из Москвы
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
        if (GM.City == 15) // Диалог Новороссийск
        {
            if (TaskCounter == 2)
            {
                TaskPan[0].SetActive(false);
                TaskPan[6].SetActive(false);
            }
            if (TaskCounter == 1)
            {
                TaskPan[5].SetActive(false);
                TaskPan[6].SetActive(true);
                TaskCounter = 2;
                if (GM.Food >= 30)
                {
                    GM.Food -= 30;
                    StartCoroutine(PlusFood(0 - 30));
                }
                if (GM.Food < 30)
                {
                    GM.Food = 0;
                    StartCoroutine(PlusFood(0 - GM.Food));
                }
            }
            if (TaskCounter == 0)
            {
                TaskPan[4].SetActive(false);
                TaskPan[5].SetActive(true);
                TaskCounter = 1;
            }
        }
        if(GM.City == 13)
        {
            TaskPan[0].SetActive(false);
            TaskPan[7].SetActive(true);
        }
        if (GM.City == 20)
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[9].SetActive(false);
            }
            if (TaskCounter == 0)
            {
                TaskPan[8].SetActive(false);
                TaskPan[9].SetActive(true);
                TaskCounter = 1;
            }
        }
        if (GM.City == 19)
        {
            TaskPan[0].SetActive(false);
            TaskPan[10].SetActive(false);
            GM.Score += 20;
            ScoreCount();
        }
        if (GM.City == 24)
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[12].SetActive(false);
                GM.ActiveTask = true;
                GM.CityTask = 6;
                GM.CargoSpec1TransportCount = 9;
            }
            if (TaskCounter == 0)
            {
                TaskPan[11].SetActive(false);
                TaskPan[12].SetActive(true);
                TaskCounter = 1;
            }
        }
        if (GM.City == 6) // Награда за доставку в Саратов из Уренгой
        {
            if (TaskCounter == 0)
            {
                TaskPan[0].SetActive(false);
                TaskPan[13].SetActive(false);
                GM.CargoSpec1TransportCount = 0;
                GM.AirShipActive = true;
            }
        }
        if (GM.City == 28)
        {
            TaskPan[0].SetActive(false);
            TaskPan[14].SetActive(false);
            GM.Money += 2000;
            StartCoroutine(PlusMoney(2000));
            ResourceTextUpdate();
        }
        if (GM.City == 32)
        {
            TaskPan[0].SetActive(false);
            TaskPan[15].SetActive(false);
            GM.Score += 20;
            ScoreCount();
        }
        if (GM.City == 41)
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[17].SetActive(false);
            }
            if (TaskCounter == 0)
            {
                TaskPan[16].SetActive(false);
                TaskPan[17].SetActive(true);
                TaskCounter = 1;
            }
        }
        if(GM.City == 44)
        {
            if (TaskCounter == 1)
            {
                TaskPan[0].SetActive(false);
                TaskPan[19].SetActive(false);
            }
            if (TaskCounter == 0)
            {
                TaskPan[18].SetActive(false);
                TaskPan[19].SetActive(true);
                TaskCounter = 1;
                int H = GM.TimeInGameStatistic / 60;
                int M = GM.TimeInGameStatistic - (H * 60);
                TimeInGameStatisticText.text = "Время в игре: " + H + "ч. " + M + "м."; //
                FoodStatisticText.text = "Собрано еды: " + GM.FoodPlusStatistic.ToString(); //
                CoalPlusStatisticText.text = "Собрано угля: " + GM.CoalPlusStatistic.ToString(); //
                WaterPlusStatisticText.text = "Собрано воды: " + GM.WaterPlusStatistic.ToString(); //
                WarmPlusStatisticText.text = "Собрано тепла: " + GM.WarmPlusStatistic.ToString(); //
                MoneyPlusStatisticText.text = "Заработано денег: " + GM.MoneyPlusStatistic.ToString(); //
                DistancePlusStatisticText.text = "Пройдено расстояние: " + GM.DistancePlusStatistic * 10; //
                ScorePlusStatisticText.text = "Очков: " + GM.Score.ToString(); //
            }
        }
    }
}
