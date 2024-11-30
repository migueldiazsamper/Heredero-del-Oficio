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
        if ( playerInRange )
        {
            visualCue.SetActive( true );

            if (  Input.GetKeyDown( KeyCode.E ) )
            {
                ChangeScenes.LoadScene("DialogoInterior");
            }
        }
        else
        {
            visualCue.SetActive( false );
        }
    }
}
