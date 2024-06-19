using UnityEngine;
using UnityEngine.UI;

public class ActivarCanvas : MonoBehaviour
{
    public void ActivateBoolCanvas(GameObject canvas)
    {
        canvas.SetActive(!canvas.gameObject.activeSelf);
    }
}