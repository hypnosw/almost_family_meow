using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    public float speed = 80f;
    public Image icon;
    public AudioClip powerUpSound;
    public string tutorialMessage;
    LevelManager levelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon.enabled = false;
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
            levelManager.DisplayTutorialMessage(tutorialMessage);
            if(powerUpSound != null)
            {
                AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
