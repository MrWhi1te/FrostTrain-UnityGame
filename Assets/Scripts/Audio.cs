using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource thisAudio;
    [SerializeField]private AudioClip enterPanel;
    [SerializeField] private AudioClip clickBttn;
    [SerializeField] private AudioClip clickTrain;
    [SerializeField] private AudioClip enterExitStation;
    [SerializeField] private AudioClip takeResource;
    [SerializeField] private AudioClip train;
    [SerializeField] private AudioClip payment;

    // Start is called before the first frame update
    void Start()
    {
        thisAudio = GetComponent<AudioSource>();
    }
    public void PlayAudioEnterPanel()
    {
        thisAudio.clip = enterPanel;
        thisAudio.Play();
    }
    public void PlayAudioClickBttn()
    {
        //thisAudio.clip = clickBttn;
        thisAudio.PlayOneShot(clickBttn);
    }
    public void PlayAudioStation()
    {
        thisAudio.PlayOneShot(enterExitStation);
    }
    public void PlayAudioTakeResource()
    {
        thisAudio.PlayOneShot(takeResource);
    }
    public void PlayAudioTrain()
    {
        thisAudio.clip = train;
        thisAudio.Play();
    }
    public void StopAudio()
    {
        thisAudio.Stop();
    }
    public void PlayAudioClickTrain()
    {
        thisAudio.PlayOneShot(clickTrain);
    }
    public void PlayAudioPayment()
    {
        thisAudio.PlayOneShot(payment);
    }
}
