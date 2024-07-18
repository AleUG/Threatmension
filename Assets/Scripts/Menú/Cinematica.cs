using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinem�tica : MonoBehaviour
{
    public string nextSceneName; // Nombre de la escena a cargar despu�s de la cinem�tica
    public float cinematicDuration = 17.0f; // Duraci�n de la cinem�tica en segundos

    private void Start()
    {
        // Reproducir la cinem�tica aqu�

        // Esperar a que la cinem�tica termine
        Invoke("LoadNextScene", cinematicDuration);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Cargar la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}