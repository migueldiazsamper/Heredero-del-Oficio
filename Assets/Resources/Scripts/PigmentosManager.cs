using System;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image; //Hay overlap entre el image de UnityUI y el VSCode. Esto lo arregla

// Clase que gestiona la lógica de los pigmentos y los slots
public class PigmentosManager : MonoBehaviour
{

    [SerializeField] GameObject cuchara;
    public Image mixedColorSpriteImage;

    public int colorCounter {get; private set;} = 0;
    private int[] colorPalette = new int[5]; //Este array contiene el nº usado de cada color en formato {b, m, c, y, w}

    public static Color colorNaranja = new Color(1.0f, 0.392f, 0.129f); // Naranja
    public static Color colorVerdeOlivaOscuro = new Color(0.388f, 0.349f, 0.078f); // Verde Oliva Oscuro
    public static Color colorVerdeOlivaClaro = new Color(0.663f, 0.627f, 0.271f); // Verde Oliva Claro
    public static Color colorAzulOscuro = new Color(0.267f, 0.337f, 0.659f); // Azul Oscuro
    public static Color colorAzulClaro = new Color(0.588f, 0.733f, 0.898f); // Azul Claro
    public static Color colorMarron = new Color(0.596f, 0.329f, 0.0f); // Marrón
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
            case "02030": return colorNaranja;
            case "10220": return colorVerdeOlivaOscuro;
            case "10112": return colorVerdeOlivaClaro;
            case "10400": return colorAzulOscuro;
            case "00302": return colorAzulClaro;
            case "01112": return colorMarron;
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