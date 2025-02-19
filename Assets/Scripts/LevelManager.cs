using UnityEngine;
using TMPro;
using System;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public TMP_Text levelText;
    public GameObject nextButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelWon()
    {
        DisplayLevelMessage("LEVEL COMPLETE!");
        nextButton.SetActive(true);
    }

    public void LevelLost()
    {
        DisplayLevelMessage("YOU LOST!");
        Invoke("ReloadSameScene", 2f);
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
