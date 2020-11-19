using System;
using UnityEngine;

public class FlyingEnemyCollision : MonoBehaviour
{
    public static Action onEnemyCollision;

    public void OnEnemyCollision()
    {
        if(LevelManager.instance.isPlaying)
            onEnemyCollision?.Invoke();
    }
}
