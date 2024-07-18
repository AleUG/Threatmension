using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExit : MonoBehaviour
{
    private Animator animator;
    private CambiarEscena cambiarEscena;
    private Rigidbody playerRB;

    private PlayerMovement playerMovement;
    private BoxCollider playerCollider;

    private Animator animatorLevel;
    private bool moveToDoor = false;
    private Vector3 puertaPosicion;
    private Vector3 initialScale; // Nueva variable para la escala inicial

    private CanvasGroup canvasUI;

    private GameManager gameManager;
    public int levelCompleted;

    private bool hasTouched = false;

    //Audios
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    // Start is called before the first frame update
    void Awake()
    {
        cambiarEscena = FindAnyObjectByType<CambiarEscena>();

        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();

        animator = GetComponent<Animator>();
        animatorLevel = GameObject.Find("Level").GetComponent<Animator>();
        canvasUI = GameObject.Find("UI").GetComponent<CanvasGroup>();

        audioSource = GetComponent<AudioSource>();
        gameManager = FindAnyObjectByType<GameManager>();

        initialScale = playerRB.transform.localScale; // Guardar la escala inicial del jugador
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasTouched)
        {
            hasTouched = true;
            animator.Play("Open");
            PlayAudioSource(audioClips[0]);

            canvasUI.interactable = false;
            gameManager.CompletarNivel(levelCompleted);

            StartCoroutine(StartTransicion());

        }
    }

    private IEnumerator StartTransicion()
    {
        yield return new WaitForSeconds(1.0f);

        // Iniciar el movimiento hacia la puerta
        puertaPosicion = transform.position;
        moveToDoor = true;

        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if(playerRB != null)
        {
            // Dar un impulso de salto hacia arriba
            playerRB.AddForce(Vector3.up * 500f);  // Ajusta la fuerza del salto según sea necesario
        }

        yield return new WaitForSeconds(0.5f); // Esperar un segundo para la transición

        animatorLevel.Play("level_end");
        PlayAudioSource(audioClips[1]);

        cambiarEscena.TimeDelayed(true);
        cambiarEscena.CambiarASiguienteEscena(1);
    }

    void FixedUpdate()
    {
        if (moveToDoor)
        {
            if (playerRB != null)
            {
                Vector3 direccionPuerta = (puertaPosicion - playerRB.position).normalized;
                playerRB.MovePosition(playerRB.position + direccionPuerta * 12.5f * Time.fixedDeltaTime);  // Ajusta la velocidad según sea necesario

                // Reducir la escala del jugador en función de la distancia a la puerta
                float distanciaTotal = Vector3.Distance(puertaPosicion, playerRB.position);
                float escalaFactor = Mathf.Clamp01(distanciaTotal / 1.25f);  // Ajusta el divisor según la distancia deseada

                playerRB.transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, escalaFactor);

                if (Vector3.Distance(playerRB.position, puertaPosicion) < 0.05f)
                {
                    moveToDoor = false;

                    // Asegurarse de que la escala del jugador sea cero al final
                    playerRB.transform.localScale = Vector3.zero;

                    // Aquí puedes hacer que el jugador desaparezca o realices cualquier otra acción necesaria.
                    // Ejemplo: Desactivar el GameObject del jugador.
                    HPSystem playerHP = FindAnyObjectByType<HPSystem>();
                    playerHP.RecibirDaño();
                }
            }
        }
    }


    private void PlayAudioSource(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
