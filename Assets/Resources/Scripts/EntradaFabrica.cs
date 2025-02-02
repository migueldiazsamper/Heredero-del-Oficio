using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaFabrica : MonoBehaviour
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

    void ManageBoxCollider()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null) return;

        bool shouldDisableCollider = 
            (PhasesManager.instance.currentPhase == 2 && PhasesManager.instance.vecesMina < 3) ||
            (PhasesManager.instance.currentPhase == 12 && PhasesManager.instance.vecesMina == 4) ||
            (PhasesManager.instance.currentPhase == 14 && PhasesManager.instance.vecesMina == 5) ||
            (PhasesManager.instance.currentPhase == 8);

        boxCollider.enabled = !shouldDisableCollider;
    }

    private void Update ()
    {
        ManageBoxCollider();

        if ( PhasesManager.instance.currentPhase == 16 )
        {
            transform.position = new Vector3( 53.55f, 99.02f, 0.0f );
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
