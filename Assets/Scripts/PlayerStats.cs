using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxSugarAmount;
    private int _currentSugarAmount;
    [SerializeField] private Slider sugarAmountSlider;
    [SerializeField] private float timeToDecreaseSugarAmount;

    private void Awake()
    {
        sugarAmountSlider.minValue = 0;
        sugarAmountSlider.maxValue = maxSugarAmount;
        _currentSugarAmount = maxSugarAmount;
    }

    private void Start()
    {
        InvokeRepeating(nameof(ManageSugarAmount), timeToDecreaseSugarAmount, timeToDecreaseSugarAmount);
    }

    private void ManageSugarAmount()
    {
        var decreased = DecreaseSugarAmount();
        if (!decreased)
        {
            Die();
        }

        sugarAmountSlider.value = _currentSugarAmount;
    }

    private void Die()
    {
        Debug.Log("Died");
    }

    private bool DecreaseSugarAmount()
    {
        _currentSugarAmount--;
        return _currentSugarAmount > 0;
    }
}