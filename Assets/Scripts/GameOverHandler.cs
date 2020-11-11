using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    public TMP_Text gameOverText;

    private void OnGameOver()
    {
        gameOverText.enabled = true;
        var spawner = FindObjectOfType<Spawner>();
        var collectibles = spawner.GetComponentsInChildren<Collectible>();
        foreach (var collectible in collectibles)
        {
            Destroy(collectible.gameObject);
        }
        spawner.enabled = false;

        var playerMovement = FindObjectOfType<PlaneMovement>();
        playerMovement.enabled = false;

        var planeEnergy = FindObjectOfType<PlaneFlyEnergy>();
        planeEnergy.DisableEnergyInvocation();
    }

    private void OnEnable()
    {
        PlaneFlyEnergy.energyIsEmpty += OnGameOver;
        DroneCollision.onEnemyCollision += OnGameOver;
    }

    private void OnDisable()
    {
        PlaneFlyEnergy.energyIsEmpty -= OnGameOver;
        DroneCollision.onEnemyCollision -= OnGameOver;
    }
}
