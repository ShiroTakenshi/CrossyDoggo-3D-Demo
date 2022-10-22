using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    // [SerializeField] Slider masterSlider;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    private void Awake()
    {
        float db;
        if (audioMixer.GetFloat("MUSICVOL", out db))
            BGMSlider.value = (db + 80) / 80;

        if (audioMixer.GetFloat("SFXVOL", out db))
            SFXSlider.value = (db + 80) / 80;

    }


    public void SetMusicVolume(float volume)
    {
        volume = volume * 80 - 80;
        audioMixer.SetFloat("MUSICVOL", volume);
        PlayerPrefs.SetFloat("MUSICVOL", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        volume = volume * 80 - 80;
        audioMixer.SetFloat("SFXVOL", volume);
        PlayerPrefs.SetFloat("SFXVOL", volume);
        PlayerPrefs.Save();
    }










}
