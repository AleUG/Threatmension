using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private LibreriaDeSonidos sonidos;
    [SerializeField] private AudioClip clipArrastre;
    private AudioSource audioSource;

    [SerializeField] private GameObject particulaSmoke;

    public LayerMask layerGround;

    private Rigidbody rb;

    [SerializeField] private float fadeOutDuration = 0.25f; // Duración del desvanecimiento en segundos

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void BoxArrastre()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clipArrastre;
            audioSource.Play();
        }

        //Instantiate(particulaSmoke, transform.position, transform.rotation);
    }

    private void BoxFall()
    {
        sonidos.PlayRandomAudio(audioSource);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & layerGround) != 0)
        {
            BoxFall();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine("FadeOut"); // Detener cualquier desvanecimiento en curso
            audioSource.volume = 1.0f; // Asegurarse de que el volumen esté al máximo
            BoxArrastre();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeOutDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Restablecer el volumen para la próxima vez
    }

    public void ActiveScript(bool state)
    {
        enabled = state;
    }
}
