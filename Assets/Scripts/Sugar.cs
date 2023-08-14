using UnityEngine;

public class Sugar : MonoBehaviour
{
    [SerializeField] private int amountOfSugar;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var statsScript = col.gameObject.GetComponent<PlayerStats>();
        statsScript.IncreaseSugarAmount(amountOfSugar);
    }
}