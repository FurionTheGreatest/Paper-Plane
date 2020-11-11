using System;
using UnityEngine;

public class DroneCollision : MonoBehaviour
{
    public static Action onEnemyCollision;

    public void OnDroneCollision()
    {
        onEnemyCollision?.Invoke();
    }
}
