using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        // Obtén la posición de la cámara
        Vector3 cameraPosition = Camera.main.transform.position;

        // Calcula la dirección desde el objeto hacia la cámara
        Vector3 directionToCamera = cameraPosition - transform.position;

        // Elimina la componente vertical para que el objeto no se incline hacia arriba o abajo
        directionToCamera.y = 0;

        // Calcula la rotación necesaria para que el objeto mire hacia la cámara
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        // Añade 90 grados en el eje Y
        targetRotation *= Quaternion.Euler(0, 90, 0);

        // Asigna la rotación al objeto
        transform.rotation = targetRotation;
    }
}
