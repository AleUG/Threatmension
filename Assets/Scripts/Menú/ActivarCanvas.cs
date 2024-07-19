using UnityEngine;
using System.Collections;

public class ActivarCanvas : MonoBehaviour
{
    public GameObject canvasObject;
    // M�todo para activar o desactivar el canvas
    public void ActivateBoolCanvas(GameObject canvas)
    {
        Animator animator = canvas.GetComponent<Animator>();

        if (animator != null)
        {
            bool isActive = !canvas.gameObject.activeSelf;

            if (isActive)
            {
                // Activa el canvas
                canvas.SetActive(true);
            }
            else
            {
                // Activa el trigger "exit" para la transici�n a la animaci�n de salida
                animator.SetTrigger("exit");
                // Inicia la corrutina para desactivar el canvas despu�s de la duraci�n de la animaci�n de salida
                StartCoroutine(DesactivarDespuesDeAnimacion(animator, canvas));
            }
        }
        else
        {
            // Si no hay un Animator, simplemente activa o desactiva el canvas
            canvas.SetActive(!canvas.gameObject.activeSelf);
        }
    }

    private IEnumerator DesactivarDespuesDeAnimacion(Animator animator, GameObject canvas)
    {
        // Espera un frame para asegurar que el estado de la animaci�n se actualiza
        yield return null;

        // Obtiene la duraci�n de la animaci�n de salida
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        // Espera la duraci�n de la animaci�n
        yield return new WaitForSecondsRealtime(animationLength);

        // Desactiva el canvas
        canvas.SetActive(false);
    }

    public void ActivateCanvas()
    {
        if (canvasObject != null)
        {
            canvasObject.SetActive(!canvasObject.activeSelf);
        }
    }
}
