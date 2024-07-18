using UnityEngine;

public class Glass : MonoBehaviour
{
    public float breakForceThreshold = 10.0f; // Umbral de fuerza para romper el vidrio
    [SerializeField] private GameObject particleBreak;

    private AudioSource audioSource;
    [SerializeField] private AudioClip vidrioClip;

    private void Awake()
    {
        audioSource = GameObject.Find("SFXVolume").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Calculamos la fuerza de impacto
        float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime;

        // Comprobamos si la fuerza supera el umbral para romper el vidrio
        if (impactForce >= breakForceThreshold)
        {
            // Llamamos a una funci�n para romper el vidrio
            BreakGlass();
        }
    }

    void BreakGlass()
    {
        Instantiate(particleBreak, transform.position, transform.rotation);
        Destroy(gameObject);
        audioSource.PlayOneShot(vidrioClip);

        // Puedes activar aqu� un efecto visual de rotura, por ejemplo una partici�n de vidrio roto.
        // Puedes usar efectos de part�culas, cambiar el modelo del objeto por otro roto, etc.
    }
}
