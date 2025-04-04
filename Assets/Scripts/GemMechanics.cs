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
        gemChecked.enabled = false;
        gemCollected = false;
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
            gemCollected = true;
            if(gemSound != null)
            {
                AudioSource.PlayClipAtPoint(gemSound, transform.position, 1);
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
