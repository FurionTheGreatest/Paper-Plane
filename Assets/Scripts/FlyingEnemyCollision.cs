using System;
using UnityEngine;

public class FlyingEnemyCollision : MonoBehaviour
{
    public static Action onEnemyCollision;

    public void OnEnemyCollision()
    {
        onEnemyCollision?.Invoke();
    }
}
