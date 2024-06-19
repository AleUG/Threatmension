using UnityEngine;

public class SpriteFollower : MonoBehaviour
{
    public Transform cameraTransform; // Transform de la cámara que el sprite seguirá

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
        // Calcula la dirección hacia la cámara sin cambiar la posición del sprite
        Vector3 lookDir = cameraTransform.position - transform.position;

        // Calcula la rotación necesaria para que el sprite mire hacia la cámara
        Quaternion rotation = Quaternion.LookRotation(lookDir);

        // Aplica la rotación al sprite, ignorando la rotación original
        transform.rotation = rotation;
    }
}
