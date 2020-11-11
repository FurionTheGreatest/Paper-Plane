using System;
using UnityEngine;

public class PlaneFlyEnergy : MonoBehaviour
{
    public static Action<float> changeEnergy;
    public static Action energyIsEmpty;
    public float maxEnergy = 100; 
    public float currentEnergy;

    public float decreaseEnergyValue = 0.1f;
    private void Start()
    {
        currentEnergy = maxEnergy;
        InvokeRepeating(nameof(DecreaseEnergy),1,1/7f);
    }

    private void DecreaseEnergy()
    {
        currentEnergy -= decreaseEnergyValue;
        if(currentEnergy > 0)
            changeEnergy?.Invoke(currentEnergy);
        else
        {
            currentEnergy = 0;
            CancelInvoke(nameof(DecreaseEnergy));
            DisablePlaneOnGameOver();
        }
    }

    private void DisablePlaneOnGameOver()
    {
        GetComponent<PlaneMovement>().enabled = false;
        energyIsEmpty?.Invoke();
    }
}
