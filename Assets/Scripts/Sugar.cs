using UnityEngine;

public class Sugar : MonoBehaviour
{
    [SerializeField] private int amountOfSugar;
    private bool _canBeCollected;
    private bool _hasBeenCollected;
    private GameObject _sugarSprite;

    private void Awake()
    {
        _sugarSprite = transform.GetChild(0).gameObject;
        _canBeCollected = true;
        _hasBeenCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_canBeCollected || _hasBeenCollected) return;
        col.TryGetComponent(out PlayerStats statsScript);
        if (!statsScript) return;
        statsScript.IncreaseSugarAmount(amountOfSugar);
        _hasBeenCollected = true;
        DeactivateSugar();
    }

    public void DeactivateSugar()
    {
        _sugarSprite.SetActive(false);
        _canBeCollected = false;
    }

    public void ActivateSugar()
    {
        _sugarSprite.SetActive(true);
        _canBeCollected = true;
    }

    public bool HasBeenCollected()
    {
        return _hasBeenCollected;
    }

    public bool CanBeCollected()
    {
        return _canBeCollected;
    }
}