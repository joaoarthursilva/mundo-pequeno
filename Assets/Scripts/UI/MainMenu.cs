using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
        }

        private void Update()
        {
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void LoadLevel1()
        {
            SceneManager.LoadScene("Level 1");
        }

        public void LoadLevel2()
        {
            SceneManager.LoadScene("Level 2");
        }

        public void LoadLevel3()
        {
            SceneManager.LoadScene("Level 3");
        }

        public void LoadLevel4()
        {
            SceneManager.LoadScene("Level 4");
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}