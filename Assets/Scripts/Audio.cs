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
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip enterCode;

    [SerializeField] private UnityEngine.UI.Image soundImg;
    [SerializeField] private Sprite[] soundSprite;
    
    public bool activeSound;

    // Start is called before the first frame update
    void Start()
    {
        thisAudio = GetComponent<AudioSource>();
    }
    public void PlayAudioEnterPanel()
    {
        if (activeSound) thisAudio.PlayOneShot(enterPanel);
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

    public void PlayAudioSuccess()
    {
        if (activeSound) thisAudio.PlayOneShot(success);
    }

    public void PlayAudioEnterCode()
    {
        if (activeSound) thisAudio.PlayOneShot(enterCode);
    }

    public void OnOffSound()
    {
        if (activeSound) 
        {
            activeSound = false;
            CheckActiveSound();
            thisAudio.Stop();
        } 
        else
        {
            activeSound = true;
            CheckActiveSound();
        }
    }

    public void CheckActiveSound()
    {
        if (activeSound) soundImg.sprite = soundSprite[0];
        else soundImg.sprite = soundSprite[1];
    }
}
