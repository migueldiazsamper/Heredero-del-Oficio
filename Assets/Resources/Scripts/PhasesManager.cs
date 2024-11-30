using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhasesManager : MonoBehaviour
{
    public static PhasesManager instance;
    public int currentPhase = 0;
    public int maxPhases = 8;

    public int puntuacionTotal = 0;

    public string[] scenes;

    Dictionary<int, string> phasesAndScenes = new Dictionary<int, string>
    {
        { 0, "" },
        { 1, "" },
        { 2, "" },
        { 3, "" },
        { 4, "" },
        { 5, "" },
        { 6, "" },
        { 7, "" }
    };

    public bool nextIsPueblo = false;

    private bool[] nextIsDialogue = { false, false, true, false, true, false, true, false };

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void NextPhase()
    {
        if (currentPhase < maxPhases)
        {
            currentPhase++;
        }
    }
}
