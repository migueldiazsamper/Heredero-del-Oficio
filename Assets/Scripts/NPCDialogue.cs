using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [ Header ( "Personaje" ) ]
    [ SerializeField ] private GameObject personaje;

    [ Header ( "Outline Skin" ) ]
    [ SerializeField ] private Sprite outlineSkin;

    [ Header ( "No Outline Skin" ) ]
    [ SerializeField ] private Sprite noOutlineSkin;

    [ Header ( "Ink JSON" ) ]
    [ SerializeField ] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake ()
    {
        playerInRange = false;
        personaje.GetComponent<SpriteRenderer>().sprite = noOutlineSkin;
    }

    private void Update ()
    {
        if ( playerInRange && ! DialogueManager.GetInstance().dialogueIsPlaying )
        {
            personaje.GetComponent<SpriteRenderer>().sprite = outlineSkin;

            if (  Input.GetKeyDown( KeyCode.E ) )
            {
                DialogueManager.GetInstance().EnterDialogueMode( inkJSON );
            }
        }
        else
        {
            personaje.GetComponent<SpriteRenderer>().sprite = noOutlineSkin;
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
