using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    public float speed = 80f;
    public Image icon;
    public int powerupID;
    public AudioClip powerUpSound;
    public string tutorialMessage;
    LevelManager levelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetInt("Powerup_collected_" + powerupID, 0) == 1)
        {
            icon.enabled = true;
        } else 
        {
            icon.enabled = false;
        }

        if (PlayerPrefs.GetInt("Powerup_claimed_" + powerupID, 0) == 1)
        {
            gameObject.SetActive(false); // Don't spawn again
            return;
        }
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Power Up Collected");
            icon.enabled = true;
            PlayerPrefs.SetInt("Powerup_claimed_" + powerupID, 1);
            PlayerPrefs.SetInt("Powerup_collected_" + powerupID, 1);
            PlayerPrefs.Save();
            levelManager.DisplayTutorialMessage(tutorialMessage);
            if(powerUpSound != null)
            {
                AudioSource.PlayClipAtPoint(powerUpSound, transform.position, 1);
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
