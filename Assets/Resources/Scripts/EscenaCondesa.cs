using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscenaCondesa : MonoBehaviour
{
    [ Header ( "Ink JSON" ) ]
    [ SerializeField ] private TextAsset inkJSON;

    [ Header ( "Is Left?" ) ]
    [ SerializeField ] private bool isLeft;

    [ Header( "Dialogue UI" ) ]
    [ SerializeField ] private Sprite portrait;
    [ SerializeField ] private string name;
    [ SerializeField ] private GameObject portraitGameObject;
    [ SerializeField ] private TextMeshProUGUI dialogueText;
    [ SerializeField ] private TextMeshProUGUI nameText;
    
    private Story currentStory;

    [SerializeField] private float typingSpeed = 0.05f; // Velocidad de tipeo
    private bool isTyping = false;

    private void Start ()
    {
        currentStory = new Story( inkJSON.text );
        NextLine();
    }

    private void Update ()
    {
        // Asigna la imagen del portrait correspondiente
        portraitGameObject.GetComponent<Image>().sprite = portrait;

        // Ajusta las posiciones según el valor de isLeft
        if (isLeft)
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
        nameText.text = name;

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
        Application.Quit();
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
