using System;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image; //Hay overlap entre el image de UnityUI y el VSCode. Esto lo arregla

// Clase que gestiona la lógica de los pigmentos y los slots
public class PigmentosManager : MonoBehaviour
{

    
    private Image mixedColorSprite;

    private int colorCounter = 0;
    private int[] colorPalette = new int[5]; //Este array contiene el nº usado de cada color en formato {b, m, c, y, w}

    // Diccionario que define las mezclas de colores
    private Dictionary<string, Color> colorMixes = new Dictionary<string, Color>
    {
        { "CyanMagenta", Color.blue }, // Cian + Magenta = Azul
        { "MagentaCyan", Color.blue }, // Magenta + Cian = Azul (inversa)
        { "CyanYellow", Color.green }, // Cian + Amarillo = Verde
        { "YellowCyan", Color.green }, // Amarillo + Cian = Verde (inversa)
        { "MagentaYellow", Color.red }, // Magenta + Amarillo = Rojo
        { "YellowMagenta", Color.red }, // Amarillo + Magenta = Rojo (inversa)
        { "CyanBlack", new Color(0f, 0.5f, 0.5f) }, // Cian + Negro = Cian Oscuro
        { "BlackCyan", new Color(0f, 0.5f, 0.5f) }, // Negro + Cian = Cian Oscuro (inversa)
        { "MagentaBlack", new Color(0.5f, 0f, 0.5f) }, // Magenta + Negro = Magenta Oscuro
        { "BlackMagenta", new Color(0.5f, 0f, 0.5f) }, // Negro + Magenta = Magenta Oscuro (inversa)
        { "YellowBlack", new Color(0.5f, 0.5f, 0f) }, // Amarillo + Negro = Amarillo Oscuro
        { "BlackYellow", new Color(0.5f, 0.5f, 0f) }, // Negro + Amarillo = Amarillo Oscuro (inversa)
        { "CyanWhite", new Color(0.5f, 1f, 1f) }, // Cian + Blanco = Cian Claro
        { "WhiteCyan", new Color(0.5f, 1f, 1f) }, // Blanco + Cian = Cian Claro (inversa)
        { "MagentaWhite", new Color(1f, 0.5f, 1f) }, // Magenta + Blanco = Magenta Claro
        { "WhiteMagenta", new Color(1f, 0.5f, 1f) }, // Blanco + Magenta = Magenta Claro (inversa)
        { "YellowWhite", new Color(1f, 1f, 0.5f) }, // Amarillo + Blanco = Amarillo Claro
        { "WhiteYellow", new Color(1f, 1f, 0.5f) }, // Blanco + Amarillo = Amarillo Claro (inversa)
        { "BlackWhite", Color.gray }, // Negro + Blanco = Gris
        { "WhiteBlack", Color.gray }, // Blanco + Negro = Gris (inversa)
        // Agrega más combinaciones según sea necesario
    };
    void Awake(){
        for(int i = 0; i < 5; i++) colorPalette[i] = 0;
        mixedColorSprite = GetComponentInChildren<Image>();
    }
    public void AddColorToMix(String color){
        if(colorCounter <= 5){
            colorCounter++;
            AddColorToPalette(color);
            DisplayColor();
        }
        //If there are 5 colors, lock the spoon to the bowl to begin stirring
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

    private void DisplayColor(){
        ///Show the last color added
    }
}