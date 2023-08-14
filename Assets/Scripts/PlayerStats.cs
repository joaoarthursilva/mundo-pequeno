using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxSugarAmount;
    private int _currentSugarAmount;
    [SerializeField] private Slider sugarAmountSlider;
    [SerializeField] private float timeToDecreaseSugarAmount;
    private bool _isAlive;

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
        var decreased = DecreaseSugarAmount();
        if (!decreased)
        {
            Die();
        }

        sugarAmountSlider.value = _currentSugarAmount;
    }

    private void Die()
    {
        _isAlive = false;
        Debug.Log("Died");
    }

    private bool DecreaseSugarAmount()
    {
        _currentSugarAmount--;
        return _currentSugarAmount > 0;
    }
}