using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;

    // M�todo llamado cuando el mouse entra en el �rea del bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!animator.GetBool("over"))
        {
            animator.SetBool("over", true);
        }
    }

    // M�todo llamado cuando el mouse sale del �rea del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator.GetBool("over"))
        {
            animator.SetBool("over", false);
        }
    }
}
