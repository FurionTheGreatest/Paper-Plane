using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public static Action OnSpeedUpCollect;
    
    public void CollectSpeedUp()
    {
        OnSpeedUpCollect?.Invoke();
    }
}
