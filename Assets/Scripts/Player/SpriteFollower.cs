using UnityEngine;

public class SpriteFollower : MonoBehaviour
{
    public Transform cameraTransform; // Transform de la c�mara que el sprite seguir�

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        LookAtCamera();
    }

    void LookAtCamera()
    {
        // Calcula la direcci�n hacia la c�mara sin cambiar la posici�n del sprite
        Vector3 lookDir = cameraTransform.position - transform.position;

        // Calcula la rotaci�n necesaria para que el sprite mire hacia la c�mara
        Quaternion rotation = Quaternion.LookRotation(lookDir);

        // Aplica la rotaci�n al sprite, ignorando la rotaci�n original
        transform.rotation = rotation;
    }
}
