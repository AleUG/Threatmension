using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sonidos/Libreria")]
public class LibreriaDeSonidos : ScriptableObject
{
    public AudioClip[] sonidos;

    public AudioClip Clip
    {
        // Devuelve un sonido aleatorio de la librería
        get { return sonidos[Random.Range(0, sonidos.Length)]; }
    }

    public void PlayRandomAudio(AudioSource audioSource)
    {
        if (sonidos.Length == 0)
        {
            Debug.LogWarning("No hay sonidos en la librería.");
            return;
        }

        audioSource.PlayOneShot(Clip);
    }
}
