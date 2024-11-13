using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [ Header( "Dialogue UI" ) ]
    [ SerializeField ] private GameObject dialoguePanel;
    [ SerializeField ] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private static DialogueManager instance;

    private void Awake ()
    {
        if ( instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy( this.gameObject );
        }
    }

    private void Start ()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive( false );
    }

    private void Update ()
    {
        if ( ! dialogueIsPlaying )
        {
            return;
        }

        if (  Input.GetKeyDown( KeyCode.Space ) )
        {
            ContinueStory();
        }
    }

    public static DialogueManager GetInstance ()
    {
        return instance;
    }

    public void EnterDialogueMode ( TextAsset inkJSON )
    {
        currentStory = new Story( inkJSON.text );
        dialogueIsPlaying = true;
        dialoguePanel.SetActive( true );
        ContinueStory();
    }

    private void ExitDialogueMode ()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive( false );
        dialogueText.text = "";
    }

    private void ContinueStory ()
    {
        bool thereAreMoreLines = currentStory.canContinue;
        if ( thereAreMoreLines )
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}