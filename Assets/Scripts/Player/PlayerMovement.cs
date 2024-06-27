using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del jugador
    public LayerMask clickableLayer;  // Capas donde se puede hacer click para moverse
    public GameObject clickEffectPrefab;  // Prefab de las part�culas para indicar el click
    public float clickEffectOffset = 0.1f;  // Compensaci�n en la altura para el prefab de part�culas
    private Vector3 targetPosition;  // Posici�n objetivo a la que el jugador se mover�
    private bool isMoving = false;  // Indicador de si el jugador est� movi�ndose hacia un objetivo
    private Rigidbody rb;  // Referencia al componente Rigidbody

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtener el componente Rigidbody
        targetPosition = transform.position;  // Inicialmente, la posici�n objetivo es la posici�n actual
    }

    private void Update()
    {
        // Detectar el click izquierdo del rat�n
        if (Input.GetMouseButtonDown(0))
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
                targetPosition.y = transform.position.y;  // Mantener la misma altura del jugador
                isMoving = true;  // Comenzar movimiento hacia el punto clickeado

                // Instanciar el prefab de part�culas en la posici�n del click con una compensaci�n en Y
                Vector3 clickPosition = hit.point;
                clickPosition.y += clickEffectOffset;
                Instantiate(clickEffectPrefab, clickPosition, Quaternion.identity);
            }
        }

        // Mover al jugador hacia la posici�n objetivo usando Rigidbody
        if (isMoving)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            // Detener el movimiento si estamos lo suficientemente cerca del objetivo
            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
            {
                rb.velocity = Vector3.zero;
                isMoving = false;
            }
        }
    }

    public void StopMoving()
    {
        isMoving = false;  // Detener el movimiento del jugador
        rb.velocity = Vector3.zero;  // Detener el Rigidbody
    }
}
