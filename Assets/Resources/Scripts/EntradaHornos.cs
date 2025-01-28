using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaHornos : MonoBehaviour
{
    private GameObject player;

    private bool playerInRange;

    private MainCharacterManager mainCharacterManager;
    [SerializeField] private GameObject transitionImage;
    private Animator transitionImageAnimator;

    private void Awake()
    {
        player = GameObject.Find("PersonajePrincipal");
        playerInRange = false;
        mainCharacterManager = FindObjectOfType<MainCharacterManager>();
        transitionImageAnimator = transitionImage.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool playerEntered = collision.gameObject == player;
        if (  playerEntered )
        {
            playerInRange = true;
            mainCharacterManager.SetVisualCue( true );
        }
    }

    private void OnTriggerExit2D ( Collider2D collision )
    {
        bool playerEntered = collision.gameObject == player;
        if (  playerEntered )
        {
            playerInRange = false;
            mainCharacterManager.SetVisualCue( false );
        }
    }

    private void Update ()
    {
        if ( ! PhasesManager.instance.tieneQueHablarConSabio && ( PhasesManager.instance.currentPhase == 8 || PhasesManager.instance.currentPhase == 14 ) )
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.enabled = true;
            }
        }
        else
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
        }

        if ( playerInRange && !transitionImageAnimator.GetCurrentAnimatorStateInfo(0).IsName("TransitionImage_In") && !transitionImageAnimator.GetCurrentAnimatorStateInfo(0).IsName("TransitionImage_Out") )
        {   
            if (  Input.GetKeyDown( KeyCode.E ) )
            {
                // Reproducir sonido entrar edificio
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().enterBuilding, AudioManager.GetInstance().enterBuildingVolume);

                if (PhasesManager.instance.currentPhase > 15)
                {
                    if (PhasesManager.instance.PuntuacionTotal() >= PhasesManager.instance.PuntuacionCondesa()) // Final Condesa
                    {
                        ChangeScenes.LoadScene("Condesa");
                    }
                    else // Final con el padre
                    {
                        ChangeScenes.LoadScene("Manel");
                    }
                }
                else
                {
                    ChangeScenes.LoadScene("DialogoInterior");
                }
            }
        }
    }
}
