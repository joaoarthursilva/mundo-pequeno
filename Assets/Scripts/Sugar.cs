using UnityEngine;

public class Sugar : MonoBehaviour
{
    [SerializeField] private int amountOfSugar;
    private bool _hasBeenCollected;
    private GameObject _sugarSprite;

    private void Awake()
    {
        _sugarSprite = transform.GetChild(0).gameObject;
        _hasBeenCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_hasBeenCollected) return;
        var statsScript = col.gameObject.GetComponent<PlayerStats>();
        statsScript.IncreaseSugarAmount(amountOfSugar);
        DeactivateSugar();
    }

    private void DeactivateSugar()
    {
        _sugarSprite.SetActive(false);
        _hasBeenCollected = true;
    }

    public bool HasBeenCollected()
    {
        return _hasBeenCollected;
    }
}