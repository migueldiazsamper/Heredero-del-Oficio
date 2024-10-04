using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdatePercentage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI percentageText;
    static public int percentageOfTotal = 0;
    void Update()
    {
        percentageText.text = percentageOfTotal.ToString() + "%";
    }
}
