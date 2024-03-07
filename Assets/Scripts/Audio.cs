using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource thisAudio;

    [SerializeField] private AudioClip enterPanel;
    [SerializeField] private AudioClip clickBttn;
    [SerializeField] private AudioClip clickTrain;
    [SerializeField] private AudioClip enterExitStation;
    [SerializeField] private AudioClip enterStation;
    [SerializeField] private AudioClip takeResource;
    [SerializeField] private AudioClip train;
    [SerializeField] private AudioClip payment;
    [SerializeField] private AudioClip clickTrees;
    [SerializeField] private AudioClip takePass;
    [SerializeField] private AudioClip notTakePass;

    [SerializeField] private UnityEngine.UI.Image soundImg;
    [SerializeField] private Sprite[] soundSprite;
    
    public bool activeSound;

    // Start is called before the first frame update
    void Start()
    {
        thisAudio = GetComponent<AudioSource>();
        if (activeSound) soundImg.sprite = soundSprite[0];
        else soundImg.sprite = soundSprite[1];
    }
    public void PlayAudioEnterPanel()
    {
        if (activeSound)
        {
            thisAudio.clip = enterPanel;
            thisAudio.Play();
        }
    }
    public void PlayAudioClickBttn()
    {
        if (activeSound) thisAudio.PlayOneShot(clickBttn);
    }
    public void PlayAudioStation()
    {
        if (activeSound) thisAudio.PlayOneShot(enterExitStation);
    }
    public void PlayAudioEnterStation()
    {
        if (activeSound) thisAudio.PlayOneShot(enterStation);
    }

    public void PlayAudioTakeResource()
    {
        if (activeSound) thisAudio.PlayOneShot(takeResource);
    }
    public void PlayAudioTrain()
    {
        if (activeSound)
        {
            thisAudio.clip = train;
            thisAudio.Play();
        }
    }
    public void StopAudio()
    {
        if (activeSound) thisAudio.Stop();
    }
    public void PlayAudioClickTrain()
    {
        if (activeSound) thisAudio.PlayOneShot(clickTrain);
    }
    public void PlayAudioPayment()
    {
        if (activeSound) thisAudio.PlayOneShot(payment);
    }

    public void PlayAudioTrees()
    {
        if (activeSound) thisAudio.PlayOneShot(clickTrees);
    }

    public void PlayAudioTakePass()
    {
        if (activeSound) thisAudio.PlayOneShot(takePass);
    }

    public void PlayAudioNotTakePass()
    {
        if (activeSound) thisAudio.PlayOneShot(notTakePass);
    }

    public void OnOffSound()
    {
        if (activeSound) 
        {
            activeSound = false;
            soundImg.sprite = soundSprite[1];
        } 
        else
        {
            activeSound = true;
            soundImg.sprite = soundSprite[0];
        }
    }
}
