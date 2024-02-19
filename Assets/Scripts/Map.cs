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
                GetNextStation(index);
            }
        }
        else if (GM.City == 1)
        {
            if (index == 0 || index == 2 || index == 3)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 2)
        {
            if (index == 1 || index == 3 || index == 4 || index == 5 || index == 6 || index == 7)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 3)
        {
            if (index == 1 || index == 2 || index == 8)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 4)
        {
            if (index == 2 || index == 9)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 5)
        {
            if (index == 2 || index == 6 || index == 10)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 6)
        {
            if (index == 2 || index == 5 || index == 11)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 7)
        {
            if (index == 2 || index == 8 || index == 12)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 8)
        {
            if (index == 3 || index == 7 || index == 13 || index == 14)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 9)
        {
            if (index == 4 || index == 15)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 10)
        {
            if (index == 5)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 11)
        {
            if (index == 6 || index == 12 || index == 16)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 12)
        {
            if (index == 7 || index == 11 || index == 16)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 13)
        {
            if (index == 8)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 14)
        {
            if (index == 8 || index == 16 || index == 17)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 15)
        {
            if (index == 9)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 16)
        {
            if (index == 11 || index == 12 || index == 14 || index == 18)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 17)
        {
            if (index == 14 || index == 18 || index == 19)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 18)
        {
            if (index == 16 || index == 17 || index == 20)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 19)
        {
            if (index == 17 || index == 21 || index == 22)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 20)
        {
            if (index == 18)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 21)
        {
            if (index == 19 || index == 23 || index == 24 || index == 25)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 22)
        {
            if (index == 19 || index == 26)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 23)
        {
            if (index == 21)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 24)
        {
            if (index == 21)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 25)
        {
            if (index == 21)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 26)
        {
            if (index == 22 || index == 27 || index == 28 || index == 29)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 27)
        {
            if (index == 26)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 28)
        {
            if (index == 26)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 29)
        {
            if (index == 26 || index == 30)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 30)
        {
            if (index == 29 || index == 31)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 31)
        {
            if (index == 30 || index == 32)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 32)
        {
            if (index == 31 || index == 33)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 33)
        {
            if (index == 32 || index == 34)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 34)
        {
            if (index == 33 || index == 35 || index == 36)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 35)
        {
            if (index == 34)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 36)
        {
            if (index == 34 || index == 37 || index == 38)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 37)
        {
            if (index == 36 || index == 39)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 38)
        {
            if (index == 36 || index == 40)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 39)
        {
            if (index == 37 || index == 41)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 40)
        {
            if (index == 38 || index == 42 || index == 43 || index == 44)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 41)
        {
            if (index == 39)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 42)
        {
            if (index == 40 || index == 43)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 43)
        {
            if (index == 40 || index == 42)
            {
                GetNextStation(index);
            }
        }
        else if (GM.City == 44)
        {
            if (index == 40)
            {
                GetNextStation(index);
            }
        }
    }

    void GetNextStation(int index)
    {
        PointCity[index].color = new Color(36, 255, 0, 255);
        GM.ChoiceCity = index;
        NextStation.enabled = true;
        ChoiceCityNameText.text = "Следующая станция: " + GM.NameCity[GM.ChoiceCity];
        ChoiceCityTemperatureText.text = "Температура:" + GM.TemperatureForCity[GM.ChoiceCity] + "°C";
        ChoiceCityTimeText.text = "Расстояние:" + GM.TimeForCity[GM.ChoiceCity] * 10 + "км.";
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
