using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider mainVolume;

    public void SetMainVolume(float volume)
    {
        audioMixer.SetFloat("Main", volume);
    }
    public void SetHumanVolume(float volume)
    {
        audioMixer.SetFloat("Human", volume);
    }
    public void SetOtherVolume(float volume)
    {
        audioMixer.SetFloat("Other", volume);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetMainVolume(100f);
        mainVolume.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        SetMainVolume(mainVolume.value);
    }
}
