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

    [Header("ТаймерСледующейСтанции")]
    int IndexWag; // Индекс вагона
    int TimerWag; // Таймер для производства (Сколько времени необходимо)
    int ProductCount; // Количество единиц производства


    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        for (int i = 0; i < GM.WagoneData.Capacity; i++) // Проверка по длине списка данных поезда
        {
            if (!GM.WagoneData[i].WagoneActive) // Если вагон не активен, то резервируем
            {
                IndexWag = i;
                GM.WagoneData[IndexWag].WagoneActive = true;
                if (GM.WagoneData[IndexWag].Name != "") // Если имя не пусто, то просчет данных
                {
                    WagoneDataCount();
                    WagoneWorkData();
                    if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water")
                    {
                        StartCoroutine(FoodWorkWagone()); // Запуск корутины Производства еды/воды
                        StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагона
                    }
                    else if (GM.WagoneData[IndexWag].Name == "Boiler")
                    {
                        StartCoroutine(BoilerWorkWagone()); // Запуск корутины производства тепла
                    }
                    else if (GM.WagoneData[IndexWag].Name == "Pass")
                    {
                        StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагона
                    }
                    ViewWagone();
                }
                else if (GM.WagoneData[IndexWag].Name == "")
                {
                    ViewWagone();
                    WagoneName.text = "Пустой";
                }
                break;
            }
        }
    }
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    public void ClickOnWagone() // Клик по вагону и открытие панелей
    {
        if (GM.WagoneData[IndexWag].Name == "") // Если вагон пуст
        {
            if (!ChoiceWagonePan.activeInHierarchy)
            {
                if (GM.PanWagon != null) { GM.PanWagon.SetActive(false); }
                ChoiceWagonePan.SetActive(true); 
                GM.PanWagon = ChoiceWagonePan;
            }
            else
            {
                ChoiceWagonePan.SetActive(false);
                GM.PanWagon = null;
            }
        }
        else if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water") // Если вагон еда\вода
        {
            if (!FoodWaterWagonePan.activeInHierarchy)
            {
                if (GM.PanWagon != null) { GM.PanWagon.SetActive(false); }
                WagoneWorkText();
                FoodWaterWagonePan.SetActive(true);
                GM.PanWagon = FoodWaterWagonePan;
            }
            else
            {
                FoodWaterWagonePan.SetActive(false);
                GM.PanWagon = null;
            }
        }
        else if (GM.WagoneData[IndexWag].Name == "Pass") // Если вагон Пассажир
        {
            if (!PassWagonePan.activeInHierarchy)
            {
                if (GM.PanWagon != null) { GM.PanWagon.SetActive(false); }
                PassWagonePan.SetActive(true);
                GM.PanWagon = PassWagonePan;
            }
            else
            {
                PassWagonePan.SetActive(false);
                GM.PanWagon = null;
            }
        }
        else if (GM.WagoneData[IndexWag].Name == "Storage") // Если вагон грузовой
        {
            if (!StorageWagonePan.activeInHierarchy)
            {
                if (GM.PanWagon != null) { GM.PanWagon.SetActive(false); }
                StorageWagonePan.SetActive(true);
                GM.PanWagon = StorageWagonePan;
            }
            else
            {
                StorageWagonePan.SetActive(false);
                GM.PanWagon = null;
            }
        }
        else if (GM.WagoneData[IndexWag].Name == "Boiler") // Если вагон котельная
        {
            if (!BoilerWagonePan.activeInHierarchy)
            {
                if (GM.PanWagon != null) { GM.PanWagon.SetActive(false); }
                WagoneWorkText();
                BoilerWagonePan.SetActive(true);
                GM.PanWagon = BoilerWagonePan;
            }
            else
            {
                BoilerWagonePan.SetActive(false);
                GM.PanWagon = null;
            }
        }
    }

    // Выбор назначения вагона!
    public void ChoiceRoleWagon(int index)
    {
        GM.WagoneData[IndexWag].LevelWagone = 1;
        if (index == 0)
        {
            GM.WagoneData[IndexWag].Name = "Food";
            StartCoroutine(FoodWorkWagone()); // Запуск корутины Производства еды/воды
            StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагон
            if (GM.Trainer[0] == false)
            {
                GM.WagoneTrainerChoice[1] = true;
                GM.Trainer2();
            }
        }
        else if (index == 1)
        {
            GM.WagoneData[IndexWag].Name = "Water";
            StartCoroutine(FoodWorkWagone()); // Запуск корутины Производства еды/воды
            StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагон
            if (GM.Trainer[0] == false)
            {
                GM.WagoneTrainerChoice[2] = true;
                GM.Trainer2();
            }
        }
        else if (index == 2)
        {
            GM.WagoneData[IndexWag].Name = "Pass";
            GM.FreeWorker += 5;
            GM.AllWorker += 5;
            StartCoroutine(WarmInWagone()); // Запуск корутины просчета температуры вагона
            if (GM.Trainer[0] == false)
            {
                GM.WagoneTrainerChoice[0] = true;
                GM.Trainer2();
            }
        }
        else if (index == 3)
        {
            GM.WagoneData[IndexWag].Name = "Storage";
            GM.MaxStorageWagone();
        }
        else if (index == 4)
        {
            GM.WagoneData[IndexWag].Name = "Boiler";
            StartCoroutine(BoilerWorkWagone());
            if (GM.Trainer[0] == false)
            {
                GM.WagoneTrainerChoice[3] = true;
                GM.Trainer2();
            }
        }
        GM.WagoneData[IndexWag].WagoneActive = true;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        WagoneDataCount();
        WagoneWorkData();
        ChoiceWagonePan.SetActive(false);
        GM.PanWagon = null;
    }

    // Добавление и удаление рабочих в вагоне производств
    public void AddWork() // Добавление рабочих от уровня
    {
        if (GM.WagoneData[IndexWag].WorkerInWagone < (5 * GM.WagoneData[IndexWag].LevelWagone) & GM.FreeWorker >=1)
        {
            GM.WagoneData[IndexWag].WorkerInWagone++;
            GM.FreeWorker--;
            WagoneDataCount();
            WagoneWorkData();
            GM.ResourceTextUpdate();
        }
    }
    public void RemoveWork() // Удаление рабочих в вагоне от 
    {
        if (GM.WagoneData[IndexWag].WorkerInWagone > 0)
        {
            GM.WagoneData[IndexWag].WorkerInWagone--;
            GM.FreeWorker++;
            WagoneDataCount();
            WagoneWorkData();
            GM.ResourceTextUpdate();
        }
    }

    //
    public void CollectProduct()
    {
        int Razn = 0;
        GM.EffectCollect.SetActive(false);
        GM.EffectCollect.transform.position = DoneProductionPan.transform.position;
        GM.EffectCollect.SetActive(true);

        if (GM.WagoneData[IndexWag].Name == "Food")
        {
            Razn = GM.FoodMax - GM.Food;
            if (ProductCount < Razn)
            {
                GM.Food += ProductCount;
                GM.FoodPlusStatistic += ProductCount;
                GM.StartPlusFood(ProductCount);
            }
            else if ( ProductCount >= Razn)
            {
                GM.Food += Razn;
                GM.FoodPlusStatistic += Razn;
                GM.StartPlusFood(Razn);
            }
            GM.CollectResources[0].SetActive(false); GM.CollectResources[0].SetActive(true);
            StartCoroutine(FoodWorkWagone());
        }
        else if (GM.WagoneData[IndexWag].Name == "Water")
        {
            Razn = GM.WaterMax - GM.Water;
            if (ProductCount < Razn)
            {
                GM.Water += ProductCount;
                GM.WaterPlusStatistic += ProductCount;
                GM.StartPlusWater(ProductCount);
            }
            else if (ProductCount >= Razn)
            {
                GM.Water += Razn;
                GM.WaterPlusStatistic += Razn;
                GM.StartPlusWater(Razn);
            }
            GM.CollectResources[1].SetActive(false); GM.CollectResources[1].SetActive(true);
            StartCoroutine(FoodWorkWagone());
        }
        else if (GM.WagoneData[IndexWag].Name == "Boiler")
        {
            Razn = GM.WarmMax - GM.Warm;
            if (ProductCount < Razn)
            {
                GM.Warm += ProductCount;
                GM.WarmPlusStatistic += ProductCount;
                GM.StartPlusWarm(ProductCount);
            }
            else if (ProductCount >= Razn)
            {
                GM.Warm += Razn;
                GM.WarmPlusStatistic += Razn;
                GM.StartPlusWarm(Razn);
            }
            GM.CollectResources[2].SetActive(false); GM.CollectResources[2].SetActive(true);
            StartCoroutine(BoilerWorkWagone());
        }

        GM.ResourceTextUpdate();
        GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag;
        DoneProductionPan.SetActive(false);

        if (GM.TaskCount == 2)
        {
            GM.TaskCount = 3;
            GM.TaskCounter();
        }
    }
    void WagoneDataCount() // Ввод данных вагона
    {
        WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
        if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water") //
        {
            FoodLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            FoodWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
            FoodTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
            if (GM.TemperatureOnStreet == 0) { FoodNeedWarmText.text = "-10"; }
            else if (GM.TemperatureOnStreet < 0) { FoodNeedWarmText.text = GM.TemperatureOnStreet.ToString(); }
            if (GM.WagoneData[IndexWag].Name == "Food")
            {
                FoodProdText.text = "Производство пищи:";
                WagoneName.text = "Пища";
                FoodWagoneNameText.text = "Пища";
            }
            else if (GM.WagoneData[IndexWag].Name == "Water")
            {
                FoodProdText.text = "Производство воды:";
                WagoneName.text = "Вода";
                FoodWagoneNameText.text = "Вода";
            }
        }
        else if (GM.WagoneData[IndexWag].Name == "Pass") //
        {
            WagoneName.text = "Рабочий";
            PassLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
            if (GM.TemperatureOnStreet == 0) { PassNeedWarmText.text = "-10"; }
            else if (GM.TemperatureOnStreet < 0) { PassNeedWarmText.text = GM.TemperatureOnStreet.ToString(); }
            PassNeedFoodText.text = "-" + GM.WagoneData[IndexWag].LevelWagone * 5;
            PassNeedWaterText.text = "-" + GM.WagoneData[IndexWag].LevelWagone * 5;
        }
        else if (GM.WagoneData[IndexWag].Name == "Boiler") //
        {
            WagoneName.text = "Котельная";
            BoilerLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            BoilerWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            BoilerTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
            BoilerNeedCoalText.text = "-" + GM.WagoneData[IndexWag].WorkerInWagone * 20;
        }
        else if (GM.WagoneData[IndexWag].Name == "Storage") //
        {
            WagoneName.text = "Склад";
            StorageLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
        }
    }

    void WagoneWorkText()
    {
        if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water") //
        {
            FoodWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
        }
        else if (GM.WagoneData[IndexWag].Name == "Boiler") //
        {
            BoilerWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
        }
    }

    void WagoneWorkData() // Просчет данных для производства вагона от уровня и кол-ва работников
    {
        if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water")
        {
            if(GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 30;
                FoodUpgradeText.text = "2000$";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 50;
                FoodUpgradeText.text = "4000$";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 80;
                FoodUpgradeText.text = "Максимум";
            }
            FoodProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
            if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
        }
        else if (GM.WagoneData[IndexWag].Name == "Boiler")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 20;
                BoilerUpgradeText.text = "2000$";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 40;
                BoilerUpgradeText.text = "4000$";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 60;
                BoilerUpgradeText.text = "Максимум";
            }
            BoilerProdCountText.text = ProductCount + "ед. за: " + TimerWag + "сек."; //
            if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
        }
        else if (GM.WagoneData[IndexWag].Name == "Pass")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = 5; // Максимальная вместимость в вагон от уровня
                PassUpgradeText.text = "2000$";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = 10; // Максимальная вместимость в вагон от уровня
                PassUpgradeText.text = "4000$";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = 15; // Максимальная вместимость в вагон от уровня
                PassUpgradeText.text = "Максимум";
            }
            PassMaxCapacityText.text = ProductCount + " Человек";
        }
        else if (GM.WagoneData[IndexWag].Name == "Storage")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = 50; // Максимальная вместимость в вагон от уровня
                StorageUpgradeText.text = "2000$";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = 75; // Максимальная вместимость в вагон от уровня
                StorageUpgradeText.text = "4000$";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = 100; // Максимальная вместимость в вагон от уровня
                StorageUpgradeText.text = "Максимум";
            }
            StorageMaxCapacityText.text = ProductCount + " Ресурсов";
        }
    }

    IEnumerator FoodWorkWagone() // Корутина производства вагона (Еда\вода)
    {
        while (true)
        {
            if (GM.WagoneData[IndexWag].WorkerInWagone >= 1 & GM.WagoneData[IndexWag].TemperatureWagone >= 0) // Если 1 и больше активных работников и температура выше 0
            {
                GM.WagoneData[IndexWag].TimerActiveProduction--;
                FoodTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) // Если активный таймер 0,
                {
                    DoneProductionPan.SetActive(true); // Активация панели сбора награды
                    break;
                }
                else if (GM.WagoneData[IndexWag].TimerActiveProduction > 0) // Если активный таймер работает
                {
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
                else if (GM.WagoneData[IndexWag].TimerActiveProduction > 0) // Если активный таймер работает
                {
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

    IEnumerator WarmInWagone() // Потребление Тепла вагона
    {
        while (true)
        {
            if (GM.TemperatureOnStreet == 0)
            {
                if (GM.Warm >= 10)
                {
                    GM.Warm -= 10;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 10);
                    GM.WagoneData[IndexWag].TemperatureWagone = 10;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                }
                else if (GM.Warm < 10)
                {
                    FoodTermometrText.text = GM.TemperatureOnStreet + "°C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "°C";
                }
                SnowWagone.SetActive(false);
                yield return new WaitForSeconds(10);
            }
            else if (GM.TemperatureOnStreet < 0)
            {
                int r = GM.TemperatureOnStreet;
                Mathf.Abs(r);
                if (GM.Warm >= r)
                {
                    GM.Warm -= r;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - r);
                    GM.WagoneData[IndexWag].TemperatureWagone = 10;
                    SnowWagone.SetActive(false);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "°C";
                }
                else if (GM.Warm < r)
                {
                    GM.Warm = 0;
                    GM.ResourceTextUpdate();
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    FoodTermometrText.text = GM.TemperatureOnStreet + "°C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "°C";
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    GM.StartMessage("Вагон замерзает!");
                    SnowWagone.SetActive(true);
                }
                yield return new WaitForSeconds(10);
            }
        }
    }

    public void ResetWagone() // Сброс вагона до исходных.
    {
        ClickOnWagone();
        if (GM.WagoneData[IndexWag].Name == "Pass")
        {
            GM.DeleteWorkerTrain(IndexWag);
            GM.ResourceTextUpdate();
        }
        else if(GM.WagoneData[IndexWag].Name == "Storage")
        {
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
            GM.FreeWorker += GM.WagoneData[IndexWag].WorkerInWagone;
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
                    GM.FreeWorker += 5;
                    GM.AllWorker += 5;
                    GM.ResourceTextUpdate();
                }
                else if (GM.WagoneData[IndexWag].Name == "Storage")
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
                    GM.FreeWorker += 5;
                    GM.AllWorker += 5;
                    GM.ResourceTextUpdate();
                }
                else if (GM.WagoneData[IndexWag].Name == "Storage")
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
        WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage = GM.wagoneSprites[GM.WagoneData[IndexWag].Name][GM.TextureTrain];
    }
}
