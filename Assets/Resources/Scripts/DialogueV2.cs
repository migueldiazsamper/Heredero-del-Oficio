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
    /* [ SerializeField ] private Sprite fondoMina;
    [ SerializeField ] private Sprite fondoDialogoNormal;
    [ SerializeField ] private GameObject fondo; */
    
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
        /* if ( PhasesManager.instance.currentPhase == 2 || PhasesManager.instance.currentPhase == 3 )
        {
            fondo.GetComponent<Image>().sprite = fondoMina;
        }
        else
        {
            fondo.GetComponent<Image>().sprite = fondoDialogoNormal;
        } */

        if (Time.timeScale == 1)
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
        
    }

    private void NextNPC ()
    {
        if (Time.timeScale == 1)
        {
            if ( PhasesManager.instance.currentPhase >= 2 )
            {
                PhasesManager.instance.nextIsPueblo = !PhasesManager.instance.nextIsPueblo;
            }
            else
            {
                PhasesManager.instance.nextIsPueblo = true;
            }
            
            PhasesManager.instance.NextPhase();


            if (  PhasesManager.instance.nextIsPueblo )
            {
                ChangeScenes.LoadScene("Pueblo");
            }
            else
            {
                if (PhasesManager.instance.currentPhase == 11)
                {
                    ChangeScenes.LoadScene("Esmalte");
                }
                else
                {
                    ChangeScenes.LoadScene("Minijuego " + PhasesManager.instance.currentPhase / 2);
                }
            }
            
            currentStory = stories[ PhasesManager.instance.currentPhase ];
        }
    }

    private void NextLine ()
    {
        if (Time.timeScale == 1)
        {
            bool thereAreMoreLines = currentStory.canContinue;
            if ( thereAreMoreLines )
            {
                // Reproducir sonido npc (falta personalizar a cada uno)
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().voiceMaria);

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
    }

    private IEnumerator TypeLine(string line)
    {
        if (Time.timeScale == 1)
        {
            dialogueText.text = "";
            foreach (char letter in line.ToCharArray())
            {                
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);

                // Reproducir sonido pieza
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().dialogueSound);
            }
            isTyping = false;
        }
    }
}
