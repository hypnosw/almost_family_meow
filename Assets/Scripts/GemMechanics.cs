using UnityEngine;
using UnityEngine.UI;

public class GemMechanics : MonoBehaviour
{
    public int rotationSpeed = 100;
    public Image gemChecked;
    public AudioClip gemSound;
    public static bool gemCollected { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gemCollected = false;

        // Check if gem was already collected
        if (PlayerPrefs.GetInt("GemCollected", 0) == 1)
        {
            gemChecked.enabled = true;
            gameObject.SetActive(false); // prevent gem from spawning again
            gemCollected = true;
        }
        else
        {
            gemChecked.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Gem Collected");
            gemChecked.enabled = true;
            PlayerPrefs.SetInt("GemCollected", 1);
            PlayerPrefs.Save();
            gemCollected = true;
            if(gemSound != null)
            {
                AudioSource.PlayClipAtPoint(gemSound, transform.position, 1);
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
