using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXHandler : MonoBehaviour
{
    public static SoundFXHandler instance;
    [SerializeField] private AudioSource soundFXObject;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnPoint, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource, clipLength);

    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnPoint, float volume)
    {
        int random = Random.Range(0, audioClip.Length);
        AudioSource audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = audioClip[random];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource, clipLength);

    }
}
