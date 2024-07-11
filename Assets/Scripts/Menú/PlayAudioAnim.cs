using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioAnim : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private LibreriaDeSonidos libreria;

    public void PlayAudio()
    {
        if (audioSource != null || libreria != null)
        {
            libreria.PlayRandomAudio(audioSource);
        }
    }
}
