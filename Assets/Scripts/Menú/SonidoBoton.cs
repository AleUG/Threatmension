using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SonidoBoton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip sonidoSeleccion;
    public AudioClip sonidoPresion;
    public Button[] botones;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Agregar los eventos de rat�n a cada bot�n
        for (int i = 0; i < botones.Length; i++)
        {
            int index = i; // Almacenar el valor de 'i' en una variable temporal para evitar problemas de captura incorrecta en los eventos
            botones[i].gameObject.AddComponent<EventTrigger>();

            EventTrigger trigger = botones[i].gameObject.GetComponent<EventTrigger>();

            // Evento OnPointerEnter
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => ReproducirSonidoSeleccion());
            trigger.triggers.Add(pointerEnter);

            // Evento OnPointerClick
            EventTrigger.Entry pointerClick = new EventTrigger.Entry();
            pointerClick.eventID = EventTriggerType.PointerClick;
            pointerClick.callback.AddListener((eventData) => ReproducirSonidoPresion(index));
            trigger.triggers.Add(pointerClick);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ReproducirSonidoSeleccion();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Buscar el �ndice del bot�n en el arreglo
        int index = -1;
        for (int i = 0; i < botones.Length; i++)
        {
            if (botones[i].gameObject == eventData.pointerPress)
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            ReproducirSonidoPresion(index);
        }
    }

    public void ReproducirSonidoSeleccion()
    {
        audioSource.PlayOneShot(sonidoSeleccion);
    }

    public void ReproducirSonidoPresion(int index)
    {
        audioSource.PlayOneShot(sonidoPresion);

        // Ejecutar la acci�n correspondiente al bot�n presionado
        // Aqu� puedes agregar tu l�gica personalizada seg�n el bot�n seleccionado
        Debug.Log("Bot�n " + index + " presionado");
    }
}