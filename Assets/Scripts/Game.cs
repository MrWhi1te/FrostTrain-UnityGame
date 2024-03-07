using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Game : MonoBehaviour
{
    public Training TR;
    public Audio AO;
    public SaveLoad SV;

    public List<WagoneData> WagoneData = new List<WagoneData>(); // Лист с данными вагонов
    public int WagonCol; // Количество активных вагонов
    [SerializeField] private GameObject Station; //
    [SerializeField] private GameObject GamePan; //
    [SerializeField] private Sprite[] SPR0; // Вид вагона стандарт
    [SerializeField] private Sprite[] SPR1; // Вид вагона цветной
    [SerializeField] private Sprite[] SPR2; // Вид вагона Золотой
    [SerializeField] private Sprite[] SPR3; // Вид вагона Фиолетовый
    public int TextureTrain; // Выбранный вид поезда
    public bool[] Texture; // 1-Цветной поезд / 2-Золотой поезд / 3-Фиолетовый
    
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
    [HideInInspector] public int[] PassResourceUse; // Дополнительное потребление ресурсе пассажирами
    public GameObject[] CollectResources; // 
    [SerializeField] private Text MoneyText; // Текст денег
    [SerializeField] private Text CoalText; // Текст угля
    [SerializeField] private Text WorkerText; // Текст свободных рабочих
    [SerializeField] private Text WarmText; // Текст тепла
    [SerializeField] private Text FoodText; // Текст еды
    [SerializeField] private Text WaterText; // Текст воды
    [SerializeField] private Text DiamondText; // Роскошь текст
    [SerializeField] private Text[] plusResourceText; // 0-money / 1-Coal / 2-Food / 3-Water / 4-Warm
    [SerializeField] private Slider[] SliderResource; // Слайдер ресурсов (В наличие и макс)
    private int StorageCount = 50; // Емкость хранилища
    
    [Header("Температура")]
    [HideInInspector] public int TemperatureOnStreet; // Температура за бортом
    [SerializeField] private Text TermometrText; // Текст термометра температуры за бортом
   
    [Header("ТаймерСтанции")]
    public Slider NextStationSlide; // Слайдер (прогресс) следующей станции
    public Text NextStationTimeText; // Текст времени до следующей станции
    public int NextStationTime; // Таймер до следующей станции
    public int NextStationTimeCount; //
    [SerializeField] private Button TimerX2Bttn; //
    [SerializeField] private GameObject TimerX2Pan; //
    private float Timer = 1; //
    
    [Header("Локомотив")]
    [SerializeField] private Text LevelLocoText; // Текст уроень локомотива
    [SerializeField] private Text StorageCoalText; // Текст Хранилище угля
    [SerializeField] private Text NeedCoalText; // Текст потребления угля локомотивом
    [SerializeField] private Text MaxWagoneText; // Текст максимальное количество вагонов
    [SerializeField] private GameObject LocoPan; // Панель Локомотива
    [SerializeField] private GameObject UpgradeLocoPan; // Панель Улучшения локомотива
    [SerializeField] private GameObject[] TextureChoice; //
    [SerializeField] private GameObject SmokeParticle; //
    public int LevelLoco = 1; // уровень локомотива. Уровень зависит от вида локомотива. ПОкупается на станции
    public int MaxWagone; // Максимальное кол-во вагонов
    [SerializeField] private Image LocoSprite; // 
    private int NeedCoal;// Сколько потребляет угля
    private int TimerLoco;// Таймер за сколько потребляет уголь
    [HideInInspector] public int ActiveTimerLoco; // Активный таймер локомотива
    
    [Header("CoalFree")]
    [SerializeField] private GameObject CoalADS; // Объект угля за рекламу
    [SerializeField] private GameObject StartLocoEnginePan; // Запуск двигателя
    [SerializeField] public GameObject TreesClickParticle; //
    [SerializeField] private GameObject TreesFreeObj; //
    public GameObject CoalHelpObj; //
    public bool CoalHelp; // Подсказка при первом появлении угля

    [Header("Message")]
    [SerializeField] private Text Message; // Текст сообщений
   
    [Header("Улучшение локомотива")]
    [SerializeField] private Text LevelEngineText; // Уровень Котла
    [SerializeField] private Text LevelCoalStorageText; // Уровень Тендера
    [SerializeField] private Text LevelChassisText; // Уровень Шасси
    [HideInInspector] public int LevelEngine = 1; // Уровень двигателя локомотива
    private int LevelCostEngine; // Стоимость улучшения двигателя
    [HideInInspector] public int LevelCoalStorage = 1; // Уровень тендера локомотива
    private int LevelCostCoalStorage; // Стоимость улучшения тендера локо 
    [HideInInspector] public int LevelChassis = 1; // Уровень шасси локомотива
    private int LevelCostChassis; // Стоимость улучшения шасси локо 

    [Header("Passengers")]
    public int passWagoneCount;
    public int passCount;
    public bool helpPass;
    [SerializeField] private GameObject[] passWagon; 

    [Header("CargoTrasport")]
    public GameObject[] CargoTransportWagone; // Обьекты вагонов задания перевозки
    [HideInInspector] public int CargoTransportCount; // Количество вагонов
    [HideInInspector] public int RewardCargoTransport; // Награда за доставку
   
    [Header("CargoSpecTrasport")]
    public GameObject[] CargoSpecTransportWagone; // Обьекты вагонов задания перевозки
    [HideInInspector] public int CargoSpecTransportCount; // Количество вагонов
   
    [Header("CargoSpec1Trasport")]
    public GameObject[] CargoSpec1TransportWagone; // Обьекты вагонов задания перевозки
    [HideInInspector] public int CargoSpec1TransportCount; // Количество вагонов
   
    [Header("Город")]
    [HideInInspector] public int City; // Текущий город
    [HideInInspector] public int ChoiceCity; // Выбор города
    public string[] NameCity; // Название выбранного города
    public int[] TimeForCity; // Время до следующего города
    public int[] TemperatureForCity; // Температура до следующего города
   
    [Header("Background")]
    [HideInInspector] public int SpeedFon; // Скорость движения фона
    public GameObject[] ParallaxObj; //

    [Header("Barrier")]
    public GameObject barrierObj; //
    public GameObject helpBarrierObj; //
    [HideInInspector] public bool helpBarrier;

    [Header("LocoRepair")]
    public GameObject repairTrainObj; //
    public GameObject helpTrainRepairObj; //
    [HideInInspector] public bool helpRepair;

    [Header("Trainer")]
    [HideInInspector] public bool[] Trainer; //
    [HideInInspector] public int trainingCount; //
    
    [Header("Help")]
    [SerializeField] private GameObject HelpPan; //
    [SerializeField] private GameObject TargetHelp; //
    [SerializeField] private GameObject WagoneHelp; //
    [SerializeField] private GameObject LocomotiveHelp; //
   
    [Header("ADS")]
    [SerializeField] private GameObject ShopPan; //
    [SerializeField] private GameObject ADSMoneyActivePan; // Панель предложения денег за рекламу
    [SerializeField] private Slider ADSMoneyActiveSlide; // слайдер таймера денег за рекламу
    [SerializeField] private Text ADSMoneyActiveText; // текст денег за рекламу
    private int ADSMoneyCol; // награда денег за рекламу
    
    [Header("Menu")]
    [SerializeField] private GameObject MenuPan; //
    public Text ScoreMenu; // 
    
    //
    [SerializeField] private GameObject TaskPan; // Активация панели задания
    [SerializeField] private Button TaskDone; // Если задание выполнено, то собрать награду
    [SerializeField] private Text TaskText; // Текст задания
    public int TaskCount; // Счетчик задания
    
    [Header("AirShip")]
    [SerializeField] private GameObject AirShipObj; //
    [SerializeField] private GameObject AirShipPan;
    [SerializeField] private Text TimerAirShipText; //
    [SerializeField] private Text TimerAirShipText1; //
    [HideInInspector] public bool AirShipActive; //
    [HideInInspector] public int TimerAirShip = 900; //
    
    [Header("Thanks")]
    [SerializeField] private GameObject ThanksPan;//
    [SerializeField] private GameObject Feedback; //
    [HideInInspector] public bool Thanks; //
    //
    [Header("STATISTIC")]
    [HideInInspector] public int TimeInGameStatistic; //
    [HideInInspector] public int CoalPlusStatistic; //
    [HideInInspector] public int FoodPlusStatistic; //
    [HideInInspector] public int WaterPlusStatistic; //
    [HideInInspector] public int WarmPlusStatistic; //
    [HideInInspector] public int MoneyPlusStatistic; //
    [HideInInspector] public int DistancePlusStatistic; //
    [SerializeField] private Text TimeInGameStatisticText; //

    public GameObject EffectCollect;
    [SerializeField] private GameObject ParticleTrain;
    //

    [HideInInspector] public GameObject PanWagon; // Открытая панель вагона
    public GameObject trailer; //

    public Dictionary<string, Sprite[]> wagoneSprites = new();
    private Dictionary<int, Sprite[]> LocoSprites = new();

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    void StartGame()
    {
        if (Trainer[0] == false)
        {
            trailer.SetActive(true);
            TR.trainingPanGame[0].SetActive(true);
            AO.PlayAudioEnterPanel();
            TR.trainingPanGame[2].SetActive(true);
            NextStationTimeCount = 60;
            NextStationSlide.maxValue = NextStationTimeCount;
            NextStationTime = NextStationTimeCount;
            ActiveTimerLoco = TimerLoco;
        }
        if(Trainer[0] == true)
        {
            StartNextStation();
            SV.StartAutoSave();
            TaskCounter();
        }
        wagoneSprites.Add("", new Sprite[] { SPR0[0], SPR1[0], SPR2[0], SPR3[0] });
        wagoneSprites.Add("Food", new Sprite[] { SPR0[0], SPR1[0], SPR2[0], SPR3[0] });
        wagoneSprites.Add("Water", new Sprite[] { SPR0[1], SPR1[1], SPR2[1], SPR3[1] });
        wagoneSprites.Add("Pass", new Sprite[] { SPR0[2], SPR1[2], SPR2[2], SPR3[2] });
        wagoneSprites.Add("Boiler", new Sprite[] { SPR0[3], SPR1[3], SPR2[3], SPR3[3] });
        wagoneSprites.Add("Storage", new Sprite[] { SPR0[0], SPR1[0], SPR2[0], SPR3[0] });
        wagoneSprites.Add("PassWagon", new Sprite[] { SPR0[4], SPR1[4], SPR2[4], SPR3[4] });
        wagoneSprites.Add("Cargo", new Sprite[] { SPR0[8], SPR1[8] });
        LocoSprites.Add(1, new Sprite[] { SPR0[5], SPR1[5], SPR2[5], SPR3[5] });
        LocoSprites.Add(2, new Sprite[] { SPR0[6], SPR1[6], SPR2[6], SPR3[6] });
        LocoSprites.Add(3, new Sprite[] { SPR0[7], SPR1[7], SPR2[7], SPR3[7] });
        ActiveWagone();
        ViewLoco();
        MaxStorageWagone();
        LevelLocoUpdater();
        TextLoco();
        ResourceTextUpdate();
        StartCoroutine(TimerInGame());
        StartCoroutine("PassFoodNeed");
        StartCoroutine("PassFoodWater");
    }

    public void ActiveWagone()
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
        if (passWagoneCount > 0)
        {
            for (int i = 0; i < passWagoneCount; i++)
            {
                passWagon[i].SetActive(true);
                passWagon[i].GetComponent<Image>().sprite = wagoneSprites["PassWagon"][TextureTrain];
            }
        }
        if (CargoTransportCount >= 1) // Если активно грузовое задание
        {
            for (int i = 0; i < CargoTransportCount; i++)
            {
                CargoTransportWagone[i].SetActive(true);
                CargoTransportWagone[i].GetComponent<Image>().sprite = wagoneSprites["Cargo"][TextureTrain];
            }
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
        if (AirShipActive == true)
        {
            AirShipObj.SetActive(true);
            if (TextureTrain == 0)
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

    public void StartNextStation()
    {
        StartCoroutine(TimerNextStation());
        AO.PlayAudioStation();
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

    public void UpdateLoco(int index)
    {
        if(index == 0 && Money >= LevelCostEngine)
        {
            Money -= LevelCostEngine;
            LevelEngine++;
            PlusResources(0, 0 - LevelCostEngine);
        }
        
        else if(index == 1 && Money >= LevelCostChassis)
        {
            Money -= LevelCostChassis;
            LevelChassis++;
            PlusResources(0, 0 - LevelCostChassis);
        }
        
        else if(index == 2 && Money >= LevelCostCoalStorage)
        {
            Money -= LevelCostCoalStorage;
            LevelCoalStorage++;
            PlusResources(0, 0 - LevelCostCoalStorage);
            if (TaskCount == 0)
            {
                TaskCount = 1;
                TaskCounter();
            }
        }

        LevelLocoUpdater();
        TextLoco();
        ResourceTextUpdate();
        AO.PlayAudioClickTrain();
    }


    private int[,] coalNeedEngine = new int[,]
    {
        { 100, 90, 80, 78, 75 },
        { 140, 100, 80, 75, 70 },
        { 120, 100, 80, 72, 60 },
    };
    
    private int[,] maxWagonLoco = new int[,]
    {
        { 5, 7, 8, 10, 12 },
        { 15, 18, 21, 27, 30 },
        { 30, 34, 38, 44, 50 },
    };

    private int[,] storageCoalLoco = new int[,]
    {
        { 500, 650, 850, 1000, 1200 },
        { 1500, 1800, 2100, 2500, 2800 },
        { 3000, 3400, 3800, 4400, 5000 },
    };
    public void LevelLocoUpdater() // Уровень Локомотива, данные
    {
        NeedCoal = coalNeedEngine[LevelLoco, LevelEngine];
        LevelCostEngine = 500 * LevelLoco * LevelEngine;
        
        if (LevelEngine < 5) LevelEngineText.text = "Улучшение " + LevelEngine + ". Стоимость: " + LevelCostEngine + "р -потребление угля";
        else LevelEngineText.text = "Максимальный уровень Котла";


        MaxWagone = maxWagonLoco[LevelLoco, LevelChassis];
        LevelCostChassis = 500 * LevelLoco * LevelChassis;

        if (LevelChassis < 5) LevelChassisText.text = "Улучшение " + LevelChassis + ". Стоимость: " + LevelCostChassis + "р +Мах количество вагонов";
        else LevelChassisText.text = "Максимальный уровень Шасси";


        CoalMax = storageCoalLoco[LevelLoco, LevelCoalStorage];
        LevelCostCoalStorage = 500 * LevelLoco * LevelCoalStorage;

        if(LevelCoalStorage < 5) LevelCoalStorageText.text = "Улучшение " + LevelCoalStorage + ". Стоимость: " + LevelCostCoalStorage + "р +хранилище угля";
        else LevelCoalStorageText.text = "Максимальный уровень Тендера";
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
        AO.PlayAudioClickTrain();
    }
    public void OpenClosedLocoUpgradePan() // Открытие панели улучшения локомотива
    {
        if(!UpgradeLocoPan.activeInHierarchy) UpgradeLocoPan.SetActive(true);
        else UpgradeLocoPan.SetActive(false);
        AO.PlayAudioClickTrain();
    }
    public void ResourceTextUpdate() // Обновление текстов и слайдера ресурсов 
    {
        MoneyText.text = Money + "р";
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
        int timeBackground = 0; // таймер скорости фона
        int timeADS = 0; // таймер денег за рекламу
        int timeBarrier = UnityEngine.Random.Range(20, 600); // Таймер барьер
        int timeRepair = UnityEngine.Random.Range(20, 600); //  Таймер ремонта поезда
        NextStationSlide.maxValue = NextStationTimeCount;
        ParticleTrain.SetActive(false); ParticleTrain.SetActive(true);
        AO.PlayAudioTrain();
        
        while (true)
        {
            timeBarrier--;
            timeRepair--;
            NextStationTime--;
            DistancePlusStatistic++;
            NextStationTimeText.text = (NextStationTime * 100) + " дист.";
            NextStationSlide.value = NextStationTimeCount - NextStationTime;
            
            if (timeBarrier <= 0)
            {
                AO.StopAudio();
                SpeedFon = 0;
                StartMessage("Препятствие на пути!");
                barrierObj.SetActive(true);
                if (!helpBarrier)
                {
                    AO.PlayAudioEnterPanel();
                    helpBarrierObj.SetActive(true);
                }
                break;
            }
            
            if (timeRepair <= 0)
            {
                AO.StopAudio();
                SpeedFon = 0;
                StartMessage("Поезд сломался!");
                repairTrainObj.SetActive(true);
                if (!helpRepair)
                {
                    AO.PlayAudioEnterPanel();
                    helpTrainRepairObj.SetActive(true);
                }
                break;
            }
            
            if (NextStationTime <= 0)
            {
                StopCoroutine("PassFoodNeed");
                StopCoroutine("PassFoodWater");
                AO.StopAudio();
                AO.PlayAudioEnterStation();
                Station.SetActive(true);
                for (int i = 0; i < WagonCol; i++)
                {
                    WagoneData[i].WagoneObj.SetActive(false);
                    WagoneData[i].WagoneActive = false;
                }
                SmokeParticle.SetActive(false);
                GamePan.SetActive(false);
                if (YandexGame.EnvironmentData.deviceType == "mobile" & City == 0 || City == 1) YandexGame.StickyAdActivity(false);
                YandexGame.FullscreenShow();
                break;
            }
            
            else if(NextStationTime > 0 & Coal >= NeedCoal) //
            {
                ActiveTimerLoco--;
                timeBackground++;
                timeADS++;
                if(ActiveTimerLoco <= 0) //
                {
                    Coal -= NeedCoal;
                    PlusResources(1, 0- NeedCoal);
                    ResourceTextUpdate();
                    TextLoco();
                    ActiveTimerLoco = TimerLoco;
                }
                if (timeBackground >= 45 & SpeedFon <= 3)
                {
                    SpeedFon++;
                    timeBackground = 0;
                }
                if(timeADS >= 40)
                {
                    if(Money >= 500) ADSMoneyCol = UnityEngine.Random.Range(500, Money);
                    else ADSMoneyCol = 500;
                    ADSMoneyActivePan.SetActive(true);
                    ADSMoneyActiveText.text = "+" + ADSMoneyCol + "р!!!" + "\n" + "За просмотр";
                    StartCoroutine(ADSMoneyActive());
                    timeADS = 0;
                }
                yield return new WaitForSeconds(Timer);
            }
            
            else
            {
                AO.StopAudio();
                SpeedFon = 0;
                if (!CoalHelp)
                {
                    AO.PlayAudioEnterPanel();
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

    IEnumerator PassFoodNeed() // Корутина пищи людей
    {
        int NeedFoodPeople = 0;
        int CicleNeedFood = 0;
        while (true)
        {
            if ((AllWorker + PassResourceUse[0]) <= Food)
            {
                Food -= AllWorker + PassResourceUse[0];
                ResourceTextUpdate();
                PlusResources(2, 0 - (AllWorker + PassResourceUse[0]));
                NeedFoodPeople = 0;
                CicleNeedFood = 0;
                yield return new WaitForSeconds(10);
            }
            else if ((AllWorker + PassResourceUse[0]) > Food)
            {
                StartMessage("Людям не хватает пищи!");
                NeedFoodPeople = (AllWorker + PassResourceUse[0]) - Food;
                CicleNeedFood++;
                PlusResources(2, 0 - Food);
                Food = 0;
                ResourceTextUpdate();
                if (CicleNeedFood >= 4) // Если 4 циклов и больше люди без еды
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
            if ((AllWorker + PassResourceUse[0]) <= Water)
            {
                Water -= AllWorker + PassResourceUse[1];
                ResourceTextUpdate();
                PlusResources(3, 0 - (AllWorker + PassResourceUse[1]));
                NeedFoodPeople = 0;
                CicleNeedFood = 0;
                yield return new WaitForSeconds(10);
            }
            else if ((AllWorker + PassResourceUse[1]) > Water)
            {
                StartMessage("Людям не хватает воды!");
                NeedFoodPeople = AllWorker + PassResourceUse[1] - Water;
                CicleNeedFood++;
                PlusResources(3, 0 - Water);
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
        LocoSprite.sprite = LocoSprites[LevelLoco][TextureTrain];
    }
    public void UpdateViewTrain() // Обновление вида вагонов
    {
        for (int i = 0; i < WagonCol; i++)
        {
            WagoneData[i].WagoneImage = wagoneSprites[WagoneData[i].Name][TextureTrain];
        }
    }
    public void ChoiceViewTrain(int index) // Выбор вида поезда!
    {
        TextureTrain = index;
        ViewLoco();
        UpdateViewTrain();
    }

    public void RandomBackground() //
    {
        int r = UnityEngine.Random.Range(0, ParallaxObj.Length);
        for(int i = 0; i < ParallaxObj.Length; i++) { ParallaxObj[i].SetActive(false); }
        ParallaxObj[r].SetActive(true);    
    }

    // HELP!!!
    public void OpenClosedHelpPan()
    {
        if (!HelpPan.activeInHierarchy)
        {
            HelpPan.SetActive(true);
            int H = TimeInGameStatistic / 60;
            int M = TimeInGameStatistic - (H * 60);
            TimeInGameStatisticText.text = "Время в игре:" + H + "ч. " + M + "м.";
            Time.timeScale = 0;
        }
        else
        {
            HelpPan.SetActive(false);
            Time.timeScale = 1;
        }
        AO.PlayAudioClickBttn();
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
        AO.PlayAudioClickBttn();
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
        else if (id == 1)
        {
            ADSCoal();
        }
        else if (id == 2)
        {
            StartCoroutine(TimerX2());
            TimerX2Pan.SetActive(false);
            if (TaskCount == 8)
            {
                TaskCount = 9;
                TaskCounter();
            }
        }
        else if (id == 3)
        {
            Diamond++;
            ResourceTextUpdate();
        }
    }

    public void OpenClosedShopPan() // Открытие панели магазина
    {
        if (!ShopPan.activeInHierarchy)
        {
            ShopPan.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            ShopPan.SetActive(false);
            Time.timeScale = 1;
        }
        AO.PlayAudioClickBttn();
    }

    public void ExchangeDiamond() // Обмен Роскоши на деньги
    {
        if (Diamond >= 1)
        {
            Money += 1500;
            Diamond--;
            PlusResources(0, 1500);
        }
    }

    public void OpenClosedTiemrX2Pan() // Открытие закрытие панели ускроения времени до станции
    {
        if (TimerX2Pan.activeInHierarchy) TimerX2Pan.SetActive(false);
        else TimerX2Pan.SetActive(true);
        AO.PlayAudioClickBttn();
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
            PlusResources(1, 350);
            ResourceTextUpdate();
        }
        else if (350 >= Razn)
        {
            Coal += Razn;
            PlusResources(1, 350);
            ResourceTextUpdate();
        }
        AO.PlayAudioTakeResource();
        CollectResources[3].SetActive(false); CollectResources[03].SetActive(true);
    }

    public void TakeADSMoney() // Взять деньги за рекламу
    {
        Money += ADSMoneyCol;
        PlusResources(0, ADSMoneyCol);
        ADSMoneyActivePan.SetActive(false);
        ResourceTextUpdate();
        AO.PlayAudioTakeResource();
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
    public void StartPlusResource(int index, int value)
    {
        StartCoroutine(PlusResources(index, value));
    }

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

    // AIRSHIPS
    public void ClickAirShipPan() // Открытие и закрытие панели локомотива
    {
        if(!AirShipPan.activeInHierarchy) AirShipPan.SetActive(true);
        else AirShipPan.SetActive(false);
        AO.PlayAudioClickTrain();
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

    // MENU!!!

    public void ReturnTravel()
    {
        AO.PlayAudioClickBttn();
        MenuPan.SetActive(false);
        StartGame();
    }
    //public void OpenClosedSettingPan()
    //{

    //}
    //public void OnOffMusic




    // TASK!!!

    [Serializable]
    public class Tasks
    {
        public int index;
        public string text;
        public int reward;
    }

    public Tasks[] tasks = new Tasks[]
    {
        new Tasks { index = 0, text = "Улучшите тендер Локомотива", reward = 1000},
        new Tasks { index = 2, text = "Соберите любой ресурс", reward = 150},
        new Tasks { index = 4, text = "Выполните задание по доставке", reward = 600},
        new Tasks { index = 6, text = "Улучшите вагон", reward = 1000},
        new Tasks { index = 8, text = "Ускорьте время до станции", reward = 300},
    };

    public void TaskCounter() //
    {
        if (TaskCount < 10)
        {
            if (TaskCount % 2 == 0)
            {
                TaskDone.interactable = false;
                TaskText.text = tasks[TaskCount].text;
            }
            else
            {
                TaskDone.interactable = false;
                TaskText.text = "Собрать награду";
            }
            TaskPan.SetActive(true);
        }
        else TaskPan.SetActive(false);
    }

    public void TaskDoner() //
    {
        Money += tasks[TaskCount - 1].reward;
        TaskCount++;
        TaskCounter();
        AO.PlayAudioTakeResource();
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
