using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonIconManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Referencia al Image del �cono
    public GameObject icon;

    void Start()
    {
        // Asegurarse de que el icono est� oculto al inicio
        icon.SetActive(false);
    }

    // M�todo llamado cuando el mouse entra en el �rea del bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Habilitar el Image del �cono y asignar el sprite del �cono
        icon.SetActive(true);
    }

    // M�todo llamado cuando el mouse sale del �rea del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        // Ocultar el Image del �cono
        icon.SetActive(false);
    }
}
