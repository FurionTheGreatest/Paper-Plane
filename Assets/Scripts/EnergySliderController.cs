using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySliderController : MonoBehaviour
{
    private Slider _energySlider;
    private PlaneFlyEnergy _planeFlyEnergy;

    private void Awake()
    {
        _energySlider = GetComponent<Slider>();
        _planeFlyEnergy = FindObjectOfType<PlaneFlyEnergy>();
    }

    private void Start()
    {
        _energySlider.maxValue = _planeFlyEnergy.maxEnergy;
        _energySlider.value = _energySlider.maxValue;
    }

    private void ChangeSliderValue(float value)
    {
        _energySlider.value = value;
    }

    private void OnEnable()
    {
        PlaneFlyEnergy.changeEnergy += ChangeSliderValue;
    }
    
    private void OnDisable()
    {
        PlaneFlyEnergy.changeEnergy -= ChangeSliderValue;
    }
}
