using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScriptsEnabler : MonoBehaviour
{
    private UpdatePassedDistance _distanceUpdater;
    private EnergySliderController _energySliderController;
    private Spawner _spawner;
    private CoinCollector _coinCollector;

    private void Awake()
    {
        _distanceUpdater = FindObjectOfType<UpdatePassedDistance>();
        _energySliderController = FindObjectOfType<EnergySliderController>();
        _spawner = FindObjectOfType<Spawner>();
        _coinCollector = FindObjectOfType<CoinCollector>();
    }

    private void OnEnable()
    {
        _distanceUpdater.enabled = true;
        _energySliderController.enabled = true;
        _spawner.enabled = true;
        _coinCollector.enabled = true;
    }

    private void OnDisable()
    {
        _distanceUpdater.enabled = false;
        _energySliderController.enabled = false;
        _spawner.enabled = false;
        _coinCollector.enabled = false;
    }
}
