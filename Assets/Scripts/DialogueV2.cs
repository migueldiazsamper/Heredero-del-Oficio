using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;

public class DialogueV2 : MonoBehaviour
{
    [ Header ( "Ink JSON" ) ]
    [ SerializeField ] private TextAsset[] inkJSONs;

    [ Header ( "Is Left?" ) ]
    [ SerializeField ] private bool[] isLeft;

    [ Header( "Dialogue UI" ) ]
    [ SerializeField ] private Sprite[] portraits;
    [ SerializeField ] private string[] names;
    [ SerializeField ] private GameObject portraitGameObject;
    [ SerializeField ] private TextMeshProUGUI dialogueText;
    [ SerializeField ] private TextMeshProUGUI nameText;
    
    private Story[] stories;
    private Story currentStory;
    private int currentGamePhase = 0;

    [SerializeField] private float typingSpeed = 0.05f; // Velocidad de tipeo
    private bool isTyping = false;

    private void Start ()
    {
        stories = new Story[ inkJSONs.Length ];
        
        for ( int i = 0; i < inkJSONs.Length; i++ )
        {
            stories[ i ] = new Story( inkJSONs[ i ].text );
        }

        currentStory = stories[ currentGamePhase ];
        NextLine();
    }

    private void Update ()
    {
        // Asigna la imagen del portrait correspondiente
        portraitGameObject.GetComponent<Image>().sprite = portraits[currentGamePhase];

        // Ajusta las posiciones según el valor de isLeft
        if (isLeft[currentGamePhase])
        {
            nameText.rectTransform.anchoredPosition = new Vector2(35, nameText.rectTransform.anchoredPosition.y);
            dialogueText.rectTransform.anchoredPosition = new Vector2(15, dialogueText.rectTransform.anchoredPosition.y);
            portraitGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-640, portraitGameObject.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else
        {
            nameText.rectTransform.anchoredPosition = new Vector2(-520, nameText.rectTransform.anchoredPosition.y);
            dialogueText.rectTransform.anchoredPosition = new Vector2(-545, dialogueText.rectTransform.anchoredPosition.y);
            portraitGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(640, portraitGameObject.GetComponent<RectTransform>().anchoredPosition.y);
        }

        // Asigna el nombre del personaje
        nameText.text = names[currentGamePhase];

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
                NextLine(); 
            }
        }
    }

    private void NextNPC ()
    {
        currentStory = stories[ ++currentGamePhase ];
        NextLine();
    }

    private void NextLine ()
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
            NextNPC();
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
