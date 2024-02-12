using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WagonScript : MonoBehaviour
{
    public Game GM; // 
    [Header("Основ.Панели")]
    public GameObject ChoiceWagonePan; // Первая панель выбора назначения вагона
    public GameObject FoodWaterWagonePan; // Панель еда или вода
    public GameObject PassWagonePan; // Панель Пассажирского вагона
    public GameObject StorageWagonePan; // Панель вагона Склада
    public GameObject ProductionWagonePan; // Панель производственного вагона
    public GameObject BoilerWagonePan; // Панель Котельной
    public Text WagoneName; // Название вагона на нем.
    public GameObject DoneProductionPan; // Панель готовности продукции для сбора
    public GameObject SnowWagone; // Иконка замерзания вагона
    [Header("ПанельЕдаВода")]
    public Text FoodWagoneNameText; // Навзание вагона еда или вода
    public Text FoodLevelText; // Уровень вагона
    public Text FoodProdText; // Что производит вагон (еда или вода)
    public Text FoodWorkCountText; // Количество работников в вагоне
    public Text FoodProdCountText; // Сколько продукции и за какое время производит
    public Text FoodTermometrText; // Температура в вагоне
    public Text FoodTimerText; // Таймер производства
    public Text FoodUpgradeText; // Улучшение вагона стоимость
    public Text FoodNeedWarmText; // 
    [Header("ПанельПасс")]
    public Text PassLevelText; // Уровень вагона
    public Text PassMaxCapacityText; // Максимальная вместимость вагона
    public Text PassCapacityText; // Сколько проживает в вагоне
    public Text PassTermometrText; // Температура в вагоне
    public Text PassUpgradeText; // Улучшение вагона стоимость
    public Text PassNeedFoodText; //
    public Text PassNeedWaterText; //
    public Text PassNeedWarmText; //
    [Header("ПанельХранилище")]
    public Text StorageLevelText; // Уровень вагона
    public Text StorageMaxCapacityText; // Максимальная вместимость вагона
    public Text StorageCapacityText; // Сколько на складе товаров
    public Text StorageUpgradeText; // Улучшение вагона стоимость
    [Header("ПанельКотельная")]
    public Text BoilerLevelText; // Уровень вагона
    public Text BoilerWorkCountText; // Количество работников в вагоне
    public Text BoilerProdCountText; // Сколько продукции и за какое время производит
    public Text BoilerTimerText; // Таймер производства
    public Text BoilerUpgradeText; // Улучшение вагона стоимость
    public Text BoilerNeedCoalText; //
    [Header("Вид вагона")]
    public Image WagoneImage; // Вид вагона
    public Sprite BaseWagone; // Стартовый вид вагона
    
    int PanOpenClosed; // Переменная открытия/закрытия панели при нажатии на вагон
    int Worker;
    [Header("ТаймерСледующейСтанции")]
    int IndexWag; // Индекс вагона
    int TimerWag; // Таймер для производства (Сколько времени необходимо)
    int ProductCount; // Количество единиц производства


    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        for (int i = 0; i < GM.WagoneData.Capacity; i++) // Проверка по длине списка данных поезда
        {
            if (GM.WagoneData[i].WagoneActive == 0) // Если вагон не активен, то резервируем
            {
                IndexWag = i;
                GM.WagoneData[IndexWag].WagoneActive = 1;
                if (GM.WagoneData[IndexWag].Name != "") // Если имя не пусто, то просчет данных
                {
                    WagoneDataCount();
                    WagoneWorkData();
                    if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water")
                    {
                        StartCoroutine(FoodWorkWagone()); // Запуск корутины Производства еды/воды
                        StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагона
                    }
                    if (GM.WagoneData[IndexWag].Name == "Boiler")
                    {
                        StartCoroutine(BoilerWorkWagone()); // Запуск корутины производства тепла
                    }
                    if (GM.WagoneData[IndexWag].Name == "Pass")
                    {
                        StartCoroutine(PassFoodNeed()); // Запуск корутины потребления  еды.
                        StartCoroutine(PassWaterNeed()); // Запуск корутины потребления  воды.
                        StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагона
                    }
                    ViewWagone();
                }
                if (GM.WagoneData[IndexWag].Name == "")
                {
                    ViewWagone();
                    WagoneName.text = "Пустой";
                }
                break;
            }
        }
    }
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClickOnWagone() // Клик по вагону и открытие панелей
    {
        if(GM.WagoneData[IndexWag].Name == "") // Если вагон пуст
        {
            if (PanOpenClosed == 1)
            {
                ChoiceWagonePan.SetActive(false);
                PanOpenClosed = 0;
            }
            else
            {
                ChoiceWagonePan.SetActive(true);
                PanOpenClosed = 1;
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Food") // Если вагон еда\вода
        {
            if (PanOpenClosed == 1)
            {
                FoodWaterWagonePan.SetActive(false);
                PanOpenClosed = 0;
            }
            else
            {
                FoodWaterWagonePan.SetActive(true);
                PanOpenClosed = 1;
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Water") // Если вагон еда\вода
        {
            if (PanOpenClosed == 1)
            {
                FoodWaterWagonePan.SetActive(false);
                PanOpenClosed = 0;
            }
            else
            {
                FoodWaterWagonePan.SetActive(true);
                PanOpenClosed = 1;
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Pass") // Если вагон Пассажир
        {
            if (PanOpenClosed == 1)
            {
                PassWagonePan.SetActive(false);
                PanOpenClosed = 0;
            }
            else
            {
                PassWagonePan.SetActive(true);
                PanOpenClosed = 1;
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Storage") // Если вагон грузовой
        {
            if (PanOpenClosed == 1)
            {
                StorageWagonePan.SetActive(false);
                PanOpenClosed = 0;
            }
            else
            {
                StorageWagonePan.SetActive(true);
                PanOpenClosed = 1;
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Production") // Если вагон Производств
        {
            if (PanOpenClosed == 1)
            {
                ProductionWagonePan.SetActive(false);
                PanOpenClosed = 0;
            }
            else
            {
                ProductionWagonePan.SetActive(true);
                PanOpenClosed = 1;
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Boiler") // Если вагон котельная
        {
            if (PanOpenClosed == 1)
            {
                BoilerWagonePan.SetActive(false);
                PanOpenClosed = 0;
            }
            else
            {
                BoilerWagonePan.SetActive(true);
                PanOpenClosed = 1;
            }
        }
    }

    // Выбор назначения вагона!
    public void ChoiceFoodWagone() // Вагон еда
    {
        GM.WagoneData[IndexWag].Name = "Food";
        GM.WagoneData[IndexWag].WagoneActive = 1;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        GM.WagoneData[IndexWag].LevelWagone = 1;
        WagoneDataCount();
        WagoneWorkData();
        StartCoroutine(FoodWorkWagone()); // Запуск корутины Производства еды/воды
        StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагона
        PanOpenClosed = 0;
        ChoiceWagonePan.SetActive(false);
        if(GM.Trainer[0] == false)
        {
            GM.WagoneTrainerChoice[1] = true;
            GM.Trainer2();
        }
    }
    public void ChoiceWaterWagone() // Вагон Вода
    {
        GM.WagoneData[IndexWag].Name = "Water";
        GM.WagoneData[IndexWag].WagoneActive = 1;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        GM.WagoneData[IndexWag].LevelWagone = 1;
        WagoneDataCount();
        WagoneWorkData();
        StartCoroutine(FoodWorkWagone()); // Запуск корутины Производства еды/воды
        StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагона
        PanOpenClosed = 0;
        ChoiceWagonePan.SetActive(false);
        if (GM.Trainer[0] == false)
        {
            GM.WagoneTrainerChoice[2] = true;
            GM.Trainer2();
        }
    }
    public void ChoicePassWagone() // Вагон Пассажир
    {
        GM.WagoneData[IndexWag].Name = "Pass";
        GM.WagoneData[IndexWag].WagoneActive = 1;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        GM.WagoneData[IndexWag].LevelWagone = 1;
        GM.MaxWorkerWagone();
        GM.PostWagonePeople();
        WagoneDataCount();
        WagoneWorkData();
        StartCoroutine(PassFoodNeed()); // Запуск корутины потребления  еды.
        StartCoroutine(PassWaterNeed()); // Запуск корутины потребления  воды.
        StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагона
        PanOpenClosed = 0;
        ChoiceWagonePan.SetActive(false);
        if (GM.Trainer[0] == false)
        {
            GM.WagoneTrainerChoice[0] = true;
            GM.Trainer2();
        }
    }
    public void ChoiceStorageWagone() // Вагон грузовой
    {
        GM.WagoneData[IndexWag].Name = "Storage";
        GM.WagoneData[IndexWag].WagoneActive = 1;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        GM.WagoneData[IndexWag].LevelWagone = 1;
        PanOpenClosed = 0;
        GM.MaxStorageWagone();
        WagoneDataCount();
        WagoneWorkData();
        ChoiceWagonePan.SetActive(false);
    }
    public void ChoiceProductionWagone() // Вагон Производство
    {
        GM.WagoneData[IndexWag].Name = "Production";
        GM.WagoneData[IndexWag].WagoneActive = 1;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        GM.WagoneData[IndexWag].LevelWagone = 1;
        PanOpenClosed = 0;
        ChoiceWagonePan.SetActive(false);
    }
    public void ChoiceBoilerWagone() // Вагон Котельная
    {
        GM.WagoneData[IndexWag].Name = "Boiler";
        GM.WagoneData[IndexWag].WagoneActive = 1;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        GM.WagoneData[IndexWag].LevelWagone = 1;
        WagoneDataCount();
        WagoneWorkData();
        StartCoroutine(BoilerWorkWagone());
        PanOpenClosed = 0;
        ChoiceWagonePan.SetActive(false);
        if (GM.Trainer[0] == false)
        {
            GM.WagoneTrainerChoice[3] = true;
            GM.Trainer2();
        }
    }

    // Добавление и удаление рабочих в вагоне производств
    public void AddWork() // Добавление рабочих от уровня
    {
        if(GM.WagoneData[IndexWag].LevelWagone == 1)
        {
            if (GM.WagoneData[IndexWag].WorkerInWagone < 5 & GM.Worker >=1)
            {
                GM.WagoneData[IndexWag].WorkerInWagone++;
                GM.Worker--;
                GM.People++;
                WagoneDataCount();
                WagoneWorkData();
                GM.ResourceTextUpdate();
            }
        }
        if (GM.WagoneData[IndexWag].LevelWagone == 2)
        {
            if (GM.WagoneData[IndexWag].WorkerInWagone < 10 & GM.Worker >= 1)
            {
                GM.WagoneData[IndexWag].WorkerInWagone++;
                GM.Worker--;
                GM.People++;
                WagoneDataCount();
                WagoneWorkData();
                GM.ResourceTextUpdate();
            }
        }
        if (GM.WagoneData[IndexWag].LevelWagone == 3)
        {
            if (GM.WagoneData[IndexWag].WorkerInWagone < 15 & GM.Worker >= 1)
            {
                GM.WagoneData[IndexWag].WorkerInWagone++;
                GM.Worker--;
                GM.People++;
                WagoneDataCount();
                WagoneWorkData();
                GM.ResourceTextUpdate();
            }
        }
    }
    public void RemoveWork() // Удаление рабочих в вагоне от 
    {
        if (GM.WagoneData[IndexWag].LevelWagone == 1 || GM.WagoneData[IndexWag].LevelWagone == 2 || GM.WagoneData[IndexWag].LevelWagone == 3)
        {
            if (GM.WagoneData[IndexWag].WorkerInWagone > 0)
            {
                GM.WagoneData[IndexWag].WorkerInWagone--;
                GM.Worker++;
                GM.People--;
                WagoneDataCount();
                WagoneWorkData();
                GM.ResourceTextUpdate();
            }
        }
    }

    //
    public void CollectProduct()
    {
        int Razn = 0;
        GM.EffectCollect.SetActive(false);
        GM.EffectCollect.transform.position = DoneProductionPan.transform.position;
        GM.EffectCollect.SetActive(true);
        if(GM.WagoneData[IndexWag].Name == "Food")
        {
            Razn = GM.FoodMax - GM.Food;
            if (ProductCount < Razn)
            {
                GM.Food += ProductCount;
                GM.FoodPlusStatistic += ProductCount;
                GM.ResourceTextUpdate();
                GM.StartPlusFood(ProductCount);
                GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag;
                DoneProductionPan.SetActive(false);
                StartCoroutine(FoodWorkWagone());
            }
            if ( ProductCount >= Razn)
            {
                GM.Food += Razn;
                GM.FoodPlusStatistic += Razn;
                GM.ResourceTextUpdate();
                GM.StartPlusFood(Razn);
                GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag;
                DoneProductionPan.SetActive(false);
                StartCoroutine(FoodWorkWagone());
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Water")
        {
            Razn = GM.WaterMax - GM.Water;
            if (ProductCount < Razn)
            {
                GM.Water += ProductCount;
                GM.WaterPlusStatistic += ProductCount;
                GM.ResourceTextUpdate();
                GM.StartPlusWater(ProductCount);
                GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag;
                DoneProductionPan.SetActive(false);
                StartCoroutine(FoodWorkWagone());
            }
            if (ProductCount >= Razn)
            {
                GM.Water += Razn;
                GM.WaterPlusStatistic += Razn;
                GM.ResourceTextUpdate();
                GM.StartPlusWater(Razn);
                GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag;
                DoneProductionPan.SetActive(false);
                StartCoroutine(FoodWorkWagone());
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Boiler")
        {
            Razn = GM.WarmMax - GM.Warm;
            if (ProductCount < Razn)
            {
                GM.Warm += ProductCount;
                GM.WarmPlusStatistic += ProductCount;
                GM.ResourceTextUpdate();
                GM.StartPlusWarm(ProductCount);
                GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag;
                DoneProductionPan.SetActive(false);
                StartCoroutine(BoilerWorkWagone());
            }
            if (ProductCount >= Razn)
            {
                GM.Warm += Razn;
                GM.WarmPlusStatistic += Razn;
                GM.ResourceTextUpdate();
                GM.StartPlusWarm(Razn);
                GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag;
                DoneProductionPan.SetActive(false);
                StartCoroutine(BoilerWorkWagone());
            }
        }
        if(GM.TaskCount == 2)
        {
            GM.TaskCount = 3;
            GM.TaskCounter();
        }
    }
    void WagoneDataCount() // Ввод данных вагона
    {
        Worker = GM.WagoneData[IndexWag].WorkerInWagone;
        if(GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water") //
        {
            WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
            FoodLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            FoodWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
            FoodTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
            if (GM.TemperatureOnStreet == 0) { FoodNeedWarmText.text = "-10"; }
            if (GM.TemperatureOnStreet == -10) { FoodNeedWarmText.text = "-20"; }
            if (GM.TemperatureOnStreet == -20) { FoodNeedWarmText.text = "-30"; }
            if (GM.WagoneData[IndexWag].Name == "Food")
            {
                FoodProdText.text = "Производство пищи:";
                WagoneName.text = "Пища";
                FoodWagoneNameText.text = "Пища";
            }
            if (GM.WagoneData[IndexWag].Name == "Water")
            {
                FoodProdText.text = "Производство воды:";
                WagoneName.text = "Вода";
                FoodWagoneNameText.text = "Вода";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Pass") //
        {
            WagoneName.text = "Пассажирский";
            WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
            PassLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            PassCapacityText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
            if (GM.TemperatureOnStreet == 0) { PassNeedWarmText.text = "-10"; }
            if (GM.TemperatureOnStreet == -10) { PassNeedWarmText.text = "-20"; }
            if (GM.TemperatureOnStreet == -20) { PassNeedWarmText.text = "-30"; }
            PassNeedFoodText.text = "-" + GM.WagoneData[IndexWag].WorkerInWagone;
            PassNeedWaterText.text = "-" + GM.WagoneData[IndexWag].WorkerInWagone;
        }
        if (GM.WagoneData[IndexWag].Name == "Boiler") //
        {
            WagoneName.text = "Котельная";
            WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
            BoilerLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            BoilerWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            BoilerTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
            BoilerNeedCoalText.text = "-" + GM.WagoneData[IndexWag].WorkerInWagone * 20;
        }
        if (GM.WagoneData[IndexWag].Name == "Storage") //
        {
            WagoneName.text = "Склад";
            WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
            StorageLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
        }
    }

    void WagoneWorkData() // Просчет данных для производства вагона от уровня и кол-ва работников
    {
        if (GM.WagoneData[IndexWag].Name == "Food")
        {
            if(GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 30;
                FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
                if(GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 50;
                FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "4000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 80;
                FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "Максимум";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Water")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 30;
                FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 40;
                FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "4000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 70;
                FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "Максимум";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Boiler")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 20;
                BoilerProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                BoilerUpgradeText.text = "1500$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 40;
                BoilerProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                BoilerUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 60;
                BoilerProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                BoilerUpgradeText.text = "4000$";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Pass")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = 10; // Максимальная вместимость в вагон от уровня
                PassMaxCapacityText.text = ProductCount + " Человек";
                PassUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = 15; // Максимальная вместимость в вагон от уровня
                PassMaxCapacityText.text = ProductCount + " Человек";
                PassUpgradeText.text = "4000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = 20; // Максимальная вместимость в вагон от уровня
                PassMaxCapacityText.text = ProductCount + " Человек";
                PassUpgradeText.text = "Максимум";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Storage")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = 50; // Максимальная вместимость в вагон от уровня
                StorageMaxCapacityText.text = ProductCount + " Ресурсов";
                StorageUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = 75; // Максимальная вместимость в вагон от уровня
                StorageMaxCapacityText.text = ProductCount + " Ресурсов";
                StorageUpgradeText.text = "4000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = 100; // Максимальная вместимость в вагон от уровня
                StorageMaxCapacityText.text = ProductCount + " Ресурсов";
                StorageUpgradeText.text = "Максимум";
            }
        }
    }

    //void FoodNeedTemperature() // Увеличение/уменьшение произвосдства продукции от температуры
    //{
    //    if(GM.WagoneData[IndexWag].TemperatureWagone >=0 & GM.WagoneData[IndexWag].TemperatureWagone < 10)
    //    {
    //        ProductCount -= (ProductCount / 100 * 30);
    //        FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек.";
    //    }
    //    if (GM.WagoneData[IndexWag].TemperatureWagone >= 10 & GM.WagoneData[IndexWag].TemperatureWagone < 20)
    //    {
    //        FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек.";
    //    }
    //    if (GM.WagoneData[IndexWag].TemperatureWagone >= 20)
    //    {
    //        ProductCount += (ProductCount / 100 * 30);
    //        FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек.";
    //    }
    //}

    IEnumerator FoodWorkWagone() // Корутина производства вагона (Еда\вода)
    {
        while (true)
        {
            if(GM.WagoneData[IndexWag].WorkerInWagone >= 1 & GM.WagoneData[IndexWag].TemperatureWagone >= 0) // Если 1 и больше активных работников и температура выше 0
            {
                GM.WagoneData[IndexWag].TimerActiveProduction--;
                FoodTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) // Если активный таймер 0,
                {
                    DoneProductionPan.SetActive(true); // Активация панели сбора награды
                    break;
                }
                if(GM.WagoneData[IndexWag].TimerActiveProduction > 0) // Если активный таймер работает
                {
                    //GM.WagoneData[IndexWag].TimerActiveProduction--;
                    WagoneWorkData();
                    //FoodNeedTemperature(); // Увеличение/уменьшение произвосдства продукции от температуры
                    yield return new WaitForSeconds(1);
                }
            }
            else
            {
                WagoneWorkData();
                yield return new WaitForSeconds(1);
            }
        }
    }
    IEnumerator BoilerWorkWagone() // Корутина производства Тепла котельной 
    {
        while (true)
        {
            if(GM.WagoneData[IndexWag].WorkerInWagone >= 1 & GM.Coal >= ProductCount)
            {
                GM.WagoneData[IndexWag].TimerActiveProduction--;
                BoilerTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) // Если активный таймер 0,
                {
                    GM.Coal -= ProductCount;
                    GM.ResourceTextUpdate();
                    DoneProductionPan.SetActive(true); // Активация панели сбора награды
                    break;
                }
                if (GM.WagoneData[IndexWag].TimerActiveProduction > 0) // Если активный таймер работает
                {
                    //GM.WagoneData[IndexWag].TimerActiveProduction--;
                    WagoneWorkData();
                    yield return new WaitForSeconds(1);
                }
            }
            else
            {
                WagoneWorkData();
                yield return new WaitForSeconds(1);
            }
        }
    }
    IEnumerator PassFoodNeed() // Корутина пищи людей
    {
        int NeedFoodPeople = 0;
        int CicleNeedFood = 0;
        while (true)
        {
            if(GM.WagoneData[IndexWag].WorkerInWagone <= GM.Food)
            {
                GM.Food -= GM.WagoneData[IndexWag].WorkerInWagone;
                GM.ResourceTextUpdate();
                GM.StartPlusFood(0 - GM.WagoneData[IndexWag].WorkerInWagone);
                NeedFoodPeople = 0;
                CicleNeedFood = 0;
                yield return new WaitForSeconds(10);
            }
            if (GM.WagoneData[IndexWag].WorkerInWagone > GM.Food)
            {
                GM.StartMessage("Людям не хватает пищи!");
                NeedFoodPeople = GM.WagoneData[IndexWag].WorkerInWagone - GM.Food;
                CicleNeedFood++;
                GM.StartPlusFood(0 - GM.Food);
                GM.Food = 0;
                GM.ResourceTextUpdate();
                if (CicleNeedFood >= 7) // Если 7 циклов и больше люди без еды
                {
                    GM.AllPeople -= NeedFoodPeople; // Отнимаем от всех проживающих людей, количество недоедающих
                    if(GM.WagoneData[IndexWag].WorkerInWagone >= NeedFoodPeople)
                    {
                        GM.WagoneData[IndexWag].WorkerInWagone -= NeedFoodPeople;
                        if(GM.Worker >= NeedFoodPeople)
                        {
                            GM.Worker -= NeedFoodPeople;
                            NeedFoodPeople = 0;
                            GM.ResourceTextUpdate();
                        }
                        if(GM.Worker < NeedFoodPeople)
                        {
                            NeedFoodPeople -= GM.Worker;
                            GM.Worker = 0;
                            GM.ResourceTextUpdate();
                            GM.CountPeopleTrain1();
                        }
                    }
                    //GM.CountPeopleTrain(NeedFoodPeople, NeedFoodPeople); // Просчет количества свободных работников в поезде и всего людей
                    CicleNeedFood = 0;
                    GM.PostWagonePeople();
                }
                yield return new WaitForSeconds(10);
            }
        }
    }
    IEnumerator PassWaterNeed() // Корутина воды людей
    {
        //int NeedFoodPeople = 0;
        //int CicleNeedFood = 0;
        while (true)
        {
            if (GM.WagoneData[IndexWag].WorkerInWagone <= GM.Water)
            {
                GM.Water -= GM.WagoneData[IndexWag].WorkerInWagone;
                GM.ResourceTextUpdate();
                GM.StartPlusWater(0 - GM.WagoneData[IndexWag].WorkerInWagone);
                //NeedFoodPeople = 0;
                //CicleNeedFood = 0;
                yield return new WaitForSeconds(10);
            }
            if (GM.WagoneData[IndexWag].WorkerInWagone > GM.Water)
            {
                GM.StartMessage("Людям не хватает воды!");
                //NeedFoodPeople = GM.WagoneData[IndexWag].WorkerInWagone - GM.Water;
                //CicleNeedFood++;
                GM.StartPlusWater(0 - GM.Water);
                GM.Water = 0;
                GM.ResourceTextUpdate();
                //if (CicleNeedFood >= 3)
                //{
                //    GM.People -= NeedFoodPeople;
                //    GM.CountPeopleTrain(NeedFoodPeople, NeedFoodPeople);
                //}
                yield return new WaitForSeconds(10);
            }
        }
    }
    IEnumerator WarmInWagone() // Потребление Тепла вагона
    {
        while (true)
        {
            if (GM.TemperatureOnStreet == 0)
            {
                if(GM.Warm >= 10)
                {
                    GM.Warm -= 10;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 10);
                    GM.WagoneData[IndexWag].TemperatureWagone = 10;
                    SnowWagone.SetActive(false);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if(GM.Warm < 10)
                {
                    SnowWagone.SetActive(false);
                    FoodTermometrText.text = GM.TemperatureOnStreet + "°C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "°C";
                    yield return new WaitForSeconds(10);
                }
            }
            if (GM.TemperatureOnStreet == -10)
            {
                if (GM.Warm >= 20)
                {
                    GM.Warm -= 20;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 20);
                    GM.WagoneData[IndexWag].TemperatureWagone = 10;
                    SnowWagone.SetActive(false);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm>=10 & GM.Warm < 20)
                {
                    GM.Warm -= 10;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 10);
                    GM.WagoneData[IndexWag].TemperatureWagone = 0;
                    SnowWagone.SetActive(false);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if(GM.Warm < 10)
                {
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    FoodTermometrText.text = GM.TemperatureOnStreet + "°C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "°C";
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    GM.StartMessage("Вагон замерзает!");
                    SnowWagone.SetActive(true);
                    yield return new WaitForSeconds(10);
                }
            }
            if (GM.TemperatureOnStreet == -20)
            {
                if (GM.Warm >= 30)
                {
                    GM.Warm -= 30;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 30);
                    SnowWagone.SetActive(false);
                    GM.WagoneData[IndexWag].TemperatureWagone = 10;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 20 & GM.Warm < 30)
                {
                    GM.Warm -= 20;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 20);
                    SnowWagone.SetActive(false);
                    GM.WagoneData[IndexWag].TemperatureWagone = 0;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 10 & GM.Warm < 20)
                {
                    GM.Warm -= 10;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 10);
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    GM.StartMessage("Вагон замерзает!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if(GM.Warm < 10)
                {
                    GM.WagoneData[IndexWag].TemperatureWagone = -20;
                    GM.StartMessage("Вагон замерзает!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.TemperatureOnStreet + "°C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "°C";
                    yield return new WaitForSeconds(10);
                }
            }
            if (GM.TemperatureOnStreet == -30)
            {
                if (GM.Warm >= 40)
                {
                    GM.Warm -= 40;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 40);
                    SnowWagone.SetActive(false);
                    GM.WagoneData[IndexWag].TemperatureWagone = 10;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 30 & GM.Warm < 40)
                {
                    GM.Warm -= 30;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 30);
                    SnowWagone.SetActive(false);
                    GM.WagoneData[IndexWag].TemperatureWagone = 0;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 20 & GM.Warm < 30)
                {
                    GM.Warm -= 20;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 20);
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    GM.StartMessage("Вагон замерзает!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm < 10)
                {
                    GM.WagoneData[IndexWag].TemperatureWagone = -30;
                    GM.StartMessage("Вагон замерзает!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.TemperatureOnStreet + "°C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "°C";
                    yield return new WaitForSeconds(10);
                }
            }
            if (GM.TemperatureOnStreet == -40)
            {
                if (GM.Warm >= 50)
                {
                    GM.Warm -= 50;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 50);
                    SnowWagone.SetActive(false);
                    GM.WagoneData[IndexWag].TemperatureWagone = 10;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 35 & GM.Warm < 50)
                {
                    GM.Warm -= 35;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 35);
                    SnowWagone.SetActive(false);
                    GM.WagoneData[IndexWag].TemperatureWagone = 0;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 20 & GM.Warm < 35)
                {
                    GM.Warm -= 20;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 20);
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    GM.StartMessage("Вагон замерзает!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm < 10)
                {
                    GM.WagoneData[IndexWag].TemperatureWagone = -40;
                    GM.StartMessage("Вагон замерзает!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.TemperatureOnStreet + "°C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "°C";
                    yield return new WaitForSeconds(10);
                }
            }
        }
    }

    public void ResetWagone() // Сброс вагона до исходных.
    {
        ClickOnWagone();
        if (GM.WagoneData[IndexWag].Name == "Pass")
        {
            GM.DontPeopleWagone += GM.WagoneData[IndexWag].WorkerInWagone;
            GM.WagoneData[IndexWag].WorkerInWagone = 0;
            GM.WagoneData[IndexWag].Name = "";
            GM.MaxWorkerWagone();
            GM.PostWagonePeople();
            GM.ResourceTextUpdate();
        }
        if(GM.WagoneData[IndexWag].Name == "Storage")
        {
            GM.WagoneData[IndexWag].Name = "";
            GM.MaxStorageWagone();
        }
        GM.WagoneData[IndexWag].Name = "";
        GM.WagoneData[IndexWag].LevelWagone = 0;
        GM.WagoneData[IndexWag].TimerActiveProduction = 0;
        GM.WagoneData[IndexWag].TemperatureWagone = 0;
        ViewWagone();
        WagoneName.text = "Пустой";
        if (GM.WagoneData[IndexWag].WorkerInWagone >= 1)
        {
            GM.Worker += GM.WagoneData[IndexWag].WorkerInWagone;
            GM.WagoneData[IndexWag].WorkerInWagone = 0;
        }
    }

    public void UpdateWagone() // Улучшение вагона
    {
        if (GM.WagoneData[IndexWag].LevelWagone == 1)
        {
            if (GM.Money >= 2000)
            {
                GM.Money -= 2000;
                GM.StartPlusMoney(0 - 2000);
                GM.WagoneData[IndexWag].LevelWagone = 2;
                WagoneDataCount();
                WagoneWorkData();
                if(GM.WagoneData[IndexWag].Name == "Pass")
                {
                    GM.MaxWorkerWagone();
                    GM.PostWagonePeople();
                    GM.ResourceTextUpdate();
                }
                if (GM.WagoneData[IndexWag].Name == "Storage")
                {
                    GM.MaxStorageWagone();
                }
                if(GM.TaskCount == 6)
                {
                    GM.TaskCount = 7;
                    GM.TaskCounter();
                }
            }
            else
            {
                GM.StartMessage("Не хватает денег!");
            }
        }
        if (GM.WagoneData[IndexWag].LevelWagone == 2)
        {
            if (GM.Money >= 4000)
            {
                GM.Money -= 4000;
                GM.StartPlusMoney(0 - 4000);
                GM.WagoneData[IndexWag].LevelWagone = 3;
                WagoneDataCount();
                WagoneWorkData();
                if (GM.WagoneData[IndexWag].Name == "Pass")
                {
                    GM.MaxWorkerWagone();
                    GM.PostWagonePeople();
                    GM.ResourceTextUpdate();
                }
                if (GM.WagoneData[IndexWag].Name == "Storage")
                {
                    GM.MaxStorageWagone();
                }
                if (GM.TaskCount == 6)
                {
                    GM.TaskCount = 7;
                    GM.TaskCounter();
                }
            }
            else
            {
                GM.StartMessage("Не хватает денег!");
            }
        }
    }


    // ADS!!!
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);
    }
    void Rewarded(int id)
    {
        if (id == 6)
        {
            GM.WagoneData[IndexWag].TimerActiveProduction = 1;
        }
    }

    // VIEW WAGONE
    void ViewWagone()
    {
        if (GM.TextureTrain == 0)
        {
            if (GM.WagoneData[IndexWag].Name == "")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR0[0];
                //WagoneData[1].WagoneObj.GetComponent<Image>().sprite = WagoneData[i].WagoneImage;
            }
            if (GM.WagoneData[IndexWag].Name == "Food")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR0[0];
            }
            if (GM.WagoneData[IndexWag].Name == "Water")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR0[1];
            }
            if (GM.WagoneData[IndexWag].Name == "Pass")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR0[2];
            }
            if (GM.WagoneData[IndexWag].Name == "Boiler")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR0[3];
            }
            if (GM.WagoneData[IndexWag].Name == "Storage")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR0[0];
            }
        }
        if (GM.TextureTrain == 1)
        {
            if (GM.WagoneData[IndexWag].Name == "")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR1[0];
            }
            if (GM.WagoneData[IndexWag].Name == "Food")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR1[0];
            }
            if (GM.WagoneData[IndexWag].Name == "Water")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR1[1];
            }
            if (GM.WagoneData[IndexWag].Name == "Pass")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR1[2];
            }
            if (GM.WagoneData[IndexWag].Name == "Boiler")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR1[3];
            }
            if (GM.WagoneData[IndexWag].Name == "Storage")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR1[0];
            }
        }
        if (GM.TextureTrain == 2)
        {
            if (GM.WagoneData[IndexWag].Name == "")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR2[0];
            }
            if (GM.WagoneData[IndexWag].Name == "Food")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR2[0];
            }
            if (GM.WagoneData[IndexWag].Name == "Water")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR2[1];
            }
            if (GM.WagoneData[IndexWag].Name == "Pass")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR2[2];
            }
            if (GM.WagoneData[IndexWag].Name == "Boiler")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR2[3];
            }
            if (GM.WagoneData[IndexWag].Name == "Storage")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR2[0];
            }
        }
        if (GM.TextureTrain == 3)
        {
            if (GM.WagoneData[IndexWag].Name == "")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR3[0];
            }
            if (GM.WagoneData[IndexWag].Name == "Food")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR3[0];
            }
            if (GM.WagoneData[IndexWag].Name == "Water")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR3[1];
            }
            if (GM.WagoneData[IndexWag].Name == "Pass")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR3[2];
            }
            if (GM.WagoneData[IndexWag].Name == "Boiler")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR3[3];
            }
            if (GM.WagoneData[IndexWag].Name == "Storage")
            {
                GM.WagoneData[IndexWag].WagoneImage = GM.SPR3[0];
            }
        }
        WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
    }
}
