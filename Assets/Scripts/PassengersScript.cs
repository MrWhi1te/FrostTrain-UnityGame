using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengersScript : MonoBehaviour
{
    [Header("Passengers")]
    public List<Pass> Passengers = new();

    public void CheckStation()
    {
        for (int i = 0; i < Passengers.Count; i++)
        {

        }
    }
}

[Serializable]
public class Pass
{
    public Sprite photoPass;
    public string namePass;
    public string descriptionPass;
    public string requestPass;
    public string cityLocation;
    public int statusPass; // 1 = Пассажира взяли на поезд / 2 = Пассажира не взяли на поезд
    public GameObject eventPass; // Объект события для пассажира
    public int statusEvent; // 1 = событие происходит если не взял пассажира / 2 = событие происходит если взял пассажира
}
