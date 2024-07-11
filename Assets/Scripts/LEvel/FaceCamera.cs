using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        // Obt�n la posici�n de la c�mara
        Vector3 cameraPosition = Camera.main.transform.position;

        // Calcula la direcci�n desde el objeto hacia la c�mara
        Vector3 directionToCamera = cameraPosition - transform.position;

        // Elimina la componente vertical para que el objeto no se incline hacia arriba o abajo
        directionToCamera.y = 0;

        // Calcula la rotaci�n necesaria para que el objeto mire hacia la c�mara
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        // A�ade 90 grados en el eje Y
        targetRotation *= Quaternion.Euler(0, 90, 0);

        // Asigna la rotaci�n al objeto
        transform.rotation = targetRotation;
    }
}
