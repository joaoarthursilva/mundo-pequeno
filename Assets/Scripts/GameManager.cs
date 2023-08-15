using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _currentLevel;

    [SerializeField] private Scene[] levels;
    // private Dictionary<int, Scene> _scenesAndLevels = new Dictionary<int, Scene>();

    private void Awake()
    {
        for (var i = 1; i < (levels.Length + 1); i++)
        {
            // _scenesAndLevels.Add(i, levels[i]);
        }
    }

    private void LoadNextLevel(int levelNumber)
    {
        // SceneManager.LoadScene(_scenesAndLevels);
    }
}