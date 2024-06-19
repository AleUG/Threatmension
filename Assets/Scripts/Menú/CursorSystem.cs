using UnityEngine;

public class CursorSystem : MonoBehaviour
{
    public void HabilitarCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void DesactivarCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
