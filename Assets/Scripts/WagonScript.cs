using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WagonScript : MonoBehaviour
{
    public Game GM; // 
    [Header("�����.������")]
    public GameObject ChoiceWagonePan; // ������ ������ ������ ���������� ������
    public GameObject FoodWaterWagonePan; // ������ ��� ��� ����
    public GameObject PassWagonePan; // ������ ������������� ������
    public GameObject StorageWagonePan; // ������ ������ ������
    public GameObject ProductionWagonePan; // ������ ����������������� ������
    public GameObject BoilerWagonePan; // ������ ���������
    public Text WagoneName; // �������� ������ �� ���.
    public GameObject DoneProductionPan; // ������ ���������� ��������� ��� �����
    public GameObject SnowWagone; // ������ ���������� ������
    [Header("�������������")]
    public Text FoodWagoneNameText; // �������� ������ ��� ��� ����
    public Text FoodLevelText; // ������� ������
    public Text FoodProdText; // ��� ���������� ����� (��� ��� ����)
    public Text FoodWorkCountText; // ���������� ���������� � ������
    public Text FoodProdCountText; // ������� ��������� � �� ����� ����� ����������
    public Text FoodTermometrText; // ����������� � ������
    public Text FoodTimerText; // ������ ������������
    public Text FoodUpgradeText; // ��������� ������ ���������
    public Text FoodNeedWarmText; // 
    [Header("����������")]
    public Text PassLevelText; // ������� ������
    public Text PassMaxCapacityText; // ������������ ����������� ������
    public Text PassCapacityText; // ������� ��������� � ������
    public Text PassTermometrText; // ����������� � ������
    public Text PassUpgradeText; // ��������� ������ ���������
    public Text PassNeedFoodText; //
    public Text PassNeedWaterText; //
    public Text PassNeedWarmText; //
    [Header("���������������")]
    public Text StorageLevelText; // ������� ������
    public Text StorageMaxCapacityText; // ������������ ����������� ������
    public Text StorageCapacityText; // ������� �� ������ �������
    public Text StorageUpgradeText; // ��������� ������ ���������
    [Header("���������������")]
    public Text BoilerLevelText; // ������� ������
    public Text BoilerWorkCountText; // ���������� ���������� � ������
    public Text BoilerProdCountText; // ������� ��������� � �� ����� ����� ����������
    public Text BoilerTimerText; // ������ ������������
    public Text BoilerUpgradeText; // ��������� ������ ���������
    public Text BoilerNeedCoalText; //
    [Header("��� ������")]
    public Image WagoneImage; // ��� ������
    public Sprite BaseWagone; // ��������� ��� ������
    
    int PanOpenClosed; // ���������� ��������/�������� ������ ��� ������� �� �����
    int Worker;
    [Header("����������������������")]
    int IndexWag; // ������ ������
    int TimerWag; // ������ ��� ������������ (������� ������� ����������)
    int ProductCount; // ���������� ������ ������������


    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        for (int i = 0; i < GM.WagoneData.Capacity; i++) // �������� �� ����� ������ ������ ������
        {
            if (GM.WagoneData[i].WagoneActive == 0) // ���� ����� �� �������, �� �����������
            {
                IndexWag = i;
                GM.WagoneData[IndexWag].WagoneActive = 1;
                if (GM.WagoneData[IndexWag].Name != "") // ���� ��� �� �����, �� ������� ������
                {
                    WagoneDataCount();
                    WagoneWorkData();
                    if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water")
                    {
                        StartCoroutine(FoodWorkWagone()); // ������ �������� ������������ ���/����
                        StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� ������
                    }
                    if (GM.WagoneData[IndexWag].Name == "Boiler")
                    {
                        StartCoroutine(BoilerWorkWagone()); // ������ �������� ������������ �����
                    }
                    if (GM.WagoneData[IndexWag].Name == "Pass")
                    {
                        StartCoroutine(PassFoodNeed()); // ������ �������� �����������  ���.
                        StartCoroutine(PassWaterNeed()); // ������ �������� �����������  ����.
                        StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� ������
                    }
                    ViewWagone();
                }
                if (GM.WagoneData[IndexWag].Name == "")
                {
                    ViewWagone();
                    WagoneName.text = "������";
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


    public void ClickOnWagone() // ���� �� ������ � �������� �������
    {
        if(GM.WagoneData[IndexWag].Name == "") // ���� ����� ����
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
        if (GM.WagoneData[IndexWag].Name == "Food") // ���� ����� ���\����
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
        if (GM.WagoneData[IndexWag].Name == "Water") // ���� ����� ���\����
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
        if (GM.WagoneData[IndexWag].Name == "Pass") // ���� ����� ��������
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
        if (GM.WagoneData[IndexWag].Name == "Storage") // ���� ����� ��������
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
        if (GM.WagoneData[IndexWag].Name == "Production") // ���� ����� �����������
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
        if (GM.WagoneData[IndexWag].Name == "Boiler") // ���� ����� ���������
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

    // ����� ���������� ������!
    public void ChoiceFoodWagone() // ����� ���
    {
        GM.WagoneData[IndexWag].Name = "Food";
        GM.WagoneData[IndexWag].WagoneActive = 1;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        GM.WagoneData[IndexWag].LevelWagone = 1;
        WagoneDataCount();
        WagoneWorkData();
        StartCoroutine(FoodWorkWagone()); // ������ �������� ������������ ���/����
        StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� ������
        PanOpenClosed = 0;
        ChoiceWagonePan.SetActive(false);
        if(GM.Trainer[0] == false)
        {
            GM.WagoneTrainerChoice[1] = true;
            GM.Trainer2();
        }
    }
    public void ChoiceWaterWagone() // ����� ����
    {
        GM.WagoneData[IndexWag].Name = "Water";
        GM.WagoneData[IndexWag].WagoneActive = 1;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        GM.WagoneData[IndexWag].LevelWagone = 1;
        WagoneDataCount();
        WagoneWorkData();
        StartCoroutine(FoodWorkWagone()); // ������ �������� ������������ ���/����
        StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� ������
        PanOpenClosed = 0;
        ChoiceWagonePan.SetActive(false);
        if (GM.Trainer[0] == false)
        {
            GM.WagoneTrainerChoice[2] = true;
            GM.Trainer2();
        }
    }
    public void ChoicePassWagone() // ����� ��������
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
        StartCoroutine(PassFoodNeed()); // ������ �������� �����������  ���.
        StartCoroutine(PassWaterNeed()); // ������ �������� �����������  ����.
        StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� ������
        PanOpenClosed = 0;
        ChoiceWagonePan.SetActive(false);
        if (GM.Trainer[0] == false)
        {
            GM.WagoneTrainerChoice[0] = true;
            GM.Trainer2();
        }
    }
    public void ChoiceStorageWagone() // ����� ��������
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
    public void ChoiceProductionWagone() // ����� ������������
    {
        GM.WagoneData[IndexWag].Name = "Production";
        GM.WagoneData[IndexWag].WagoneActive = 1;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        GM.WagoneData[IndexWag].LevelWagone = 1;
        PanOpenClosed = 0;
        ChoiceWagonePan.SetActive(false);
    }
    public void ChoiceBoilerWagone() // ����� ���������
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

    // ���������� � �������� ������� � ������ �����������
    public void AddWork() // ���������� ������� �� ������
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
    public void RemoveWork() // �������� ������� � ������ �� 
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
    void WagoneDataCount() // ���� ������ ������
    {
        Worker = GM.WagoneData[IndexWag].WorkerInWagone;
        if(GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water") //
        {
            WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
            FoodLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            FoodWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
            FoodTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
            if (GM.TemperatureOnStreet == 0) { FoodNeedWarmText.text = "-10"; }
            if (GM.TemperatureOnStreet == -10) { FoodNeedWarmText.text = "-20"; }
            if (GM.TemperatureOnStreet == -20) { FoodNeedWarmText.text = "-30"; }
            if (GM.WagoneData[IndexWag].Name == "Food")
            {
                FoodProdText.text = "������������ ����:";
                WagoneName.text = "����";
                FoodWagoneNameText.text = "����";
            }
            if (GM.WagoneData[IndexWag].Name == "Water")
            {
                FoodProdText.text = "������������ ����:";
                WagoneName.text = "����";
                FoodWagoneNameText.text = "����";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Pass") //
        {
            WagoneName.text = "������������";
            WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
            PassLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            PassCapacityText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
            if (GM.TemperatureOnStreet == 0) { PassNeedWarmText.text = "-10"; }
            if (GM.TemperatureOnStreet == -10) { PassNeedWarmText.text = "-20"; }
            if (GM.TemperatureOnStreet == -20) { PassNeedWarmText.text = "-30"; }
            PassNeedFoodText.text = "-" + GM.WagoneData[IndexWag].WorkerInWagone;
            PassNeedWaterText.text = "-" + GM.WagoneData[IndexWag].WorkerInWagone;
        }
        if (GM.WagoneData[IndexWag].Name == "Boiler") //
        {
            WagoneName.text = "���������";
            WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
            BoilerLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            BoilerWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            BoilerTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
            BoilerNeedCoalText.text = "-" + GM.WagoneData[IndexWag].WorkerInWagone * 20;
        }
        if (GM.WagoneData[IndexWag].Name == "Storage") //
        {
            WagoneName.text = "�����";
            WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
            StorageLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
        }
    }

    void WagoneWorkData() // ������� ������ ��� ������������ ������ �� ������ � ���-�� ����������
    {
        if (GM.WagoneData[IndexWag].Name == "Food")
        {
            if(GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 30;
                FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
                if(GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 50;
                FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "4000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 80;
                FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "��������";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Water")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 30;
                FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 40;
                FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "4000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 70;
                FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                FoodUpgradeText.text = "��������";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Boiler")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 20;
                BoilerProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                BoilerUpgradeText.text = "1500$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 40;
                BoilerProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                BoilerUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 60;
                BoilerProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
                BoilerUpgradeText.text = "4000$";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Pass")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = 10; // ������������ ����������� � ����� �� ������
                PassMaxCapacityText.text = ProductCount + " �������";
                PassUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = 15; // ������������ ����������� � ����� �� ������
                PassMaxCapacityText.text = ProductCount + " �������";
                PassUpgradeText.text = "4000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = 20; // ������������ ����������� � ����� �� ������
                PassMaxCapacityText.text = ProductCount + " �������";
                PassUpgradeText.text = "��������";
            }
        }
        if (GM.WagoneData[IndexWag].Name == "Storage")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = 50; // ������������ ����������� � ����� �� ������
                StorageMaxCapacityText.text = ProductCount + " ��������";
                StorageUpgradeText.text = "2000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = 75; // ������������ ����������� � ����� �� ������
                StorageMaxCapacityText.text = ProductCount + " ��������";
                StorageUpgradeText.text = "4000$";
            }
            if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = 100; // ������������ ����������� � ����� �� ������
                StorageMaxCapacityText.text = ProductCount + " ��������";
                StorageUpgradeText.text = "��������";
            }
        }
    }

    //void FoodNeedTemperature() // ����������/���������� ������������� ��������� �� �����������
    //{
    //    if(GM.WagoneData[IndexWag].TemperatureWagone >=0 & GM.WagoneData[IndexWag].TemperatureWagone < 10)
    //    {
    //        ProductCount -= (ProductCount / 100 * 30);
    //        FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���.";
    //    }
    //    if (GM.WagoneData[IndexWag].TemperatureWagone >= 10 & GM.WagoneData[IndexWag].TemperatureWagone < 20)
    //    {
    //        FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���.";
    //    }
    //    if (GM.WagoneData[IndexWag].TemperatureWagone >= 20)
    //    {
    //        ProductCount += (ProductCount / 100 * 30);
    //        FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���.";
    //    }
    //}

    IEnumerator FoodWorkWagone() // �������� ������������ ������ (���\����)
    {
        while (true)
        {
            if(GM.WagoneData[IndexWag].WorkerInWagone >= 1 & GM.WagoneData[IndexWag].TemperatureWagone >= 0) // ���� 1 � ������ �������� ���������� � ����������� ���� 0
            {
                GM.WagoneData[IndexWag].TimerActiveProduction--;
                FoodTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) // ���� �������� ������ 0,
                {
                    DoneProductionPan.SetActive(true); // ��������� ������ ����� �������
                    break;
                }
                if(GM.WagoneData[IndexWag].TimerActiveProduction > 0) // ���� �������� ������ ��������
                {
                    //GM.WagoneData[IndexWag].TimerActiveProduction--;
                    WagoneWorkData();
                    //FoodNeedTemperature(); // ����������/���������� ������������� ��������� �� �����������
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
    IEnumerator BoilerWorkWagone() // �������� ������������ ����� ��������� 
    {
        while (true)
        {
            if(GM.WagoneData[IndexWag].WorkerInWagone >= 1 & GM.Coal >= ProductCount)
            {
                GM.WagoneData[IndexWag].TimerActiveProduction--;
                BoilerTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) // ���� �������� ������ 0,
                {
                    GM.Coal -= ProductCount;
                    GM.ResourceTextUpdate();
                    DoneProductionPan.SetActive(true); // ��������� ������ ����� �������
                    break;
                }
                if (GM.WagoneData[IndexWag].TimerActiveProduction > 0) // ���� �������� ������ ��������
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
    IEnumerator PassFoodNeed() // �������� ���� �����
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
                GM.StartMessage("����� �� ������� ����!");
                NeedFoodPeople = GM.WagoneData[IndexWag].WorkerInWagone - GM.Food;
                CicleNeedFood++;
                GM.StartPlusFood(0 - GM.Food);
                GM.Food = 0;
                GM.ResourceTextUpdate();
                if (CicleNeedFood >= 7) // ���� 7 ������ � ������ ���� ��� ���
                {
                    GM.AllPeople -= NeedFoodPeople; // �������� �� ���� ����������� �����, ���������� �����������
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
                    //GM.CountPeopleTrain(NeedFoodPeople, NeedFoodPeople); // ������� ���������� ��������� ���������� � ������ � ����� �����
                    CicleNeedFood = 0;
                    GM.PostWagonePeople();
                }
                yield return new WaitForSeconds(10);
            }
        }
    }
    IEnumerator PassWaterNeed() // �������� ���� �����
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
                GM.StartMessage("����� �� ������� ����!");
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
    IEnumerator WarmInWagone() // ����������� ����� ������
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
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if(GM.Warm < 10)
                {
                    SnowWagone.SetActive(false);
                    FoodTermometrText.text = GM.TemperatureOnStreet + "�C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "�C";
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
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm>=10 & GM.Warm < 20)
                {
                    GM.Warm -= 10;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 10);
                    GM.WagoneData[IndexWag].TemperatureWagone = 0;
                    SnowWagone.SetActive(false);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if(GM.Warm < 10)
                {
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    FoodTermometrText.text = GM.TemperatureOnStreet + "�C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "�C";
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    GM.StartMessage("����� ���������!");
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
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 20 & GM.Warm < 30)
                {
                    GM.Warm -= 20;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 20);
                    SnowWagone.SetActive(false);
                    GM.WagoneData[IndexWag].TemperatureWagone = 0;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 10 & GM.Warm < 20)
                {
                    GM.Warm -= 10;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 10);
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    GM.StartMessage("����� ���������!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if(GM.Warm < 10)
                {
                    GM.WagoneData[IndexWag].TemperatureWagone = -20;
                    GM.StartMessage("����� ���������!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.TemperatureOnStreet + "�C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "�C";
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
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 30 & GM.Warm < 40)
                {
                    GM.Warm -= 30;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 30);
                    SnowWagone.SetActive(false);
                    GM.WagoneData[IndexWag].TemperatureWagone = 0;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 20 & GM.Warm < 30)
                {
                    GM.Warm -= 20;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 20);
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    GM.StartMessage("����� ���������!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm < 10)
                {
                    GM.WagoneData[IndexWag].TemperatureWagone = -30;
                    GM.StartMessage("����� ���������!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.TemperatureOnStreet + "�C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "�C";
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
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 35 & GM.Warm < 50)
                {
                    GM.Warm -= 35;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 35);
                    SnowWagone.SetActive(false);
                    GM.WagoneData[IndexWag].TemperatureWagone = 0;
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm >= 20 & GM.Warm < 35)
                {
                    GM.Warm -= 20;
                    GM.ResourceTextUpdate();
                    GM.StartPlusWarm(0 - 20);
                    GM.WagoneData[IndexWag].TemperatureWagone = -10;
                    GM.StartMessage("����� ���������!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                    yield return new WaitForSeconds(10);
                }
                if (GM.Warm < 10)
                {
                    GM.WagoneData[IndexWag].TemperatureWagone = -40;
                    GM.StartMessage("����� ���������!");
                    SnowWagone.SetActive(true);
                    FoodTermometrText.text = GM.TemperatureOnStreet + "�C";
                    PassTermometrText.text = GM.TemperatureOnStreet + "�C";
                    yield return new WaitForSeconds(10);
                }
            }
        }
    }

    public void ResetWagone() // ����� ������ �� ��������.
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
        WagoneName.text = "������";
        if (GM.WagoneData[IndexWag].WorkerInWagone >= 1)
        {
            GM.Worker += GM.WagoneData[IndexWag].WorkerInWagone;
            GM.WagoneData[IndexWag].WorkerInWagone = 0;
        }
    }

    public void UpdateWagone() // ��������� ������
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
                GM.StartMessage("�� ������� �����!");
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
                GM.StartMessage("�� ������� �����!");
            }
        }
    }


    // ADS!!!
    public void ExampleOpenRewardAd(int id)
    {
        // �������� ����� �������� ����� �������
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
