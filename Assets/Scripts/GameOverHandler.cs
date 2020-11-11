using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    public TMP_Text gameOverText;
    private void Awake()
    {
        gameOverText = GetComponent<TMP_Text>();
    }

    private void ShowGameOverText()
    {
        gameOverText.enabled = true;
    }

    private void OnEnable()
    {
        PlaneFlyEnergy.energyIsEmpty += ShowGameOverText;
    }

    private void OnDisable()
    {
        PlaneFlyEnergy.energyIsEmpty -= ShowGameOverText;
    }
}
