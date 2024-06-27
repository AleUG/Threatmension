using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonIconManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Referencia al Image del ícono
    public GameObject icon;

    void Start()
    {
        // Asegurarse de que el icono esté oculto al inicio
        icon.SetActive(false);
    }

    // Método llamado cuando el mouse entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Habilitar el Image del ícono y asignar el sprite del ícono
        icon.SetActive(true);
    }

    // Método llamado cuando el mouse sale del área del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        // Ocultar el Image del ícono
        icon.SetActive(false);
    }
}
