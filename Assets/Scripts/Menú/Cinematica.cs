using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Video;

public class Cinemática : MonoBehaviour
{
    public string nextSceneName; // Nombre de la escena a cargar después de la cinemática
    public float timeSceneChange = 17.0f; // Duración de la cinemática en segundos
    public float cinematicVideo;
    public VideoPlayer videoPlayer; // Referencia al VideoPlayer

    private float time;
    private bool eventPassed = false;

    public UnityEvent onVideoEnd;
    private CambiarEscena cambiarEscena;

    private void Start()
    {
        cambiarEscena = FindAnyObjectByType<CambiarEscena>();
        Invoke("LoadNextScene", timeSceneChange);

        // Configurar el VideoPlayer para que ajuste su volumen
        VolumeController volumeController = FindObjectOfType<VolumeController>();
        if (volumeController != null)
        {
            volumeController.OnMusicVolumeChanged += SetVideoVolume;
            SetVideoVolume(volumeController.MusicVolume);
        }
    }

    private void Update()
    {
        if (!eventPassed)
        {
            time += Time.deltaTime;

            if (time >= cinematicVideo)
            {
                onVideoEnd.Invoke();
                eventPassed = true;
            }
        }
    }

    public void LoadNextScene()
    {
        cambiarEscena.CambiarEscenaName(nextSceneName);
    }

    // Método para ajustar el volumen del VideoPlayer
    private void SetVideoVolume(float volume)
    {
        if (videoPlayer != null)
        {
            videoPlayer.SetDirectAudioVolume(0, volume);
        }
    }
}
