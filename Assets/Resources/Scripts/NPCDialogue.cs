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

    [SerializeField] private bool isInMina;
    [SerializeField] private bool isLluna;
    [SerializeField] private bool isSabio;

    private MainCharacterManager mainCharacterManager;
    private EntradaMina entradaMina;

    private bool playerInRange;

    private void Awake ()
    {
        playerInRange = false;
        personaje.GetComponent<SpriteRenderer>().sprite = noOutlineSkin;
        mainCharacterManager = FindObjectOfType<MainCharacterManager>();
        entradaMina = FindObjectOfType<EntradaMina>();
    }

    private void Update ()
    {
        if ( ( ! isLluna || PhasesManager.instance.currentPhase == 12 ) && ( ! isSabio || PhasesManager.instance.currentPhase == 8 ) )
        {
            if ( playerInRange && ! DialogueManager.GetInstance().dialogueIsPlaying )
            {
                personaje.GetComponent<SpriteRenderer>().sprite = outlineSkin;

                if (  Input.GetKeyDown( KeyCode.E ) )
                {
                    DialogueManager.GetInstance().EnterDialogueMode( inkJSON );

                    if ( isInMina )
                    {
                        PhasesManager.instance.vecesMina++;
                        // Desactiva su propio BoxCollider2D
                        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                        if (boxCollider != null)
                        {
                            boxCollider.enabled = false;
                        }

                        if ( isSabio )
                        {
                            PhasesManager.instance.tieneQueHablarConSabio = false;
                        }
                    }
                }
            }
            else
            {
                personaje.GetComponent<SpriteRenderer>().sprite = noOutlineSkin;
            }
        }
    }

    private void OnTriggerEnter2D ( Collider2D collider )
    {
        if ( ( ! isLluna || PhasesManager.instance.currentPhase == 12 ) && ( ! isSabio || PhasesManager.instance.currentPhase == 8 ) )
        {
            bool playerEntered = collider.gameObject.tag == "Player";
            if (  playerEntered )
            {
                playerInRange = true;
                mainCharacterManager.SetVisualCue( true );
            }
        }
    }

    private void OnTriggerExit2D ( Collider2D collider )
    {
        if ( ( ! isLluna || PhasesManager.instance.currentPhase == 12 ) && ( ! isSabio || PhasesManager.instance.currentPhase == 8 ) )
        {
            bool playerEntered = collider.gameObject.tag == "Player";
            if (  playerEntered )
            {
                playerInRange = false;
                mainCharacterManager.SetVisualCue( false );
            }
        }
    }
}
