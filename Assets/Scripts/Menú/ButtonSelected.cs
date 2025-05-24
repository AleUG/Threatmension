using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    private void Awake()
    {
        if (transform.parent != null)
        {
            animator = transform.parent.GetComponent<Animator>();
        }
    }
    // Método llamado cuando el mouse entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!animator.GetBool("over"))
        {
            animator.SetBool("over", true);
        }
    }

    // Método llamado cuando el mouse sale del área del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator.GetBool("over"))
        {
            animator.SetBool("over", false);
        }
    }
}
