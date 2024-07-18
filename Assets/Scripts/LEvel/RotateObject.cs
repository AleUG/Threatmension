using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    private static bool anyIsRotating = false; // Variable est�tica para todas las instancias
    private bool isRotating = false;
    [SerializeField] private float rotationDuration = 0.25f; // Duraci�n de la rotaci�n en segundos
    [SerializeField] private float wobbleDuration = 0.25f; // Duraci�n del tambaleo en segundos
    [SerializeField] private float wobbleAmount = 10f; // Cantidad de tambaleo

    [SerializeField] private LibreriaDeSonidos libreria;
    [SerializeField] private AudioSource audioSource;

    private bool canRotate = false;

    private void Start()
    {
        StartCoroutine(EnableThis());
    }
    // Esta funci�n se puede llamar desde otros scripts, botones, etc.
    public void Rotate()
    {
        if (!isRotating && !anyIsRotating && canRotate)
        {
            StartCoroutine(RotateAndWobble(180, 0, 0, rotationDuration, wobbleDuration));
        }
    }

    public void RotateWall()
    {
        if (!isRotating && !anyIsRotating && canRotate)
        {
            StartCoroutine(RotateAndWobble(-90, 0, 0, rotationDuration, wobbleDuration));
        }
    }

    public void RotateHorizontal(bool isRight)
    {
        if (!isRotating && !anyIsRotating && canRotate)
        {
            StartCoroutine(RotateAndWobble(0, isRight ? 90 : -90, 0, rotationDuration, wobbleDuration, true));
        }
    }

    private IEnumerator RotateAndWobble(float angleX, float angleY, float angleZ, float rotationDuration, float wobbleDuration, bool isHorizontal = false)
    {
        isRotating = true;
        anyIsRotating = true; // Marcar que una rotaci�n est� en progreso

        // Rotaci�n progresiva
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        libreria.PlayRandomAudio(audioSource);

        if (isHorizontal)
        {
            endRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + angleY, transform.eulerAngles.z);
        }
        else
        {
            endRotation = transform.rotation * Quaternion.Euler(angleX, angleY, angleZ);
        }

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
        anyIsRotating = false; // Marcar que ninguna rotaci�n est� en progreso
    }

    private IEnumerator EnableThis()
    {
        yield return new WaitForSeconds(1.3f);

        canRotate = true;
    }
}
