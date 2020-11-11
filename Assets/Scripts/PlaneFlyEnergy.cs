using System;
using UnityEngine;

public class PlaneFlyEnergy : MonoBehaviour
{
    public static Action<float> changeEnergy;
    public static Action energyIsEmpty;
    public float maxEnergy = 100; 
    public float currentEnergy;

    public float decreaseEnergyValue = 0.1f;
    public float increaseEnergyValue = 20f;
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
            DisablePlaneWhenOutOfEnergy();
        }
    }

    private void DisablePlaneWhenOutOfEnergy()
    {
        GetComponent<PlaneMovement>().enabled = false;
        DisableEnergyInvocation();
        energyIsEmpty?.Invoke();
    }

    private void IncreaseEnergy()
    {
        if (currentEnergy + increaseEnergyValue > 100)
            currentEnergy = maxEnergy;
        else
            currentEnergy += increaseEnergyValue;
        
        changeEnergy?.Invoke(currentEnergy);
    }

    public void DisableEnergyInvocation()
    {
        CancelInvoke(nameof(DecreaseEnergy));
    }
    private void OnEnable()
    {
        SpeedUp.OnSpeedUpCollect += IncreaseEnergy;
    }

    private void OnDisable()
    {
        SpeedUp.OnSpeedUpCollect -= IncreaseEnergy;
    }
}
