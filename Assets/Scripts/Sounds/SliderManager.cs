using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider, soundFXVolumeSlider, musicVolumeSlider;
    private void Awake()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 1f);
        soundFXVolumeSlider.value = PlayerPrefs.GetFloat("soundFXVolume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
    }
}
