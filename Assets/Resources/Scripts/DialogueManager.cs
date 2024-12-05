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

    private bool isTyping = false;

    [SerializeField] private float typingSpeed = 0.05f; // Velocidad de tipeo

    private Animator animator;

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

        animator = GameObject.Find( "PersonajePrincipal" ).GetComponent<Animator>();
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

        if (Time.timeScale == 1)
        {
            if (  Input.GetKeyDown( KeyCode.Space ) )
            {
                if ( isTyping )
                {
                    StopAllCoroutines();
                    isTyping = false;
                    dialogueText.text = currentStory.currentText;
                }
                else
                {
                    ContinueStory();
                }
            }
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
        animator.SetFloat( "speedX", 0 );
        animator.SetFloat( "speedY", 0 );
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
            string nextLine = currentStory.Continue();
            StopAllCoroutines();
            isTyping = true;
            StartCoroutine(TypeLine(nextLine));
        }
        else
        {
            ExitDialogueMode();
            isTyping = false;
        }
    }

    private IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
