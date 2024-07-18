using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> panelBlocks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        ActualizarDatos();
    }

    private void ActualizarDatos()
    {
        if (panelBlocks != null)
        {
            for (int i = 0; i < panelBlocks.Count; i++)
            {
                // Verificar si el nivel se ha completado usando PlayerPrefs
                if (PlayerPrefs.GetInt("Level_" + i, 0) == 1)
                {
                    // Si el nivel se ha completado, desactivar el objeto correspondiente
                    panelBlocks[i].SetActive(false);
                }
                else
                {
                    // Si el nivel no se ha completado, asegurar que el objeto esté activo
                    panelBlocks[i].SetActive(true);
                }
            }
        }
    }

    // Llamar a esta función cuando el jugador complete un nivel
    public void CompletarNivel(int level)
    {
        // Guardar el estado de que el nivel se ha completado
        PlayerPrefs.SetInt("Level_" + level, 1);
        // Asegurarse de que los cambios se guarden inmediatamente
        PlayerPrefs.Save();
        // Actualizar los datos para reflejar el cambio
        ActualizarDatos();
    }

    public void DeleteData()
    {
        // Eliminar datos de todos los niveles guardados
        for (int i = 0; i < panelBlocks.Count; i++)
        {
            PlayerPrefs.DeleteKey("Level_" + i);
        }
        // Asegurarse de que los cambios se guarden inmediatamente
        PlayerPrefs.Save();
        // Actualizar los datos para reflejar los cambios
        ActualizarDatos();
    }
}
