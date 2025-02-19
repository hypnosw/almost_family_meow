using UnityEngine;
using TMPro;
using System;

public class CrowBehavior : MonoBehaviour
{
    public TMP_Text crowText;
    public float messageDuration = 5f;
    LevelManager levelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(Camera.main.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with crow!");
            if (GemMechanics.gemCollected) 
            {
                levelManager.LevelWon();
            } else {
                Debug.Log("Showing message for " + messageDuration + " seconds.");
                crowText.enabled = true;
                Invoke("HideText", messageDuration);
            }
        }
    }

    void HideText()
    {
        Debug.Log("Hiding message now!");
        crowText.enabled = false;
    }
}
