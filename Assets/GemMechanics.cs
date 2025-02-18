using UnityEngine;

public class GemMechanics : MonoBehaviour
{
    public int rotationSpeed = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Gem Collected");
            Destroy(gameObject, 0.1f);
        }
    }
}
