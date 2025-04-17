using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    bool isGamePaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        pauseMenuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        pauseMenuPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReloadSameScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        ResumeGame();
    }

    public void ExitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}
