using UnityEngine;
using TMPro;
using System;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text tutorialText;
    public GameObject nextButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayTutorialMessage("Move mouse to look around.\nUse WASD to move.");
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
    }

    public void LevelLost()
    {
        PlayerStatus.isAlive = false;
        DisplayLevelMessage("YOU LOST!");
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
}
