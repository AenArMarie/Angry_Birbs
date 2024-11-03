using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private float Convert(float value)
    {
        return Mathf.Log10(value) * 20f;
    }

    private void Awake()
    {
        audioMixer.SetFloat("masterVolume", Convert(PlayerPrefs.GetFloat("masterVolume", 0f)));
        audioMixer.SetFloat("soundFXVolume", Convert(PlayerPrefs.GetFloat("soundFXVolume", 0f)));
        audioMixer.SetFloat("musicVolume", Convert(PlayerPrefs.GetFloat("musicVolume", 0f)));
    }

    

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", Convert(volume));
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetSoundFXVolume(float volume)
    {
        audioMixer.SetFloat("soundFXVolume", Convert(volume));
        PlayerPrefs.SetFloat("soundFXVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Convert(volume));
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
}
