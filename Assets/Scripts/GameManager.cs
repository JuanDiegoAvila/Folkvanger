using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseMenu;

        private void Start()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

    }
}