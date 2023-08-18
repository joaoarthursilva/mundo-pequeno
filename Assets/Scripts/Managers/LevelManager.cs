using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        private Sugar[] _allSugars;
        private int _amountOfSugars;
        private bool _hasWon;
        private int _sugarsLeftToCollect;
        [SerializeField] private Canvas youWinScreen;
        [SerializeField] private Canvas youDiedScreen;

        private void Start()
        {
            Time.timeScale = 1;
            youWinScreen.gameObject.SetActive(false);
            youDiedScreen.gameObject.SetActive(false);
            _hasWon = false;
            _allSugars = FindObjectsOfType<Sugar>();
            _amountOfSugars = _allSugars.Length;
        }

        private void Update()
        {
            _sugarsLeftToCollect = _amountOfSugars;
            if (_allSugars.Length == 0) return;
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
            FindObjectOfType<PlayerStats>().Win();
            youWinScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void Died()
        {
            youDiedScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}