using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StationScripts : MonoBehaviour
{
    public Game GM; // ������ ����
    public PassengersScript PS; //
    public Audio AO;
    public Training TR;
    public Questions QS;
    public DeliveryTask DT;

    public GameObject StationPan; // ������ �������
    public GameObject GamePan; //
   
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

    [Header("NextStation")]
    public Text NextStationText; // ����� ��������� �������
    public GameObject[] BttnNextStation; //
    public bool StationVibor; //
    
    [Header("AddWagone")]
    public Text CountWagon; // ������� ���-�� �������
    public Text BuyWagoneText; //

    [Header("AddPassWagon")]
    public Text countPassWagon;
    public Text buyPassWagonText;

    [Header("Delivery")]
    public GameObject QuestPan; // ������ �������
    public int targetDeliveryCity; //

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
    
    [Header("ADS")]
    public GameObject ShopPan; //
    public GameObject ADSMoneyActive; //
    public Text ADSMoneyActiveText; // ����� ����� �� �������
    int ADSMoneyCol; // ������� ����� �� �������
    
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
        NextStationText.text = "�� " + GM.NameCity[GM.ChoiceCity] + ": " + GM.NextStationTime + " ����." + "�����������: " + GM.TemperatureForCity[GM.ChoiceCity] + "�C";
    }

    public void ExitStation()
    {
        PS.ExitStation();
    }

    public void StartNextStation() // ����� ��������� �������
    {
        GamePan.SetActive(true);
        GM.ActiveWagone();
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

    public void ResourceTextUpdate() // ���������� �������
    {
        MoneyText.text = GM.Money + "�";
        DiamondText.text = GM.Diamond.ToString();
        CoalText.text = GM.Coal + "/" + GM.CoalMax;
        FoodText.text = GM.Food + "/" + GM.FoodMax;
        WaterText.text = GM.Water + "/" + GM.WaterMax;
        WarmText.text = GM.Warm + "/" + GM.WarmMax;
        WorkerText.text = GM.FreeWorker + "/" + GM.AllWorker; //
        CountWagon.text = "�������: " + GM.WagonCol + " �� " + GM.MaxWagone; //
        BuyWagoneText.text = (300 * GM.WagonCol) + "�";
        countPassWagon.text = "���� ����������: " + ((GM.passWagoneCount * 3) - GM.passCount) + " �� " + (GM.passWagoneCount * 3);
        buyPassWagonText.text = (1000 * GM.passWagoneCount) + "�";
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
                AO.PlayAudioPayment();
            }
            else
            {
                StartCoroutine(MoneyShow());
            }
        }
    }

    public void BuyPassWagone() // ������� ������� / ����������
    {
        if (GM.Money >= (1000 * GM.passWagoneCount))
        {
            GM.Money -= 1000 * GM.passWagoneCount;
            StartCoroutine(PlusMoney(0 - (1000 * GM.passWagoneCount)));
            GM.passWagoneCount++;
            ResourceTextUpdate();
            AO.PlayAudioPayment();
        }
        else
        {
            StartCoroutine(MoneyShow());
        }
    }

    public void ActiveSlider(int index) // ��������� ���������
    {
        if (index < 4)
        {
            SliderMarket[index].maxValue = resourcesInfo[index % 4, 0];
            ColMoneyText[index].text = "+" + (SliderMarket[index].value * resourcesInfo[index % 4, 2]) + "�";
        }
        else
        {
            SliderMarket[index].maxValue = resourcesInfo[index % 4, 1] - resourcesInfo[index % 4, 0];
            ColMoneyText[index].text = "-" + SliderMarket[index].value * resourcesInfo[index % 4, 3] + "�";
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
        AO.PlayAudioPayment();
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
            DT.UpdateDelivery();
        }
        AO.PlayAudioClickBttn();
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
        AO.PlayAudioClickBttn();
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
        AO.PlayAudioClickBttn();
    }
    public void ExchangeDiamond() // ����� ������� �� ������
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
            ADSMoneyActiveText.text = "+" + ADSMoneyCol + "�!!!" + "\n" + "�� ��������";
        }
        else
        {
            ADSMoneyActive.SetActive(true);
            ADSMoneyCol = 500;
            ADSMoneyActiveText.text = "+" + ADSMoneyCol + "�!!!" + "\n" + "�� ��������";
        }
    }
        //else if(GM.City == 44)
        //{
        //    if (TaskCounter == 1)
        //    {
        //        TaskPan[0].SetActive(false);
        //        TaskPan[19].SetActive(false);
        //    }
        //    else if (TaskCounter == 0)
        //    {
        //        TaskPan[18].SetActive(false);
        //        TaskPan[19].SetActive(true);
        //        TaskCounter = 1;
        //        int H = GM.TimeInGameStatistic / 60;
        //        int M = GM.TimeInGameStatistic - (H * 60);
        //        TimeInGameStatisticText.text = "����� � ����: " + H + "�. " + M + "�."; //
        //        FoodStatisticText.text = "������� ���: " + GM.FoodPlusStatistic.ToString(); //
        //        CoalPlusStatisticText.text = "������� ����: " + GM.CoalPlusStatistic.ToString(); //
        //        WaterPlusStatisticText.text = "������� ����: " + GM.WaterPlusStatistic.ToString(); //
        //        WarmPlusStatisticText.text = "������� �����: " + GM.WarmPlusStatistic.ToString(); //
        //        MoneyPlusStatisticText.text = "���������� �����: " + GM.MoneyPlusStatistic.ToString(); //
        //        DistancePlusStatisticText.text = "�������� ����������: " + GM.DistancePlusStatistic * 10; //
        //        ScorePlusStatisticText.text = "�����: " + GM.Score.ToString(); //
        //    }
        //}
}
