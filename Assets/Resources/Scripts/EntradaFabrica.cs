using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaFabrica : MonoBehaviour
{
    [ Header ( "Visual Cue" ) ]
    [ SerializeField ] private GameObject visualCue;

    private GameObject player;

    private bool playerInRange;

    private void Awake()
    {
        player = GameObject.Find("PersonajePrincipal");
        visualCue.SetActive( false );
        playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool playerEntered = collision.gameObject == player;
        if (  playerEntered )
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D ( Collider2D collision )
    {
        bool playerEntered = collision.gameObject == player;
        if (  playerEntered )
        {
            playerInRange = false;
        }
    }

    private void Update ()
    {
        if ( PhasesManager.instance.currentPhase == 2 )
        {
            this.gameObject.SetActive( false );
        }
        else
        {
            this.gameObject.SetActive( true );
        }

        if ( playerInRange )
        {
            visualCue.SetActive( true );

            if (  Input.GetKeyDown( KeyCode.E ) )
            {
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
        else
        {
            visualCue.SetActive( false );
        }
    }
}
