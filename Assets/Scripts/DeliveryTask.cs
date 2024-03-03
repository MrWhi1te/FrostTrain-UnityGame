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
    public GameObject rewardQuestPan; //
    public Text rewardQuestText; //
    public Text activeTextQuest; //
    public int targetDeliveryCity;

    [Header("DeliveryCargo")]
    public GameObject deliveryObj;
    public Text deliveryCity;
    public Text cargoWagoneTransport; //
    public Text cargoWagoneCoal; //
    public Text cargoWagoneReward; //

    public void CheckDelivery()
    {
        if (GM.CargoTransportCount >= 1 && GM.City == targetDeliveryCity)
        {
            for (int i = 0; i < GM.CargoTransportCount; i++)
            {
                GM.CargoTransportWagone[i].SetActive(false);
            }
            rewardQuestPan.SetActive(true);
            rewardQuestText.text = "�� ���������: " + GM.CargoTransportCount + " �������. ������� ���������: " + GM.RewardCargoTransport + "$";
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
            deliveryCity.text = "������� �������� ��: " + GM.NameCity[cityDelivery];
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


    public void EnterRewardQuest() // ������� ������� �� ��������
    {
        GM.Money += GM.RewardCargoTransport;
        GM.MoneyPlusStatistic += GM.RewardCargoTransport;
        GM.CargoTransportCount = cargoWagon = 0;
        rewardQuestPan.SetActive(false);
        activeTextQuest.text = "";
        ST.ResourceTextUpdate();
        GM.Score += GM.RewardCargoTransport / 50;
        ST.ScoreCount();
    }

    void CargoTransportationUpdate() // ��������� ������ ��������
    {
        deliveryCity.text = "�������� ��: " + GM.NameCity[cityDelivery];
        cargoWagoneTransport.text = cargoWagon.ToString();
        cargoWagoneCoal.text = "-" + cargoWagon * 20;
        cargoWagoneReward.text = cargoReward + "�";
    }
    public void AddCargoTransport() // �������� ����� ��������
    {
        if (cargoWagon < 10)
        {
            cargoWagon++;
            cargoReward = cargoWagon * (100 * multiplierReward);
            CargoTransportationUpdate();
        }
    }
    public void RemoveCargoTransport() // ����� ����� ��������
    {
        if (cargoWagon > 0)
        {
            cargoWagon--;
            cargoReward = cargoWagon * (100 * multiplierReward);
            CargoTransportationUpdate();
        }
    }
    public void EnterQuestCargo() // ������� �������
    {
        GM.CargoTransportCount = cargoWagon;
        GM.RewardCargoTransport = cargoReward;
        if (cargoWagon >= 1)
        {
            activeTextQuest.text = "������� �������!" + "\n" + "�������� �������: " + cargoWagon + GM.NameCity[cityDelivery];
            deliveryObj.SetActive(false);
            deliveryCity.text = "������� �������� ��: " + GM.NameCity[cityDelivery];
        }
        else { activeTextQuest.text = ""; }
    }
}
