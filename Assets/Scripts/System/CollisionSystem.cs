using UnityEngine;
using UnityEngine.Events;

public class CollisionSystem : MonoBehaviour
{
    public UnityEvent onEnter, onStay, onExit;
    public string tagObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagObject))
        {
            onEnter.Invoke();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagObject))
        {
            onStay.Invoke();
        }    
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagObject))
        {
            onExit.Invoke();
        } 
    }
}
