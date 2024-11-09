using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [ Header ( "Visual Cue" ) ]
    [ SerializeField ] private GameObject visualCue;

    [ Header ( "Ink JSON" ) ]
    [ SerializeField ] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake ()
    {
        playerInRange = false;
        visualCue.SetActive( false );
    }

    private void Update ()
    {
        if ( playerInRange )
        {
            visualCue.SetActive( true );

            if (  Input.GetKeyDown( KeyCode.E ) )
            {
                Debug.Log( "Dialogue Triggered" );
            }
        }
        else
        {
            visualCue.SetActive( false );
        }
    }

    private void OnTriggerEnter2D ( Collider2D collider )
    {
        bool playerEntered = collider.gameObject.tag == "Player";
        if (  playerEntered )
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D ( Collider2D collider )
    {
        bool playerEntered = collider.gameObject.tag == "Player";
        if (  playerEntered )
        {
            playerInRange = false;
        }
    }
}
