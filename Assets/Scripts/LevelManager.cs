using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text tutorialText;
    public GameObject nextButton;
    public static bool isPlaying { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayTutorialMessage("Move mouse to look around.\nUse W, A, S, D to move.");
        isPlaying = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelWon()
    {
        PlayerStatus.isAlive = false;
        DisplayLevelMessage("LEVEL COMPLETE!");
        nextButton.SetActive(true);
        isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LevelLost()
    {
        PlayerStatus.isAlive = false;
        DisplayLevelMessage("YOU LOST!");
        isPlaying = false;
        Invoke("ReloadSameScene", 2f);
    }

    public void DisplayTutorialMessage(string message)
    {
        tutorialText.enabled = true;
        tutorialText.text = message;
        StartCoroutine(HideTutorialAfterDelay(10f));
    }

    private System.Collections.IEnumerator HideTutorialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tutorialText.enabled = false;
    }

    void DisplayLevelMessage(string message)
    {
        levelText.enabled = true;
        levelText.text = message;
    }

    public void ReloadSameScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
