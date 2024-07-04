using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;

    // M�todo llamado cuando el mouse entra en el �rea del bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Over");
        animator.SetBool("over", true);

    }

    // M�todo llamado cuando el mouse sale del �rea del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        // Ocultar el Image del �cono
        animator.SetBool("over", false);
    }
}
