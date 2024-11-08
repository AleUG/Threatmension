using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System;

public class CambiarEscena : MonoBehaviour
{
    public string[] nombresEscenas; // Los nombres de las escenas a las que se desea cambiar
    public Button[] botonesCambiarEscena; // Los botones que se utilizarán para cambiar de escena
    private Animator animator;
    [SerializeField] private AnimationClip animacionFinal;

    private bool timeDelayed = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // Agregar el evento de clic a cada botón
        for (int i = 0; i < botonesCambiarEscena.Length; i++)
        {
            int index = i; // Almacenar el valor de 'i' en una variable temporal para evitar problemas de captura incorrecta en los eventos
            botonesCambiarEscena[i].onClick.AddListener(() => CambiarASiguienteEscena(index));
        }
    }

    public void CambiarASiguienteEscena(int index)
    {
        StartCoroutine(CargarEscenaConTransicion(index)); // Inicia la corutina para cargar la escena con transición
    }

    public void CambiarEscenaName(string name)
    {
        StartCoroutine(CargarEscenaTransicion(name)); // Inicia la corutina para cargar la escena con transición
    }

    public IEnumerator CargarEscenaConTransicion(int index)
    {
        if (timeDelayed)
        {
            yield return new WaitForSeconds(2.35f);
        }

        // Iniciar animación de oscurecimiento
        animator.SetTrigger("Iniciar");

        // Esperar un breve momento para que la animación de oscurecimiento se complete
        yield return new WaitForSeconds(animacionFinal.length);

        // Verificar si el índice es válido y cargar la escena correspondiente
        if (index >= 0 && index < nombresEscenas.Length)
        {
            SceneManager.LoadScene(nombresEscenas[index]);
        }
    }

    public IEnumerator CargarEscenaTransicion(string name)
    {
        if (timeDelayed)
        {
            yield return new WaitForSeconds(2.35f);
        }

        // Iniciar animación de oscurecimiento
        animator.SetTrigger("Iniciar");

        // Esperar un breve momento para que la animación de oscurecimiento se complete
        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(name);
    }

    public void TimeDelayed(bool state)
    {
        timeDelayed = state;
    }
}