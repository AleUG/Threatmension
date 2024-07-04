using UnityEngine;

public class Glass : MonoBehaviour
{
    public float breakForceThreshold = 10.0f; // Umbral de fuerza para romper el vidrio

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
        // Ejemplo: Desactivamos este objeto (vidrio) y activamos un efecto de rotura
        gameObject.SetActive(false);

        // Puedes activar aqu� un efecto visual de rotura, por ejemplo una partici�n de vidrio roto.
        // Puedes usar efectos de part�culas, cambiar el modelo del objeto por otro roto, etc.
    }
}
