using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del jugador
    public LayerMask clickableLayer;  // Capas donde se puede hacer click para moverse
    public GameObject clickEffectPrefab;  // Prefab de las part�culas para indicar el click
    public float clickEffectOffset = 0.1f;  // Compensaci�n en la altura para el prefab de part�culas
    private Vector3 targetPosition;  // Posici�n objetivo a la que el jugador se mover�
    private bool isMoving = false;  // Indicador de si el jugador est� movi�ndose hacia un objetivo
    private Rigidbody rb;  // Referencia al componente Rigidbody
    private bool hasClicked = false;  // Indicador de si se ha hecho click y las part�culas se han instanciado

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtener el componente Rigidbody
        targetPosition = transform.position;  // Inicialmente, la posici�n objetivo es la posici�n actual
        rb.constraints = RigidbodyConstraints.FreezeRotation;  // Conservar rotaci�n libre pero no mover en Y
    }

    private void Update()
    {
        // Detectar el click izquierdo del rat�n
        if (Input.GetMouseButton(0))
        {
            // Comprobar si el cursor est� sobre un elemento de la UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            // Obtener la posici�n en el mundo a la que se hizo click
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                // Si se hizo click en un lugar v�lido (que tiene un collider y est� en la capa clickableLayer)
                targetPosition = hit.point;
                isMoving = true;  // Comenzar movimiento hacia el punto clickeado

                // Instanciar el prefab de part�culas solo una vez
                if (!hasClicked)
                {
                    Vector3 clickPosition = hit.point;
                    clickPosition.y += clickEffectOffset;
                    Instantiate(clickEffectPrefab, clickPosition, Quaternion.identity);
                    hasClicked = true;  // Marcar que ya se instanciaron las part�culas
                }
            }
        }

        // Mover al jugador hacia la posici�n objetivo usando Rigidbody
        if (isMoving)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);

            // Detener el movimiento si estamos lo suficientemente cerca del objetivo
            Vector3 horizontalTargetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            if (Vector3.Distance(transform.position, horizontalTargetPosition) <= 0.1f)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                isMoving = false;
            }
        }

        if (!Input.GetMouseButton(0))
        {
            hasClicked = false;  // Reiniciar para permitir una nueva instancia de part�culas en el siguiente click
        }
    }

    public void StopMoving()
    {
        isMoving = false;  // Detener el movimiento del jugador
        rb.velocity = new Vector3(0, rb.velocity.y, 0);  // Detener el Rigidbody
    }

    public void ImpulseRotate()
    {
        // Congelar el movimiento en el eje Y manteniendo otras restricciones
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        rb.AddForce(Vector3.up * 50f, ForceMode.Impulse);

        // Llamar a la corutina para desactivar isKinematic despu�s de 1 segundo
        StartCoroutine(DisableKinematicAfterDelay(0.15f));
    }

    private IEnumerator DisableKinematicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Restablecer las restricciones a solo congelar la posici�n en Y y mantener otras restricciones necesarias
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }
}
