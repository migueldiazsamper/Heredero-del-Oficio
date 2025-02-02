using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaMina : MonoBehaviour
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
        if ( PhasesManager.instance.currentPhase == 2 && ( PhasesManager.instance.vecesMina == 2 || PhasesManager.instance.vecesMina == 0 ) )
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
                // Reproducir sonido camino del pueblo a la mina
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().enterMine, AudioManager.GetInstance().enterMineVolume);
                
                if ( PhasesManager.instance.vecesMina == 0 )
                {
                    ChangeScenes.LoadScene("Mina");
                }
                else
                {
                    ChangeScenes.LoadScene("Pueblo");
                }

                PhasesManager.instance.vecesMina++;

                PhasesManager.instance.currentQuest = "Fabrica";
            }
        }
    }
}
