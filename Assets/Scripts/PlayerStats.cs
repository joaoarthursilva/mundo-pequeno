using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxSugarAmount;
    private int _currentSugarAmount;
    [SerializeField] private Slider sugarAmountSlider;
    [SerializeField] private float timeToDecreaseSugarAmount;
    private bool _isAlive;
    [SerializeField]private PostProcessVolume postProcessVolume;
    private void Awake()
    {
        _isAlive = true;
        sugarAmountSlider.minValue = 0;
        sugarAmountSlider.maxValue = maxSugarAmount;

        _currentSugarAmount = maxSugarAmount;
        sugarAmountSlider.value = _currentSugarAmount;
    }

    private void Start()
    {
        InvokeRepeating(nameof(ManageSugarAmount), timeToDecreaseSugarAmount, timeToDecreaseSugarAmount);
    }

    private void ManageSugarAmount()
    {
        if (!_isAlive) return;
        var hasDecreased = DecreaseSugarAmount();
        if (!hasDecreased)
        {
            Die();
        }

        UpdateSlider();
        UpdateVisualFeedback();
    }

    private void UpdateVisualFeedback()
    {
        // fazer profiles diferentes e alternar entre elas
    }
    private void UpdateSlider()
    {
        sugarAmountSlider.value = _currentSugarAmount;
    }

    private void Die()
    {
        _isAlive = false;
        Debug.Log("Died");
    }

    private bool DecreaseSugarAmount()
    {
        _currentSugarAmount = Math.Max(0, _currentSugarAmount - 1);
        return _currentSugarAmount > 0;
    }

    public void IncreaseSugarAmount(int amount)
    {
        _currentSugarAmount = Math.Min(maxSugarAmount, _currentSugarAmount + amount);
        UpdateSlider();
    }
}