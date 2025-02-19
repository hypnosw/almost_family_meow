using UnityEngine;
using UnityEngine.UI;

public class GemMechanics : MonoBehaviour
{
    public int rotationSpeed = 100;
    public Image gemChecked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gemChecked.enabled = false;
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
            Destroy(gameObject, 0.1f);
        }
    }
}
