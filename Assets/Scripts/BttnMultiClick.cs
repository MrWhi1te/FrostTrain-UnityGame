using UnityEngine;

public class BttnMultiClick : MonoBehaviour
{
    [SerializeField] private Game GM;
    [SerializeField] private Audio AO;

    [SerializeField] private float clickInterval;
    [SerializeField] private UnityEngine.UI.Text clickText;
    

    private int clickMultiplierCount;
    private int clickCount;
    private float timer = 2f;
    private float timeText;

    void Update()
    {
        timer += Time.deltaTime;
        timeText += Time.deltaTime;

        if (timer >= clickInterval)
        {
            if (clickMultiplierCount >= 5 && (GM.Coal+3) < GM.CoalMax)
            {
                GM.Coal += 3;
                AO.PlayAudioTakeResource();
                GM.CollectResources[3].SetActive(false); GM.CollectResources[03].SetActive(true);
                clickText.text = "+3";
                GM.ResourceTextUpdate();
                GM.TextLoco();
            }
            clickMultiplierCount = 0;
            timer = 0;
        }

        if(timeText >= 1)
        {
            timeText = 0;
            if(clickCount <= 0)
            {
                clickText.text = "";
            }
        }
    }

    public void Click() 
    {
        if (GM.Coal < GM.CoalMax)
        {
            clickCount++;
            GM.Coal++;
            GM.CoalPlusStatistic++;
            clickText.text = "+1";
            AO.PlayAudioTrees();
            GM.ResourceTextUpdate();
            GM.TextLoco();
        }
        clickMultiplierCount++;
        GM.TreesClickParticle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GM.TreesClickParticle.SetActive(false);
        GM.TreesClickParticle.SetActive(true);
        if (clickCount >= 5)
        {
            clickCount = 0;
            AO.PlayAudioTakeResource();
            GM.CollectResources[3].SetActive(false); GM.CollectResources[03].SetActive(true);
        }
        if (!GM.CoalHelp)
        {
            GM.CoalHelpObj.SetActive(false);
            GM.CoalHelp = true;
        }
    }


}
