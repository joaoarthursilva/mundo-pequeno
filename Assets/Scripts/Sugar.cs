using System;
using UnityEngine;

public class Sugar : MonoBehaviour
{
    [SerializeField] private int amountOfSugar;
    private bool _canBeCollected;

    private void Awake()
    {
        _canBeCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_canBeCollected) return;
        var statsScript = col.gameObject.GetComponent<PlayerStats>();
        statsScript.IncreaseSugarAmount(amountOfSugar);
        DeactivateSugar();
    }

    private void DeactivateSugar()
    {
        gameObject.GetComponent<Sprite>();
        _canBeCollected = false;
    }
}