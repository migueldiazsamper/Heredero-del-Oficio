using System;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image; //Hay overlap entre el image de UnityUI y el VSCode. Esto lo arregla

// Clase que gestiona la lógica de los pigmentos y los slots
public class PigmentosManager : MonoBehaviour
{

    [SerializeField] GameObject cuchara;
    [SerializeField] private Image mixedColorSpriteImage;

    public int colorCounter {get; private set;} = 0;
    private int[] colorPalette = new int[5]; //Este array contiene el nº usado de cada color en formato {b, m, c, y, w}

    void Awake(){
        for(int i = 0; i < 5; i++) colorPalette[i] = 0;
    }

    public void AddColorToMix(String colorString){
        if(colorCounter <= 5){
            colorCounter++;
            AddColorToPalette(colorString);
            DisplayColor(colorString);
            Debug.Log(colorCounter);

            //If there are 5 colors, lock the spoon for mixing
            if(colorCounter == 5) cuchara.GetComponent<DragDropCuchara>().LockToBowl();
        }
    }

    private void AddColorToPalette(String color){
        switch (color){
            case "Black": colorPalette[0]++; break;
            case "Magenta": colorPalette[1]++; break;
            case "Cyan": colorPalette[2]++; break;
            case "Yellow": colorPalette[3]++; break;
            case "White": colorPalette[4]++; break;
        }
    }

    private void DisplayColor(String colorString){
        ///Show the last color added
        mixedColorSpriteImage.color = ProvideColor(colorString);
        Debug.Log(mixedColorSpriteImage.color);
    }

    public Color ProvideColor(String colorString){
        switch (colorString){
            case "Black":   return Color.black;
            case "Magenta": return Color.magenta;
            case "Cyan":    return Color.cyan;
            case "Yellow":  return Color.yellow;
            case "White":   return Color.white;
            //La paleta está en formato BMCYW
            case "02030": return new Color(0.98f, 0.38f, 0.01f);
            case "10220": return new Color(0.07f, 0.29f, 0.03f);
            case "10112": return new Color(0.44f, 0.94f, 0.37f);
            case "10400": return new Color(0.00f, 0.07f, 0.37f);
            case "02120": return new Color(0.42f, 0.20f, 0.02f);
            case "00302": return new Color(0.22f, 0.80f, 0.95f);
            case "01112": return new Color(0.82f, 0.71f, 0.55f);
            default: return Color.grey;
        }
    }

    public void MixColors(){
        //Conversión del array paleta a un string para poder hacer los chequeos
        string colorPaletteString = string.Join("", colorPalette);
        Debug.Log(colorPaletteString);
        DisplayColor(colorPaletteString);
    }
}