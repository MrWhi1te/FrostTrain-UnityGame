using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public Game GM; // Скрипт Гейм
    public StationScripts ST; // Скрипт станции
    public Image[] PointCity; // Спрайты городов
    public Button NextStation; // Кнопка продолжения
    public GameObject MapPan; // 
    public Text ChoiceCityNameText; // Текст следующей станции
    public Text ChoiceCityTemperatureText; //
    public Text ChoiceCityTimeText; //
    public Text ChoiceCityBttnText; //
    public GameObject[] MapLock; // Блокировка карты
    public GameObject[] QuestionPoint; // Точки вопросов
    public GameObject TaskPoint; // Точка задания

    public Text[] ScoreTextView; //

    private void OnEnable()
    {
        TaskPoint.SetActive(false);
        for (int i = 0; i < 45; i++)
        {
            PointCity[i].color = new Color(36, 255, 0, 0);
        }
        PointCity[GM.City].color = new Color(0, 150, 255, 255);
        NextStation.enabled = false;
        ScoreCount();
        if(GM.Score >= 500)
        {
            MapLock[0].SetActive(false);
        }
        if (GM.Score >= 1000)
        {
            MapLock[1].SetActive(false);
        }
        for(int i = 0; i < QuestionPoint.Length; i++)
        {
            if(GM.QuestionPoint[i] == true)
            {
                QuestionPoint[i].SetActive(false);
            }
        }
        if(GM.ActiveTask == true)
        {
            TaskPoint.SetActive(true);
            TaskPoint.transform.position = PointCity[GM.CityTask].transform.position;
        }
        ChoiceCityNameText.text = "Выберите следующую станцию на карте!";
        ChoiceCityBttnText.text = "Выберите станцию на карте!";
    }

    public void ChoicePoint(int index) //
    {
        for (int i = 0; i < 45; i++)
        {
            PointCity[i].color = new Color(36, 255, 0, 0);
        }
        PointCity[GM.City].color = new Color(0, 150, 255, 255);
        ChoiceCityBttnText.text = "Продолжить путешествие";
        if (GM.City == 0)
        {
            if(index == 1)
            {
                PointCity[1].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 1;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity]*10 + "км.";
            }
        }
        if (GM.City == 1)
        {
            if (index == 0)
            {
                PointCity[0].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 0;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 2)
            {
                PointCity[2].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 2;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 3)
            {
                PointCity[3].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 3;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 2)
        {
            if (index == 1)
            {
                PointCity[1].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 1;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 3)
            {
                PointCity[3].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 3;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 4)
            {
                PointCity[4].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 4;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 5)
            {
                PointCity[5].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 5;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 6)
            {
                PointCity[6].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 6;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 7)
            {
                PointCity[7].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 7;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 3)
        {
            if (index == 1)
            {
                PointCity[1].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 1;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 2)
            {
                PointCity[2].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 2;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 8)
            {
                PointCity[8].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 8;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 4)
        {
            if (index == 2)
            {
                PointCity[2].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 2;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 9)
            {
                PointCity[9].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 9;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 5)
        {
            if (index == 2)
            {
                PointCity[2].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 2;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 6)
            {
                PointCity[6].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 6;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 10)
            {
                PointCity[10].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 10;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 6)
        {
            if (index == 2)
            {
                PointCity[2].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 2;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 5)
            {
                PointCity[5].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 5;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 11)
            {
                PointCity[11].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 11;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 7)
        {
            if (index == 2)
            {
                PointCity[2].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 2;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 8)
            {
                PointCity[8].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 8;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 12)
            {
                PointCity[12].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 12;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 8)
        {
            if (index == 3)
            {
                PointCity[3].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 3;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 7)
            {
                PointCity[7].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 7;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 13)
            {
                PointCity[13].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 13;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 14)
            {
                PointCity[14].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 14;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 9)
        {
            if (index == 4)
            {
                PointCity[4].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 4;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 15)
            {
                PointCity[15].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 15;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 10)
        {
            if (index == 5)
            {
                PointCity[5].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 5;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 11)
        {
            if (index == 6)
            {
                PointCity[6].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 6;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 12)
            {
                PointCity[12].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 12;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 16)
            {
                PointCity[16].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 16;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 12)
        {
            if (index == 7)
            {
                PointCity[7].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 7;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 11)
            {
                PointCity[11].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 11;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 16)
            {
                PointCity[16].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 16;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 13)
        {
            if (index == 8)
            {
                PointCity[8].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 8;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 14)
        {
            if (index == 8)
            {
                PointCity[8].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 8;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 16)
            {
                PointCity[16].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 16;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 17)
            {
                PointCity[17].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 17;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 15)
        {
            if (index == 9)
            {
                PointCity[9].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 9;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 16)
        {
            if (index == 11)
            {
                PointCity[11].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 11;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 12)
            {
                PointCity[12].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 12;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 14)
            {
                PointCity[14].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 14;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 18)
            {
                PointCity[18].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 18;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 17)
        {
            if (index == 14)
            {
                PointCity[14].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 14;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 18)
            {
                PointCity[18].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 18;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 19)
            {
                PointCity[19].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 19;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 18)
        {
            if (index == 16)
            {
                PointCity[16].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 16;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 17)
            {
                PointCity[17].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 17;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 20)
            {
                PointCity[20].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 20;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 19)
        {
            if (index == 17)
            {
                PointCity[17].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 17;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 21)
            {
                PointCity[21].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 21;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 22)
            {
                PointCity[22].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 22;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 20)
        {
            if (index == 18)
            {
                PointCity[18].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 18;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 21)
        {
            if (index == 19)
            {
                PointCity[19].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 19;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 23)
            {
                PointCity[23].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 23;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 24)
            {
                PointCity[24].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 24;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 25)
            {
                PointCity[25].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 25;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 22)
        {
            if (index == 19)
            {
                PointCity[19].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 19;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 26)
            {
                PointCity[26].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 26;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 23)
        {
            if (index == 21)
            {
                PointCity[21].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 21;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 24)
        {
            if (index == 21)
            {
                PointCity[21].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 21;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 25)
        {
            if (index == 21)
            {
                PointCity[21].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 21;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 26)
        {
            if (index == 22)
            {
                PointCity[22].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 22;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 27)
            {
                PointCity[27].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 27;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 28)
            {
                PointCity[28].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 28;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 29)
            {
                PointCity[29].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 29;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 27)
        {
            if (index == 26)
            {
                PointCity[26].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 26;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 28)
        {
            if (index == 26)
            {
                PointCity[26].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 26;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 29)
        {
            if (index == 26)
            {
                PointCity[26].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 26;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 30)
            {
                PointCity[30].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 30;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 30)
        {
            if (index == 29)
            {
                PointCity[29].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 29;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 31)
            {
                PointCity[31].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 31;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 31)
        {
            if (index == 30)
            {
                PointCity[30].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 30;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 32)
            {
                PointCity[32].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 32;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 32)
        {
            if (index == 31)
            {
                PointCity[31].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 31;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 33)
            {
                PointCity[33].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 33;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 33)
        {
            if (index == 32)
            {
                PointCity[32].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 32;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 34)
            {
                PointCity[34].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 34;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 34)
        {
            if (index == 33)
            {
                PointCity[33].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 33;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 35)
            {
                PointCity[35].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 35;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 36)
            {
                PointCity[36].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 36;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 35)
        {
            if (index == 34)
            {
                PointCity[34].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 34;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 36)
        {
            if (index == 34)
            {
                PointCity[34].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 34;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 37)
            {
                PointCity[37].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 37;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 38)
            {
                PointCity[38].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 38;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 37)
        {
            if (index == 36)
            {
                PointCity[36].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 36;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 39)
            {
                PointCity[39].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 39;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 38)
        {
            if (index == 36)
            {
                PointCity[36].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 36;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 40)
            {
                PointCity[40].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 40;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 39)
        {
            if (index == 37)
            {
                PointCity[37].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 37;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 41)
            {
                PointCity[41].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 41;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 40)
        {
            if (index == 38)
            {
                PointCity[38].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 38;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 42)
            {
                PointCity[42].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 42;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 43)
            {
                PointCity[43].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 43;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 44)
            {
                PointCity[44].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 44;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 41)
        {
            if (index == 39)
            {
                PointCity[39].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 39;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 42)
        {
            if (index == 40)
            {
                PointCity[40].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 40;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 43)
            {
                PointCity[43].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 43;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 43)
        {
            if (index == 40)
            {
                PointCity[40].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 40;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
            if (index == 42)
            {
                PointCity[42].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 42;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
        if (GM.City == 44)
        {
            if (index == 40)
            {
                PointCity[40].color = new Color(36, 255, 0, 255);
                GM.ChoiceCity = 40;
                NextStation.enabled = true;
                ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
                ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
                ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
            }
        }
    }

    public void NextStationGO()
    {
        ST.TimerNextStation();
        MapPan.SetActive(false);
    }

    public void ScoreCount() // Вывод очков
    {
        int r = 0;
        int number = GM.Score;
        while (number > 0)
        {
            int digit = number % 10;
            number /= 10;
            ScoreTextView[r].text = digit.ToString();
            r++;
        }
    }
}
