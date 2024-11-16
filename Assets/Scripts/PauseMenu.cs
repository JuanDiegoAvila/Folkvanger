using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        public void ResumeGame()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        public void GoToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("PaginaInicio");
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}