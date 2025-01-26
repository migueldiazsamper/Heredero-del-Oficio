using System;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image; //Hay overlap entre el image de UnityUI y el VSCode. Esto lo arregla

// Clase que gestiona la lógica de los pigmentos y los slots
public class PigmentosManager : MonoBehaviour
{

    [SerializeField] GameObject cuchara;
    public Image mixedColorSpriteImage;
    public Image reflejoSpriteImage;

    public int colorCounter {get; private set;} = 0;
    private int[] colorPalette = new int[5]; //Este array contiene el nº usado de cada color en formato {b, m, c, y, w}

    [SerializeField] private Sprite[] liquidSprites; // Array con los 5 tamaños del líquido del pote grande
    [SerializeField] private Sprite[] reflejosSprites; // Array con los 5 tamaños del reflejo del pote grande


    //Colores base de los potecitos
    public static Color colorRojo = new Color(169f / 255f, 45f / 255f, 45f / 255f); // Rojo
    public static Color colorAzul = new Color(87f / 255f, 162f / 255f, 220f / 255f); // Azul
    public static Color colorAmarillo = new Color(255f / 255f, 226f / 255f, 109f / 255f); // Amarillo

    //Colores de la paleta
    public static Color colorNaranja = new Color(197f / 255f, 95f / 255f, 33f / 255f); // Naranja
    public static Color colorVerdeOlivaOscuro = new Color(67f / 255f, 58f / 255f, 3f / 255f); // Verde Oliva Oscuro
    public static Color colorVerdeOlivaClaro = new Color(141f / 255f, 132f / 255f, 73f / 255f); // Verde Oliva Claro
    public static Color colorAzulOscuro = new Color(68f / 255f, 86f / 255f, 168f / 255f); // Azul Oscuro
    public static Color colorAzulClaro = new Color(186f / 255f, 221f / 255f, 243f / 255f); // Azul Claro
    public static Color colorMarron = new Color(194f / 255f, 140f / 255f, 102f / 255f); // Marrón
    public static Color colorDorado = new Color(235f / 255f, 169f / 255f, 48f / 255f); // Dorado

    void Awake(){
        for(int i = 0; i < 5; i++) colorPalette[i] = 0;
    }

    public void AddColorToMix(String colorString){
        if(colorCounter <= 5){
            colorCounter++;
            mixedColorSpriteImage.sprite = liquidSprites[colorCounter-1];
            reflejoSpriteImage.sprite = reflejosSprites[colorCounter-1];
            AddColorToPalette(colorString);
            DisplayColor(colorString);
            Debug.Log(colorCounter);

            //If there are 5 colors, lock the spoon for mixing
            if(colorCounter == 5) cuchara.GetComponent<DragDropCuchara>().LockToBowl();
        }
    }

    private void AddColorToPalette(String color){
        switch (color){
            case "Negro": colorPalette[0]++; break;
            case "Rojo": colorPalette[1]++; break;
            case "Azul": colorPalette[2]++; break;
            case "Amarillo": colorPalette[3]++; break;
            case "Blanco": colorPalette[4]++; break;
        }
    }

    private void DisplayColor(String colorString){
        ///Show the last color added
        mixedColorSpriteImage.color = ProvideColor(colorString);

    }

    public Color ProvideColor(String colorString){
        switch (colorString){
            case "Negro":       return Color.black;
            case "Rojo":        return colorRojo;
            case "Azul":        return colorAzul;
            case "Amarillo":    return colorAmarillo;
            case "Blanco":      return Color.white;
            //La paleta está en formato N-R-Az-Am-B
            case "02030": return colorNaranja;
            case "10220": return colorVerdeOlivaOscuro;
            case "10112": return colorVerdeOlivaClaro;
            case "10400": return colorAzulOscuro;
            case "00302": return colorAzulClaro;
            case "01112": return colorMarron;
            case "01031": return colorDorado;
            default: return Color.grey;
        }
    }

    public void MixColors(){
        //Conversión del array paleta a un string para poder hacer los chequeos
        string colorPaletteString = string.Join("", colorPalette);
        Debug.Log(colorPaletteString);
        DisplayColor(colorPaletteString);
    }

    public void SaveColor(){
        PhasesManager.instance.coloresMancerina[PhasesManager.instance.savedColors] = mixedColorSpriteImage.color;
        PhasesManager.instance.savedColors++;
        if(PhasesManager.instance.savedColors >= 3) ChangeScenes.LoadSceneButton("DialogoInterior"); 
        else ChangeScenes.LoadSceneButton("Minijuego 6");
    }
    
}