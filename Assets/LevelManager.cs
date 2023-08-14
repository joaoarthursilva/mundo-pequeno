using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Sugar[] _allSugars;
    private int _amountOfSugars;
    private bool _hasWon;
    private int _sugarsLeftToCollect;
    private void Start()
    {
        _hasWon = false;
        _allSugars = FindObjectsOfType<Sugar>();
        _amountOfSugars = _allSugars.Length;
    }

    private void Update()
    {
        _sugarsLeftToCollect = _amountOfSugars;
        foreach (var sugar in _allSugars)
        {
            if (sugar.HasBeenCollected())
            {
                _sugarsLeftToCollect--;
            }
        }

        if (_sugarsLeftToCollect <= 0)
        {
            Win();
        }
    }

    private void Win()
    {
        if (_hasWon) return;
        _hasWon = true;
        Debug.Log("ganhou");
    }
}