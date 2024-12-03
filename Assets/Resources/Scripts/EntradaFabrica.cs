using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaFabrica : MonoBehaviour
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
            transform.position = new Vector3( 52.2f, 98.76f, 0.0f );
        }

        if ( playerInRange )
        {
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
    }
}