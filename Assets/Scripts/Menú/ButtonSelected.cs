using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;

    // Método llamado cuando el mouse entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Over");
        animator.SetBool("over", true);

    }

    // Método llamado cuando el mouse sale del área del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        // Ocultar el Image del ícono
        animator.SetBool("over", false);
    }
}
