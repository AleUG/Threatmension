using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; // Array para almacenar las Cinemachine Virtual Cameras
    private int currentCameraIndex;

    private bool canSwitch = true; // Variable para controlar el cooldown

    public float switchCooldown = 1.5f; // Duraci�n del cooldown

    void Start()
    {
        // Desactiva todas las c�maras excepto la primera al inicio
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = (i == 0) ? 10 : 0;
        }
        currentCameraIndex = 0;
    }

    public void SwitchToNextCamera()
    {
        if (canSwitch)
        {
            // Desactiva la c�mara actual
            cameras[currentCameraIndex].Priority = 0;

            // Incrementa el �ndice de la c�mara actual y envu�lvelo si es necesario
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Activa la nueva c�mara
            cameras[currentCameraIndex].Priority = 10;

            // Inicia el cooldown
            StartCoroutine(Cooldown());
        }
    }

    public void SwitchToPreviousCamera()
    {
        if (canSwitch)
        {
            // Desactiva la c�mara actual
            cameras[currentCameraIndex].Priority = 0;

            // Decrementa el �ndice de la c�mara actual y envu�lvelo si es necesario
            currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;

            // Activa la nueva c�mara
            cameras[currentCameraIndex].Priority = 10;

            // Inicia el cooldown
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        canSwitch = false; // Desactiva la capacidad de cambiar de c�mara
        yield return new WaitForSeconds(switchCooldown); // Espera el tiempo del cooldown
        canSwitch = true; // Reactiva la capacidad de cambiar de c�mara
    }
}
