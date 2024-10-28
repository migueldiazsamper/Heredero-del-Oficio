using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TemperaturaHorno : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    private float totalTemperature;
    [SerializeField] GameObject[] itemSlotArray;
    public float coolingValue;
    [SerializeField] Image thermometer;
    [SerializeField] float maxTemp = 1500f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTotalTemp();
        text.text = Math.Floor(totalTemperature).ToString();
        if(totalTemperature <= 0) totalTemperature = 0;
        if (totalTemperature >= maxTemp) totalTemperature = maxTemp;
        thermometer.fillAmount = totalTemperature/maxTemp;
    }

    public void CalculateTotalTemp()
    {
        float maderitaHeatValue;
        foreach (GameObject itemSlot in itemSlotArray)
        {   
            maderitaHeatValue = itemSlot.GetComponent<ItemSlot>().CurrentMaderitaHeatValue();
            if(maderitaHeatValue >= 0) totalTemperature += maderitaHeatValue*Time.deltaTime;
            else totalTemperature -= coolingValue*Time.deltaTime;
            //else coolingMultiplier += 2f;
        }
    }
}
