using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField] private float typingSpeed = 0.05f; // Velocidad de tipeo
    private bool isTyping = false;

    private void Start ()
    {
        stories = new Story[ inkJSONs.Length ];
        
        for ( int i = 0; i < inkJSONs.Length; i++ )
        {
            stories[ i ] = new Story( inkJSONs[ i ].text );
        }

        currentStory = stories[ PhasesManager.instance.currentPhase ];
        NextLine();
    }

    private void Update ()
    {
        if ( PhasesManager.instance.currentPhase == 8 )
        {
            if (PhasesManager.instance.puntuacionTotal >= 10) // Final Condesa
            {
                isLeft[PhasesManager.instance.currentPhase] = true;

                string portraitPath = "Art/Portraits Diálogos/CondesaSprite";
                Sprite portraitSprite = Resources.Load<Sprite>(portraitPath);
                portraitGameObject.GetComponent<Image>().sprite = portraitSprite;

                nameText.text = "CONDESA DE ARANDA";

                // Carga el JSON específico para el final de la Condesa
                string jsonPath = "Dialogues/Diálogos Finales/Condesa";
                TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonPath);
                currentStory = new Story(jsonTextAsset.text);
            }
            else // Final con el padre
            {
                isLeft[PhasesManager.instance.currentPhase] = false;

                string portraitPath = "Art/Portraits Diálogos/PadreSprite";
                Sprite portraitSprite = Resources.Load<Sprite>(portraitPath);
                portraitGameObject.GetComponent<Image>().sprite = portraitSprite;

                nameText.text = "MANEL";

                // Carga el JSON específico para el final con el padre
                string jsonPath = "Dialogues/Diálogos Finales/Manel";
                TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonPath);
                currentStory = new Story(jsonTextAsset.text);
            }
        }
        else
        {
            // Asigna la imagen del portrait correspondiente
            portraitGameObject.GetComponent<Image>().sprite = portraits[PhasesManager.instance.currentPhase];

            // Ajusta las posiciones según el valor de isLeft
            if (isLeft[PhasesManager.instance.currentPhase])
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
            nameText.text = names[PhasesManager.instance.currentPhase];
        }

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
        PhasesManager.instance.nextIsPueblo = !PhasesManager.instance.nextIsPueblo;
        Debug.Log(PhasesManager.instance.nextIsPueblo);

        if (  PhasesManager.instance.nextIsPueblo )
        {
            ChangeScenes.LoadScene("Pueblo");
            PhasesManager.instance.NextPhase();
        }
        else
        {
            if (PhasesManager.instance.currentPhase == 8)
            {
                Application.Quit();
            }
            else if (PhasesManager.instance.currentPhase == 5)
            {
                ChangeScenes.LoadScene("Esmalte");
            }
            else
            {
                ChangeScenes.LoadScene("Minijuego " + PhasesManager.instance.currentPhase);
            }

        }
        
        currentStory = stories[ PhasesManager.instance.currentPhase ];
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
