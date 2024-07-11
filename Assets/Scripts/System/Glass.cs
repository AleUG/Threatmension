using UnityEngine;

public class Glass : MonoBehaviour
{
    public float breakForceThreshold = 10.0f; // Umbral de fuerza para romper el vidrio
    [SerializeField] private GameObject particleBreak;

    private void OnCollisionEnter(Collision collision)
    {
        // Calculamos la fuerza de impacto
        float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime;

        // Comprobamos si la fuerza supera el umbral para romper el vidrio
        if (impactForce >= breakForceThreshold)
        {
            // Llamamos a una función para romper el vidrio
            BreakGlass();
        }
    }

    void BreakGlass()
    {
        Instantiate(particleBreak, transform.position, transform.rotation);
        Destroy(gameObject);

        // Puedes activar aquí un efecto visual de rotura, por ejemplo una partición de vidrio roto.
        // Puedes usar efectos de partículas, cambiar el modelo del objeto por otro roto, etc.
    }
}
