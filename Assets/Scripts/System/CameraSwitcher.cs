using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; // Array para almacenar las Cinemachine Virtual Cameras
    private int currentCameraIndex;

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
        // Desactiva la c�mara actual
        cameras[currentCameraIndex].Priority = 0;

        // Incrementa el �ndice de la c�mara actual y envu�lvelo si es necesario
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Activa la nueva c�mara
        cameras[currentCameraIndex].Priority = 10;
    }

    public void SwitchToPreviousCamera()
    {
        // Desactiva la c�mara actual
        cameras[currentCameraIndex].Priority = 0;

        // Decrementa el �ndice de la c�mara actual y envu�lvelo si es necesario
        currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;

        // Activa la nueva c�mara
        cameras[currentCameraIndex].Priority = 10;
    }
}
