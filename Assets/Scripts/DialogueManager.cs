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
    private bool dialogueIsPlaying = false;
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

    public static DialogueManager GetInstance ()
    {
        return instance;
    }
}
