using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private Game GM;
    [SerializeField] private Questions QS;
    [SerializeField] private PassengersScript PS;
 
    [SerializeField] private UnityEngine.UI.Text SaveText; //
    [SerializeField] private GameObject DeletePan;
    [HideInInspector] public bool Saver;
    [HideInInspector] public bool SaverNew;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    public void GetLoad()
    {
        Saver = YandexGame.savesData.Saver;
        SaverNew = YandexGame.savesData.SaverNew;
        if (Saver) GM.updatePan.SetActive(true);
        if (SaverNew == true)
        {
            GM.WagonCol = YandexGame.savesData.WagonCol;
            GM.TextureTrain = YandexGame.savesData.TextureTrain;
            for (int i = 0; i < GM.Texture.Length; i++)
            {
                GM.Texture[i] = YandexGame.savesData.Texture[i];
            }
            //
            GM.Money = YandexGame.savesData.Money;
            GM.Diamond = YandexGame.savesData.Diamond;
            GM.Score = YandexGame.savesData.Score;
            GM.Coal = YandexGame.savesData.Coal;
            GM.Food = YandexGame.savesData.Food;
            GM.Water = YandexGame.savesData.Water;
            GM.Warm = YandexGame.savesData.Warm;
            GM.FreeWorker = YandexGame.savesData.Worker;
            GM.AllWorker = YandexGame.savesData.AllWorker;
            //
            GM.TemperatureOnStreet = YandexGame.savesData.TemperatureOnStreet;
            GM.NextStationTime = YandexGame.savesData.NextStationTime;
            GM.NextStationTimeCount = YandexGame.savesData.NextStationTimeCount;
            GM.LevelLoco = YandexGame.savesData.LevelLoco;
            GM.ActiveTimerLoco = YandexGame.savesData.ActiveTimerLoco;
            GM.LevelEngine = YandexGame.savesData.LevelEngine;
            GM.LevelCoalStorage = YandexGame.savesData.LevelCoalStorage;
            GM.LevelChassis = YandexGame.savesData.LevelChassis;
            //
            GM.CoalHelp = YandexGame.savesData.CoalHelp;
            GM.helpPass = YandexGame.savesData.helpPass;
            GM.helpRepair = YandexGame.savesData.helpRepair;
            GM.helpBarrier = YandexGame.savesData.helpBarrier;
            //
            GM.passCount = YandexGame.savesData.passCount;
            GM.passWagoneCount = YandexGame.savesData.passWagoneCount;
            //
            GM.CargoTransportCount = YandexGame.savesData.CargoTransportCount;
            GM.RewardCargoTransport = YandexGame.savesData.RewardCargoTransport;
            GM.targetDeliveryCity = YandexGame.savesData.targetDeliveryCity;
            //
            GM.CargoSpecTransportCount = YandexGame.savesData.CargoSpecTransportCount;
            //
            GM.CargoSpec1TransportCount = YandexGame.savesData.CargoSpec1TransportCount;
            //
            GM.City = YandexGame.savesData.City;
            GM.AirShipActive = YandexGame.savesData.AirShipActive; //
            GM.TimerAirShip = YandexGame.savesData.TimerAirShip; //
            GM.Trainer[0] = YandexGame.savesData.Trainer[0];
            GM.Trainer[1] = YandexGame.savesData.Trainer[1];
            GM.TaskCount = YandexGame.savesData.TaskCount;
            GM.TimeInGameStatistic = YandexGame.savesData.TimeInGameStatistic; //
            GM.CoalPlusStatistic = YandexGame.savesData.CoalPlusStatistic; //
            GM.FoodPlusStatistic = YandexGame.savesData.FoodPlusStatistic; //
            GM.WaterPlusStatistic = YandexGame.savesData.WaterPlusStatistic; //
            GM.WarmPlusStatistic = YandexGame.savesData.WarmPlusStatistic; //
            GM.MoneyPlusStatistic = YandexGame.savesData.MoneyPlusStatistic; //
            GM.DistancePlusStatistic = YandexGame.savesData.DistancePlusStatistic;
            GM.Thanks = YandexGame.savesData.Thanks;
            for (int i = 0; i < GM.WagonCol; i++)
            {
                GM.WagoneData[i].WagoneActive = false;// YandexGame.savesData.WagoneActive[i];
                GM.WagoneData[i].Name = YandexGame.savesData.Name[i];
                GM.WagoneData[i].LevelWagone = YandexGame.savesData.LevelWagone[i];
                GM.WagoneData[i].TimerActiveProduction = YandexGame.savesData.TimerActiveProduction[i];
                GM.WagoneData[i].WorkerInWagone = YandexGame.savesData.WorkerInWagone[i];
                GM.WagoneData[i].TemperatureWagone = YandexGame.savesData.TemperatureWagone[i];
            }
            for(int i = 0; i < QS.quest.Count; i++)
            {
                QS.quest[i].doneTask = YandexGame.savesData.doneTask[i];
            }
            for(int i = 0; i< PS.pass.Count; i++)
            {
                PS.pass[i].statusPass = YandexGame.savesData.statusPass[i]; 
            }
            GM.AO.activeSound = YandexGame.savesData.activeAudio;
            GM.ScoreMenu.text = "Очки: " + GM.Score;
            if (YandexGame.EnvironmentData.reviewCanShow == false)
            {
                GM.OffFeedback();
            }
        }
    }

    public void Save()
    {
        if (SaverNew == false)
        {
            SaverNew = true;
            YandexGame.savesData.SaverNew = SaverNew;
        }
        YandexGame.savesData.WagonCol = GM.WagonCol;
        YandexGame.savesData.TextureTrain = GM.TextureTrain;
        for (int i = 0; i < GM.Texture.Length; i++)
        {
            YandexGame.savesData.Texture[i] = GM.Texture[i];
        }
        //
        YandexGame.savesData.Money = GM.Money;
        YandexGame.savesData.Diamond = GM.Diamond;
        YandexGame.savesData.Score = GM.Score;
        YandexGame.savesData.Coal = GM.Coal;
        YandexGame.savesData.Food = GM.Food;
        YandexGame.savesData.Water = GM.Water;
        YandexGame.savesData.Warm = GM.Warm;
        YandexGame.savesData.Worker = GM.FreeWorker;
        YandexGame.savesData.AllWorker = GM.AllWorker;
        //
        YandexGame.savesData.TemperatureOnStreet = GM.TemperatureOnStreet;
        YandexGame.savesData.NextStationTime = GM.NextStationTime;
        YandexGame.savesData.NextStationTimeCount = GM.NextStationTimeCount;
        YandexGame.savesData.LevelLoco = GM.LevelLoco;
        YandexGame.savesData.ActiveTimerLoco = GM.ActiveTimerLoco;
        YandexGame.savesData.LevelEngine = GM.LevelEngine;
        YandexGame.savesData.LevelCoalStorage = GM.LevelCoalStorage;
        YandexGame.savesData.LevelChassis = GM.LevelChassis;
        //
        YandexGame.savesData.CoalHelp = GM.CoalHelp;
        YandexGame.savesData.helpPass = GM.helpPass;
        YandexGame.savesData.helpRepair = GM.helpRepair;
        YandexGame.savesData.helpBarrier = GM.helpBarrier;
        //
        YandexGame.savesData.passCount = GM.passCount;
        YandexGame.savesData.passWagoneCount = GM.passWagoneCount;
        //
        YandexGame.savesData.CargoTransportCount = GM.CargoTransportCount;
        YandexGame.savesData.RewardCargoTransport = GM.RewardCargoTransport;
        YandexGame.savesData.targetDeliveryCity = GM.targetDeliveryCity;
        //
        YandexGame.savesData.CargoSpecTransportCount = GM.CargoSpecTransportCount;
        //
        YandexGame.savesData.CargoSpec1TransportCount = GM.CargoSpec1TransportCount;
        //
        YandexGame.savesData.City = GM.City;
        YandexGame.savesData.Trainer[0] = GM.Trainer[0];
        YandexGame.savesData.Trainer[1] = GM.Trainer[1];
        YandexGame.savesData.TaskCount = GM.TaskCount;
        YandexGame.savesData.AirShipActive = GM.AirShipActive; //
        YandexGame.savesData.TimerAirShip = GM.TimerAirShip; //
        YandexGame.savesData.TimeInGameStatistic = GM.TimeInGameStatistic; //
        YandexGame.savesData.CoalPlusStatistic = GM.CoalPlusStatistic; //
        YandexGame.savesData.FoodPlusStatistic = GM.FoodPlusStatistic; //
        YandexGame.savesData.WaterPlusStatistic = GM.WaterPlusStatistic; //
        YandexGame.savesData.WarmPlusStatistic = GM.WarmPlusStatistic; //
        YandexGame.savesData.MoneyPlusStatistic = GM.MoneyPlusStatistic; //
        YandexGame.savesData.DistancePlusStatistic = GM.DistancePlusStatistic; //
        YandexGame.savesData.Thanks = GM.Thanks;
        YandexGame.savesData.activeAudio = GM.AO.activeSound;
        for (int i = 0; i < GM.WagonCol; i++)
        {
            YandexGame.savesData.WagoneActive[i] = GM.WagoneData[i].WagoneActive;
            YandexGame.savesData.Name[i] = GM.WagoneData[i].Name;
            YandexGame.savesData.LevelWagone[i] = GM.WagoneData[i].LevelWagone;
            YandexGame.savesData.TimerActiveProduction[i] = GM.WagoneData[i].TimerActiveProduction;
            YandexGame.savesData.WorkerInWagone[i] = GM.WagoneData[i].WorkerInWagone;
            YandexGame.savesData.TemperatureWagone[i] = GM.WagoneData[i].TemperatureWagone;
        }
        for (int i = 0; i < QS.quest.Count; i++)
        {
            YandexGame.savesData.doneTask[i] = QS.quest[i].doneTask;
        }
        for (int i = 0; i < PS.pass.Count; i++)
        {
            YandexGame.savesData.statusPass[i] = PS.pass[i].statusPass;
        }
        YandexGame.SaveProgress();
        YandexGame.NewLeaderboardScores("LeaderBoard", GM.Score);
        StartCoroutine(SaveColor());
    }

    IEnumerator SaveColor()
    {
        SaveText.color = new Color(0, 255, 0, 255);
        yield return new WaitForSeconds(2);
        SaveText.color = new Color(0, 0, 0, 255);
        yield break;
    }

    public void StartAutoSave()
    {
        StartCoroutine(AutoSave());
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
        GM.AO.PlayAudioClickBttn();
    }
    public void OpenClosedDeleteSave()
    {
        if (!DeletePan.activeInHierarchy) DeletePan.SetActive(true);
        else DeletePan.SetActive(false);
        GM.AO.PlayAudioClickBttn();
    }
}
