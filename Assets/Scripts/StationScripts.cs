using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StationScripts : MonoBehaviour
{
    [SerializeField] private Game GM; // Скрипт Гейм
    [SerializeField] private PassengersScript PS; //
    [SerializeField] private Audio AO;
    [SerializeField] private Training TR;
    [SerializeField] private Questions QS;
    [SerializeField] private DeliveryTask DT;

    [SerializeField] private GameObject StationPan; // Панель станции
    [SerializeField] private GameObject GamePan; //
   
    [Header("Market")]
    [SerializeField] private GameObject MarketPan; // Панель рынка
    [SerializeField] private Slider[] SliderMarket; // Слайдеры рынка
    [SerializeField] private Text[] ColResourceText; // Выбор Кол-во ресурсов текст
    [SerializeField] private Text[] ColMoneyText; // Сумма покупки/продажи

    [Header("Train")]
    [SerializeField] private GameObject TrainPan; // Панель поезда
    [SerializeField] private GameObject LocoBuyPan; // Панель покупки локомотива
    [SerializeField] private GameObject ViewTrainPan; // Панель Смены вида поезда
    [SerializeField] private GameObject SpecialWagPan; // Панель Специальных вагонов
    [SerializeField] private Text[] TrainViewText; //
    [SerializeField] private Text[] LocoUpdateText; //

    [Header("NextStation")]
    [SerializeField] private Text NextStationText; // Текст следующей станции
    [SerializeField] private GameObject[] BttnNextStation; //
    [HideInInspector] public bool StationVibor; //
    
    [Header("AddWagone")]
    [SerializeField] private Text CountWagon; // Подсчет кол-ва вагонов
    [SerializeField] private Text BuyWagoneText; //

    [Header("AddPassWagon")]
    [SerializeField] private Text countPassWagon;
    [SerializeField] private Text buyPassWagonText;

    [Header("Delivery")]
    [SerializeField] private GameObject QuestPan; // Панель заданий

    [Header("Resource")]
    [SerializeField] private Text MoneyText; // Текст денег
    [SerializeField] private Text DiamondText; // Текст Роскошь
    [SerializeField] private Text CoalText; // Текст угля
    [SerializeField] private Text FoodText; // Текст еды
    [SerializeField] private Text WaterText; // Текст воды
    [SerializeField] private Text WarmText; // Текст тепла
    [SerializeField] private Text WorkerText; // Текст рабочих
    [SerializeField] private Text[] plusResourceText; // 0-Уголь / 1-Еда / 2-Вода / 3-Тепло
    [SerializeField] private Text PlusMoneyText; // 
    
    [Header("MoneyView")]
    [SerializeField] private Text MoneyShowText; // 
    
    [Header("ADS")]
    [SerializeField] private GameObject ShopPan; //
    [SerializeField] private GameObject ADSMoneyActive; //
    [SerializeField] private Text ADSMoneyActiveText; // текст денег за рекламу
    private int ADSMoneyCol; // награда денег за рекламу

    //
    [SerializeField] private Text[] ScoreTextView; //

    private int[,] resourcesInfo; // Количество ресурса, максимально ресурса, цена продажи, цена покупки.

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
            TR.trainingPanStation.SetActive(true);
            AO.PlayAudioEnterPanel();
        }
        PS.EnterStation();
        BttnNextStation[1].SetActive(false);
        StationVibor = false;
        TimerNextStation();
        ResourceTextUpdate();
        GM.Score+=10;
        ScoreCount();
        QS.CheckQuest();
        for (int i = 0; i < 9; i++)
        {
            ActiveSlider(i);
        }
        DT.CheckDelivery();
        UpdateADSMoneyActive();
    }
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    public void TimerNextStation() //
    {
        GM.NextStationTimeCount = GM.TimeForCity[GM.ChoiceCity];
        GM.NextStationTime = GM.TimeForCity[GM.ChoiceCity];
        GM.NextStationSlide.maxValue = GM.NextStationTime;
        NextStationText.text = "До " + GM.NameCity[GM.ChoiceCity] + ": " + (GM.NextStationTime*100) + " дист." + "Температура: " + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
    }

    public void ExitStation()
    {
        PS.ExitStation();
    }

    public void StartNextStation() // Старт следующей станции
    {
        GamePan.SetActive(true);
        GM.ActiveWagone();
        GM.StartNextStation();
        GM.City = GM.ChoiceCity;
        GM.ChoiceCity = 0;
        GM.SpeedFon = 0.1f;
        GM.RandomBackground();
        StationPan.SetActive(false);
        GM.SV.Save();
        GM.TaskCounter();
        if (YandexGame.EnvironmentData.deviceType == "mobile")
        {
            YandexGame.StickyAdActivity(true);
        }
        YandexGame.FullscreenShow();
    }

    public void ResourceTextUpdate() // Обновление текстов
    {
        MoneyText.text = GM.Money + "р";
        DiamondText.text = GM.Diamond.ToString();
        CoalText.text = GM.Coal + "/" + GM.CoalMax;
        FoodText.text = GM.Food + "/" + GM.FoodMax;
        WaterText.text = GM.Water + "/" + GM.WaterMax;
        WarmText.text = GM.Warm + "/" + GM.WarmMax;
        WorkerText.text = GM.FreeWorker + "/" + GM.AllWorker; //
        CountWagon.text = "Вагонов: " + GM.WagonCol + " из " + GM.MaxWagone; //
        BuyWagoneText.text = (200 * GM.WagonCol) + "р";
        countPassWagon.text = "Мест пассажирам: " + ((GM.passWagoneCount * 3) - GM.passCount) + " из " + (GM.passWagoneCount * 3);
        buyPassWagonText.text = (800 * GM.passWagoneCount) + "р";
    }

    public void BuyWagone() // Покупка вагонов / Добавление
    {
        if(GM.WagonCol < GM.MaxWagone)
        {
            if(GM.Money >= (200 * GM.WagonCol))
            {
                GM.Money -= (200 * GM.WagonCol);
                StartCoroutine(PlusMoney(0 - (200 * GM.WagonCol)));
                GM.WagonCol++;
                ResourceTextUpdate();
                AO.PlayAudioPayment();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
    }

    public void BuyPassWagone() // Покупка вагонов / Добавление
    {
        if (GM.Money >= (800 * GM.passWagoneCount))
        {
            GM.Money -= 800 * GM.passWagoneCount;
            StartCoroutine(PlusMoney(0 - (800 * GM.passWagoneCount)));
            GM.passWagoneCount++;
            ResourceTextUpdate();
            AO.PlayAudioPayment();
        }
        else
        {
            StartCoroutine(MoneyShow());
        }
    }

    public void ActiveSlider(int index) // Активация слайдеров
    {
        if (index < 4)
        {
            SliderMarket[index].maxValue = resourcesInfo[index % 4, 0];
            ColMoneyText[index].text = "+" + (SliderMarket[index].value * resourcesInfo[index % 4, 2]) + "р";
        }
        else
        {
            SliderMarket[index].maxValue = resourcesInfo[index % 4, 1] - resourcesInfo[index % 4, 0];
            ColMoneyText[index].text = "-" + SliderMarket[index].value * resourcesInfo[index % 4, 3] + "р";
        }
        ColResourceText[index].text = SliderMarket[index].value.ToString();
    }

    public void BuySellResource(int index) // Покупка / Продажа Ресурсов на рынке
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
        AO.PlayAudioPayment();
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
        else if (index == 1)
        {
            MarketPan.SetActive(false);
            TrainPan.SetActive(true);
            QuestPan.SetActive(false);
            if(GM.LevelLoco == 2 )
            {
                LocoUpdateText[0].text = "Приобретено";
                LocoUpdateText[1].text = "Текущий";
            }
            else if (GM.LevelLoco == 3)
            {
                LocoUpdateText[0].text = "Приобретено";
                LocoUpdateText[1].text = "Приобретено";
                LocoUpdateText[2].text = "Текущий";
            }
        }
        else if (index == 2)
        {
            MarketPan.SetActive(false);
            TrainPan.SetActive(false);
            QuestPan.SetActive(true);
            DT.UpdateDelivery();
        }
        AO.PlayAudioClickBttn();
    }

    public void OpenTrainPan(int index) // Открытие панелей поезда
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
        AO.PlayAudioClickBttn();
    }

    private void UpdateTextTrainView()
    {
        for (int i = 0; i < TrainViewText.Length; i++)
        {
            if (i == GM.TextureTrain) TrainViewText[i].text = "Текущий";
            else if (GM.Texture[i]) TrainViewText[i].text = "Доступен";
        }
    }

    public void BuyViewTrain(int index) // Покупка внешнего вида поезда
    {
        // Покупка текстуры
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
                AO.PlayAudioPayment();
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

    public void BuyUpdateLoco(int index) // Покупка улушение поезда
    {
        if(index == 0 & GM.LevelLoco <= 1)
        {
            if(GM.Money >= 10000)
            {
                GM.Money -= 10000;
                StartCoroutine(PlusMoney(0 - 10000));
                GM.LevelLoco = 1;
                GM.LevelEngine = 0;
                GM.LevelChassis = 0;
                GM.LevelCoalStorage = 0;
                GM.LevelLocoUpdater();
                GM.TextLoco();
                ResourceTextUpdate();
                GM.ViewLoco();
                LocoUpdateText[0].text = "Приобретено";
                LocoUpdateText[1].text = "Текущий";
                AO.PlayAudioPayment();
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
                GM.LevelLoco = 2;
                GM.LevelEngine = 0;
                GM.LevelChassis = 0;
                GM.LevelCoalStorage = 0;
                GM.LevelLocoUpdater();
                GM.TextLoco();
                ResourceTextUpdate();
                GM.ViewLoco();
                LocoUpdateText[0].text = "Приобретено";
                LocoUpdateText[1].text = "Приобретено";
                LocoUpdateText[2].text = "Текущий";
                AO.PlayAudioPayment();
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
                AO.PlayAudioPayment();
            }
        }
    }

    public void OpenMapPan()
    {
        BttnNextStation[1].SetActive(true);
        BttnNextStation[0].SetActive(true);
        StationVibor = true;
        AO.PlayAudioClickBttn();
    }

    public void ScoreCount() // Вывод очков
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


    // Корутины на тексты прибавления и отнимания ресурсов!
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

    public void OpenClosedShopPan() // Открытие панели магазина
    {
        if(!ShopPan.activeInHierarchy) ShopPan.SetActive(true);
        else ShopPan.SetActive(false);
        AO.PlayAudioClickBttn();
    }
    public void ExchangeDiamond() // Обмен Роскоши на деньги
    {
        if (GM.Diamond >= 1)
        {
            GM.Money += 1500;
            GM.Diamond--;
            StartCoroutine(PlusMoney(1500));
            ResourceTextUpdate();
            AO.PlayAudioPayment();
        }
    }
    void UpdateADSMoneyActive() //
    {
        if (GM.Money >= 500)
        {
            ADSMoneyActive.SetActive(true);
            ADSMoneyCol = Random.Range(500, GM.Money);
            ADSMoneyActiveText.text = "+" + ADSMoneyCol + "р!!!" + "\n" + "За просмотр";
        }
        else
        {
            ADSMoneyActive.SetActive(true);
            ADSMoneyCol = 500;
            ADSMoneyActiveText.text = "+" + ADSMoneyCol + "р!!!" + "\n" + "За просмотр";
        }
    }
}
