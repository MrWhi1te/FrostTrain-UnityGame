using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public List<WagoneData> WagoneData = new List<WagoneData>(); // Лист с данными вагонов
    public int WagonCol; // Количество активных вагонов
    public GameObject Station; //
    public GameObject GamePan; //
    public Sprite[] SPR0; // Вид вагона стандарт
    public Sprite[] SPR1; // Вид вагона цветной
    public Sprite[] SPR2; // Вид вагона Золотой
    public Sprite[] SPR3; // Вид вагона Фиолетовый
    public int TextureTrain; // Выбранный вид поезда
    public bool Texture1; // Открыта ли цветной вид поезда
    public bool Texture2; // Открыта ли золотой вид поезда
    public bool Texture3; // Открыта ли фиолетовый вид поезда
    
    [Header("Ресурсы")]
    public int Money; // Количество денег
    public int Diamond; // Роскошь
    public int Score; // Очки
    public int Coal; // Количество угля
    public int CoalMax; // Максимум угля для хранения
    public int FreeWorker; // Количество свободных рабочих
    public int AllWorker; // Количество всего рабочих в поезде
    public int Warm; // Количество тепла
    public int WarmMax; // Максимум тепла для хранения
    public int Food; // Количество еды
    public int FoodMax; // Максимум еды для хранения
    public int Water; // Количество воды
    public int WaterMax; // Максимум воды для хранения
    public Text MoneyText; // Текст денег
    public Text CoalText; // Текст угля
    public Text WorkerText; // Текст свободных рабочих
    public Text WarmText; // Текст тепла
    public Text FoodText; // Текст еды
    public Text WaterText; // Текст воды
    public Text DiamondText; // Роскошь текст
    public Text PlusFoodText; // 
    public Text PlusWaterText; //
    public Text PlusWarmText; //
    public Text PlusCoalText; //
    public Text PlusWorkerText; //
    public Text PlusMoneyText; // 
    int StorageCount = 50; // Емкость хранилища
    public Slider[] SliderResource; // Слайдер ресурсов (В наличие и макс)
    public GameObject[] CollectResources; //
    
    [Header("Температура")]
    public int TemperatureOnStreet; // Температура за бортом
    public Text TermometrText; // Текст термометра температуры за бортом
   
    [Header("ТаймерСтанции")]
    public Slider NextStationSlide; // Слайдер (прогресс) следующей станции
    public Text NextStationTimeText; // Текст времени до следующей станции
    public int NextStationTime; // Таймер до следующей станции
    public int NextStationTimeCount; //
    public Button TimerX2Bttn; //
    public GameObject TimerX2Pan; //
    int x2Pan; //
    float Timer = 1; //
    int StationCount; // Количество станций
    
    [Header("Локомотив")]
    public Text LevelLocoText; // Текст уроень локомотива
    public Text StorageCoalText; // Текст Хранилище угля
    public Text NeedCoalText; // Текст потребления угля локомотивом
    public Text MaxWagoneText; // Текст максимальное количество вагонов
    public GameObject LocoPan; // Панель Локомотива
    public GameObject UpgradeLocoPan; // Панель Улучшения локомотива
    public GameObject ViewTrainPan; // Панель выбора вида поезда
    public GameObject[] TextureChoice; //
    public GameObject SmokeParticle; //
    public int LevelLoco = 1; // уровень локомотива. Уровень зависит от вида локомотива. ПОкупается на станции
    public int MaxWagone; // Максимальное кол-во вагонов
    public Image LocoSprite; // 
    int NeedCoal;// Сколько потребляет угля
    int TimerLoco;// Таймер за сколько потребляет уголь
    int ActiveTimerLoco; // Активный таймер локомотива
    
    [Header("CoalFree")]
    public GameObject CoalADS; // Объект угля за рекламу
    public GameObject StartLocoEnginePan; // Запуск двигателя
    public GameObject CoalHelpObj; //
    public GameObject TreesClickParticle; //
    public GameObject TreesFreeObj; //
    int coalCountClick;
    bool CoalHelp; // Подсказка при первом появлении угля
    
    [Header("Message")]
    public Text Message; // Текст сообщений
   
    [Header("Улучшение локомотива")]
    public Text LevelEngineText; // Уровень Котла
    public Text LevelCoalStorageText; // Уровень Тендера
    public Text LevelChassisText; // Уровень Шасси
    public int LevelEngine = 1; // Уровень двигателя локомотива
    int LevelCostEngine; // Стоимость улучшения двигателя
    public int LevelCoalStorage = 1; // Уровень тендера локомотива
    int LevelCostCoalStorage; // Стоимость улучшения тендера локо 
    public int LevelChassis = 1; // Уровень шасси локомотива
    int LevelCostChassis; // Стоимость улучшения шасси локо 
    
    [Header("PassTrasport")]
    public GameObject[] PTransportWagone; // Обьекты вагонов задания перевозки
    public int PTransportCount; // Количество вагонов
    public int RewardPTransport; // Награда за доставку
   
    [Header("CargoTrasport")]
    public GameObject[] CargoTransportWagone; // Обьекты вагонов задания перевозки
    public int CargoTransportCount; // Количество вагонов
    public int RewardCargoTransport; // Награда за доставку
   
    [Header("CargoSpecTrasport")]
    public GameObject[] CargoSpecTransportWagone; // Обьекты вагонов задания перевозки
    public int CargoSpecTransportCount; // Количество вагонов
    public int RewardCargoSpecTransport; // Награда за доставку
   
    [Header("CargoSpec1Trasport")]
    public GameObject[] CargoSpec1TransportWagone; // Обьекты вагонов задания перевозки
    public int CargoSpec1TransportCount; // Количество вагонов
    public int RewardCargoSpec1Transport; // Награда за доставку
   
    [Header("Город")]
    public int City; // Текущий город
    public int ChoiceCity; // Выбор города
    public string[] NameCity; // Название выбранного города
    public int[] TimeForCity; // Время до следующего города
    public int[] TemperatureForCity; // Температура до следующего города
   
    [Header("Background")]
    public int SpeedFon; // Скорость движения фона
    public GameObject[] ParallaxObj; //

    [Header("Barrier")]
    public GameObject BarrierObj; //
    int clickCountBarrier; //
   
    [Header("Trainer")]
    public bool[] Trainer; //
    public bool[] WagoneTrainerChoice; //
    public int TrainCount; //
    public GameObject[] TrainPan; //
    
    [Header("Help")]
    public GameObject HelpPan; //
    public GameObject TargetHelp; //
    public GameObject WagoneHelp; //
    public GameObject LocomotiveHelp; //
   
    [Header("ADS")]
    public GameObject ShopPan; //
    public GameObject ADSMoneyActivePan; // Панель предложения денег за рекламу
    public Slider ADSMoneyActiveSlide; // слайдер таймера денег за рекламу
    public Text ADSMoneyActiveText; // текст денег за рекламу
    int ADSMoneyCol; // награда денег за рекламу
    
    [Header("SAVE")]
    public Text SaveText; //
    public bool Saver;
    public GameObject DeletePan;
    
    [Header("Menu")]
    public GameObject MenuPan; //
    public Text ScoreMenu; // 
    
    [Header("Task")]
    public bool[] QuestionPoint; //
    public bool ActiveTask; //
    public int CityTask; //
    
    //
    public GameObject TaskPan; // Активация панели задания
    public Button TaskDone; // Если задание выполнено, то собрать награду
    public Text TaskText; // Текст задания
    public int TaskCount; // Счетчик задания
    [Header("SpecWagone")]
    public GameObject AirShipObj; //
    public GameObject AirShipPan;
    public Text TimerAirShipText; //
    public Text TimerAirShipText1; //
    public bool AirShipActive; //
    public int TimerAirShip = 900; //
    [Header("Thanks")]
    public GameObject ThanksPan;//
    public bool Thanks; //
    public GameObject Feedback; //
    //
    [Header("STATISTIC")]
    public int TimeInGameStatistic; //
    public int CoalPlusStatistic; //
    public int FoodPlusStatistic; //
    public int WaterPlusStatistic; //
    public int WarmPlusStatistic; //
    public int MoneyPlusStatistic; //
    public int DistancePlusStatistic; //
    public Text TimeInGameStatisticText; //

    public GameObject EffectCollect;
    //
    public string Lang;

    public GameObject PanWagon; // Открытая панель вагона


    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        YandexGame.GetDataEvent += GetLoad;
        Lang = YandexGame.savesData.language;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
        YandexGame.GetDataEvent -= GetLoad;
    }

    public void GetLoad()
    {
        Saver = YandexGame.savesData.Saver;
        if(Saver == true)
        {
            WagonCol = YandexGame.savesData.WagonCol;
            TextureTrain = YandexGame.savesData.TextureTrain;
            Texture1 = YandexGame.savesData.Texture1;
            Texture2 = YandexGame.savesData.Texture2;
            Texture3 = YandexGame.savesData.Texture3;
            //
            Money = YandexGame.savesData.Money;
            Diamond = YandexGame.savesData.Diamond;
            Score = YandexGame.savesData.Score;
            Coal = YandexGame.savesData.Coal;
            Food = YandexGame.savesData.Food;
            Water = YandexGame.savesData.Water;
            Warm = YandexGame.savesData.Warm;
            FreeWorker = YandexGame.savesData.Worker;
            AllWorker = YandexGame.savesData.AllPeople;
            //
            TemperatureOnStreet = YandexGame.savesData.TemperatureOnStreet;
            NextStationTime = YandexGame.savesData.NextStationTime;
            NextStationTimeCount = YandexGame.savesData.NextStationTimeCount;
            LevelLoco = YandexGame.savesData.LevelLoco;
            ActiveTimerLoco = YandexGame.savesData.ActiveTimerLoco;
            LevelEngine = YandexGame.savesData.LevelEngine;
            LevelCoalStorage = YandexGame.savesData.LevelCoalStorage;
            LevelChassis = YandexGame.savesData.LevelChassis;
            //
            CoalHelp = YandexGame.savesData.CoalHelp;
            //
            PTransportCount = YandexGame.savesData.PTransportCount;
            RewardPTransport = YandexGame.savesData.RewardPTransport;
            //
            CargoTransportCount = YandexGame.savesData.CargoTransportCount;
            RewardCargoTransport = YandexGame.savesData.RewardCargoTransport;
            //
            CargoSpecTransportCount = YandexGame.savesData.CargoSpecTransportCount;
            RewardCargoSpecTransport = YandexGame.savesData.RewardCargoSpecTransport;
            //
            CargoSpec1TransportCount = YandexGame.savesData.CargoSpec1TransportCount;
            RewardCargoSpec1Transport = YandexGame.savesData.RewardCargoSpec1Transport;
            //
            City = YandexGame.savesData.City;
            AirShipActive = YandexGame.savesData.AirShipActive; //
            TimerAirShip = YandexGame.savesData.TimerAirShip; //
            Trainer[0] = YandexGame.savesData.Trainer[0];
            Trainer[1] = YandexGame.savesData.Trainer[1];
            TaskCount = YandexGame.savesData.TaskCount;
            ActiveTask = YandexGame.savesData.ActiveTask; //
            CityTask = YandexGame.savesData.CityTask; //
            TimeInGameStatistic = YandexGame.savesData.TimeInGameStatistic; //
            CoalPlusStatistic = YandexGame.savesData.CoalPlusStatistic; //
            FoodPlusStatistic = YandexGame.savesData.FoodPlusStatistic; //
            WaterPlusStatistic = YandexGame.savesData.WaterPlusStatistic; //
            WarmPlusStatistic = YandexGame.savesData.WarmPlusStatistic; //
            MoneyPlusStatistic = YandexGame.savesData.MoneyPlusStatistic; //
            DistancePlusStatistic = YandexGame.savesData.DistancePlusStatistic;
            Thanks = YandexGame.savesData.Thanks;
            for (int i = 0; i < WagonCol; i++)
            {
                WagoneData[i].WagoneActive = false;// YandexGame.savesData.WagoneActive[i];
                WagoneData[i].Name = YandexGame.savesData.Name[i];
                WagoneData[i].LevelWagone = YandexGame.savesData.LevelWagone[i];
                WagoneData[i].TimerActiveProduction = YandexGame.savesData.TimerActiveProduction[i];
                WagoneData[i].WorkerInWagone = YandexGame.savesData.WorkerInWagone[i];
                WagoneData[i].TemperatureWagone = YandexGame.savesData.TemperatureWagone[i];
            }
            for (int i = 0; i < QuestionPoint.Length; i++)
            {
                QuestionPoint[i] = YandexGame.savesData.QuestionPoint[i];
            }
            ScoreMenu.text = "Очки: " + Score;
            if(YandexGame.EnvironmentData.reviewCanShow == false)
            {
                OffFeedback();
            }
        }
    }

    void StartGame()
    {
        if (Trainer[0] == false)
        {
            TrainPan[0].SetActive(true);
            TrainPan[38].SetActive(true);
            NextStationTimeCount = 60;
            NextStationSlide.maxValue = NextStationTimeCount;
            NextStationTime = NextStationTimeCount;
            Time.timeScale = 0;
            ActiveTimerLoco = TimerLoco;
        }
        if(Trainer[0] == true)
        {
            StartNextStation();
            StartCoroutine(AutoSave());
            TaskCounter();
        }
        ActiveWagone();
        ViewLoco();
        MaxStorageWagone();
        LevelLocoUpdater();
        TextLoco();
        ResourceTextUpdate();
        StartCoroutine(TimerInGame());
        StartCoroutine("PassFoodNeed");
        StartCoroutine("PassFoodWater");
        if (PTransportCount >= 1) // Если активно Пассажирское задание
        {
            for (int i = 0; i < PTransportCount; i++)
            {
                PTransportWagone[i].SetActive(true);
                if (TextureTrain == 0)
                {
                    PTransportWagone[i].GetComponent<Image>().sprite = SPR0[4];
                }
                else if (TextureTrain == 1)
                {
                    PTransportWagone[i].GetComponent<Image>().sprite = SPR1[4];
                }
                else if (TextureTrain == 2)
                {
                    PTransportWagone[i].GetComponent<Image>().sprite = SPR2[4];
                }
                else if (TextureTrain == 3)
                {
                    PTransportWagone[i].GetComponent<Image>().sprite = SPR3[4];
                }
            }
            StartTransportWagoneQuest();
        }
        if (CargoTransportCount >= 1) // Если активно грузовое задание
        {
            for (int i = 0; i < CargoTransportCount; i++)
            {
                CargoTransportWagone[i].SetActive(true);
                if (TextureTrain == 0)
                {
                    CargoTransportWagone[i].GetComponent<Image>().sprite = SPR0[8];
                }
                else if (TextureTrain == 1)
                {
                    CargoTransportWagone[i].GetComponent<Image>().sprite = SPR1[8];
                }
                else if (TextureTrain == 2)
                {
                    CargoTransportWagone[i].GetComponent<Image>().sprite = SPR1[8];
                }
                else if (TextureTrain == 3)
                {
                    CargoTransportWagone[i].GetComponent<Image>().sprite = SPR1[8];
                }
            }
            StartTransportCargoWagoneQuest();
        }
        if (CargoSpecTransportCount >= 1) // Если активно грузовое Spec задание
        {
            for (int i = 0; i < CargoSpecTransportCount; i++)
            {
                CargoSpecTransportWagone[i].SetActive(true);
            }
        }
        if (CargoSpec1TransportCount >= 1) // Если активно грузовое Spec задание
        {
            for (int i = 0; i < CargoSpec1TransportCount; i++)
            {
                CargoSpec1TransportWagone[i].SetActive(true);
            }
        }
        if(AirShipActive == true)
        {
            AirShipObj.SetActive(true);
            if(TextureTrain == 0)
            {
                AirShipObj.GetComponent<Image>().sprite = SPR0[9];
            }
            else if (TextureTrain >= 1)
            {
                AirShipObj.GetComponent<Image>().sprite = SPR1[11];
            }
            StartTimerAirShip();
        }
    }

    void ActiveWagone()
    {
        for (int a = 0; a < WagonCol; a++)
        {
            if (WagoneData[a].Name != "")
            {
                WagoneData[a].WagoneObj.SetActive(true);
            }
        }
        for (int a = 0; a < WagonCol; a++)
        {
            if (WagoneData[a].Name == "")
            {
                WagoneData[a].WagoneObj.SetActive(true);
            }
        }
    }
    public void StartNextStation()
    {
        StartCoroutine(TimerNextStation());
        SmokeParticle.SetActive(true);
    }
    public void TextLoco() // Обновление Текстов панели локомотива
    {
        LevelLocoText.text = LevelLoco.ToString();
        StorageCoalText.text = Coal + "ед./ " + CoalMax + "ед.";
        MaxWagoneText.text = WagonCol + " из " + MaxWagone;
        NeedCoalText.text = NeedCoal + "ед. за: " + TimerLoco + "сек.";
        TermometrText.text = TemperatureOnStreet + "°C";
    }
    public void UpdateEngineLoco() // Улучшения Котла
    {
        if(Money >= LevelCostEngine)
        {
            Money -= LevelCostEngine;
            StartPlusMoney(0 - LevelCostEngine);
            LevelEngine++;
            LevelLocoUpdater();
            TextLoco();
            ResourceTextUpdate();
        }
    }
    public void UpdateChassisLoco() // Улучшение Шасси
    {
        if(Money >= LevelCostChassis)
        {
            Money -= LevelCostChassis;
            StartPlusMoney(0 - LevelCostChassis);
            LevelChassis++;
            LevelLocoUpdater();
            TextLoco();
            ResourceTextUpdate();
        }
    }
    public void UpdateStorageLoco() // Улучшение Хранилища
    {
        if (Money >= LevelCostCoalStorage)
        {
            Money -= LevelCostCoalStorage;
            StartPlusMoney(0 - LevelCostCoalStorage);
            LevelCoalStorage++;
            LevelLocoUpdater();
            TextLoco();
            ResourceTextUpdate();
            if (TaskCount == 0)
            {
                TaskCount = 1;
                TaskCounter();
            }
        }
    }
    public void LevelLocoUpdater() // Уровень Локомотива, данные
    {
        if (LevelLoco == 1) // Если 1 локомотив
        {
            if(LevelEngine == 1) // Уровни Котла
            {
                NeedCoal = 100;
                TimerLoco = 30;
                LevelCostEngine = 500;
                LevelEngineText.text = "Улучшение 1. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if(LevelEngine == 2)
            {
                NeedCoal = 90;
                TimerLoco = 30;
                LevelCostEngine = 1000;
                LevelEngineText.text = "Улучшение 2. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine == 3)
            {
                NeedCoal = 80;
                TimerLoco = 35;
                LevelCostEngine = 1500;
                LevelEngineText.text = "Улучшение 3. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine == 4)
            {
                NeedCoal = 78;
                TimerLoco = 35;
                LevelCostEngine = 2500;
                LevelEngineText.text = "Улучшение 4. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine >= 5)
            {
                NeedCoal = 75;
                TimerLoco = 35;
                LevelEngineText.text = "Максимальный уровень Котла";
            }
            if (LevelChassis == 1) // Уровни Шасси
            {
                MaxWagone = 5;
                LevelCostChassis = 500;
                LevelChassisText.text = "Улучшение 1. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis == 2)
            {
                MaxWagone = 7;
                LevelCostChassis = 1000;
                LevelChassisText.text = "Улучшение 2. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis == 3)
            {
                MaxWagone = 8;
                LevelCostChassis = 1500;
                LevelChassisText.text = "Улучшение 3. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis == 4)
            {
                MaxWagone = 10;
                LevelCostChassis = 2500;
                LevelChassisText.text = "Улучшение 4. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis >= 5)
            {
                MaxWagone = 12;
                LevelChassisText.text = "Максимальный уровень Шасси";
            }
            if (LevelCoalStorage == 1) // Уровни Тендера
            {
                CoalMax = 500;
                LevelCostCoalStorage = 500;
                LevelCoalStorageText.text = "Улучшение 1. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage == 2)
            {
                CoalMax = 650;
                LevelCostCoalStorage = 1000;
                LevelCoalStorageText.text = "Улучшение 2. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage == 3)
            {
                CoalMax = 850;
                LevelCostCoalStorage = 1500;
                LevelCoalStorageText.text = "Улучшение 3. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage == 4)
            {
                CoalMax = 1000;
                LevelCostCoalStorage = 2500;
                LevelCoalStorageText.text = "Улучшение 4. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage >= 5)
            {
                CoalMax = 1200;
                LevelCoalStorageText.text = "Максимальный уровень Тендера";
            }
        }
        if (LevelLoco == 2) // Если 1 локомотив
        {
            if (LevelEngine == 1) // Уровни Котла
            {
                NeedCoal = 140;
                TimerLoco = 30;
                LevelCostEngine = 1000;
                LevelEngineText.text = "Улучшение 1. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine == 2)
            {
                NeedCoal = 100;
                TimerLoco = 30;
                LevelCostEngine = 1500;
                LevelEngineText.text = "Улучшение 2. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine == 3)
            {
                NeedCoal = 80;
                TimerLoco = 35;
                LevelCostEngine = 2500;
                LevelEngineText.text = "Улучшение 3. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine == 4)
            {
                NeedCoal = 75;
                TimerLoco = 35;
                LevelCostEngine = 3500;
                LevelEngineText.text = "Улучшение 4. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine >= 5)
            {
                NeedCoal = 65;
                TimerLoco = 35;
                LevelEngineText.text = "Максимальный уровень Котла";
            }
            if (LevelChassis == 1) // Уровни Шасси
            {
                MaxWagone = 15;
                LevelCostChassis = 1000;
                LevelChassisText.text = "Улучшение 1. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis == 2)
            {
                MaxWagone = 18;
                LevelCostChassis = 2000;
                LevelChassisText.text = "Улучшение 2. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis == 3)
            {
                MaxWagone = 21;
                LevelCostChassis = 3000;
                LevelChassisText.text = "Улучшение 3. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis == 4)
            {
                MaxWagone = 27;
                LevelCostChassis = 4500;
                LevelChassisText.text = "Улучшение 4. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis >= 5)
            {
                MaxWagone = 30;
                LevelChassisText.text = "Максимальный уровень Шасси";
            }
            if (LevelCoalStorage == 1) // Уровни Тендера
            {
                CoalMax = 1500;
                LevelCostCoalStorage = 1000;
                LevelCoalStorageText.text = "Улучшение 1. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage == 2)
            {
                CoalMax = 1800;
                LevelCostCoalStorage = 2000;
                LevelCoalStorageText.text = "Улучшение 2. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage == 3)
            {
                CoalMax = 2100;
                LevelCostCoalStorage = 3000;
                LevelCoalStorageText.text = "Улучшение 3. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage == 4)
            {
                CoalMax = 2500;
                LevelCostCoalStorage = 4500;
                LevelCoalStorageText.text = "Улучшение 4. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage >= 5)
            {
                CoalMax = 2800;
                LevelCoalStorageText.text = "Максимальный уровень Тендера";
            }
        }
        if (LevelLoco == 3) // Если 1 локомотив
        {
            if (LevelEngine == 1) // Уровни Котла
            {
                NeedCoal = 120;
                TimerLoco = 30;
                LevelCostEngine = 1000;
                LevelEngineText.text = "Улучшение 1. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine == 2)
            {
                NeedCoal = 100;
                TimerLoco = 30;
                LevelCostEngine = 1500;
                LevelEngineText.text = "Улучшение 2. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine == 3)
            {
                NeedCoal = 80;
                TimerLoco = 35;
                LevelCostEngine = 2500;
                LevelEngineText.text = "Улучшение 3. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine == 4)
            {
                NeedCoal = 65;
                TimerLoco = 35;
                LevelCostEngine = 3500;
                LevelEngineText.text = "Улучшение 4. Стоимость: " + LevelCostEngine + "$ -потребление угля";
            }
            if (LevelEngine >= 5)
            {
                NeedCoal = 60;
                TimerLoco = 35;
                LevelEngineText.text = "Максимальный уровень Котла";
            }
            if (LevelChassis == 1) // Уровни Шасси
            {
                MaxWagone = 30;
                LevelCostChassis = 1000;
                LevelChassisText.text = "Улучшение 1. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis == 2)
            {
                MaxWagone = 34;
                LevelCostChassis = 2000;
                LevelChassisText.text = "Улучшение 2. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis == 3)
            {
                MaxWagone = 38;
                LevelCostChassis = 3000;
                LevelChassisText.text = "Улучшение 3. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis == 4)
            {
                MaxWagone = 44;
                LevelCostChassis = 4500;
                LevelChassisText.text = "Улучшение 4. Стоимость: " + LevelCostChassis + "$ +Мах количество вагонов";
            }
            if (LevelChassis >= 5)
            {
                MaxWagone = 50;
                LevelChassisText.text = "Максимальный уровень Шасси";
            }
            if (LevelCoalStorage == 1) // Уровни Тендера
            {
                CoalMax = 3000;
                LevelCostCoalStorage = 1000;
                LevelCoalStorageText.text = "Улучшение 1. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage == 2)
            {
                CoalMax = 3400;
                LevelCostCoalStorage = 2000;
                LevelCoalStorageText.text = "Улучшение 2. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage == 3)
            {
                CoalMax = 3800;
                LevelCostCoalStorage = 3000;
                LevelCoalStorageText.text = "Улучшение 3. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage == 4)
            {
                CoalMax = 4200;
                LevelCostCoalStorage = 4500;
                LevelCoalStorageText.text = "Улучшение 4. Стоимость: " + LevelCostCoalStorage + "$ +хранилище угля";
            }
            if (LevelCoalStorage >= 5)
            {
                CoalMax = 5000;
                LevelCoalStorageText.text = "Максимальный уровень Тендера";
            }
        }
    }

    public void ClickLocoPan() // Открытие и закрытие панели локомотива
    {
        if (!LocoPan.activeInHierarchy) 
        {
            if (PanWagon != null) { PanWagon.SetActive(false); }
            LocoPan.SetActive(true);
            PanWagon = LocoPan;
        } 
        else
        {
            LocoPan.SetActive(false);
            PanWagon = null;
        }
    }
    public void OpenLocoUpgradePan() // Открытие панели улучшения локомотива
    {
        UpgradeLocoPan.SetActive(true);
    }
    public void ClosedLocoUpgradePan() // Закрытие панели улучшения локомотива
    {
        UpgradeLocoPan.SetActive(false);
    }
    public void OpenTrainViewPan() // Открытие панели внешнего вида поезда
    {
        ViewTrainPan.SetActive(true);
        if(Texture1 == true)
        {
            TextureChoice[0].SetActive(true);
        }
    }
    public void ClosedTrainViewPan() // Закрытие панели внешнего вида поезда
    {
        ViewTrainPan.SetActive(false);
    }
    public void ResourceTextUpdate() // Обновление текстов и слайдера ресурсов 
    {
        MoneyText.text = Money + "$";
        DiamondText.text = Diamond.ToString();
        CoalText.text = Coal + "/" + CoalMax;
        WorkerText.text = FreeWorker + "/" + AllWorker;
        WarmText.text = Warm + "/" + WarmMax;
        FoodText.text = Food + "/" + FoodMax;
        WaterText.text = Water + "/" + WaterMax;
        SliderResource[0].maxValue = FoodMax;
        SliderResource[1].maxValue = WaterMax;
        SliderResource[2].maxValue = WarmMax;
        SliderResource[4].maxValue = CoalMax;
        SliderResource[0].value = Food;
        SliderResource[1].value = Water;
        SliderResource[2].value = Warm;
        SliderResource[4].value = Coal;
    }
    public void BuyAddWagone() // Покупка и добавление пустого вагона
    {
        WagoneData[WagonCol].WagoneObj.SetActive(true);
        WagonCol++;
        TextLoco();
        if (TextureTrain == 0)
        {
            WagoneData[WagonCol].WagoneImage = SPR0[0];
        }
        else if (TextureTrain == 1)
        {
            WagoneData[WagonCol].WagoneImage = SPR1[0];
        }
    }
    public void MaxStorageWagone() // Максимальная вместимость ресурсов. Зависит от грузовых вагонов
    {
        StorageCount = 50;
        for(int i=0; i < WagonCol; i++)
        {
            if(WagoneData[i].Name == "Storage")
            {
                if(WagoneData[i].LevelWagone == 1)
                {
                    StorageCount += 50;
                }
                else if(WagoneData[i].LevelWagone == 2)
                {
                    StorageCount += 75;
                }
                else if(WagoneData[i].LevelWagone == 3)
                {
                    StorageCount += 100;
                }
            }
        }
        WarmMax = StorageCount * 2; FoodMax = StorageCount; WaterMax = StorageCount;
        SliderResource[0].maxValue = FoodMax; SliderResource[1].maxValue = WaterMax; SliderResource[2].maxValue = WarmMax;
        SliderResource[0].value = Food; SliderResource[1].value = Water; SliderResource[2].value = Warm;
        ResourceTextUpdate();
    }

    // Таймер до следующей станции
    IEnumerator TimerNextStation() //
    {
        int S = 0; // таймер скорости фона
        int ADS = 0; // таймер денег за рекламу
        int R = UnityEngine.Random.Range(10, 500);
        NextStationSlide.maxValue = NextStationTimeCount;
        while (true)
        {
            R--;
            NextStationTime--;
            DistancePlusStatistic++;
            int Min = NextStationTime / 60;
            int Sec = NextStationTime - (Min * 60);
            NextStationTimeText.text = Min + "мин. " + Sec + "сек.";
            NextStationSlide.value = NextStationTimeCount - NextStationTime;
            if (R <= 0)
            {
                StartMessage("Препятствие на пути!");
                BarrierObj.SetActive(true);
                break;
            }
            if (NextStationTime <= 0)
            {
                StopCoroutine("PassFoodNeed");
                StopCoroutine("PassFoodWater");
                Station.SetActive(true);
                for (int i = 0; i < WagonCol; i++)
                {
                    WagoneData[i].WagoneObj.SetActive(false);
                    WagoneData[i].WagoneActive = false;
                }
                SmokeParticle.SetActive(false);
                GamePan.SetActive(false);
                if (YandexGame.EnvironmentData.deviceType == "mobile" & City == 0 || City == 1)
                {
                    YandexGame.StickyAdActivity(false);
                }
                YandexGame.FullscreenShow();
                break;
            }
            else if(NextStationTime > 0 & Coal >= NeedCoal) //
            {
                ActiveTimerLoco--;
                S++;
                ADS++;
                if(ActiveTimerLoco <= 0) //
                {
                    Coal -= NeedCoal;
                    StartPlusCoal(0- NeedCoal);
                    ResourceTextUpdate();
                    TextLoco();
                    ActiveTimerLoco = TimerLoco;
                }
                if (S >= 45 & SpeedFon <= 3)
                {
                    SpeedFon++;
                    S = 0;
                }
                if(ADS >= 40)
                {
                    if(Money >= 500)
                    {
                        ADSMoneyCol = UnityEngine.Random.Range(500, Money);
                        ADSMoneyActivePan.SetActive(true);
                        ADSMoneyActiveText.text = "+" + ADSMoneyCol + "$!!!" + "\n" + "За просмотр"; 
                        StartCoroutine(ADSMoneyActive());
                        ADS = 0;
                    }
                    else
                    {
                        ADSMoneyCol = 500;
                        ADSMoneyActivePan.SetActive(true);
                        ADSMoneyActiveText.text = "+" + ADSMoneyCol + "$!!!" + "\n" + "За просмотр";
                        StartCoroutine(ADSMoneyActive());
                        ADS = 0;
                    }
                }
                yield return new WaitForSeconds(Timer);
            }
            else
            {
                SpeedFon = 0;
                if (!CoalHelp)
                {
                    CoalHelpObj.SetActive(true);
                }
                TreesFreeObj.SetActive(true);
                SmokeParticle.SetActive(false);
                StartLocoEnginePan.SetActive(true);
                StartCoroutine(MessageView("Закончилось топливо!"));
                break;
            }
        }
    }

    public void ClickBarrier()
    {
        TreesClickParticle.SetActive(false);
        TreesClickParticle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TreesClickParticle.SetActive(true);
        clickCountBarrier++;
        if (clickCountBarrier >= 10)
        {
            clickCountBarrier = 0;
            BarrierObj.SetActive(false);
            StartEngineLoco();
        }
    }
    public void ClickTreesCoalFree() // Клик по деревьям для угля
    {
        if(Coal < CoalMax)
        {
            coalCountClick++;
            Coal++;
            CoalPlusStatistic++;
            ResourceTextUpdate();
            TextLoco();
        }
        TreesClickParticle.SetActive(false);
        TreesClickParticle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TreesClickParticle.SetActive(true);
        if(coalCountClick >= 5)
        {
            coalCountClick = 0;
            CollectResources[3].SetActive(false); CollectResources[03].SetActive(true);
        }
        if (!CoalHelp)
        {
            CoalHelpObj.SetActive(false);
            CoalHelp = true;
        }
    }
    public void StartEngineLoco() // Запуск поезда, если достаточно угля
    {
        if(Coal >= NeedCoal)
        {
            SpeedFon = 1;
            StartLocoEnginePan.SetActive(false);
            CoalADS.SetActive(false);
            TreesFreeObj.SetActive(false);
            StartCoroutine(TimerNextStation());
            SmokeParticle.SetActive(true);
        }
        else { StartCoroutine(MessageView("Не хватает топлива!")); }
    }
    public void StartMessage(string mes) // Запуск вывода сообщений из скрипта вагона
    {
        StartCoroutine(MessageView(mes));
    }
    IEnumerator MessageView(string Mes) // Вывод сообщений
    {
        Message.text = Mes;
        yield return new WaitForSeconds(3);
        Message.text = "";
        yield break;
    }
    public void StartTransportWagoneQuest() //Запуск задания по транспортивке
    {
        StartCoroutine(WorkTransportWagone());
    }
    public void StartTransportCargoWagoneQuest() //Запуск задания по транспортивке
    {
        StartCoroutine(WorkTransportCargoWagone());
    }
    IEnumerator WorkTransportWagone() // Корутина расхода ресурсов транспортировки
    {
        while (true)
        {
            if (NextStationTime <= 0 || PTransportCount<=0)
            {
                break;
            }
            if(NextStationTime > 0 & PTransportCount >= 1)
            {
                if(Food < PTransportCount*10||Water < PTransportCount * 10||Warm < PTransportCount * 10)
                {
                    RewardPTransport -= 30;
                    Food -= PTransportCount * 10;
                    Water -= PTransportCount * 10;
                    Warm -= PTransportCount * 10;
                    StartPlusFood(0 - (PTransportCount * 10));
                    StartPlusWater(0 - (PTransportCount * 10));
                    StartPlusWarm(0 - (PTransportCount * 10));
                    if (Food < 0) { Food = 0; }
                    if (Water < 0) { Water = 0; }
                    if (Warm < 0) { Warm = 0; }
                    yield return new WaitForSeconds(30);
                }
                else
                {
                    Food -= PTransportCount * 10;
                    Water -= PTransportCount * 10;
                    Warm -= PTransportCount * 10;
                    StartPlusFood(0 - (PTransportCount * 10));
                    StartPlusWater(0 - (PTransportCount * 10));
                    StartPlusWarm(0 - (PTransportCount * 10));
                    yield return new WaitForSeconds(30);
                }
            }
        }
    }
    IEnumerator WorkTransportCargoWagone() // Корутина расхода ресурсов транспортировки Грузовой
    {
        while (true)
        {
            if (NextStationTime <= 0 || CargoTransportCount <= 0)
            {
                break;
            }
            if (NextStationTime > 0 & CargoTransportCount >= 1)
            {
                if (Coal < CargoTransportCount * 20)
                {
                    RewardCargoTransport -= 30;
                    Coal -= CargoTransportCount * 20;
                    StartPlusCoal(0 - (CargoTransportCount * 20));
                    if (Coal < 0) { Coal = 0; }
                    yield return new WaitForSeconds(30);
                }
                else
                {
                    Coal -= CargoTransportCount * 20;
                    StartPlusCoal(0 - (CargoTransportCount * 20));
                    yield return new WaitForSeconds(30);
                }
            }
        }
    }

    IEnumerator PassFoodNeed() // Корутина пищи людей
    {
        int NeedFoodPeople = 0;
        int CicleNeedFood = 0;
        while (true)
        {
            if (AllWorker <= Food)
            {
                Food -= AllWorker;
                ResourceTextUpdate();
                StartPlusFood(0 - AllWorker);
                NeedFoodPeople = 0;
                CicleNeedFood = 0;
                yield return new WaitForSeconds(10);
            }
            else if (AllWorker > Food)
            {
                StartMessage("Людям не хватает пищи!");
                NeedFoodPeople = AllWorker - Food;
                CicleNeedFood++;
                StartPlusFood(0 - Food);
                Food = 0;
                ResourceTextUpdate();
                if (CicleNeedFood >= 7) // Если 7 циклов и больше люди без еды
                {
                    for(int i = 0; i < WagoneData.Count; i++)
                    {
                        if (NeedFoodPeople <= 0) { break; }
                        if (WagoneData[i].WorkerInWagone >= NeedFoodPeople)
                        {
                            WagoneData[i].WorkerInWagone -= NeedFoodPeople;
                            FreeWorker += NeedFoodPeople;
                            NeedFoodPeople = 0;
                        }
                        else
                        {
                            NeedFoodPeople -= WagoneData[i].WorkerInWagone;
                            FreeWorker += WagoneData[i].WorkerInWagone;
                            WagoneData[i].WorkerInWagone = 0;
                        }
                    }
                    CicleNeedFood = 0;
                }
                yield return new WaitForSeconds(10);
            }
        }
    }
    IEnumerator PassFoodWater() // Корутина пищи людей
    {
        int NeedFoodPeople = 0;
        int CicleNeedFood = 0;
        while (true)
        {
            if (AllWorker <= Water)
            {
                Water -= AllWorker;
                ResourceTextUpdate();
                StartPlusFood(0 - AllWorker);
                NeedFoodPeople = 0;
                CicleNeedFood = 0;
                yield return new WaitForSeconds(10);
            }
            else if (AllWorker > Water)
            {
                StartMessage("Людям не хватает воды!");
                NeedFoodPeople = AllWorker - Water;
                CicleNeedFood++;
                StartPlusFood(0 - Water);
                Water = 0;
                ResourceTextUpdate();
                if (CicleNeedFood >= 7) // Если 7 циклов и больше люди без еды
                {
                    for (int i = 0; i < WagoneData.Count; i++)
                    {
                        if (NeedFoodPeople <= 0) { break; }
                        if (WagoneData[i].WorkerInWagone >= NeedFoodPeople)
                        {
                            WagoneData[i].WorkerInWagone -= NeedFoodPeople;
                            FreeWorker += NeedFoodPeople;
                            NeedFoodPeople = 0;
                        }
                        else
                        {
                            NeedFoodPeople -= WagoneData[i].WorkerInWagone;
                            FreeWorker += WagoneData[i].WorkerInWagone;
                            WagoneData[i].WorkerInWagone = 0;
                        }
                    }
                    CicleNeedFood = 0;
                }
                yield return new WaitForSeconds(10);
            }
        }
    }

    public void DeleteWorkerTrain(int index)
    {
        int excessWorker = 5 * WagoneData[index].LevelWagone;
        if (FreeWorker >= excessWorker)
        {
            FreeWorker -= excessWorker;
            AllWorker -= excessWorker;
        }
        else
        {
            excessWorker -= FreeWorker;
            AllWorker -= FreeWorker;
            FreeWorker = 0;
            AllWorker -= excessWorker;
            for(int i = 0; i < WagoneData.Count; i++)
            {
                if(excessWorker <= 0) { break; }
                if (WagoneData[i].WorkerInWagone >= excessWorker)
                {
                    WagoneData[i].WorkerInWagone -= excessWorker;
                    excessWorker = 0;
                }
                else
                {
                    excessWorker -= WagoneData[i].WorkerInWagone;
                    WagoneData[i].WorkerInWagone = 0;
                }
            }
        }
    }

    public void ViewLoco() // Вид локомотива от уровня и выбранной текстуры
    {
        if(LevelLoco == 1)
        {
            if(TextureTrain == 0)
            {
                LocoSprite.sprite = SPR0[5];
            }
            if (TextureTrain == 1)
            {
                LocoSprite.sprite = SPR1[5];
            }
            if (TextureTrain == 2)
            {
                LocoSprite.sprite = SPR2[5];
            }
            if (TextureTrain == 3)
            {
                LocoSprite.sprite = SPR3[5];
            }
        }
        if (LevelLoco == 2)
        {
            if (TextureTrain == 0)
            {
                LocoSprite.sprite = SPR0[6];
            }
            if (TextureTrain == 1)
            {
                LocoSprite.sprite = SPR1[6];
            }
            if (TextureTrain == 2)
            {
                LocoSprite.sprite = SPR2[6];
            }
            if (TextureTrain == 3)
            {
                LocoSprite.sprite = SPR3[6];
            }
        }
        if(LevelLoco == 3)
        {
            if (TextureTrain == 0)
            {
                LocoSprite.sprite = SPR0[7];
            }
            if (TextureTrain == 1)
            {
                LocoSprite.sprite = SPR1[7];
            }
            if (TextureTrain == 2)
            {
                LocoSprite.sprite = SPR2[7];
            }
            if (TextureTrain == 3)
            {
                LocoSprite.sprite = SPR3[7];
            }
        }
    }
    public void UpdateViewTrain() // Обновление вида вагонов
    {
        for(int i = 0; i < WagonCol; i++)
        {
            if(TextureTrain == 0)
            {
                if (WagoneData[i].Name == "")
                {
                    WagoneData[i].WagoneImage = SPR0[0];
                }
                if (WagoneData[i].Name == "Food")
                {
                    WagoneData[i].WagoneImage = SPR0[0];
                }
                if (WagoneData[i].Name == "Water")
                {
                    WagoneData[i].WagoneImage = SPR0[1];
                }
                if (WagoneData[i].Name == "Pass")
                {
                    WagoneData[i].WagoneImage = SPR0[2];
                }
                if (WagoneData[i].Name == "Boiler")
                {
                    WagoneData[i].WagoneImage = SPR0[3];
                }
            }
            if (TextureTrain == 1)
            {
                if (WagoneData[i].Name == "")
                {
                    WagoneData[i].WagoneImage = SPR1[0];
                }
                if (WagoneData[i].Name == "Food")
                {
                    WagoneData[i].WagoneImage = SPR1[0];
                }
                if (WagoneData[i].Name == "Water")
                {
                    WagoneData[i].WagoneImage = SPR1[1];
                }
                if (WagoneData[i].Name == "Pass")
                {
                    WagoneData[i].WagoneImage = SPR1[2];
                }
                if (WagoneData[i].Name == "Boiler")
                {
                    WagoneData[i].WagoneImage = SPR1[3];
                }
            }
            if (TextureTrain == 2)
            {
                if (WagoneData[i].Name == "")
                {
                    WagoneData[i].WagoneImage = SPR2[0];
                }
                if (WagoneData[i].Name == "Food")
                {
                    WagoneData[i].WagoneImage = SPR2[0];
                }
                if (WagoneData[i].Name == "Water")
                {
                    WagoneData[i].WagoneImage = SPR2[1];
                }
                if (WagoneData[i].Name == "Pass")
                {
                    WagoneData[i].WagoneImage = SPR2[2];
                }
                if (WagoneData[i].Name == "Boiler")
                {
                    WagoneData[i].WagoneImage = SPR2[3];
                }
            }
            if (TextureTrain == 3)
            {
                if (WagoneData[i].Name == "")
                {
                    WagoneData[i].WagoneImage = SPR3[0];
                }
                if (WagoneData[i].Name == "Food")
                {
                    WagoneData[i].WagoneImage = SPR3[0];
                }
                if (WagoneData[i].Name == "Water")
                {
                    WagoneData[i].WagoneImage = SPR3[1];
                }
                if (WagoneData[i].Name == "Pass")
                {
                    WagoneData[i].WagoneImage = SPR3[2];
                }
                if (WagoneData[i].Name == "Boiler")
                {
                    WagoneData[i].WagoneImage = SPR3[3];
                }
            }
        }
    }
    public void ChoiceViewTrain(int index) // Выбор вида поезда!
    {
        if(index == 0)
        {
            TextureTrain = 0;
            ViewLoco();
            UpdateViewTrain();
        }
        else if (index == 1)
        {
            TextureTrain = 1;
            ViewLoco();
            UpdateViewTrain();
        }
        else if (index == 2)
        {
            TextureTrain = 2;
            ViewLoco();
            UpdateViewTrain();
        }
        else if (index == 3)
        {
            TextureTrain = 3;
            ViewLoco();
            UpdateViewTrain();
        }
    }

    public void RandomBackground() //
    {
        int r = UnityEngine.Random.Range(0, ParallaxObj.Length);
        for(int i = 0; i < ParallaxObj.Length; i++) { ParallaxObj[i].SetActive(false); }
        ParallaxObj[r].SetActive(true);    
    }

    // TRAINER!!!
    public void NextTrainPan()
    {
        if (TrainCount == 17)
        {
            Time.timeScale = 1;
            StartNextStation();
            StartCoroutine(AutoSave());
            TaskCounter();
            Trainer[0] = true;
            TrainPan[37].SetActive(false);
            TrainPan[38].SetActive(false);
        }
        else if (TrainCount == 16)
        {
            TrainPan[33].SetActive(false);
            TrainPan[34].SetActive(false);
            TrainPan[35].SetActive(true);
            TrainPan[36].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 15)
        {
            TrainPan[20].SetActive(false);
            TrainPan[27].SetActive(false);
            TrainPan[30].SetActive(false);
            TrainPan[33].SetActive(true);
            TrainPan[34].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 14)
        {
            TrainPan[27].SetActive(false);
            TrainPan[30].SetActive(true);
            TrainPan[31].SetActive(true);
            TrainPan[32].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 13)
        {
            TrainPan[24].SetActive(false);
            TrainPan[27].SetActive(true);
            TrainPan[28].SetActive(true);
            TrainPan[29].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 12)
        {
            TrainPan[21].SetActive(false);
            TrainPan[24].SetActive(true);
            TrainPan[25].SetActive(true);
            TrainPan[26].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 11)
        {
            TrainPan[19].SetActive(false);
            TrainPan[21].SetActive(true);
            TrainPan[22].SetActive(true);
            TrainPan[23].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 10)
        {
            TrainPan[18].SetActive(false);
            TrainPan[19].SetActive(true);
            TrainPan[20].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 9)
        {
            Time.timeScale = 1;
            TrainPan[0].SetActive(false);
            TrainPan[17].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 8)
        {
            TrainPan[14].SetActive(false);
            TrainPan[15].SetActive(false);
            TrainPan[16].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 7)
        {
            TrainPan[12].SetActive(false);
            TrainPan[13].SetActive(false);
            TrainPan[14].SetActive(true);
            TrainPan[15].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 6)
        {
            TrainPan[9].SetActive(false);
            TrainPan[10].SetActive(false);
            TrainPan[11].SetActive(false);
            TrainPan[12].SetActive(true);
            TrainPan[13].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 5)
        {
            TrainPan[7].SetActive(false);
            TrainPan[8].SetActive(false);
            TrainPan[9].SetActive(true);
            TrainPan[10].SetActive(true);
            TrainPan[11].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 4)
        {
            TrainPan[5].SetActive(false);
            TrainPan[6].SetActive(false);
            TrainPan[7].SetActive(true);
            TrainPan[8].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 3)
        {
            TrainPan[4].SetActive(false);
            TrainPan[5].SetActive(true);
            TrainPan[6].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 2)
        {
            TrainPan[3].SetActive(false);
            TrainPan[4].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 1)
        {
            TrainPan[2].SetActive(false);
            TrainPan[3].SetActive(true);
            TrainCount++;
        }
        else if (TrainCount == 0)
        {
            TrainPan[1].SetActive(false);
            TrainPan[2].SetActive(true);
            TrainCount++;
        }
    }
    public void EscapeTrain()
    {
        Time.timeScale = 1;
        StartNextStation();
        StartCoroutine(AutoSave());
        TaskCounter();
        Trainer[0] = true;
        TrainPan[0].SetActive(false);
        TrainPan[17].SetActive(false);
        TrainPan[37].SetActive(false);
        TrainPan[38].SetActive(false);
    }
    public void Trainer2() //
    {
        if(WagoneTrainerChoice[0] == true & WagoneTrainerChoice[1] == true & WagoneTrainerChoice[2] == true & WagoneTrainerChoice[3] == true)
        {
            TrainPan[17].SetActive(false);
            TrainPan[37].SetActive(true);
            Time.timeScale = 0;
        }
    }

    // HELP!!!
    public void OpenHelpPan()
    {
        HelpPan.SetActive(true);
        int H = TimeInGameStatistic / 60;
        int M = TimeInGameStatistic - (H * 60);
        TimeInGameStatisticText.text = "Время в игре:" + H + "ч. " + M + "м.";
        Time.timeScale = 0;
    }
    public void ClosedHelpPan()
    {
        HelpPan.SetActive(false);
        Time.timeScale = 1;
    }
    public void ChoiceHelp(int index)
    {
        if(index == 0)
        {
            TargetHelp.SetActive(true);
            WagoneHelp.SetActive(false);
            LocomotiveHelp.SetActive(false);
        }
        else if (index == 1)
        {
            TargetHelp.SetActive(false);
            WagoneHelp.SetActive(true);
            LocomotiveHelp.SetActive(false);
        }
        else if (index == 2)
        {
            TargetHelp.SetActive(false);
            WagoneHelp.SetActive(false);
            LocomotiveHelp.SetActive(true);
        }
    }


    // AD!!!
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);
    }
    void Rewarded(int id)
    {
        if (id == 0)
        {
            TakeADSMoney();
        }
        if (id == 1)
        {
            ADSCoal();
        }
        if (id == 2)
        {
            StartCoroutine(TimerX2());
            TimerX2Pan.SetActive(false);
            if (TaskCount == 8)
            {
                TaskCount = 9;
                TaskCounter();
            }
        }
        if (id == 3)
        {
            Diamond++;
            ResourceTextUpdate();
        }
    }

    public void OpenShopPan() // Открытие панели магазина
    {
        ShopPan.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClosedShopPan() // Закрытие панели магазина
    {
        ShopPan.SetActive(false);
        Time.timeScale = 1;
    }
    public void ExchangeDiamond() // Обмен Роскоши на деньги
    {
        if (Diamond >= 1)
        {
            Money += 1500;
            Diamond--;
            StartPlusMoney(1500);
        }
    }
    public void OpenClosedTiemrX2Pan() // Открытие закрытие панели ускроения времени до станции
    {
        if(x2Pan == 1)
        {
            TimerX2Pan.SetActive(false);
            x2Pan = 0;
        }
        else
        {
            TimerX2Pan.SetActive(true);
            x2Pan = 1;
        }
    }
    IEnumerator TimerX2() // Ускорение времени на минуту
    {
        Timer = 0.5f;
        TimerX2Bttn.interactable = false;
        yield return new WaitForSeconds(60);
        Timer = 1;
        TimerX2Bttn.interactable = true;
        yield break;
    }

    public void ADSCoal() // Добавление угля за просмотр рекламы!
    {
        int Razn = CoalMax - Coal;
        if (350 < Razn)
        {
            Coal += 350;
            StartPlusCoal(350);
            ResourceTextUpdate();
        }
        else if (350 >= Razn)
        {
            Coal += Razn;
            StartPlusCoal(350);
            ResourceTextUpdate();
        }
    }

    public void TakeADSMoney() // Взять деньги за рекламу
    {
        Money += ADSMoneyCol;
        StartPlusMoney(ADSMoneyCol);
        ADSMoneyActivePan.SetActive(false);
        ResourceTextUpdate();
    }
    IEnumerator ADSMoneyActive() // Активность таймера на плюс денег
    {
        ADSMoneyActiveSlide.value = 0;
        int t = 0;
        while (true)
        {
            if (t < 10)
            {
                t++;
                ADSMoneyActiveSlide.value = t;
                yield return new WaitForSeconds(1);
            }
            else
            {
                ADSMoneyActivePan.SetActive(false);
                yield break;
            }
        }
    }

    
    // Корутины на тексты прибавления и отнимания ресурсов!
    public void StartPlusFood(int F)
    {
        StartCoroutine(PlusFood(F));
    }
    public void StartPlusWater(int F)
    {
        StartCoroutine(PlusWater(F));
    }
    public void StartPlusCoal(int F)
    {
        StartCoroutine(PlusCoal(F));
    }
    public void StartPlusWarm(int F)
    {
        StartCoroutine(PlusWarm(F));
    }
    public void StartPlusWorker(int F)
    {
        StartCoroutine(PlusWorker(F));
    }
    public void StartPlusMoney(int F)
    {
        StartCoroutine(PlusMoney(F));
    }
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


    // AIRSHIPS
    public void ClickAirShipPan() // Открытие и закрытие панели локомотива
    {
        if(!AirShipPan.activeInHierarchy) AirShipPan.SetActive(true);
        else AirShipPan.SetActive(false);
    }
    public void StartTimerAirShip() //
    {
        StartCoroutine(TimeAirShip());
    }
    IEnumerator TimeAirShip() //
    {
        while (true)
        {
            TimerAirShip--;
            int Min = TimerAirShip / 60;
            int Sec = TimerAirShip - (Min * 60);
            TimerAirShipText.text = "Осталось: "+ Min + "мин. " + Sec + "сек.";
            TimerAirShipText1.text = TimerAirShip.ToString();
            if (TimerAirShip <= 0)
            {
                Diamond++;
                TimerAirShip = 900;
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    public void Save()
    {
        if(Saver == false)
        {
            Saver = true;
            YandexGame.savesData.Saver = Saver;
        }
        YandexGame.savesData.WagonCol = WagonCol;
        YandexGame.savesData.TextureTrain = TextureTrain;
        YandexGame.savesData.Texture1 = Texture1;
        YandexGame.savesData.Texture2 = Texture2;
        YandexGame.savesData.Texture3 = Texture3;
        //
        YandexGame.savesData.Money = Money;
        YandexGame.savesData.Diamond = Diamond;
        YandexGame.savesData.Score = Score;
        YandexGame.savesData.Coal = Coal;
        YandexGame.savesData.Food = Food;
        YandexGame.savesData.Water = Water;
        YandexGame.savesData.Warm = Warm;
        YandexGame.savesData.Worker = FreeWorker;
        YandexGame.savesData.AllPeople = AllWorker;
        //
        YandexGame.savesData.TemperatureOnStreet = TemperatureOnStreet;
        YandexGame.savesData.NextStationTime = NextStationTime;
        YandexGame.savesData.NextStationTimeCount = NextStationTimeCount;
        YandexGame.savesData.LevelLoco = LevelLoco;
        YandexGame.savesData.ActiveTimerLoco = ActiveTimerLoco;
        YandexGame.savesData.LevelEngine = LevelEngine;
        YandexGame.savesData.LevelCoalStorage = LevelCoalStorage;
        YandexGame.savesData.LevelChassis = LevelChassis;
        //
        YandexGame.savesData.CoalHelp = CoalHelp;
        //
        YandexGame.savesData.PTransportCount = PTransportCount;
        YandexGame.savesData.RewardPTransport = RewardPTransport;
        //
        YandexGame.savesData.CargoTransportCount = CargoTransportCount;
        YandexGame.savesData.RewardCargoTransport = RewardCargoTransport;
        //
        YandexGame.savesData.CargoSpecTransportCount = CargoSpecTransportCount;
        YandexGame.savesData.RewardCargoSpecTransport = RewardCargoSpecTransport;
        //
        YandexGame.savesData.CargoSpec1TransportCount = CargoSpec1TransportCount;
        YandexGame.savesData.RewardCargoSpec1Transport = RewardCargoSpec1Transport;
        //
        YandexGame.savesData.City = City;
        YandexGame.savesData.Trainer[0] = Trainer[0];
        YandexGame.savesData.Trainer[1] = Trainer[1];
        YandexGame.savesData.TaskCount = TaskCount;
        YandexGame.savesData.ActiveTask = ActiveTask; //
        YandexGame.savesData.CityTask = CityTask; //
        YandexGame.savesData.AirShipActive = AirShipActive; //
        YandexGame.savesData.TimerAirShip = TimerAirShip; //
        YandexGame.savesData.TimeInGameStatistic = TimeInGameStatistic; //
        YandexGame.savesData.CoalPlusStatistic = CoalPlusStatistic; //
        YandexGame.savesData.FoodPlusStatistic = FoodPlusStatistic; //
        YandexGame.savesData.WaterPlusStatistic = WaterPlusStatistic; //
        YandexGame.savesData.WarmPlusStatistic = WarmPlusStatistic; //
        YandexGame.savesData.MoneyPlusStatistic = MoneyPlusStatistic; //
        YandexGame.savesData.DistancePlusStatistic = DistancePlusStatistic; //
        YandexGame.savesData.Thanks = Thanks;
        for(int i = 0; i < WagonCol; i++)
        {
            YandexGame.savesData.WagoneActive[i] = WagoneData[i].WagoneActive;
            YandexGame.savesData.Name[i] = WagoneData[i].Name;
            YandexGame.savesData.LevelWagone[i] = WagoneData[i].LevelWagone;
            YandexGame.savesData.TimerActiveProduction[i] = WagoneData[i].TimerActiveProduction;
            YandexGame.savesData.WorkerInWagone[i] = WagoneData[i].WorkerInWagone;
            YandexGame.savesData.TemperatureWagone[i] = WagoneData[i].TemperatureWagone;
        }
        for(int i = 0; i < QuestionPoint.Length; i++)
        {
            YandexGame.savesData.QuestionPoint[i] = QuestionPoint[i];
        }
        YandexGame.SaveProgress();
        YandexGame.NewLeaderboardScores("LeaderBoard", Score);
        StartCoroutine(SaveColor());
    }

    IEnumerator SaveColor()
    {
        SaveText.color = new Color(0, 255, 0, 255);
        yield return new WaitForSeconds(2);
        SaveText.color = new Color(0, 0, 0, 255);
        yield break;
    }
    IEnumerator AutoSave()
    {
        int SaveCount = 0;
        while (true)
        {
            SaveCount++;
            if (SaveCount >= 35)
            {
                SaveCount = 0;
                Save();
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }


    public void DeleteSave()
    {
        YandexGame.ResetSaveProgress();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenDeleteSave()
    {
        DeletePan.SetActive(true);
    }
    public void ClosedDeleteSave()
    {
        DeletePan.SetActive(false);
    }

    // MENU!!!

    public void ReturnTravel()
    {
        MenuPan.SetActive(false);
        StartGame();
    }


    // TASK!!!

    public void TaskCounter() //
    {
        if(TaskCount == 0)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = false;
            TaskText.text = "Улучшите тендер Локомотива";
        }
        else if (TaskCount == 1)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = true;
            TaskText.text = "Собрать награду";
        }
        else if (TaskCount == 2)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = false;
            TaskText.text = "Соберите любой ресурс";
        }
        else if (TaskCount == 3)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = true;
            TaskText.text = "Собрать награду";
        }
        else if (TaskCount == 4)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = false;
            TaskText.text = "Выполните задание по доставке";
        }
        else if (TaskCount == 5)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = true;
            TaskText.text = "Собрать награду";
        }
        else if (TaskCount == 6)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = false;
            TaskText.text = "Улучшите вагон";
        }
        else if (TaskCount == 7)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = true;
            TaskText.text = "Собрать награду";
        }
        else if (TaskCount == 8)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = false;
            TaskText.text = "Ускорьте время до станции";
        }
        else if (TaskCount == 9)
        {
            TaskPan.SetActive(true);
            TaskDone.interactable = true;
            TaskText.text = "Собрать награду";
        }
    }
    public void TaskDoner() //
    {
        if (TaskCount == 1)
        {
            Money += 1000;
            PlusMoney(1000);
            TaskCount++;
            TaskCounter();
        }
        else if (TaskCount == 3)
        {
            Money += 500;
            PlusMoney(500);
            TaskCount++;
            TaskCounter();
        }
        else if (TaskCount == 5)
        {
            Money += 1000;
            PlusMoney(1000);
            TaskCount++;
            TaskCounter();
        }
        else if (TaskCount == 7)
        {
            Money += 800;
            PlusMoney(800);
            TaskCount++;
            TaskCounter();
        }
        else if (TaskCount == 9)
        {
            Diamond++;
            TaskCount++;
            TaskPan.SetActive(false);
        }
    }

    //
    IEnumerator TimerInGame() // Счетчик времени в игре!
    {
        while (true)
        {
            TimeInGameStatistic++;
            if(TimeInGameStatistic >= 20 & Thanks == false)
            {
                OpenThanksPan();
            }
            yield return new WaitForSeconds(60);
        }
    }

    //THANKS!!!
    void OpenThanksPan()
    {
        ThanksPan.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClosedThanksPan()
    {
        ThanksPan.SetActive(false);
        Thanks = true;
        Time.timeScale = 1;
    }
    public void OffFeedback()
    {
        Feedback.SetActive(false);
    }
}

[Serializable]
public class WagoneData // Данные вагона
{
    public bool WagoneActive; // Активация скрипта вагона
    public GameObject WagoneObj; // Активация обьекта вагона
    public string Name; // Название вагона (Назначение)
    public int LevelWagone; // Уровень вагона
    public int TimerActiveProduction; // Текующее время для таймера производства
    public int WorkerInWagone; // Количество активных работников и проживающих в пасс в вагоне (Кол. материалов в грузвом)
    public int TemperatureWagone; // Температура в вагоне
    public Sprite WagoneImage; // Спрайт вагона (Внешний вид)
}