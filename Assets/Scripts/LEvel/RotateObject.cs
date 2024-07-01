using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    private bool isRotating = false;
    [SerializeField] private float rotationDuration = 0.25f; // Duración de la rotación en segundos
    [SerializeField] private float wobbleDuration = 0.25f; // Duración del tambaleo en segundos
    [SerializeField] private float wobbleAmount = 10f; // Cantidad de tambaleo

    void Update()
    {
        // Nada en Update, la rotación solo se activará mediante una llamada de función
    }

    // Esta función se puede llamar desde otros scripts, botones, etc.
    public void Rotate()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateAndWobble(180, rotationDuration, wobbleDuration));
        }
    }

    public void RotateWall()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateAndWobbleWall(90, rotationDuration, wobbleDuration));
        }
    }

    private IEnumerator RotateAndWobble(float angle, float rotationDuration, float wobbleDuration)
    {
        isRotating = true;

        // Rotación progresiva
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(angle, 0, 0);
        float elapsed = 0.0f;

        while (elapsed < rotationDuration)
        {
            float progress = elapsed / rotationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, progress);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        // Efecto de tambaleo después de la rotación
        elapsed = 0.0f;
        while (elapsed < wobbleDuration)
        {
            float progress = elapsed / wobbleDuration;
            float wobble = Mathf.Sin(progress * Mathf.PI * 4) * wobbleAmount * (1 - progress);
            transform.rotation = endRotation * Quaternion.Euler(wobble, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Asegurar que la rotación final sea exacta
        transform.rotation = endRotation;
        isRotating = false;
    }

    private IEnumerator RotateAndWobbleWall(float angle, float rotationDuration, float wobbleDuration)
    {
        isRotating = true;

        // Rotación progresiva
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 0, angle);
        float elapsed = 0.0f;

        while (elapsed < rotationDuration)
        {
            float progress = elapsed / rotationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, progress);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        // Efecto de tambaleo después de la rotación
        elapsed = 0.0f;
        while (elapsed < wobbleDuration)
        {
            float progress = elapsed / wobbleDuration;
            float wobble = Mathf.Sin(progress * Mathf.PI * 4) * wobbleAmount * (1 - progress);
            transform.rotation = endRotation * Quaternion.Euler(wobble, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Asegurar que la rotación final sea exacta
        transform.rotation = endRotation;
        isRotating = false;
    }
}
