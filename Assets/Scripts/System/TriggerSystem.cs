using UnityEngine;
using UnityEngine.Events;

public class TriggerSystem : MonoBehaviour
{
    public UnityEvent onEnter, onStay, onExit;
    public string tagObject;

    private void OnTriggerEnter(Collider collision)
    {
        if (tagObject == "" || collision.CompareTag(tagObject))
        {
            onEnter.Invoke();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (tagObject == "" || collision.CompareTag(tagObject))
        {
            onStay.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (tagObject == "" || collision.CompareTag(tagObject))
        {
            onExit.Invoke();
        }
    }
}
