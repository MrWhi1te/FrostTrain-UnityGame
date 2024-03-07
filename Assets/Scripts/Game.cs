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

    public List<WagoneData> WagoneData = new List<WagoneData>(); // ���� � ������� �������
    public int WagonCol; // ���������� �������� �������
    [SerializeField] private GameObject Station; //
    [SerializeField] private GameObject GamePan; //
    [SerializeField] private Sprite[] SPR0; // ��� ������ ��������
    [SerializeField] private Sprite[] SPR1; // ��� ������ �������
    [SerializeField] private Sprite[] SPR2; // ��� ������ �������
    [SerializeField] private Sprite[] SPR3; // ��� ������ ����������
    public int TextureTrain; // ��������� ��� ������
    public bool[] Texture; // 1-������� ����� / 2-������� ����� / 3-����������
    
    [Header("�������")]
    public int Money; // ���������� �����
    public int Diamond; // �������
    public int Score; // ����
    public int Coal; // ���������� ����
    public int CoalMax; // �������� ���� ��� ��������
    public int FreeWorker; // ���������� ��������� �������
    public int AllWorker; // ���������� ����� ������� � ������
    public int Warm; // ���������� �����
    public int WarmMax; // �������� ����� ��� ��������
    public int Food; // ���������� ���
    public int FoodMax; // �������� ��� ��� ��������
    public int Water; // ���������� ����
    public int WaterMax; // �������� ���� ��� ��������
    [HideInInspector] public int[] PassResourceUse; // �������������� ����������� ������� �����������
    public GameObject[] CollectResources; // 
    [SerializeField] private Text MoneyText; // ����� �����
    [SerializeField] private Text CoalText; // ����� ����
    [SerializeField] private Text WorkerText; // ����� ��������� �������
    [SerializeField] private Text WarmText; // ����� �����
    [SerializeField] private Text FoodText; // ����� ���
    [SerializeField] private Text WaterText; // ����� ����
    [SerializeField] private Text DiamondText; // ������� �����
    [SerializeField] private Text[] plusResourceText; // 0-money / 1-Coal / 2-Food / 3-Water / 4-Warm
    [SerializeField] private Slider[] SliderResource; // ������� �������� (� ������� � ����)
    private int StorageCount = 50; // ������� ���������
    
    [Header("�����������")]
    [HideInInspector] public int TemperatureOnStreet; // ����������� �� ������
    [SerializeField] private Text TermometrText; // ����� ���������� ����������� �� ������
   
    [Header("�������������")]
    public Slider NextStationSlide; // ������� (��������) ��������� �������
    public Text NextStationTimeText; // ����� ������� �� ��������� �������
    public int NextStationTime; // ������ �� ��������� �������
    public int NextStationTimeCount; //
    [SerializeField] private Button TimerX2Bttn; //
    [SerializeField] private GameObject TimerX2Pan; //
    private float Timer = 1; //
    
    [Header("���������")]
    [SerializeField] private Text LevelLocoText; // ����� ������ ����������
    [SerializeField] private Text StorageCoalText; // ����� ��������� ����
    [SerializeField] private Text NeedCoalText; // ����� ����������� ���� �����������
    [SerializeField] private Text MaxWagoneText; // ����� ������������ ���������� �������
    [SerializeField] private GameObject LocoPan; // ������ ����������
    [SerializeField] private GameObject UpgradeLocoPan; // ������ ��������� ����������
    [SerializeField] private GameObject[] TextureChoice; //
    [SerializeField] private GameObject SmokeParticle; //
    public int LevelLoco = 1; // ������� ����������. ������� ������� �� ���� ����������. ���������� �� �������
    public int MaxWagone; // ������������ ���-�� �������
    [SerializeField] private Image LocoSprite; // 
    private int NeedCoal;// ������� ���������� ����
    private int TimerLoco;// ������ �� ������� ���������� �����
    [HideInInspector] public int ActiveTimerLoco; // �������� ������ ����������
    
    [Header("CoalFree")]
    [SerializeField] private GameObject CoalADS; // ������ ���� �� �������
    [SerializeField] private GameObject StartLocoEnginePan; // ������ ���������
    [SerializeField] public GameObject TreesClickParticle; //
    [SerializeField] private GameObject TreesFreeObj; //
    public GameObject CoalHelpObj; //
    public bool CoalHelp; // ��������� ��� ������ ��������� ����

    [Header("Message")]
    [SerializeField] private Text Message; // ����� ���������
   
    [Header("��������� ����������")]
    [SerializeField] private Text LevelEngineText; // ������� �����
    [SerializeField] private Text LevelCoalStorageText; // ������� �������
    [SerializeField] private Text LevelChassisText; // ������� �����
    [HideInInspector] public int LevelEngine = 1; // ������� ��������� ����������
    private int LevelCostEngine; // ��������� ��������� ���������
    [HideInInspector] public int LevelCoalStorage = 1; // ������� ������� ����������
    private int LevelCostCoalStorage; // ��������� ��������� ������� ���� 
    [HideInInspector] public int LevelChassis = 1; // ������� ����� ����������
    private int LevelCostChassis; // ��������� ��������� ����� ���� 

    [Header("Passengers")]
    public int passWagoneCount;
    public int passCount;
    public bool helpPass;
    [SerializeField] private GameObject[] passWagon; 

    [Header("CargoTrasport")]
    public GameObject[] CargoTransportWagone; // ������� ������� ������� ���������
    [HideInInspector] public int CargoTransportCount; // ���������� �������
    [HideInInspector] public int RewardCargoTransport; // ������� �� ��������
   
    [Header("CargoSpecTrasport")]
    public GameObject[] CargoSpecTransportWagone; // ������� ������� ������� ���������
    [HideInInspector] public int CargoSpecTransportCount; // ���������� �������
   
    [Header("CargoSpec1Trasport")]
    public GameObject[] CargoSpec1TransportWagone; // ������� ������� ������� ���������
    [HideInInspector] public int CargoSpec1TransportCount; // ���������� �������
   
    [Header("�����")]
    [HideInInspector] public int City; // ������� �����
    [HideInInspector] public int ChoiceCity; // ����� ������
    public string[] NameCity; // �������� ���������� ������
    public int[] TimeForCity; // ����� �� ���������� ������
    public int[] TemperatureForCity; // ����������� �� ���������� ������
   
    [Header("Background")]
    [HideInInspector] public int SpeedFon; // �������� �������� ����
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
    [SerializeField] private GameObject ADSMoneyActivePan; // ������ ����������� ����� �� �������
    [SerializeField] private Slider ADSMoneyActiveSlide; // ������� ������� ����� �� �������
    [SerializeField] private Text ADSMoneyActiveText; // ����� ����� �� �������
    private int ADSMoneyCol; // ������� ����� �� �������
    
    [Header("Menu")]
    [SerializeField] private GameObject MenuPan; //
    public Text ScoreMenu; // 
    
    //
    [SerializeField] private GameObject TaskPan; // ��������� ������ �������
    [SerializeField] private Button TaskDone; // ���� ������� ���������, �� ������� �������
    [SerializeField] private Text TaskText; // ����� �������
    public int TaskCount; // ������� �������
    
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

    [HideInInspector] public GameObject PanWagon; // �������� ������ ������
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
        if (CargoTransportCount >= 1) // ���� ������� �������� �������
        {
            for (int i = 0; i < CargoTransportCount; i++)
            {
                CargoTransportWagone[i].SetActive(true);
                CargoTransportWagone[i].GetComponent<Image>().sprite = wagoneSprites["Cargo"][TextureTrain];
            }
        }
        if (CargoSpecTransportCount >= 1) // ���� ������� �������� Spec �������
        {
            for (int i = 0; i < CargoSpecTransportCount; i++)
            {
                CargoSpecTransportWagone[i].SetActive(true);
            }
        }
        if (CargoSpec1TransportCount >= 1) // ���� ������� �������� Spec �������
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
    public void TextLoco() // ���������� ������� ������ ����������
    {
        LevelLocoText.text = LevelLoco.ToString();
        StorageCoalText.text = Coal + "��./ " + CoalMax + "��.";
        MaxWagoneText.text = WagonCol + " �� " + MaxWagone;
        NeedCoalText.text = NeedCoal + "��. ��: " + TimerLoco + "���.";
        TermometrText.text = TemperatureOnStreet + "�C";
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
    public void LevelLocoUpdater() // ������� ����������, ������
    {
        NeedCoal = coalNeedEngine[LevelLoco, LevelEngine];
        LevelCostEngine = 500 * LevelLoco * LevelEngine;
        
        if (LevelEngine < 5) LevelEngineText.text = "��������� " + LevelEngine + ". ���������: " + LevelCostEngine + "� -����������� ����";
        else LevelEngineText.text = "������������ ������� �����";


        MaxWagone = maxWagonLoco[LevelLoco, LevelChassis];
        LevelCostChassis = 500 * LevelLoco * LevelChassis;

        if (LevelChassis < 5) LevelChassisText.text = "��������� " + LevelChassis + ". ���������: " + LevelCostChassis + "� +��� ���������� �������";
        else LevelChassisText.text = "������������ ������� �����";


        CoalMax = storageCoalLoco[LevelLoco, LevelCoalStorage];
        LevelCostCoalStorage = 500 * LevelLoco * LevelCoalStorage;

        if(LevelCoalStorage < 5) LevelCoalStorageText.text = "��������� " + LevelCoalStorage + ". ���������: " + LevelCostCoalStorage + "� +��������� ����";
        else LevelCoalStorageText.text = "������������ ������� �������";
    }

    public void ClickLocoPan() // �������� � �������� ������ ����������
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
    public void OpenClosedLocoUpgradePan() // �������� ������ ��������� ����������
    {
        if(!UpgradeLocoPan.activeInHierarchy) UpgradeLocoPan.SetActive(true);
        else UpgradeLocoPan.SetActive(false);
        AO.PlayAudioClickTrain();
    }
    public void ResourceTextUpdate() // ���������� ������� � �������� �������� 
    {
        MoneyText.text = Money + "�";
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
    public void BuyAddWagone() // ������� � ���������� ������� ������
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
    public void MaxStorageWagone() // ������������ ����������� ��������. ������� �� �������� �������
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

    // ������ �� ��������� �������
    IEnumerator TimerNextStation() //
    {
        int timeBackground = 0; // ������ �������� ����
        int timeADS = 0; // ������ ����� �� �������
        int timeBarrier = UnityEngine.Random.Range(20, 600); // ������ ������
        int timeRepair = UnityEngine.Random.Range(20, 600); //  ������ ������� ������
        NextStationSlide.maxValue = NextStationTimeCount;
        ParticleTrain.SetActive(false); ParticleTrain.SetActive(true);
        AO.PlayAudioTrain();
        
        while (true)
        {
            timeBarrier--;
            timeRepair--;
            NextStationTime--;
            DistancePlusStatistic++;
            NextStationTimeText.text = (NextStationTime * 100) + " ����.";
            NextStationSlide.value = NextStationTimeCount - NextStationTime;
            
            if (timeBarrier <= 0)
            {
                AO.StopAudio();
                SpeedFon = 0;
                StartMessage("����������� �� ����!");
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
                StartMessage("����� ��������!");
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
                    ADSMoneyActiveText.text = "+" + ADSMoneyCol + "�!!!" + "\n" + "�� ��������";
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
                StartCoroutine(MessageView("����������� �������!"));
                break;
            }
        }
    }

    public void StartEngineLoco() // ������ ������, ���� ���������� ����
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
        else { StartCoroutine(MessageView("�� ������� �������!")); }
    }
    public void StartMessage(string mes) // ������ ������ ��������� �� ������� ������
    {
        StartCoroutine(MessageView(mes));
    }
    IEnumerator MessageView(string Mes) // ����� ���������
    {
        Message.text = Mes;
        yield return new WaitForSeconds(3);
        Message.text = "";
        yield break;
    }

    IEnumerator PassFoodNeed() // �������� ���� �����
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
                StartMessage("����� �� ������� ����!");
                NeedFoodPeople = (AllWorker + PassResourceUse[0]) - Food;
                CicleNeedFood++;
                PlusResources(2, 0 - Food);
                Food = 0;
                ResourceTextUpdate();
                if (CicleNeedFood >= 4) // ���� 4 ������ � ������ ���� ��� ���
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
    IEnumerator PassFoodWater() // �������� ���� �����
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
                StartMessage("����� �� ������� ����!");
                NeedFoodPeople = AllWorker + PassResourceUse[1] - Water;
                CicleNeedFood++;
                PlusResources(3, 0 - Water);
                Water = 0;
                ResourceTextUpdate();
                if (CicleNeedFood >= 7) // ���� 7 ������ � ������ ���� ��� ���
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

    public void ViewLoco() // ��� ���������� �� ������ � ��������� ��������
    {
        LocoSprite.sprite = LocoSprites[LevelLoco][TextureTrain];
    }
    public void UpdateViewTrain() // ���������� ���� �������
    {
        for (int i = 0; i < WagonCol; i++)
        {
            WagoneData[i].WagoneImage = wagoneSprites[WagoneData[i].Name][TextureTrain];
        }
    }
    public void ChoiceViewTrain(int index) // ����� ���� ������!
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
            TimeInGameStatisticText.text = "����� � ����:" + H + "�. " + M + "�.";
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
        // �������� ����� �������� ����� �������
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

    public void OpenClosedShopPan() // �������� ������ ��������
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

    public void ExchangeDiamond() // ����� ������� �� ������
    {
        if (Diamond >= 1)
        {
            Money += 1500;
            Diamond--;
            PlusResources(0, 1500);
        }
    }

    public void OpenClosedTiemrX2Pan() // �������� �������� ������ ��������� ������� �� �������
    {
        if (TimerX2Pan.activeInHierarchy) TimerX2Pan.SetActive(false);
        else TimerX2Pan.SetActive(true);
        AO.PlayAudioClickBttn();
    }
    IEnumerator TimerX2() // ��������� ������� �� ������
    {
        Timer = 0.5f;
        TimerX2Bttn.interactable = false;
        yield return new WaitForSeconds(60);
        Timer = 1;
        TimerX2Bttn.interactable = true;
        yield break;
    }

    public void ADSCoal() // ���������� ���� �� �������� �������!
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

    public void TakeADSMoney() // ����� ������ �� �������
    {
        Money += ADSMoneyCol;
        PlusResources(0, ADSMoneyCol);
        ADSMoneyActivePan.SetActive(false);
        ResourceTextUpdate();
        AO.PlayAudioTakeResource();
    }
    IEnumerator ADSMoneyActive() // ���������� ������� �� ���� �����
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

    
    // �������� �� ������ ����������� � ��������� ��������!
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
    public void ClickAirShipPan() // �������� � �������� ������ ����������
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
            TimerAirShipText.text = "��������: "+ Min + "���. " + Sec + "���.";
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
        new Tasks { index = 0, text = "�������� ������ ����������", reward = 1000},
        new Tasks { index = 2, text = "�������� ����� ������", reward = 150},
        new Tasks { index = 4, text = "��������� ������� �� ��������", reward = 600},
        new Tasks { index = 6, text = "�������� �����", reward = 1000},
        new Tasks { index = 8, text = "�������� ����� �� �������", reward = 300},
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
                TaskText.text = "������� �������";
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
    IEnumerator TimerInGame() // ������� ������� � ����!
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
public class WagoneData // ������ ������
{
    public bool WagoneActive; // ��������� ������� ������
    public GameObject WagoneObj; // ��������� ������� ������
    public string Name; // �������� ������ (����������)
    public int LevelWagone; // ������� ������
    public int TimerActiveProduction; // �������� ����� ��� ������� ������������
    public int WorkerInWagone; // ���������� �������� ���������� � ����������� � ���� � ������ (���. ���������� � �������)
    public int TemperatureWagone; // ����������� � ������
    public Sprite WagoneImage; // ������ ������ (������� ���)
}
