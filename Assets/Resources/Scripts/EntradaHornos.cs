using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaHornos : MonoBehaviour
{
    private GameObject player;

    private bool playerInRange;

    private MainCharacterManager mainCharacterManager;

    private void Awake()
    {
        player = GameObject.Find("PersonajePrincipal");
        playerInRange = false;
        mainCharacterManager = FindObjectOfType<MainCharacterManager>();
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

        if ( playerInRange )
        {
            if (  Input.GetKeyDown( KeyCode.E ) )
            {
                // Reproducir sonido entrar edificio
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().enterBuilding);

                if (PhasesManager.instance.currentPhase > 15)
                {
                    if (PhasesManager.instance.puntuacionTotal >= 10) // Final Condesa
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
