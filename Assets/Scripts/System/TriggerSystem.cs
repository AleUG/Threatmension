using UnityEngine;
using UnityEngine.Events;

public class TriggerSystem : MonoBehaviour
{
    public UnityEvent onEnter, onStay, onExit;
    public string tagObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            onEnter.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            onStay.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            onExit.Invoke();
        }
    }
}
