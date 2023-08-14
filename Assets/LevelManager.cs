using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Sugar[] _allSugars;
    private void Start()
    {
        _allSugars = FindObjectsOfType<Sugar>();
    }

    private void Update()
    {
        foreach (var sugar in _allSugars)
        {
            
        }
    }

    private void Win()
    {
        
    }
}
