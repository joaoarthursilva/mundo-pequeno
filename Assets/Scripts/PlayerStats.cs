using System;
using UnityEngine;
// using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxSugarAmount;
    private int _currentSugarAmount;
    [SerializeField] private Slider sugarAmountSlider;
    [SerializeField] private float timeToDecreaseSugarAmount;

    private bool _isAlive;

    private bool _hasWon;

    private PlayerController _playerController;

    // [SerializeField]private PostProcessVolume postProcessVolume;
    private void Awake()
    {
        _isAlive = true;
        sugarAmountSlider.minValue = 0;
        sugarAmountSlider.maxValue = maxSugarAmount;
        _playerController = GetComponent<PlayerController>();
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
        // UpdateVisualFeedback();
    }

    // private void UpdateVisualFeedback()
    // {
    //     // fazer profiles diferentes e alternar entre elas
    // }
    private void UpdateSlider()
    {
        sugarAmountSlider.value = _currentSugarAmount;
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

    public void Kill()
    {
        if (!_isAlive) return;
        _currentSugarAmount = 0;
        UpdateSlider();
        Die();
    }

    private void Die()
    {
        _isAlive = false;
        TurnOffMovement();
        Debug.Log("Died");
    }

    public void Win()
    {
        _currentSugarAmount = maxSugarAmount;
        UpdateSlider();
        TurnOffMovement();
    }

    private void TurnOffMovement()
    {
        _playerController.TurnOffMovement();
    }
}