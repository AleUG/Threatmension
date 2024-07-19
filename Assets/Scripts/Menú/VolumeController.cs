using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] public Slider volumeSlider;
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] public GameObject canvasOptions;

    public delegate void MusicVolumeChanged(float volume);
    public event MusicVolumeChanged OnMusicVolumeChanged;

    public float MusicVolume => musicSlider.value;

    private void Start()
    {
        canvasOptions.SetActive(false);

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
            SetMasterVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        OnMusicVolumeChanged?.Invoke(volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void SetMasterVolume()
    {
        float volume2 = volumeSlider.value;
        myMixer.SetFloat("master", Mathf.Log10(volume2) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume2);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");

        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
    }

    public void DesactivarCanvasOptions()
    {
        canvasOptions.SetActive(false);
    }
}
