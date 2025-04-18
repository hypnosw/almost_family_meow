using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{
    public Button resumeButton;

    void Start()
    {
        // Only show Resume if there's a saved game
        if (PlayerPrefs.HasKey("HasSavedGame") && PlayerPrefs.GetInt("HasSavedGame") == 1)
        {
            resumeButton.gameObject.SetActive(true);
        }
        else
        {
            resumeButton.gameObject.SetActive(false);
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level1");
    }

    public void ResumeGame()
    {
        int level = PlayerPrefs.GetInt("CurrentLevel", 1);
        SceneManager.LoadScene($"Level{level}");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}

