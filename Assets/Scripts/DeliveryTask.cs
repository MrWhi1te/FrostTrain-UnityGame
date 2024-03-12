using UnityEngine;
using UnityEngine.UI;

public class DeliveryTask : MonoBehaviour
{
    [SerializeField] private Game GM;
    [SerializeField] private StationScripts ST;
    [SerializeField] private Audio AO;

    private int cargoWagon;
    private int cargoReward;
    private int cityDelivery;
    private int multiplierReward;

    [Header("Delivery")]
    [SerializeField] private GameObject rewardQuestPan; //
    [SerializeField] private Text rewardQuestText; //
    [SerializeField] private Text activeTextQuest; //

    [Header("DeliveryCargo")]
    [SerializeField] private GameObject deliveryObj;
    [SerializeField] private Text deliveryCity;
    [SerializeField] private Text cargoWagoneTransport; //
    [SerializeField] private Text cargoWagoneCoal; //
    [SerializeField] private Text cargoWagoneReward; //

    public void CheckDelivery()
    {
        if (GM.CargoTransportCount >= 1 && GM.City == GM.targetDeliveryCity)
        {
            for (int i = 0; i < GM.CargoTransportCount; i++)
            {
                GM.CargoTransportWagone[i].SetActive(false);
            }
            rewardQuestPan.SetActive(true);
            rewardQuestText.text = "Вы доставили: " + GM.CargoTransportCount + " вагонов. Награда составила: " + GM.RewardCargoTransport + "$";
            if (GM.TaskCount == 4)
            {
                GM.TaskCount = 5;
            }
        }
    }
    public void UpdateDelivery()
    {
        if(GM.CargoTransportCount > 0)
        {
            deliveryObj.SetActive(false);
            deliveryCity.text = "Текущая Доставка до: " + GM.NameCity[cityDelivery];
        }
        else
        {
            if(GM.City == 0 || GM.City == 1)
            {
                cityDelivery = GM.City;
                cityDelivery += Random.Range(1, 3);
            }
            else if(GM.City == 43 || GM.City == 44)
            {
                cityDelivery = GM.City;
                cityDelivery -= Random.Range(1, 3);
            }
            else
            {
                cityDelivery = GM.City;
                cityDelivery += Random.Range(-2, 2);
            }
            multiplierReward = Random.Range(1, 3);
            deliveryObj.SetActive(true);
            CargoTransportationUpdate();
        }
    }


    public void EnterRewardQuest() // Принять награду за доставку
    {
        GM.Money += GM.RewardCargoTransport;
        GM.MoneyPlusStatistic += GM.RewardCargoTransport;
        GM.CargoTransportCount = cargoWagon = 0;
        rewardQuestPan.SetActive(false);
        activeTextQuest.text = "";
        ST.ResourceTextUpdate();
        GM.Score += GM.RewardCargoTransport / 50;
        ST.ScoreCount();
        AO.PlayAudioTakeResource();
    }

    void CargoTransportationUpdate() // Обнвление панели доставки
    {
        deliveryCity.text = "Доставка до: " + GM.NameCity[cityDelivery];
        cargoWagoneTransport.text = cargoWagon.ToString();
        cargoWagoneCoal.text = "-" + cargoWagon * 10;
        cargoWagoneReward.text = cargoReward + "р";
    }
    public void AddCargoTransport() // Добавить вагон доставки
    {
        if (cargoWagon < 10)
        {
            cargoWagon++;
            cargoReward = cargoWagon * (100 * multiplierReward);
            CargoTransportationUpdate();
            AO.PlayAudioClickTrain();
        }
    }
    public void RemoveCargoTransport() // Минус вагон доставки
    {
        if (cargoWagon > 0)
        {
            cargoWagon--;
            cargoReward = cargoWagon * (100 * multiplierReward);
            CargoTransportationUpdate();
            AO.PlayAudioClickTrain();
        }
    }
    public void EnterQuestCargo() // Принять задание
    {
        GM.CargoTransportCount = cargoWagon;
        GM.RewardCargoTransport = cargoReward;
        GM.targetDeliveryCity = cityDelivery;
        if (cargoWagon >= 1)
        {
            activeTextQuest.text = "Активно задание!" + "\n" + "Доставка вагонов: " + cargoWagon + " в " + GM.NameCity[cityDelivery];
            deliveryObj.SetActive(false);
            deliveryCity.text = "Текущая Доставка до: " + GM.NameCity[cityDelivery];
            AO.PlayAudioClickBttn();
        }
        else { activeTextQuest.text = ""; }
    }
}
