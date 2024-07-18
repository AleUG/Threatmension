using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public float delay = 1f;
    public void Cerrar()
    {
        StartCoroutine(DelayExit());
    }

    private IEnumerator DelayExit()
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Saliendo del juego");
        Application.Quit(); // Cierra la aplicación

        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

}