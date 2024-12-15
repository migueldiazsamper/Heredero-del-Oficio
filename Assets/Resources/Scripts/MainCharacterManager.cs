using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterManager : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private float speedX;
    private float speedY;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private GameObject visualCue;
    private Animator animator;
    [SerializeField] private GameObject mapa;
    private bool isShowingMap = false;

    private Rigidbody2D rb;
    [SerializeField] private GameObject transitionImage;
    private Animator transitionImageAnimator;

    public void SetVisualCue(bool toggle)
    {
        visualCue.SetActive(toggle);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        visualCue.SetActive(false);
        characterTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        transitionImageAnimator = transitionImage.GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateMap();
        if(transitionImageAnimator.GetCurrentAnimatorStateInfo(0).IsName("TransitionImage_In")){
            animator.SetFloat("speedX", 0f);
            animator.SetFloat("speedY", 0f);
        }
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying || transitionImageAnimator.GetCurrentAnimatorStateInfo(0).IsName("TransitionImage_In"))
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        UpdateMovement();
    }

    private void UpdateMap ()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                // mapa.SetActive(true);
                isShowingMap = !isShowingMap;
                
                if (isShowingMap)
                {
                    mapa.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    mapa.SetActive(false);
                    Time.timeScale = 1;
                }
            }

        }
    }

    private void UpdateMovement()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying && !transitionImageAnimator.GetCurrentAnimatorStateInfo(0).IsName("TransitionImage_In"))
        {
            if (!isShowingMap)
            {
                // Obtiene las entradas de movimiento
                float speedX = Input.GetAxis("Horizontal");
                float speedY = Input.GetAxis("Vertical");

                // Crea un vector de velocidad y normaliza su magnitud
                Vector2 inputVelocity = new Vector2(speedX, speedY);
                inputVelocity = Vector2.ClampMagnitude(inputVelocity, 1.0f); // Normaliza la velocidad a un máximo de 1

                animator.SetFloat("speedX", inputVelocity.x);
                animator.SetFloat("speedY", inputVelocity.y);

                rb.velocity = inputVelocity * speed;

                if(rb.velocity.magnitude >= 4.0f)
                {
                    if(!CharacterMovementSound.GetInstance().characterMovementSource.isPlaying)
                    {
                        // Reproducir sonido del personaje moviéndose
                        CharacterMovementSound.GetInstance().PlayCharacterMovement();   
                    }           
                }
                else
                {
                    // Detener sonido del personaje moviéndose
                    CharacterMovementSound.GetInstance().StopCharacterMovement();
                }
            }
        }
    }
}
