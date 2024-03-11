using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindCode : MonoBehaviour
{
    [SerializeField] private Dropdown[] dropdown;
    [SerializeField] private Text[] dropdownText;
    [SerializeField] private Text attemptText;
    [SerializeField] private Text enteredText;

    private int[] inventNumber = new int[4];
    private List<int> correctlyNumber = new();
    private int attempt;

    private void OnEnable()
    {
        NewNumber();
    }

    private void NewNumber()
    {
        attempt = 5;
        enteredText.text = "";
        for (int i = 0; i < 4; i++) 
        {
            dropdown[i].value = 0;
            dropdownText[i].color = new Color32(0, 0, 0, 255);
            inventNumber[i] = Random.Range(0, 10);
        }
        foreach (int number in inventNumber)
        {
            Debug.Log(number);
        }
        attemptText.text = "Попыток: " + attempt;
    }

    public void CheckNumber()
    {
        correctlyNumber.Clear();
        string[] entText = new string[4];
        for (int i = 0; i < 4; i++)
        {
            entText[i] = dropdown[i].value.ToString();
            dropdownText[i].color = new Color32(0, 0, 0, 255);
            if (dropdown[i].value == inventNumber[i])
            {
                dropdownText[i].color = Color.green;
                entText[i] = $"<color=green>{dropdown[i].value}</color>";
                correctlyNumber.Add(i);
            }
            else
            {
                for(int r = 0; r < 4; r++)
                {
                    if(!correctlyNumber.Contains(r) && dropdown[i].value == inventNumber[r])
                    {
                        dropdownText[i].color = Color.yellow;
                        entText[i] = $"<color=yellow>{dropdown[i].value}</color>";
                        break;
                    }
                }
            }
        }
        
        if(correctlyNumber.Count >= 4)
        {
            Debug.Log("WIN!");
        }
        
        attempt--;
        attemptText.text = "Попыток: " + attempt;
        enteredText.text += entText[0] + entText[1] + entText[2] + entText[3] + "\n";
        if (attempt <= 0)
        {
            NewNumber();
        }
    }
}
