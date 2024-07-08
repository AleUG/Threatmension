using UnityEngine;
using UnityEngine.Events;

public class HPSystem : MonoBehaviour
{
    public int hp = 1;

    public GameObject deathParticle;

    public UnityEvent onHit, onDeath;

    private void Update()
    {
        if (hp <= 0)
        {
            onDeath.Invoke();
        }
    }

    public void Death()
    {
        Instantiate(deathParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void RecibirDaño()
    {
        hp--;
    }
}
