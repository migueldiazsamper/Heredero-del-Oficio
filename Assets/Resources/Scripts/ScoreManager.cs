using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public void AddMinigameScore(){
        int scoreToAdd = 0;
        switch (SceneManager.GetActiveScene().name)
        {
        case "Minijuego 1":
            scoreToAdd = CalculateMinigameScore1();
            break;
        case "Minijuego 2":
            scoreToAdd = CalculateMinigameScore2();
            break;
        case "Minijuego 3":
            scoreToAdd = CalculateMinigameScore3();
            break;
        case "Minijuego 4":
            scoreToAdd = CalculateMinigameScore4();
            break;
        case "Minijuego 5":
            scoreToAdd = CalculateMinigameScore5();
            break;
        case "Minijuego 6":
            scoreToAdd = CalculateMinigameScore6();
            break;
        case "Minijuego 7":
            scoreToAdd = CalculateMinigameScore7();
            break;
        default:
            scoreToAdd = 0;
            break;
        }
        Debug.Log("Score to add: " + scoreToAdd);
        PhasesManager.instance.AddScore(scoreToAdd);
    }


    //El rango de puntuación del minijuego 1 es de [0, 10]
    private int CalculateMinigameScore1(){
        //Si la compleción es menor del 100%, no suma puntos
        if(UpdatePercentage.percentageOfTotal == 100){
            int elapsedTimeMinutes = Mathf.FloorToInt(FindAnyObjectByType<Timer>().ElapsedTime()/60);

            //La puntuación viene dada por  [10 / (minutos empleados + 1)]
            return Mathf.FloorToInt(10 / (elapsedTimeMinutes + 1));
        }
        else return 0;
    }

    //El rango de puntuación del minijuego 2 es de [0, 20]
    private int CalculateMinigameScore2(){
        int elapsedTimeMinutes = Mathf.FloorToInt(FindAnyObjectByType<Timer>().ElapsedTime()/60);

        //La puntuación viene dada por  [20 / (minutos empleados + 1)]
        return Mathf.FloorToInt(20 / (elapsedTimeMinutes + 1));
    }

    //El rango de puntuación del minijuego 3 es de [0, 20]
    private int CalculateMinigameScore3(){
        bool isVictory = FindAnyObjectByType<CountDownTimer>().GetIsVictory();
        CheckIfDefeat checkIfDefeat = FindAnyObjectByType<CheckIfDefeat>();
        if(isVictory){
            if(checkIfDefeat.GetScore()>=40) return 20;
            else return Mathf.FloorToInt(checkIfDefeat.GetScore()/2);
        }
        else return 0;
    }

    //El rango de puntuación del minijuego 4 es de [0, 20]
    private int CalculateMinigameScore4(){
        int minigame4Score = FindAnyObjectByType<TemperaturaHorno>().GetScore();
        if(minigame4Score>=60) return 20;
        else return Mathf.FloorToInt(minigame4Score/3);
    }

    //El rango de puntuación del minijuego 5 es de [0, 0]
    private int CalculateMinigameScore5(){
        return 0;
    }

    //El rango de puntuación del minijuego 6 es de [0, 30]
    private int CalculateMinigameScore6(){
        return FindAnyObjectByType<DragDropCuchara>().NumOfCorrectColors() * 5;
        
    }

    //El rango de puntuación del minijuego 7 es de [0, 30]
    private int CalculateMinigameScore7(){
        int numberOfMoves = FindAnyObjectByType<Minigame7Manager>().NumberOfMoves();
        return Mathf.Max(0, 30 - 5 * Mathf.FloorToInt((numberOfMoves - 100) / 50));
    }
}
