using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del jugador
    public LayerMask clickableLayer;  // Capas donde se puede hacer click para moverse
    public GameObject clickEffectPrefab;  // Prefab de las partículas para indicar el click
    public float clickEffectOffset = 0.1f;  // Compensación en la altura para el prefab de partículas
    private Vector3 targetPosition;  // Posición objetivo a la que el jugador se moverá
    private bool isMoving = false;  // Indicador de si el jugador está moviéndose hacia un objetivo
    private Rigidbody rb;  // Referencia al componente Rigidbody
    private bool hasClicked = false;  // Indicador de si se ha hecho click y las partículas se han instanciado

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtener el componente Rigidbody
        targetPosition = transform.position;  // Inicialmente, la posición objetivo es la posición actual
    }

    private void Update()
    {
        // Detectar el click izquierdo del ratón
        if (Input.GetMouseButton(0))
        {
            // Comprobar si el cursor está sobre un elemento de la UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            // Obtener la posición en el mundo a la que se hizo click
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                // Si se hizo click en un lugar válido (que tiene un collider y está en la capa clickableLayer)
                targetPosition = hit.point;
                targetPosition.y = transform.position.y;  // Mantener la misma altura del jugador
                isMoving = true;  // Comenzar movimiento hacia el punto clickeado

                // Instanciar el prefab de partículas solo una vez
                if (!hasClicked)
                {
                    Vector3 clickPosition = hit.point;
                    clickPosition.y += clickEffectOffset;
                    Instantiate(clickEffectPrefab, clickPosition, Quaternion.identity);
                    hasClicked = true;  // Marcar que ya se instanciaron las partículas
                }
            }
        }

        // Mover al jugador hacia la posición objetivo usando Rigidbody
        if (isMoving)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            // Detener el movimiento si estamos lo suficientemente cerca del objetivo
            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
            {
                rb.velocity = Vector3.zero;
                isMoving = false;
                hasClicked = false;  // Reiniciar para permitir una nueva instancia de partículas en el siguiente click
            }
        }
    }

    public void StopMoving()
    {
        isMoving = false;  // Detener el movimiento del jugador
        rb.velocity = Vector3.zero;  // Detener el Rigidbody
    }

    public void ImpulseRotate()
    {
        // Congelar el movimiento en el eje Y manteniendo otras restricciones
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        rb.AddForce(Vector3.up * 50f, ForceMode.Impulse);

        // Llamar a la corutina para desactivar isKinematic después de 1 segundo
        StartCoroutine(DisableKinematicAfterDelay(0.35f));
    }

    private IEnumerator DisableKinematicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Restablecer las restricciones a solo congelar la posición en Y y mantener otras restricciones necesarias
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }
}

