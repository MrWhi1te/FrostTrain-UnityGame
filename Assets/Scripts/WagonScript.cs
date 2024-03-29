using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WagonScript : MonoBehaviour
{
    [SerializeField] private Game GM; // 
    [SerializeField] private Audio AO;
    
    [Header("�����.������")]
    public GameObject ChoiceWagonePan; // ������ ������ ������ ���������� ������
    public GameObject FoodWaterWagonePan; // ������ ��� ��� ����
    public GameObject PassWagonePan; // ������ ������������� ������
    public GameObject StorageWagonePan; // ������ ������ ������
    public GameObject ProductionWagonePan; // ������ ����������������� ������
    public GameObject BoilerWagonePan; // ������ ���������
    public Text WagoneName; // �������� ������ �� ���.
    [SerializeField] private GameObject DoneProductionPan; // ������ ���������� ��������� ��� �����
    public GameObject SnowWagone; // ������ ���������� ������
    
    [Header("�������������")]
    [SerializeField] private Text FoodWagoneNameText; // �������� ������ ��� ��� ����
    [SerializeField] private Text FoodLevelText; // ������� ������
    [SerializeField] private Text FoodProdText; // ��� ���������� ����� (��� ��� ����)
    [SerializeField] private Text FoodWorkCountText; // ���������� ���������� � ������
    [SerializeField] private Text FoodProdCountText; // ������� ��������� � �� ����� ����� ����������
    [SerializeField] private Text FoodTermometrText; // ����������� � ������
    [SerializeField] private Text FoodTimerText; // ������ ������������
    [SerializeField] private Text FoodUpgradeText; // ��������� ������ ���������
    [SerializeField] private Text FoodNeedWarmText; // 
    
    [Header("����������")]
    [SerializeField] private Text PassLevelText; // ������� ������
    [SerializeField] private Text PassMaxCapacityText; // ������������ ����������� ������
    [SerializeField] private Text PassTermometrText; // ����������� � ������
    [SerializeField] private Text PassUpgradeText; // ��������� ������ ���������
    [SerializeField] private Text PassNeedFoodText; //
    [SerializeField] private Text PassNeedWaterText; //
    [SerializeField] private Text PassNeedWarmText; //
    
    [Header("���������������")]
    [SerializeField] private Text StorageLevelText; // ������� ������
    [SerializeField] private Text StorageMaxCapacityText; // ������������ ����������� ������
    [SerializeField] private Text StorageCapacityText; // ������� �� ������ �������
    [SerializeField] private Text StorageUpgradeText; // ��������� ������ ���������
    
    [Header("���������������")]
    [SerializeField] private Text BoilerLevelText; // ������� ������
    [SerializeField] private Text BoilerWorkCountText; // ���������� ���������� � ������
    [SerializeField] private Text BoilerProdCountText; // ������� ��������� � �� ����� ����� ����������
    [SerializeField] private Text BoilerTimerText; // ������ ������������
    [SerializeField] private Text BoilerUpgradeText; // ��������� ������ ���������
    [SerializeField] private Text BoilerNeedCoalText; //
    
    [Header("��� ������")]
    [SerializeField] private Image WagoneImage; // ��� ������

    [Header("����������������������")]
    private int IndexWag; // ������ ������
    private int TimerWag; // ������ ��� ������������ (������� ������� ����������)
    private int ProductCount; // ���������� ������ ������������


    private void OnEnable()
    {
        for (int i = 0; i < GM.WagoneData.Capacity; i++) // �������� �� ����� ������ ������ ������
        {
            if (!GM.WagoneData[i].WagoneActive) // ���� ����� �� �������, �� �����������
            {
                IndexWag = i;
                GM.WagoneData[IndexWag].WagoneActive = true;
                if (GM.WagoneData[IndexWag].Name != "") // ���� ��� �� �����, �� ������� ������
                {
                    WagoneDataCount();
                    WagoneWorkData();
                    if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water")
                    {
                        StartCoroutine(FoodWorkWagone()); // ������ �������� ������������ ���/����
                        StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� ������
                    }
                    else if (GM.WagoneData[IndexWag].Name == "Boiler")
                    {
                        StartCoroutine(BoilerWorkWagone()); // ������ �������� ������������ �����
                    }
                    else if (GM.WagoneData[IndexWag].Name == "Pass")
                    {
                        StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� ������
                    }
                    ViewWagone();
                }
                else if (GM.WagoneData[IndexWag].Name == "")
                {
                    ViewWagone();
                    WagoneName.text = "������";
                }
                break;
            }
        }
    }

    public void ClickOnWagone() // ���� �� ������ � �������� �������
    {
        if (GM.WagoneData[IndexWag].Name == "") // ���� ����� ����
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
        else if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water") // ���� ����� ���\����
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
        else if (GM.WagoneData[IndexWag].Name == "Pass") // ���� ����� ��������
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
        else if (GM.WagoneData[IndexWag].Name == "Storage") // ���� ����� ��������
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
        else if (GM.WagoneData[IndexWag].Name == "Boiler") // ���� ����� ���������
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
        AO.PlayAudioClickTrain();
    }

    // ����� ���������� ������!
    public void ChoiceRoleWagon(int index)
    {
        GM.WagoneData[IndexWag].LevelWagone = 1;
        if (index == 0)
        {
            GM.WagoneData[IndexWag].Name = "Food";
            StartCoroutine(FoodWorkWagone()); // ������ �������� ������������ ���/����
            StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� �����
            if (GM.Trainer[0] == false)
            {
                GM.TR.wagoneTrainerChoice[1] = true;
                GM.TR.TrainingGameWagonActive();
            }
        }
        else if (index == 1)
        {
            GM.WagoneData[IndexWag].Name = "Water";
            StartCoroutine(FoodWorkWagone()); // ������ �������� ������������ ���/����
            StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� �����
            if (GM.Trainer[0] == false)
            {
                GM.TR.wagoneTrainerChoice[2] = true;
                GM.TR.TrainingGameWagonActive();
            }
        }
        else if (index == 2)
        {
            GM.WagoneData[IndexWag].Name = "Pass";
            GM.FreeWorker += 5;
            GM.AllWorker += 5;
            StartCoroutine(WarmInWagone()); // ������ �������� �������� ����������� ������
            if (GM.Trainer[0] == false)
            {
                GM.TR.wagoneTrainerChoice[0] = true;
                GM.TR.TrainingGameWagonActive();
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
                GM.TR.wagoneTrainerChoice[3] = true;
                GM.TR.TrainingGameWagonActive();
            }
        }
        GM.WagoneData[IndexWag].WagoneActive = true;
        WagoneName.text = GM.WagoneData[IndexWag].Name;
        ViewWagone();
        WagoneDataCount();
        WagoneWorkData();
        ChoiceWagonePan.SetActive(false);
        GM.PanWagon = null;
        AO.PlayAudioClickTrain();
    }

    // ���������� � �������� ������� � ������ �����������
    public void AddWork() // ���������� ������� �� ������
    {
        if (GM.WagoneData[IndexWag].WorkerInWagone < (5 * GM.WagoneData[IndexWag].LevelWagone) & GM.FreeWorker >=1)
        {
            GM.WagoneData[IndexWag].WorkerInWagone++;
            GM.FreeWorker--;
            WagoneDataCount();
            WagoneWorkData();
            GM.ResourceTextUpdate();
            AO.PlayAudioClickTrain();
        }
    }
    public void RemoveWork() // �������� ������� � ������ �� 
    {
        if (GM.WagoneData[IndexWag].WorkerInWagone > 0)
        {
            GM.WagoneData[IndexWag].WorkerInWagone--;
            GM.FreeWorker++;
            WagoneDataCount();
            WagoneWorkData();
            GM.ResourceTextUpdate();
            AO.PlayAudioClickTrain();
        }
    }

    //
    public void CollectProduct()
    {
        int Razn = 0;
        GM.EffectCollect.SetActive(false);
        GM.EffectCollect.transform.position = DoneProductionPan.transform.position;
        GM.EffectCollect.SetActive(true);
        GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag;

        if (GM.WagoneData[IndexWag].Name == "Food")
        {
            Razn = GM.FoodMax - GM.Food;
            if (ProductCount < Razn)
            {
                GM.Food += ProductCount;
                GM.FoodPlusStatistic += ProductCount;
                GM.StartPlusResource(2, ProductCount);
            }
            else if ( ProductCount >= Razn)
            {
                GM.Food += Razn;
                GM.FoodPlusStatistic += Razn;
                GM.StartPlusResource(2, Razn);
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
                GM.StartPlusResource(3, ProductCount);
            }
            else if (ProductCount >= Razn)
            {
                GM.Water += Razn;
                GM.WaterPlusStatistic += Razn;
                GM.StartPlusResource(3, Razn);
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
                GM.StartPlusResource(4, ProductCount);
            }
            else if (ProductCount >= Razn)
            {
                GM.Warm += Razn;
                GM.WarmPlusStatistic += Razn;
                GM.StartPlusResource(4, Razn);
            }
            GM.CollectResources[2].SetActive(false); GM.CollectResources[2].SetActive(true);
            StartCoroutine(BoilerWorkWagone());
        }

        GM.ResourceTextUpdate();
        DoneProductionPan.SetActive(false);
        AO.PlayAudioTakeResource();

        if (GM.TaskCount == 2)
        {
            GM.TaskCount = 3;
            GM.TaskCounter();
        }
    }
    void WagoneDataCount() // ���� ������ ������
    {
        WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage;
        if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water") //
        {
            FoodLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            FoodWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
            FoodTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
            if (GM.TemperatureOnStreet == 0) { FoodNeedWarmText.text = "-10"; }
            else if (GM.TemperatureOnStreet < 0) { FoodNeedWarmText.text = GM.TemperatureOnStreet.ToString(); }
            if (GM.WagoneData[IndexWag].Name == "Food")
            {
                FoodProdText.text = "������������ ����:";
                WagoneName.text = "����";
                FoodWagoneNameText.text = "����";
            }
            else if (GM.WagoneData[IndexWag].Name == "Water")
            {
                FoodProdText.text = "������������ ����:";
                WagoneName.text = "����";
                FoodWagoneNameText.text = "����";
            }
        }
        else if (GM.WagoneData[IndexWag].Name == "Pass") //
        {
            WagoneName.text = "�������";
            PassLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
            if (GM.TemperatureOnStreet == 0) { PassNeedWarmText.text = "-10"; }
            else if (GM.TemperatureOnStreet < 0) { PassNeedWarmText.text = GM.TemperatureOnStreet.ToString(); }
            PassNeedFoodText.text = "-" + GM.WagoneData[IndexWag].LevelWagone * 5;
            PassNeedWaterText.text = "-" + GM.WagoneData[IndexWag].LevelWagone * 5;
        }
        else if (GM.WagoneData[IndexWag].Name == "Boiler") //
        {
            WagoneName.text = "���������";
            BoilerLevelText.text = GM.WagoneData[IndexWag].LevelWagone.ToString();
            BoilerWorkCountText.text = GM.WagoneData[IndexWag].WorkerInWagone.ToString();
            BoilerTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
            BoilerNeedCoalText.text = "-" + GM.WagoneData[IndexWag].WorkerInWagone * 10;
        }
        else if (GM.WagoneData[IndexWag].Name == "Storage") //
        {
            WagoneName.text = "�����";
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

    void WagoneWorkData() // ������� ������ ��� ������������ ������ �� ������ � ���-�� ����������
    {
        if (GM.WagoneData[IndexWag].Name == "Food" || GM.WagoneData[IndexWag].Name == "Water")
        {
            if(GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 5; //
                TimerWag = 20;
                FoodUpgradeText.text = "2000�";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 5; //
                TimerWag = 23;
                FoodUpgradeText.text = "4000�";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 10; //
                TimerWag = 32;
                FoodUpgradeText.text = "��������";
            }
            FoodProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
            if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
        }
        else if (GM.WagoneData[IndexWag].Name == "Boiler")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 15;
                BoilerUpgradeText.text = "2000�";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 20; //
                TimerWag = 15;
                BoilerUpgradeText.text = "4000�";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = GM.WagoneData[IndexWag].WorkerInWagone * 25; //
                TimerWag = 20;
                BoilerUpgradeText.text = "��������";
            }
            BoilerProdCountText.text = ProductCount + "��. ��: " + TimerWag + "���."; //
            if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) { GM.WagoneData[IndexWag].TimerActiveProduction = TimerWag; }
        }
        else if (GM.WagoneData[IndexWag].Name == "Pass")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = 5; // ������������ ����������� � ����� �� ������
                PassUpgradeText.text = "2000�";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = 10; // ������������ ����������� � ����� �� ������
                PassUpgradeText.text = "4000�";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = 15; // ������������ ����������� � ����� �� ������
                PassUpgradeText.text = "��������";
            }
            PassMaxCapacityText.text = ProductCount + " �������";
        }
        else if (GM.WagoneData[IndexWag].Name == "Storage")
        {
            if (GM.WagoneData[IndexWag].LevelWagone == 1)
            {
                ProductCount = 50; // ������������ ����������� � ����� �� ������
                StorageUpgradeText.text = "2000�";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 2)
            {
                ProductCount = 75; // ������������ ����������� � ����� �� ������
                StorageUpgradeText.text = "4000�";
            }
            else if (GM.WagoneData[IndexWag].LevelWagone == 3)
            {
                ProductCount = 100; // ������������ ����������� � ����� �� ������
                StorageUpgradeText.text = "��������";
            }
            StorageMaxCapacityText.text = ProductCount + " ��������";
        }
    }

    IEnumerator FoodWorkWagone() // �������� ������������ ������ (���\����)
    {
        while (true)
        {
            if (GM.WagoneData[IndexWag].WorkerInWagone >= 1 & GM.WagoneData[IndexWag].TemperatureWagone >= 0) // ���� 1 � ������ �������� ���������� � ����������� ���� 0
            {
                GM.WagoneData[IndexWag].TimerActiveProduction--;
                FoodTimerText.text = GM.WagoneData[IndexWag].TimerActiveProduction.ToString();
                if (GM.WagoneData[IndexWag].TimerActiveProduction <= 0) // ���� �������� ������ 0,
                {
                    DoneProductionPan.SetActive(true); // ��������� ������ ����� �������
                    break;
                }
                else if (GM.WagoneData[IndexWag].TimerActiveProduction > 0) // ���� �������� ������ ��������
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
                    GM.Coal -= ProductCount / 2;
                    GM.ResourceTextUpdate();
                    DoneProductionPan.SetActive(true); // ��������� ������ ����� �������
                    break;
                }
                else if (GM.WagoneData[IndexWag].TimerActiveProduction > 0) // ���� �������� ������ ��������
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

    IEnumerator WarmInWagone() // ����������� ����� ������
    {
        while (true)
        {
            int r = Mathf.Abs(GM.TemperatureOnStreet);
            if (GM.Warm >= r)
            {
                GM.Warm -= r;
                GM.ResourceTextUpdate();
                GM.StartPlusResource(4, 0 - r);
                GM.WagoneData[IndexWag].TemperatureWagone = 10;
                SnowWagone.SetActive(false);
                FoodTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
                PassTermometrText.text = GM.WagoneData[IndexWag].TemperatureWagone + "�C";
            }
            else if (GM.Warm < r)
            {
                GM.Warm = 0;
                GM.ResourceTextUpdate();
                FoodTermometrText.text = GM.TemperatureOnStreet + "�C";
                PassTermometrText.text = GM.TemperatureOnStreet + "�C";
                GM.WagoneData[IndexWag].TemperatureWagone = -10;
                GM.StartMessage("����� ���������!");
                SnowWagone.SetActive(true);
            }
            yield return new WaitForSeconds(30);
        }
    }

    public void ResetWagone() // ����� ������ �� ��������.
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
        WagoneName.text = "������";
        if (GM.WagoneData[IndexWag].WorkerInWagone >= 1)
        {
            GM.FreeWorker += GM.WagoneData[IndexWag].WorkerInWagone;
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
                GM.StartPlusResource(0, 0 - 2000);
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
                AO.PlayAudioClickTrain();
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
                GM.StartPlusResource(0, 0 - 4000);
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
                AO.PlayAudioClickTrain();
            }
            else
            {
                GM.StartMessage("�� ������� �����!");
            }
        }
    }

    // VIEW WAGONE
    void ViewWagone()
    {
        WagoneImage.sprite = GM.WagoneData[IndexWag].WagoneImage = GM.wagoneSprites[GM.WagoneData[IndexWag].Name][GM.TextureTrain];
    }
}
