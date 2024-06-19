using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del jugador
    public LayerMask clickableLayer;  // Capas donde se puede hacer click para moverse
    private Vector3 targetPosition;  // Posición objetivo a la que el jugador se moverá
    private bool isMoving = false;  // Indicador de si el jugador está moviéndose hacia un objetivo

    private void Update()
    {
        // Detectar el click izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            // Obtener la posición en el mundo a la que se hizo click
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                // Si se hizo click en un lugar válido (que tiene un collider y está en la capa clickableLayer)
                targetPosition = hit.point;
                targetPosition.y = transform.position.y;  // Mantener la misma altura del jugador
                isMoving = true;  // Comenzar movimiento hacia el punto clickeado
            }
        }

        // Mover al jugador hacia la posición objetivo
        if (isMoving && Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            //transform.LookAt(targetPosition);  // Mirar hacia la posición objetivo
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void StopMoving()
    {
        isMoving = false;  // Detener el movimiento del jugador
    }
}
