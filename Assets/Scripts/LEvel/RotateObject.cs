using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    private bool isRotating = false;
    [SerializeField] private float rotationDuration = 0.25f; // Duraci�n de la rotaci�n en segundos
    [SerializeField] private float wobbleDuration = 0.25f; // Duraci�n del tambaleo en segundos
    [SerializeField] private float wobbleAmount = 10f; // Cantidad de tambaleo

    void Update()
    {
        // Nada en Update, la rotaci�n solo se activar� mediante una llamada de funci�n
    }

    // Esta funci�n se puede llamar desde otros scripts, botones, etc.
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

        // Rotaci�n progresiva
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

        // Efecto de tambaleo despu�s de la rotaci�n
        elapsed = 0.0f;
        while (elapsed < wobbleDuration)
        {
            float progress = elapsed / wobbleDuration;
            float wobble = Mathf.Sin(progress * Mathf.PI * 4) * wobbleAmount * (1 - progress);
            transform.rotation = endRotation * Quaternion.Euler(wobble, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Asegurar que la rotaci�n final sea exacta
        transform.rotation = endRotation;
        isRotating = false;
    }

    private IEnumerator RotateAndWobbleWall(float angle, float rotationDuration, float wobbleDuration)
    {
        isRotating = true;

        // Rotaci�n progresiva
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

        // Efecto de tambaleo despu�s de la rotaci�n
        elapsed = 0.0f;
        while (elapsed < wobbleDuration)
        {
            float progress = elapsed / wobbleDuration;
            float wobble = Mathf.Sin(progress * Mathf.PI * 4) * wobbleAmount * (1 - progress);
            transform.rotation = endRotation * Quaternion.Euler(wobble, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Asegurar que la rotaci�n final sea exacta
        transform.rotation = endRotation;
        isRotating = false;
    }
}
