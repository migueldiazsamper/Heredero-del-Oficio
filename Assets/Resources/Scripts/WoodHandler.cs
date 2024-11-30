using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WoodHandler : MonoBehaviour
{
    [SerializeField] GameObject[] woodPool;
    [SerializeField] List<GameObject> deckSlots;
    private int woodTypesNum = 3;
    private void Start()
    {
        /*
        Ayudas para developer, al comenzar el play desaparecen todas las maderas que son solo parte del pool
        pero no deben estar en el deck, reorganizan las que están en el deck y les da unos valores determinados
        para que comience la partida (1 madera de cada tipo en el deck)
        */
        for (int i = 4; i < woodPool.Length; i++)
        {
            woodPool[i].SetActive(false);
        }

        OrganizeDeck();
        for (int i = 0; i < 4; i++)
        {
            GiveWoodNewType(deckSlots[i], i);
        }
    }
    public void ResetMaderita(DragDropMinigame4 dragDropScript, bool comesFromDeck)
    {
        //El reseteo de una madera solo requiere que no esté activa y cambiar su estado de quemado
        dragDropScript.GetComponentInParent<CombustibleHorno>().ChangeBurntStatus(false);
        dragDropScript.gameObject.SetActive(false);

        if(comesFromDeck)
        {
            RemoveMaderitaFromDeck(dragDropScript.gameObject);
        }
        AddMaderitaToDeck();
    }

    public void RemoveMaderitaFromDeck(GameObject maderita)
    {
        foreach (GameObject woodInDeck in deckSlots)
        {
            if(woodInDeck == maderita)
            {
                deckSlots.Remove(woodInDeck);
                break;
            }
        }
    }

    public void AddMaderitaToDeck()
    {
        //Para añadir una nueva maderita a la baraja, primero debemos comprobar si quedan espacios vacíos
        if(deckSlots.Count < 4)
        {
            GameObject maderita = GetFirstInactiveMaderita(); //Obtenemos la primera madera libre del pool
            maderita.SetActive(true);
            GiveWoodNewType(maderita, Random.Range(0, woodTypesNum));
            deckSlots.Insert(deckSlots.Count, maderita); //Insertamos la nueva madera en la lista
            OrganizeDeck(); //Por último movemos las maderas que ya hay a sus posiciones correctas
        }

    }

    private void GiveWoodNewType(GameObject maderita, int value)
    {
        //Nuevo tipo de madera
        switch(value)
        {
            case 0: //Madera pequeña
                maderita.GetComponent<CombustibleHorno>().SetWoodLife(3f); //Nuevo valor de duración (el calor viene dado por la duración)
                maderita.GetComponent<Image>().color = Color.blue; //Nuevo sprite dependiendo del tipo
                break;
            case 1: //Madera mediana
                maderita.GetComponent<CombustibleHorno>().SetWoodLife(5f); //Nuevo valor de duración (el calor viene dado por la duración)
                maderita.GetComponent<Image>().color = Color.green; //Nuevo sprite dependiendo del tipo
                break;
            default: //Madera grande
                maderita.GetComponent<CombustibleHorno>().SetWoodLife(7f); //Nuevo valor de duración (el calor viene dado por la duración)
                maderita.GetComponent<Image>().color = Color.magenta; //Nuevo sprite dependiendo del tipo
                break;
        }
    }

    private GameObject GetFirstInactiveMaderita()
    {
        foreach (GameObject maderita in woodPool)
        {
            if(!maderita.activeSelf) return maderita;
        }
        return null;
    }

    private void OrganizeDeck()
    {
        for (int i = 0; i < deckSlots.Count; i++)
        {
            switch(i)
            {
                case 0:
                    deckSlots[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(749, 45);
                    break;
                case 1:
                    deckSlots[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(872, 45);
                    break;
                case 2:
                    deckSlots[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(749, 157);
                    break;
                case 3:
                    deckSlots[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(872, 157);
                    break;
            }
        }
    }
}
