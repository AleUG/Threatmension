using UnityEngine;
using Cinemachine;

public class GrabAndDrag : MonoBehaviour
{
    private CinemachineVirtualCamera[] virtualCameras;
    private CinemachineVirtualCamera currentVirtualCamera;

    private GameObject selectedObject;
    private Vector3 objectOffset;

    void Start()
    {
        // Obtener todas las c�maras virtuales configuradas en la escena
        virtualCameras = new CinemachineVirtualCamera[4]; // Ajusta el tama�o del array seg�n tus necesidades
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i] = GameObject.Find("Camara" + (i + 1)).GetComponent<CinemachineVirtualCamera>(); // Ajusta el nombre seg�n tus c�maras virtuales
        }

        // Establecer la primera c�mara virtual como la c�mara actual
        currentVirtualCamera = virtualCameras[0];
    }

    void OnMouseDrag()
    {
        if (selectedObject != null)
        {
            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, currentVirtualCamera.transform.position.z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // Ajustar la posici�n bas�ndonos en el desplazamiento inicial del objeto
            selectedObject.transform.position = worldPosition + objectOffset;
        }
    }

    void OnMouseDown()
    {
        RaycastHit hitInfo;
        if (RaycastObject(out hitInfo))
        {
            selectedObject = hitInfo.transform.gameObject;
            // Calcular el offset desde el punto de contacto hasta el centro del objeto
            objectOffset = selectedObject.transform.position - hitInfo.point;
        }
    }

    void OnMouseUp()
    {
        selectedObject = null;
    }

    bool RaycastObject(out RaycastHit hitInfo)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hitInfo);
    }
}
