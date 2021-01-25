using General;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Panel
{
    public class PauseMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        private static bool _gameIsPaused;

        public GameObject pauseMenuUI;
        public GameObject inst;
     [SerializeField] private GameState gameState;


        // Update is called once per frame

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !gameState.GetPaused())
            {
                if (_gameIsPaused)
                {
                    Resume();
                    Cursor.lockState = CursorLockMode.Locked;
                }

                else
                {
                    Pause();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }

        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            _gameIsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            _gameIsPaused = true;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void NewGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("SampleScene");
        }

        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        public void Instruction()
        {
            inst.SetActive(true);
        }

        public void Returen()
        {
            inst.SetActive(false);
        }
    }
}