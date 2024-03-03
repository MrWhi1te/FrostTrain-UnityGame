using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    [SerializeField] private Game GM;
    [SerializeField] private StationScripts ST;
    [SerializeField] private Audio AO;

    [Header("Game")]
    public GameObject[] trainingPanGame;
    [SerializeField] private GameObject[] trainingDialogGame;
    [SerializeField] private GameObject[] trainingWagonePan;
    private int trainingWagonCount;
    public bool[] wagoneTrainerChoice; //

    [Header("Station")]
    public GameObject trainingPanStation;
    [SerializeField] private GameObject[] trainingDialogStation;
    private int trainingStationCount;


    #region GAME 
    public void NextTrainingGame()
    {
        GM.trainingCount++;
        
        if(GM.trainingCount == 18)
        {
            trainingPanGame[1].SetActive(false);
            GM.StartNextStation();
            GM.StartAutoSave();
            GM.TaskCounter();
            GM.Trainer[0] = true;
        }
        
        if (GM.trainingCount == 10) trainingPanGame[0].SetActive(false);
        
        else if(GM.trainingCount != 10 && GM.trainingCount != 18)
        {
            trainingDialogGame[GM.trainingCount-1].SetActive(false);
            trainingDialogGame[GM.trainingCount].SetActive(true);
        }
        
        if(GM.trainingCount >= 11 && GM.trainingCount < 16)
        {
            trainingWagonePan[trainingWagonCount].SetActive(true);
            trainingWagonCount++;
        }
        else if(GM.trainingCount == 16) trainingWagonePan[0].SetActive(false);
        
        AO.PlayAudioEnterPanel();
    }

    public void TrainingGameWagonActive()
    {
        if (wagoneTrainerChoice[0] == true & wagoneTrainerChoice[1] == true & wagoneTrainerChoice[2] == true & wagoneTrainerChoice[3] == true)
        {
            trainingPanGame[1].SetActive(true);
            AO.PlayAudioEnterPanel();
        }
    }

    public void EscapeTrainingGame()
    {
        GM.StartNextStation();
        GM.StartAutoSave();
        GM.TaskCounter();
        GM.Trainer[0] = true;
        trainingPanGame[0].SetActive(false); 
        trainingPanGame[1].SetActive(false);
        trainingPanGame[2].SetActive(false);
        GM.Food = GM.FoodMax;
        GM.Water = GM.WaterMax;
        GM.ResourceTextUpdate();
    }
    #endregion

    #region Station

    public void NextTrainingStation()
    {
        trainingStationCount++;
        if(trainingStationCount < 4)
        {
            trainingDialogStation[trainingStationCount - 1].SetActive(false);
            trainingDialogStation[trainingStationCount].SetActive(true);
        }
        else
        {
            trainingPanStation.SetActive(false);
            GM.Trainer[1] = true;
            GM.passWagoneCount++;
            ST.ResourceTextUpdate();
        }
        AO.PlayAudioEnterPanel();
    }

    #endregion
}
